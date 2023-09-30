using PricingLibrary.RebalancingOracleDescriptions;


namespace HedgingLibrary
{
    public class Oracle
    {
        private readonly DelegateOracle handler; // When the handler is called, it redirects to the correct RebalancingTime oracle according to the oracle type.
        public DelegateOracle Handler { get { return handler; } }
        private int index;
        private DateTime nextRebalancingDate; // Smart computation of the next theoretical Rebalancing Date
        public Oracle(IRebalancingOracleDescription oracle, DateTime today)
        {
            if (oracle.Type == RebalancingOracleType.Weekly)
            {
                WeeklyOracleDescription weeklyOracleDescription = (WeeklyOracleDescription)oracle;
                handler = WeeklyRebalancingTime;
                NextWeeklyRebalancingDate(today, weeklyOracleDescription.RebalancingDay);
            }
            else
            {
                handler = RegularRebalancingTime;
            }
            index = 0;
        }
        /*
         * Delegate to call the coherent rebalancing function
         */
        public delegate bool DelegateOracle(DateTime today, IRebalancingOracleDescription oracle);

        /*
         * Intelligent rebalancing time function for Weekly oracles. If the day exceeds the theoretical next rebalancing
         * date of the oracle, the oracle calls for a rebalancing.
         */ 
        private bool WeeklyRebalancingTime(DateTime today, IRebalancingOracleDescription oracle)
        {
            WeeklyOracleDescription weeklyOracle = (WeeklyOracleDescription)oracle;
            index++;
            if (today.DayOfWeek == weeklyOracle.RebalancingDay || today >= nextRebalancingDate)
            {
                NextWeeklyRebalancingDate(today, weeklyOracle.RebalancingDay);
                return true;
            }
            return false;
        }
        /*
         * Rebalancing time function for Regular oracles.
         */
        private bool RegularRebalancingTime(DateTime today, IRebalancingOracleDescription oracle)
        {
            RegularOracleDescription regularOracle = (RegularOracleDescription)oracle;
            index++;
            return index % regularOracle.Period == 0;
        }
        /*
         * Smart computation of the next theoretical rebalancing date
         */
        private void NextWeeklyRebalancingDate(DateTime today, DayOfWeek rebalancingDay)
        {
            nextRebalancingDate = today.AddDays(7).AddDays(-1*(7+(today.DayOfWeek-rebalancingDay))%7);
        }

    }
}
