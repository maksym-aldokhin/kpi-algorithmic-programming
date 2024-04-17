using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using Lab4;

namespace Benchmarks;

[CsvExporter]
[RPlotExporter]
public class Lab4Benchmark
{
    [Params("i don't", "justin bieber", "thug", "invasion")]
    public string searchOptions;
    
    public List<TrackModel> Tracks;
    
    public Lab4Benchmark()
    {
        ListExtensions.ParseCsv(out Tracks, "../../../../../../../../Benchmarks/Data/spotify_songs_sorted.csv");
    }

    [Benchmark]
    public void LinearSearch()
    {
        Search.Linear(Tracks, searchOptions).ToArray();
    }

    [Benchmark]
    public void JumpSearch()
    {
        Search.Jump(Tracks, searchOptions).ToArray();
    }
    
}