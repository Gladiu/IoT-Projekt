using Api.Benchmarks.ApiProcessingTime;
using BenchmarkDotNet.Running;

namespace Api.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<ApiProcessingPostMethodsTimeBenchmark>();
    }
}