using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using DesktopInterface.Control;

namespace Api.Benchmarks.ApiProcessingTime
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Monitoring, warmupCount: 3)]
    [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
    public class ApiProcessingTimeBenchmark
    {
        private const string _id = "temperature";

        private readonly Uri _baseRequestUri = new Uri("http://192.168.1.98:5000");

        [Params(5, 10, 15)]
        public int N;

        [GlobalSetup]
        public void Setup() 
        {
            ApiHelper.InitializeClient();
            ApiHelper.ApiClient!.BaseAddress = _baseRequestUri;
        }

        [Benchmark(Baseline = true)]
        public async Task ProcessGetDataObjectByIdBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataObjectById(_id);
        }

        [Benchmark]
        public async Task ProcessGetDataStructsBenchmark() 
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataStructsList();
        }

        [Benchmark]
        public async Task ProcessGetDataObjectsBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataStructsList();
        }
    }
}
