using System.Globalization;

namespace SwingThree
{
    public class CSVLoader
    {

        /// <summary>
        /// Loads historical price data from a CSV file, expecting Yahoo Finance standard columns: Date, Open, High, Low, Close.
        /// </summary>
        public static List<PriceBar> LoadFromCsv(string filePath)
        {
            var data = new List<PriceBar>();
            var lines = File.ReadAllLines(filePath);

            // Skip header line.
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');

                // Expecting OHLC standard columns: Date, Open, High, Low, Close.
                if (DateTime.TryParse(parts[0], CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                    double.TryParse(parts[1], CultureInfo.InvariantCulture, out var open) &&
                    double.TryParse(parts[2], CultureInfo.InvariantCulture, out var high) &&
                    double.TryParse(parts[3], CultureInfo.InvariantCulture, out var low) &&
                    double.TryParse(parts[4], CultureInfo.InvariantCulture, out var close))
                {
                    data.Add(new PriceBar(date, open, high, low, close));
                }
            }

            // Ensure ascending chronological order
            data.Sort((x, y) => x.Date.CompareTo(y.Date));
            return data;
        }


        /// <summary>
        /// Retrieves historical price data from the specified file after validating that the file exists.
        /// </summary>
        public static List<PriceBar> GetHistory(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File not found at {filename}");
                return new List<PriceBar>();
            }
            return LoadFromCsv(filename);
        }

    }
}
