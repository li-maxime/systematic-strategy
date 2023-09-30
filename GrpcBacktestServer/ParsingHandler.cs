using Google.Protobuf.WellKnownTypes;
using PricingLibrary.DataClasses;
using PricingLibrary.MarketDataFeed;
using PricingLibrary.RebalancingOracleDescriptions;

namespace GrpcBacktestServer
{
    public static class ParsingHandler
    {
        /*
         * Parses Test Parameters from client input (test_proto format) to our Pricing Library format
         */
        public static BasketTestParameters ParseTestParameters(TestParams parameters)
        {

            BasketTestParameters testParams = new()
            {
                //          Basket option           //
                BasketOption = new Basket
                {
                    Maturity = parameters.BasketParams.Maturity.ToDateTime(),
                    Strike = parameters.BasketParams.Strike,
                    UnderlyingShareIds = parameters.BasketParams.ShareIds.ToArray(),
                    Weights = parameters.BasketParams.Weights.ToArray()
                },

                //          Pricing params          //
                PricingParams = new BasketPricingParameters()
            };
            //          Correlations            //
            var corrs = new List<double[]>();
            foreach (var corr in parameters.PriceParams.Corrs)
            {
                corrs.Add(corr.Value.ToArray());
            }
            testParams.PricingParams.Correlations = corrs.ToArray();
                
            testParams.PricingParams.Volatilities = parameters.PriceParams.Vols.ToArray();

            //          Rebalancing Oracle Description          //
            switch (parameters.RebParams.RebTypeCase)
            {
                case RebalancingParams.RebTypeOneofCase.None:
                    break;

                case RebalancingParams.RebTypeOneofCase.Regular:
                    RegularOracleDescription oracleRegular = new()
                    {
                        Period = parameters.RebParams.Regular.Period
                    };
                    testParams.RebalancingOracleDescription = oracleRegular;
                    break;

                case RebalancingParams.RebTypeOneofCase.Weekly:
                    WeeklyOracleDescription oracleWeekly = new()
                    {
                        RebalancingDay = (DayOfWeek)parameters.RebParams.Weekly.Day
                    };
                    testParams.RebalancingOracleDescription = oracleWeekly;
                    break;
            }
            return testParams;
        }

        /*
         * Parses Test Data from client input (test_proto format) to our Pricing Library format
         */
        public static IEnumerable<ShareValue> ParseMarketData(DataParams data)
        {
            List<ShareValue> testData = new();
            foreach(var row in data.DataValues.ToArray())
            {
                ShareValue newValue = new()
                {
                    Value = row.Value,
                    Id = row.Id,
                    DateOfPrice = row.Date.ToDateTime()
                };
                testData.Add(newValue);
            }
            return testData;
        }

        /*
         * Parses OutputData from server output (Pricing Library classes format) to client format (test_proto)
         */
        public static BacktestOutput ParseToOutput(List<OutputData> outputDatas)
        {
            BacktestOutput output = new();
            foreach(var outputData in outputDatas)
            {
                BacktestInfo info = new()
                {
                    Date = Timestamp.FromDateTime(DateTime.SpecifyKind(outputData.Date, DateTimeKind.Utc)),
                    PortfolioValue = outputData.Value,
                    Price = outputData.Price,
                    PriceStddev = outputData.PriceStdDev
                };
                info.Delta.Add(outputData.Deltas);
                info.DeltaStddev.Add(outputData.DeltasStdDev);

                output.BacktestInfo.Add(info);
            }
            return output;
        }
    }
}
