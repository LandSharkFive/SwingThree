
namespace SwingThree
{

    /// <summary>
    /// Represents a trading portfolio that tracks capital, performance metrics, and trade history.
    /// </summary>
    public class Portfolio
    {
        public double Capital { get; set; }
        public double PeakCapital { get; set; }
        public double MaxDrawdown { get; set; }
        public int WinningTrades { get; set; }
        public int LosingTrades { get; set; }
        public double TotalGains { get; set; }
        public double TotalLosses { get; set; }
        public List<Trade> ClosedTrades { get; set; }


        /// <summary>
        /// Initializes a new instance of the Portfolio class with a specified starting capital.
        /// </summary>
        public Portfolio(double startingCapital)
        {
            Capital = startingCapital;
            PeakCapital = startingCapital;
            ClosedTrades = new List<Trade>();
        }

        /// <summary>
        /// Closes an active trade at a given exit price, updates the portfolio capital, tracks win/loss statistics, and archives the trade.
        /// </summary>
        public void CloseTrade(Trade trade, double exitPrice)
        {
            trade.ExitPrice = exitPrice;
            trade.Profit = trade.Shares * (exitPrice - trade.EntryPrice);
            Capital += trade.Profit;
            trade.IsOpen = false;

            if (trade.Profit > 0)
            {
                WinningTrades++;
                TotalGains += trade.Profit;
            }
            else if (trade.Profit < 0)
            {
                LosingTrades++;
                TotalLosses += Math.Abs(trade.Profit);
            }

            if (Math.Abs(trade.Profit) > 0)
            {
                ClosedTrades.Add(trade);
            }
        }

        /// <summary>
        /// Evaluates the current capital against the peak capital to update peak tracking and calculate maximum drawdown.
        /// </summary>
        public void UpdateDrawdown()
        {
            if (Capital > PeakCapital)
            {
                PeakCapital = Capital;
            }
            else
            {
                // Calculate current drawdown from the historical peak.
                double currentDrawdown = PeakCapital - Capital;
                if (currentDrawdown > MaxDrawdown)
                {
                    MaxDrawdown = currentDrawdown;
                }
            }
        }
    }
}
