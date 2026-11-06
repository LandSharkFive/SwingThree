
namespace SwingThree
{
    public class TechnicalIndicators
    {

        /// <summary>
        /// Calculates a Simple Moving Average (SMA) series for a given history of price bars over a specified period.
        /// </summary>
        public static double[] GetSMASeries(List<PriceBar> history, int period)
        {
            if (history == null || history.Count == 0 || period <= 0)
            {
                return Array.Empty<double>();
            }

            double[] sma = new double[history.Count];
            double sum = 0;

            for (int i = 0; i < history.Count; i++)
            {
                sum += history[i].Close;

                if (i >= period)
                {
                    // Subtract the price that fell out of the moving window.
                    sum -= history[i - period].Close;
                }

                // Populate SMA value once enough data points exist.
                if (i >= period - 1)
                {
                    sma[i] = sum / period;
                }
                else
                {
                    // Starting Range is empty.
                    sma[i] = double.NaN; 
                }
            }

            return sma;
        }

    }
}
