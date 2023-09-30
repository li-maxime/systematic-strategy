using Grpc.Core;
using HedgingLibrary;
using PricingLibrary.Computations;
using PricingLibrary.DataClasses;


namespace GrpcBacktestServer.Services
{
    public class BacktestService:BacktestRunner.BacktestRunnerBase
    {
        public override Task<BacktestOutput> RunBacktest(BacktestRequest request, ServerCallContext context)
        {
            var testParametersHard = ParsingHandler.ParseTestParameters(request.TstParams);
            var testDataHard = ParsingHandler.ParseMarketData(request.Data);

            Pricer pricer = new(testParametersHard);
            Strategy deltaHedging = new(testParametersHard, testDataHard, pricer);

            List<OutputData> outputs = deltaHedging.RunStrategy();
            BacktestOutput output = ParsingHandler.ParseToOutput(outputs);

            return Task.FromResult(output);
        }
    }
}