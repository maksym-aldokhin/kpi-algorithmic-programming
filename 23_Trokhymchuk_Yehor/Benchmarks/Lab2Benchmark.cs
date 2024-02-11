using System.CodeDom;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Lab2;

namespace Benchmarks;

[RankColumn]
[MemoryDiagnoser]
[RPlotExporter]
public class Lab2Benchmark
{
    private Sum _sum = new();

    [Params(10, 20, 50)] public int UpperBound;

    [Benchmark]
    public void SumRecursiveTest()
    {
        _sum.Recursive(0, UpperBound);
    }

    [Benchmark]
    public void SumIterativeTest()
    {
        _sum.Iterative(0, UpperBound);
    }
}
