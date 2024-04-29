using System.Net.Mail;
using DoubleLinkedList;

namespace Lab6;

public static class MatrixExtensions
{
    public static Action ChangeColor;
    public static Action ResetColor;
    public static Action<string> PrintAction;
    
    public const int MAX_DISPLAY_LENGTH = 30;
    public static int[,]? RandomInit(int dimLength, double percentOfNonZeroes, int from = 1, int to = 101)
    {
        var positions = new DoubleLinkedList<ValueTuple<int, int>>();
        
        for (int i = 0; i < dimLength; i++)
        {
            for (int j = 0; j < dimLength; j++)
            {
                positions.AddToEnd(new(i, j));
            }
        }
        
        if (dimLength < 1)
        {
            return null;
        }
        
        var matrix = new int[dimLength, dimLength];

        /*var maxCountOfNonZeroValues = dimLength * dimLength / 2;
        var randomCountOfNonZeroValues = Random.Shared.Next(1, maxCountOfNonZeroValues);*/

        var countOfNonZeroValues = percentOfNonZeroes * dimLength * dimLength;
        for (int i = 0; i < countOfNonZeroValues; i++)
        {
            var randInd = Random.Shared.Next(0, positions.Capacity);
            var pos = positions.TakeElementByInd(randInd);
            matrix[pos.Item1, pos.Item2] = Random.Shared.Next(from, to);
            positions.RemoveByInd(randInd); 
        }
        
        return matrix;
    }

    public static void Print(this int[,] matrix)
    {
        int dimLength = matrix.GetLength(0), zeroes = 0;
        
        if (dimLength > MAX_DISPLAY_LENGTH)
        {
            CountZeroes(matrix, out zeroes, dimLength);
        }
        else
        {
            PrintAndCountZeroes(matrix, out zeroes, dimLength);
        }

        PrintAction($"\n[Z: {zeroes}, N-z: {dimLength * dimLength - zeroes}]");
    }

    private static void PrintAndCountZeroes(int[,] matrix, out int zeroes, int dimLength)
    {
        zeroes = 0;
        for (int i = 0; i < dimLength; i++)
        {
            for (int j = 0; j < dimLength; j++)
            {
                if (ChangeColor is not null && j % 2 == 0)
                {
                    ChangeColor();
                }
                PrintAction($"{matrix[i, j], 5}");
                ResetColor();
                if (matrix[i, j] == 0)
                {
                    zeroes++;
                }
            }
            PrintAction(Environment.NewLine);
        }
    }

    private static void CountZeroes(int[,] matrix, out int zeroes, int dimLength)
    {
        zeroes = 0;
        for (int i = 0; i < dimLength; i++)
        {
            for (int j = 0; j < dimLength; j++)
            {
                if (matrix[i, j] == 0)
                {
                    zeroes++;
                }
            }
        }
    }

    public static void ParseZMatrix(out int[,] matrix, ZeroBasedMatrix<int> zmatrix)
    {
        matrix = new int[zmatrix.DimLength, zmatrix.DimLength];
        foreach (var item in zmatrix)
        {
            matrix[item.X, item.Y] = item.Data;
        }
    }

    public static void Clear(this int[,] matrix)
    {
        int dimLength = matrix.GetLength(0);
        for (int i = 0; i < dimLength; i++)
        {
            for(int j = 0; j < dimLength; j++)
            {
                matrix[i, j] = 0;
            }
        }
    }
    public static void ParseZMatrix(int[,] matrix, ZeroBasedMatrix<int> zmatrix)
    {
        foreach (var item in zmatrix)
        {
            matrix[item.X, item.Y] = item.Data;
        }
    }
}