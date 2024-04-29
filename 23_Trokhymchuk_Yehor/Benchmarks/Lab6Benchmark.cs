using BenchmarkDotNet.Attributes;
using Lab6;

namespace Benchmarks;

[RPlotExporter]
[XmlExporter]
[MemoryDiagnoser]
public class Lab6Benchmark
{
    [Params (values: new object[] {5})]
    public int DimLength;
    
    public ZeroBasedMatrix<int> zMatrixMultiplyB = null!;
    public ZeroBasedMatrix<int> zMatrixMultiplyA = null!;
    public ZeroBasedMatrix<int> zMatrixSort = null!;
    
    public Lab6Benchmark()
    {
        zMatrixMultiplyA = new();
        zMatrixMultiplyB = new();
        zMatrixSort = new();
    }
    
    [GlobalSetup]
    public void Setup()
    {
        var arr = MatrixExtensions.RandomInit(DimLength, 0.4);
        
        zMatrixMultiplyA.ParseMatrix(arr);
        zMatrixMultiplyB.ParseMatrix(arr);
        zMatrixSort.ParseMatrix(arr);
    }

    [Benchmark]
    public void InsertionSort()
    {
        zMatrixSort.Sort();
    }

    [Benchmark]
    public void Multiplication()
    {
        zMatrixMultiplyA.Multiply(zMatrixMultiplyB);
    }
}