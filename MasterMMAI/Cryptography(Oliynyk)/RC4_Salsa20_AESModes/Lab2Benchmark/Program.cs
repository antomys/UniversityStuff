using BenchmarkDotNet.Running;

namespace Lab2Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CryptoAlgoBenchmark>();
        }
    }
}
