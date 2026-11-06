
namespace SwingThree
{

    public class BacktestOptions
    {
        public int ShortPeriod { get; set; }
        public int LongPeriod { get; set; } 
        public double TrailingStopPct { get; set; } 
        public double StartingCapital { get; set; } 
        public double StopPct { get; set; }
        public int PositionSize { get; set; }
    }

}
