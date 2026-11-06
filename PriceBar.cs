
namespace SwingThree
{
    /// <summary>
    /// Represents a single period's price action (OHLCV-style) for a financial instrument.
    /// </summary>
    public record PriceBar(DateTime Date, double Open, double High, double Low, double Close);
}
