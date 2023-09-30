
using HedgingLibrary;
using PricingLibrary.RebalancingOracleDescriptions;
using System.Globalization;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        private readonly DateTime currentDate = DateTime.Parse("Sep 4, 2023", new CultureInfo("en-US"));
        [Test]
        public void TestRegularDailyOracle()
        {
            DateTime date = currentDate;
            RegularOracleDescription dailyOracleDescription = new()
            {
                Period = 1
            };
            Oracle regularOracle = new(dailyOracleDescription, date);
            Assert.That(regularOracle.Handler(date, dailyOracleDescription), Is.True);
            date = date.AddDays(1);
            Assert.That(regularOracle.Handler(date, dailyOracleDescription), Is.True);
        }
        [Test]

        public void TestRegularBidailyOracle()
        {
            DateTime date = currentDate;
            RegularOracleDescription dailyOracleDescription = new()
            {
                Period = 2
            };
            Oracle regularOracle = new(dailyOracleDescription, date);
            Assert.That(regularOracle.Handler(date, dailyOracleDescription), Is.False);
            date = date.AddDays(1);
            Assert.That(regularOracle.Handler(date, dailyOracleDescription), Is.True);
            date = date.AddDays(1);
            Assert.That(regularOracle.Handler(date, dailyOracleDescription), Is.False);
        }
        [Test]

        public void TestMondayOracle()
        {
            DateTime date = currentDate;
            WeeklyOracleDescription mondayOracleDescription = new()
            {
                RebalancingDay = DayOfWeek.Monday,
            };
            Oracle weeklyOracle = new(mondayOracleDescription, date);
            Assert.That(weeklyOracle.Handler(date, mondayOracleDescription), Is.True);
            date = date.AddDays(1);
            Assert.That(weeklyOracle.Handler(date, mondayOracleDescription), Is.False);
            date = date.AddDays(1);
            Assert.That(weeklyOracle.Handler(date, mondayOracleDescription), Is.False);
        }
        [Test]

        public void TestWednesdayOracle()
        {
            DateTime date = currentDate;
            WeeklyOracleDescription wednesdayOracleDescription = new()
            {
                RebalancingDay = DayOfWeek.Wednesday,
            };
            Oracle weeklyOracle = new(wednesdayOracleDescription, date);
            Assert.That(weeklyOracle.Handler(date, wednesdayOracleDescription), Is.False);
            date = date.AddDays(1);
            Assert.That(weeklyOracle.Handler(date, wednesdayOracleDescription), Is.False);
            date = date.AddDays(1);
            Assert.That(weeklyOracle.Handler(date, wednesdayOracleDescription), Is.True);
        }
        [Test]
        public void TestGetPortfolioValue()
        {
            string[] nameList = new string[] { "1", "2", "3" };
            Portfolio portfolio = new(currentDate ,nameList );
            double[] array1 = new double[] { 1,1,1};
            double[] array2 = new double[] {2,2,2};
            portfolio.UpdateCompo(array1, array1,currentDate, 4);

            double value = portfolio.GetPortfolioValue(currentDate, array2);
            Assert.That(value, Is.EqualTo(7));
        }
    }
}