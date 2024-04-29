using System.Numerics;
using DoubleLinkedList;

namespace Lab6;

public sealed class ZeroBasedMatrix<T> : IDisposable where T : INumber<T>
{
    public int DimLength { get; private set; }

    private DoubleLinkedList<ZeroBasedMatrixNode<T>> _nonZeroValues;

    public T this[int indA, int indB]
    {
        get
        {
            if (indA < 0  || indB < 0)
            {
                throw new IndexOutOfRangeException();
            }
            
            foreach (var item in _nonZeroValues.ReadFromHead())
            {
                if (item.X == indA && item.Y == indB)
                {
                    return item.Data;
                }
            }
            
            return T.Zero;
        }
    }
    public ZeroBasedMatrix()
    {
        _nonZeroValues = new();
    }

    private static bool IsZeroBased(T[,] matrix)
    {
        int dimLenght = matrix.GetLength(0);

        if (dimLenght * dimLenght != matrix.Length)
        {
            return false;
        }

        int zerosCount = 0;

        for (int i = 0; i < dimLenght; i++)
        {
            for (int j = 0; j < dimLenght; j++)
            {
                if (matrix[i, j] == T.Zero)
                {
                    zerosCount++;
                }
            }
        }

        if (zerosCount > matrix.Length / 2)
        {
            return true;
        }

        return false;
    }

    public bool ParseMatrix(T[,] matrix)
    {
        if (!IsZeroBased(matrix))
        {
            return false;
        }

        DimLength = matrix.GetLength(0);

        for (int i = 0; i < DimLength; i++)
        {
            for (int j = 0; j < DimLength; j++)
            {
                if (matrix[j, i] != T.Zero)
                {
                    _nonZeroValues.AddToEnd(new(i, j, matrix[j, i]));
                }
            }
        }

        return true;
    }


    private (int start, int end) CalcDist(ZeroBasedMatrixNode<T> leftBound, int startInd)
    {
        for (int i = startInd; i < _nonZeroValues.Capacity; i++, startInd++)
        {
            if (leftBound.Y != _nonZeroValues[i].Y)
            {
                return (_nonZeroValues.TakeInd(_nonZeroValues[i]) - startInd,
                    startInd);
            }

            
        }

        return (_nonZeroValues.Capacity - startInd,
            startInd);
    }

    private bool IsEmpty(int begin)
    {
        for (int i = begin; i < begin + DimLength; i++)
        {
            if (!_nonZeroValues[i].Data.Equals(0))
            {
                return true;
            }
        }

        return false;
    }

    public void Sort() // insertion
    {
        if (_nonZeroValues.Capacity == 0)
        {
            return;
        }

        int sortPos = 0;
        int ind = 0;
        int end = 0, start = 0;


        ZeroBasedMatrixNode<T> distBound = _nonZeroValues[end];

        while (sortPos < DimLength && start < _nonZeroValues.Capacity)
        {
            var res = CalcDist(distBound, end);
            end = res.end;
            var temp = _nonZeroValues[start].Y;
            if (temp <= sortPos)
            {
                /*if (end < _nonZeroValues.Capacity)
                {
                    distBound = _nonZeroValues[end];
                }*/
                distBound = _nonZeroValues.TakeElementByInd(end)!;
                
                if (temp % 2 == 0)
                {
                    for (int i = start + 1; i < end; i++)
                    {
                        var leftBound = _nonZeroValues[i - 1];

                        if (leftBound.CompareTo(_nonZeroValues[i]) > 0)
                        {
                            _nonZeroValues.Swap(i - 1, i);
                            for (int j = i - 1; j > start; j--)
                            {
                                if (_nonZeroValues[j].CompareTo(_nonZeroValues[j - 1]) < 0)
                                {
                                    _nonZeroValues.Swap(j - 1, j);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    var lencpy = DimLength;
                    for (int i = end - 1; i >= start; i--)
                    {
                        _nonZeroValues[i].X = --lencpy;
                    } 
                }
                start = end;
            } 
            sortPos++;
        }
    }
    public ZeroBasedMatrix<T> Multiply(ZeroBasedMatrix<T> matrix)
    {
        var resultMatrix = new ZeroBasedMatrix<T>();
        ZeroBasedMatrixNode<T> prev = null!;
        T result;
        
        for (int i = 0; i < DimLength; i++) 
        {
            for (int j = 0; j < DimLength; j++) 
            {
                result = default;
                for (int k = 0; k < DimLength; k++)
                {
                    result += this[i, k] * matrix[k, j];
                }
                resultMatrix._nonZeroValues.AddToEnd(new(j, i, result));
            }
        }

        resultMatrix.DimLength = DimLength;
        return resultMatrix;
    }

    public IEnumerator<ZeroBasedMatrixNode<T>> GetEnumerator()
        => _nonZeroValues.ReadFromHead().GetEnumerator();

    public void Dispose()
    {
        _nonZeroValues.Clear();
    }
}