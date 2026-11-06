
namespace SwingThree
{

    public class TrendBackTester
    {
        // Configuration options
        private readonly BacktestOptions Options;

        /// <summary>
        /// Initialize a new backtest object.
        /// </summary>
        public TrendBackTester(BacktestOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// Executes the trading backtest simulation using price history, moving average indicators, and trailing stop management.
        /// </summary>
        public BackTestResult Run(List<PriceBar> history)
        {
            if (history == null || history.Count < Options.LongPeriod)
            {
                return new BackTestResult();
            }

            var portfolio = new Portfolio(Options.StartingCapital);
            Trade activeTrade = null;
            int persistBars = 2;
            int consecutiveBullish = 0;
            double highestHigh = 0.0;

            // Pre-calculate the indicator series.
            double[] fastSeries = TechnicalIndicators.GetSMASeries(history, Options.ShortPeriod);
            double[] slowSeries = TechnicalIndicators.GetSMASeries(history, Options.LongPeriod);

            for (int i = Options.LongPeriod; i < history.Count - 1; i++)
            {
                var bar = history[i];

                // Track consecutive bars where the fast moving average stays above the slow moving average.
                if (fastSeries[i] > slowSeries[i])
                    consecutiveBullish++;
                else
                    consecutiveBullish = 0;

                // --- Exit Check ---
                if (activeTrade?.IsOpen == true)
                {
                    if (bar.High > highestHigh) 
                        highestHigh = bar.High;

                    // Evaluate stop orders.
                    double trailingStop = highestHigh * (1.0 - Options.TrailingStopPct);
                    bool hitInitialStop = bar.Low <= activeTrade.InitialStopLoss;
                    bool hitTrailingStop = bar.Low <= trailingStop;

                    if (hitInitialStop)
                    {
                        activeTrade.ExitDate = bar.Date;
                        double exitPrice = Math.Min(bar.Open, activeTrade.InitialStopLoss);
                        portfolio.CloseTrade(activeTrade, exitPrice);
                        continue;
                    }
                    else if (hitTrailingStop)
                    {
                        activeTrade.ExitDate = bar.Date;
                        double exitPrice = Math.Min(bar.Open, trailingStop);
                        portfolio.CloseTrade(activeTrade, exitPrice);
                        continue;
                    }
                }

                // --- Entry Check ---
                if (activeTrade == null || !activeTrade.IsOpen)
                {
                    // Verify trend conditions.
                    bool isUptrend = bar.Close > slowSeries[i] && fastSeries[i] > slowSeries[i];
                    bool persisted = consecutiveBullish >= persistBars;

                    if (isUptrend && persisted)
                    {
                        double entryPrice = history[i + 1].Open;
                        double initialStop = bar.Low * (1.0 - Options.StopPct);
                        double shares = Options.PositionSize / entryPrice;

                        activeTrade = new Trade
                        {
                            Shares = shares,
                            EntryPrice = entryPrice,
                            EntryDate = bar.Date,
                            InitialStopLoss = initialStop,
                            EntryIndex = i,
                            IsOpen = true
                        };

                        highestHigh = bar.High;
                    }
                }

                portfolio.UpdateDrawdown();
            }

            return new BackTestResult
            {
                StrategyName = "Trend (Trailing Stop)",
                StartingCapital = Options.StartingCapital,
                EndingCapital = portfolio.Capital,
                MaxDrawdown = portfolio.MaxDrawdown,
                WinningTrades = portfolio.WinningTrades,
                LosingTrades = portfolio.LosingTrades,
                TotalGains = portfolio.TotalGains,
                TotalLosses = portfolio.TotalLosses,
                ClosedTrades = portfolio.ClosedTrades
            };
        }
    }
}
