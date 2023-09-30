using PricingLibrary.MarketDataFeed;

namespace HedgingLibrary
{
    public class Portfolio
    {
        private readonly Dictionary<string, double> composition;
        protected readonly string[] underlyingNames; // Absolute order of assets in the portfolio. [0] is always associated with the Risk Free asset.
        private readonly DateTime creation; // Date at which the portfolio was created, to compute capitalization of the Risk Free asset.
        public Portfolio(DateTime creationDate, string[] names)
        {
            composition = new Dictionary<string, double>();
            composition["Risk Free"] = 0;
            underlyingNames = names;
            foreach (string name in names)
            {
                composition[name] = 0;
            }
            creation = creationDate;
        }
        /*
         * Updates the composition of the portfolio. Uses the positions needed for each risky asset as input to deduce
         * the quantity of Risk Free asset needed and updates the portfolio.
         */
        public void UpdateCompo(double[] compoRisky, double[] spots, DateTime today, double value)
        {
            var tau = RiskFreeRateProvider.GetRiskFreeRateAccruedValue(creation, today);
            double riskFree = value;
            for (int i = 0; i < compoRisky.Length; i++)
            {
                composition[underlyingNames[i]] = compoRisky[i];
                riskFree -= (compoRisky[i] * spots[i]);
            }
            composition["Risk Free"] = riskFree / tau;
        }
        /*
         * Computes the current portfolio value
         */
        public double GetPortfolioValue(DateTime today, double[] spots)
        {
            var tau = RiskFreeRateProvider.GetRiskFreeRateAccruedValue(creation, today);
            double portfolioValue = composition["Risk Free"] * tau;
            for (int i = 0; i < spots.Length; i++)
            {
                portfolioValue += spots[i] * composition[underlyingNames[i]];
            }
            return portfolioValue;
        }
    }
}

