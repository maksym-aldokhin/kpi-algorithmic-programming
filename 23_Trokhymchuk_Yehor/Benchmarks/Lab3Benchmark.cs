using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Lab3;

namespace Benchmarks;

[RPlotExporter]
[XmlExporter]
[MemoryDiagnoser]
[SimpleJob(RunStrategy.Throughput, launchCount: 1, warmupCount: 5, iterationCount: 1)]
public class Lab3Benchmark
{
    [Params(10, 50, 100, 500, 1000)]
    public int dimLength;
    public int[,] _arr;

    [GlobalSetup]
    public void Setup()
    {
        _arr = ArrayExtensions.InitMatrix(dimLength, -100, 101);
    }
    
    [Benchmark]
    public void MergeRecVer1()
    {
        ArrayExtensions.SortHalf(Sort.MergeRecAscVer1, _arr);
    }
    
    [Benchmark]
    public void MergeRecVer2()
    {
        ArrayExtensions.SortHalf(Sort.MergeRecAscVer2, _arr);
    }
    
    [Benchmark]
    public void MergeIter()
    {
        ArrayExtensions.SortHalf(Sort.MergeIterAsc, _arr);
    }
    
    [Benchmark]
    public void Insertion()
    {
        ArrayExtensions.SortHalf(Sort.InsertionAsc, _arr);
    }
}