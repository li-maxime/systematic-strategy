// See https://aka.ms/new-console-template for more information
using HedgingLibrary;
using PricingLibrary.Computations;
using PricingLibrary.DataClasses;

namespace BacktestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            string paramPath = args[0];
            string paramData = args[1];
            string outputPath = args[2];
            
            BasketTestParameters testParametersHard = IOHandler.ReadTestParameters(paramPath);
            var testDataHard = IOHandler.ReadData(paramData);

            Pricer pricer = new(testParametersHard);
            Strategy deltaHedging = new(testParametersHard, testDataHard, pricer);

            List<OutputData> outputs = deltaHedging.RunStrategy();

            IOHandler.Dump(outputs, outputPath);

        }
    }
}
