using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace TimeLogger;

public class TimeLogger
{
    private StringBuilder _stringBuilder;
    private Stopwatch _timer;
    private int _iterationsCount;
    
    public TimeLogger()
    {
        _stringBuilder = new();
        _timer = new();
    }
    
    public TimeLogger Start()
    {
        _timer.Start();
        return this;
    }

    public TimeLogger SetMessage([CallerMemberName]string message = " ")
    {
        _stringBuilder.Append(message + " : ");
        return this;
    }

    public void Stop()
    {
        _stringBuilder.AppendLine(_timer.Elapsed.TotalMilliseconds + " ms");
        _timer.Reset();
        _iterationsCount = 0;
    }

    public override string ToString() =>
        _stringBuilder.ToString();

    public void Clear() =>
        _stringBuilder.Clear();

    public void CountIteration() => _iterationsCount++;
}
