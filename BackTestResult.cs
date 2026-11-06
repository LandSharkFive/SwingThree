namespace SwingThree
{
    public record struct BackTestResult
    {
        public string StrategyName { get; set; }
        public double StartingCapital { get; set; }
        public double EndingCapital { get; set; }
        public double MaxDrawdown { get; set; }
        public int WinningTrades { get; set; }
        public int LosingTrades { get; set; }
        public double TotalGains { get; set; }
        public double TotalLosses { get; set; }
        public List<Trade> ClosedTrades { get; set; } 

        // Calculated Properties
        public double TotalReturnPct => StartingCapital > 0 ? ((EndingCapital - StartingCapital) / StartingCapital) * 100.0 : 0.0;
        public int TotalTrades => WinningTrades + LosingTrades;
        public double WinRatePct => TotalTrades > 0 ? ((double)WinningTrades / TotalTrades) * 100.0 : 0.0;
        public double AvgWin => WinningTrades > 0 ? TotalGains / WinningTrades : 0.0;
        public double AvgLoss => LosingTrades > 0 ? TotalLosses / LosingTrades : 0.0;
        public double ProfitFactor => TotalLosses > 0 ? TotalGains / TotalLosses : TotalGains;
    }
}
