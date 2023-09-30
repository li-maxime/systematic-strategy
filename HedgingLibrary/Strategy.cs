using PricingLibrary.Computations;
using PricingLibrary.DataClasses;
using PricingLibrary.MarketDataFeed;
using PricingLibrary.TimeHandler;

namespace HedgingLibrary
{
    public class Strategy
    {
        private readonly DateTime initial;
        private readonly DateTime maturity;
        private readonly Pricer pricer;
        private readonly Oracle oracle;
        private readonly BasketTestParameters basketTestParameters;
        private readonly List<DataFeed> marketData;
        private readonly Portfolio portfolio;
        private readonly string[] shareName;
        private readonly int size;

        public Strategy(BasketTestParameters testParams, IEnumerable<ShareValue> testData, Pricer newPricer)
        {
            maturity = testParams.BasketOption.Maturity;
            initial = testData.First().DateOfPrice;

            basketTestParameters = testParams;
            marketData = testData.GroupBy(d => d.DateOfPrice, t => new { Symb = t.Id.Trim(), Val = t.Value },(key, g) 
                => new DataFeed(key, g.ToDictionary(e => e.Symb, e => e.Val))).Where(g => g.Date <= maturity).OrderBy(g => g.Date).ToList();
            //Where -> if the data exceeds the maturity of the derivative. OrderBy -> if the data is not chronologically ordered
            pricer = newPricer;
            oracle = new Oracle(testParams.RebalancingOracleDescription, initial);
            shareName = testParams.BasketOption.UnderlyingShareIds;
            size = shareName.Length;

            portfolio = new Portfolio(initial, shareName);
        }
        
        public List<OutputData> RunStrategy()
        {
            List<OutputData> output = new();
            double[] spots = ReadSpot(marketData[0].PriceList);
            PricingResults results = pricer.Price(MathDateConverter.ConvertToMathDistance(initial, maturity), spots);

            double currentValue = results.Price;
            portfolio.UpdateCompo(results.Deltas, spots, initial, currentValue);

            IOHandler.AddOutput(results, initial, currentValue, output);

            foreach (var dailyData in from dailyData in marketData.Skip(1)
                                      where oracle.Handler(dailyData.Date, basketTestParameters.RebalancingOracleDescription)
                                      select dailyData)
            {
                spots = ReadSpot(dailyData.PriceList);
                currentValue = portfolio.GetPortfolioValue(dailyData.Date, spots);
                results = pricer.Price(MathDateConverter.ConvertToMathDistance(dailyData.Date, maturity), spots);
                portfolio.UpdateCompo(results.Deltas, spots, dailyData.Date, currentValue);
                IOHandler.AddOutput(results, dailyData.Date, currentValue, output);
            }

            return output;
        }
        /*
         * Reads the spot values of the risky assets and returns an array with the values ordered as in the basket 
         * option.
         */
        public double[] ReadSpot(Dictionary<string, double> dailySpot) {
            double[] spots = new double[size];
            for(var i =0; i< size; i++)
            {
                spots[i] = dailySpot[shareName[i]];
            }
            return spots;
        }
    }
}
