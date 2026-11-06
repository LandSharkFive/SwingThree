namespace SwingThree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("INTC - Trend Analysis");
            string filename = @"c:\bruce\stock\intc2yd.csv";

            // Load historical price bars from disk.
            var history = CSVLoader.GetHistory(filename);

            if (history.Count == 0)
            {
                Console.WriteLine("No history loaded.");
                return;
            }

            var options = new BacktestOptions
            {
                ShortPeriod = 50,
                LongPeriod = 200,
                TrailingStopPct = 0.07,
                StartingCapital = 10000.0,
                StopPct = 0.02, 
                PositionSize = 1000
            };

            var backtester = new TrendBackTester(options);
            var result = backtester.Run(history);

            if (result.EndingCapital > 0)
            {
                // Print Performance Summary.
                PerformanceReporter.Print(result);
                Console.WriteLine();

                // Print Individual Trade Log.
                PerformanceReporter.PrintTradeLog(result.ClosedTrades);
            }
        }

    }
}
