using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using DesktopInterface.Control;

namespace Api.Benchmarks.ApiProcessingTime
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Monitoring, warmupCount: 3)]
    [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
    public class ApiProcessingTimeByIdsComparisonBenchmark
    {
        private const string _temperature = "temperature";
        private const string _humidity = "humidity";
        private const string _pressure = "pressure";
        private const string _roll = "roll";
        private const string _pitch = "pitch";
        private const string _yaw = "yaw";

        private const string _url = "http://192.168.1.98:5000";

        private readonly Uri _baseRequestUri = new Uri(_url);

        [Params(1, 10)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            ApiHelper.InitializeClient();
            ApiHelper.ApiClient!.BaseAddress = _baseRequestUri;
        }

        [Benchmark]
        public async Task ProcessDataObjectByIdTemperatureBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataObjectById(_temperature);
        }

        [Benchmark]
        public async Task ProcessDataObjectByIdHumidityBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataObjectById(_humidity);
        }

        [Benchmark]
        public async Task ProcessDataObjectByIdPressureBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataObjectById(_pressure);
        }

        [Benchmark]
        public async Task ProcessDataObjectByIdRollBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataObjectById(_roll);
        }

        [Benchmark]
        public async Task ProcessDataObjectByIdPitchBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataObjectById(_pitch);
        }

        [Benchmark]
        public async Task ProcessDataObjectByIdYawBenchmark()
        {
            for (int i = 0; i < N; i++)
                await ApiHelper.GetDataObjectById(_yaw);
        }
    }
}
