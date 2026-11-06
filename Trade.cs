
namespace SwingThree
{

    /// <summary>
    /// Represents an individual trade execution, tracking its entry, exit, pricing details, and performance metrics.
    /// </summary>
    public class Trade
    {
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public double Profit { get; set; }
        public double Shares { get; set; }
        public double EntryPrice { get; set; }
        public double ExitPrice { get; set; }
        public double InitialStopLoss { get; set; }
        public int EntryIndex { get; set; }
        public bool IsOpen { get; set; } = true;
    }

}
