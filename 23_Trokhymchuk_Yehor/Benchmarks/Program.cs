using BenchmarkDotNet.Running;

namespace Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        //BenchmarkRunner.Run<Lab2Benchmark>();
        //BenchmarkRunner.Run<Lab3Benchmark>();
        //BenchmarkRunner.Run<Lab4Benchmark>();
        //BenchmarkRunner.Run<Lab5Benchmark>();
        BenchmarkRunner.Run<Lab6Benchmark>();
        Console.ReadKey();
    }
}
