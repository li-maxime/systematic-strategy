using CsvHelper;
using PricingLibrary.DataClasses;
using PricingLibrary.MarketDataFeed;
using PricingLibrary.RebalancingOracleDescriptions;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace HedgingLibrary
{
    public static class IOHandler
    {
        /*
         * Parses the .json input file containing the test parameters. Returns the test parameters in BasketTestParameters class format
         */
        public static BasketTestParameters ReadTestParameters(string path)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(), new RebalancingOracleDescriptionConverter() }
            };
            string testParametersRaw = File.ReadAllText(path: path);
            BasketTestParameters parameters = JsonSerializer.Deserialize<BasketTestParameters>(testParametersRaw, options) 
                ?? throw new InvalidOperationException("Incorrect test parameters fed to program");
            return parameters;
        }
        /*
         * Parses the .csv input file containing the market data. Returns the data as a list of ShareValue objects.
         */
        public static IEnumerable<ShareValue> ReadData(string absolutePath)
        {
            using var reader = new StreamReader(absolutePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            IEnumerable<ShareValue> testDataHard = csv.GetRecords<ShareValue>();
            return testDataHard.ToList();
        }
        /*
         * Adds a singular new OutputData to the current list "outputList" of OutputDatas.
         */
        public static void AddOutput(PricingResults results, DateTime date, double value, List<OutputData> outputList)
        {
            outputList.Add(new OutputData()
            {
                Deltas = results.Deltas,
                DeltasStdDev = results.DeltaStdDev,
                Date = date,
                Value = value,
                Price = results.Price,
                PriceStdDev = results.PriceStdDev
            }
            );
        }
        /*
         * Dumps the list of OutputData onto a .json @outputPath
         */
        public static void Dump(List<OutputData> outputs, string outputPath)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            File.WriteAllText(outputPath, JsonSerializer.Serialize(outputs, options));
        }
    }
}
