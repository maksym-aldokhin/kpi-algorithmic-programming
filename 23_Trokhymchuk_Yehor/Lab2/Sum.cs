

namespace Lab2;

public class Sum
{
    //private const double EXP = 2.718; i can use EXP and method 'my pow', but, because of precision, i prefer
    // Math.Exp
    
    private double _result;
    private TimeLogger.TimeLogger _logger;
    private ThreadLocal<double> _resultPerThread;
    
    public Sum()
    {
        _logger = new();
        _resultPerThread = new();
    }

    #region Iterative

    public double Iterative(double t, double n)
    {
        _result = 0;
        
        for (; t <= n; t++)
        {
            _result += (t + MyPowIterative(2, t)) / (1 + Math.Exp(t));
        }

        return _result;
    }
    
    public double IterativeLog(double t, double n)
    {
        _result = 0;

        _logger.SetMessage().Start();
        for (; t <= n; t++)
        {
            _result += (t + MyPowIterative(2, t)) / (1 + Math.Exp(t));
        }
        _logger.Stop();
        
        return _result;
    }

    private double MyPowIterative(double a, double b)
    {
        double temp = a;
        
        if (b == 0)
        {
            return 1;
        }
        if (b > 0)
        {
            b--;
            for (; b != 0; b--)
            {
                a *= temp;
            }

            return a;
        }

        b++;
        for (; b != 0; b++)
        {
            a *= temp;
        }

        return 1 / a;
    }
    
    #endregion

    #region Recursive

    public double Recursive(double t, double n, double res = 0)
    {
        res += (t + MyPowRecursive(2, t)) / (1 + Math.Exp(t));
        
        if (t < n)
        {
            return Recursive(t + 1, n, res);
        }

        return res;
    }
    
    public double RecursiveLog(double t, double n)
    {
        _logger.SetMessage().Start();
        _result = Recursive(t, n);
        _logger.Stop();
        
        return _result;
    }
    
    private double MyPowRecursive(double a, double b)
    {
        if (b == 0)
        {
            return 1;
        }
        if (b > 0)
        {
            a *= MyPowRecursive(a, b - 1);
        }
        else
        {
            a = 1 / a * MyPowRecursive(a, b + 1);
        }
        
        return a;
    }

    #endregion

    #region BetterRecursive
    
    private double SecondVerRecursiveHelper(double t, double n)
    {
        _resultPerThread.Value += (t + MyPowRecursive(2, t)) / (1 + Math.Exp(t));

        if (t < n)
        {
            return SecondVerRecursiveHelper(t + 1, n);
        }
        
        return _resultPerThread.Value;
    }

    public double SecondVerRecursive(double t, double n)
    {
        _resultPerThread.Value = 0;
        SecondVerRecursiveHelper(t, n);
        
        return _resultPerThread.Value;
    }
    
    public double SecondVerRecursiveLog(double t, double n)
    {
        _result = 0;
        _logger.SetMessage().Start();
        SecondVerRecursiveHelper(t, n);
        _logger.Stop();
        
        return _resultPerThread.Value;
    }

    #endregion
    
    public string GetLog() => _logger.ToString();
    public void ClearLog() => _logger.Clear();
    

    
}
