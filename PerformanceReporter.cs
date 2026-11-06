namespace SwingThree
{
    public static class PerformanceReporter
    {
        public static void Print(BackTestResult result)
        {
            Console.WriteLine($"{result.StrategyName}");
            Console.WriteLine($"Starting Capital: ${result.StartingCapital:N2}");
            Console.WriteLine($"Ending Capital:   ${result.EndingCapital:N2}");
            Console.WriteLine($"Total Return:     {result.TotalReturnPct:F2}%");
            Console.WriteLine($"Winning Trades:   {result.WinningTrades}");
            Console.WriteLine($"Losing Trades:    {result.LosingTrades}");
            Console.WriteLine($"Win Rate:         {result.WinRatePct:F2}%");
            Console.WriteLine($"Drawdown:         ${result.MaxDrawdown:N2}");
            Console.WriteLine($"Profit Factor:    {result.ProfitFactor:F2}");
            Console.WriteLine($"Avg Win:          ${result.AvgWin:N2}");
            Console.WriteLine($"Avg Loss:         ${result.AvgLoss:N2}");
            Console.WriteLine();
        }

        public static void PrintTradeLog(List<Trade> trades)
        {
            Console.WriteLine("Index,EntryDate,ExitDate,EntryPrice,ExitPrice,Profit");

            for (int i = 0; i < trades.Count; i++)
            {
                var t = trades[i];
                Console.WriteLine($"{i + 1},{t.EntryDate:d},{t.ExitDate:d},{t.EntryPrice:F2},{t.ExitPrice:F2},{t.Profit:F2}");
            }
        }

    }
}
