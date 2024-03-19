

namespace Lab3;

public static class ArrayExtensions
{
    public static int[,] InitMatrix(int size, int from = 0, int to = 10)
    {
        Random random = new();
        int[,] matrix = new int[size, size];
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = random.Next(from, to);
            }
        }

        return matrix;
    }

    public static void CopyMatrix<T>(T[,] from, out T[,] to)
    {
        int size = (int)Math.Sqrt(from.Length);
        
        to = new T[size, size];
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                to[i, j] = from[i, j];
            }
        }
    }

    public static void PrintMatrix<T>(Action<string> printAction, T[,] matrix)
    {
        int size = (int)Math.Sqrt(matrix.Length);

        if (size > 51)
        {
            printAction("\nToo many elements to print them.\n\n");
            return;
        }
            
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (j < i + 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    printAction($"{matrix[i, j], -3}");
                    Console.ResetColor();
                }
                else
                {
                    printAction($"{matrix[i, j], -3}");
                }
            }

            printAction(Environment.NewLine);
        }
    }

    public static void SortHalf<T>(Action<T[]> sort, T[,] matrix)
    {
        int size = (int)Math.Sqrt(matrix.Length);

        List<T> buffer = new(size);
        T[] bufferArr = null!;
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (j < i + 1)
                {
                    buffer.Add(matrix[i, j]);
                }
            }

            bufferArr = buffer.ToArray();
            
            sort(bufferArr);
            
            for (int j = 0; j < size; j++)
            {
                if (j < i + 1)
                {
                    matrix[i, j] = bufferArr[j];
                }
            }
            
            buffer.Clear();
        }
    }
    
    public static string SortHalfLog<T>(Action<T[]> sort, T[,] matrix, TimeLogger.TimeLogger logger)
    {
        int size = (int)Math.Sqrt(matrix.Length);

        List<T> buffer = new(size);
        T[] bufferArr = null!;

        logger.SetMessage(sort.Method.Name).Start();
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (j < i + 1)
                {
                    buffer.Add(matrix[i, j]);
                }
            }

            bufferArr = buffer.ToArray();
            sort(bufferArr);
            
            for (int j = 0; j < size; j++)
            {
                if (j < i + 1)
                {
                    matrix[i, j] = bufferArr[j];
                }
            }
            
            buffer.Clear();
        }
        
        logger.Stop();
        
        return logger.ToString();
    }

    public static bool CheckMatrixIdentity<T>(T[,] original, T[,] copy)
        where T : IComparable, IComparable<T>
    {
        if (original.Length != copy.Length)
        {
            return false;
        }

        int dimSize = (int)Math.Sqrt(original.Length);

        for (int i = 0; i < dimSize; i++)
        {
            for (int j = 0; j < dimSize; j++)
            {
                if (original[i, j].CompareTo(copy[i, j]) != 0)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}