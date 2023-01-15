using Api.Benchmarks.ApiProcessingTime;
using BenchmarkDotNet.Running;

namespace BenchmarkProfiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher
                .FromAssembly(typeof(ApiProcessingTimeBenchmark).Assembly)
                .Run(args);
        }
    }
}

