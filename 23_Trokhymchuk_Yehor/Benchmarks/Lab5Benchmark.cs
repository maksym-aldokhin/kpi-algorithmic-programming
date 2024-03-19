using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using CommandLine;
using Lab5;

namespace Benchmarks;

[RPlotExporter]
[XmlExporter]
[MemoryDiagnoser]
public class Lab5Benchmark
{
    [Params(values: new object[]
        { "99999999999999", "-2489072380472389074982", "839057893475089347095703892579823457" })]
    public string Numbers;

    public MyBigInteger a;
    public MyBigInteger b;

    [GlobalSetup]
    public void Setup()
    {
        MyBigInteger.TryParse(Numbers, out var result1);
        MyBigInteger.TryParse(Numbers, out var result2);

        a = result1;
        b = result2;
    }
    
    [Benchmark]
    public void MultiplyTest()
    {
        var result = MyBigInteger.Multiply(a, b);
    }
    
    [Benchmark]
    public void MultiplyOptimizedTest()
    {
        var result = MyBigInteger.MultiplyOptimized(a, b);
    }
}