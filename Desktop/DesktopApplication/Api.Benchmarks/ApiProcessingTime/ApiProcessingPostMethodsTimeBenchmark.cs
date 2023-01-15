using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using DesktopInterface.Control;
using DesktopInterface.Dtos;

namespace Api.Benchmarks.ApiProcessingTime
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Monitoring, warmupCount: 3)]
    [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
    public class ApiProcessingPostMethodsTimeBenchmark
    {
        private readonly Uri _baseRequestUri = new Uri("http://192.168.1.98:5000");

        private readonly List<LedDto> _ledDto = new List<LedDto>();

        [Params(1, 10, 64)]
        public int Size;

        [Params(1, 3, 5)]
        public int RequestsCount;

        [GlobalSetup]
        public void Setup()
        {
            ApiHelper.InitializeClient();
            ApiHelper.ApiClient!.BaseAddress = _baseRequestUri;

            for (int i = 0; i < Size; i++)
            {
                _ledDto.Add(new LedDto(i / 8, i % 8));
            }
        }

        [Benchmark]
        public async Task ProcessGetDataObjectByIdBenchmark()
        {
            for (int i = 0; i < RequestsCount; i++)
                await ApiHelper.PostLeds(_ledDto);
        }
    }
}
