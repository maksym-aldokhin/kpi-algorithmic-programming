using Random = System.Random;

namespace TwoDArrOps;

public class TwoDArr
{
    private const int MAX_ELEMENTS_TO_DISPLAY = 50;
    
    private Random _random;
    private int[,] _arr;
    private Action<string> _printAction;
    private int _lengthX, _lengthY;
    private TimeLogger.TimeLogger _timeLogger;
    
    public TwoDArr(Action<string> printAction)
    {
        _random = new();
        _printAction = printAction;
        _timeLogger = new();
    }

    public void Print()
    {
        if (_lengthX >= MAX_ELEMENTS_TO_DISPLAY 
            || _lengthY >= MAX_ELEMENTS_TO_DISPLAY)
        {
            _printAction("Too many elements to print them.\n");
            return;
        }
        
        for (int i = 0; i < _lengthY; i++)
        {
            for (int j = 0; j < _lengthX; j++)
            {
                _printAction($"{_arr[i, j], -3}");
            }
            _printAction(Environment.NewLine);
        }
    }

    public void Init(int lengthX, int lengthY, int minVal = 0, int maxVal = 10)
    {
        checked
        {
            try
            {
                int _ = lengthX * lengthY;
            }
            catch(OverflowException e)
            {
                _printAction(e.Message);
                return;
            }
        }
        
        _arr = new int[lengthY, lengthX];
        _lengthX = lengthX;
        _lengthY = lengthY;
        
        for (int i = 0; i < _lengthY; i++)
        {
            for (int j = 0; j < _lengthX; j++)
            {
                _arr[i, j] = _random.Next(minVal, maxVal);
            }
        }
    }

    public void FlipColumns()
    {
        if (_lengthX == 0 || _lengthY == 0)
        {
            return;
        }
        
        _timeLogger.SetMessage().Start();
        
        int constRightBound = _lengthY - 1,
            rightBound = constRightBound,
            leftBound = 0,
            temp;

        for (int j = 0; j < _lengthX; j++)
        {
            _timeLogger.CountIteration();
            while (leftBound < rightBound)
            {
                _timeLogger.CountIteration();
                
                temp = _arr[leftBound, j];
                _arr[leftBound, j] = _arr[rightBound, j];
                _arr[rightBound, j] = temp;
                rightBound--;
                leftBound++;
            }

            rightBound = constRightBound;
            leftBound = 0;
        }
        
        _timeLogger.Stop();
    }

    public void MoveRows()
    {
        if (_lengthX == 0 || _lengthY == 0)
        {
            return;
        }
        
        _timeLogger.SetMessage().Start();
        
        int[] temp = new int[_lengthX];
        
        for (int j = _lengthY - 1; j > 1; j--)
        {
            _timeLogger.CountIteration();
            
            GetSubArrFromSource(temp, j - 2);
            WriteSubArr(j - 1, j - 2);
            WriteSubArr(j, j - 1);
            CopyToSource(temp, j);
        }
        
        _timeLogger.Stop();
    }

    private void GetSubArrFromSource(int[] arr, int posY)
    {
        for (int i = 0; i < _lengthX; i++)
        {
            _timeLogger.CountIteration();
            arr[i] = _arr[posY, i];
        }
    }

    private void WriteSubArr(int fromY, int toY)
    {
        for (int i = 0; i < _lengthX; i++)
        {
            _timeLogger.CountIteration();
            _arr[toY, i] = _arr[fromY, i];
        }
    }

    private void CopyToSource(int[] arr, int toY)
    {
        for (int i = 0; i < _lengthX; i++)
        {
            _timeLogger.CountIteration();
            _arr[toY, i] = arr[i];
        }
    }

    public string GetTimeInfo() =>
        _timeLogger.ToString();

    public void ClearLog() =>
        _timeLogger.Clear();
}