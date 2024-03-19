using System.Diagnostics;

namespace Lab3;

public class Program
{
    public static void Main(string[] args)
    {
        int dimLenght;
        TimeLogger.TimeLogger logger = new();
        string userInput;

        while (true)
        {
            do
            {
                Console.Write("Enter matrix size: ");
                userInput = Console.ReadLine() ?? string.Empty;
            } while (!int.TryParse(userInput, null, out dimLenght) || dimLenght < 1);

            int[,] matrix = ArrayExtensions.InitMatrix(dimLenght);

            ArrayExtensions.CopyMatrix(matrix, out int[,] matrixInsertionAsc);
            ArrayExtensions.CopyMatrix(matrix, out int[,] matrixMergeRecAscVer1);
            ArrayExtensions.CopyMatrix(matrix, out int[,] matrixMergeRecAscVer2);
            ArrayExtensions.CopyMatrix(matrix, out int[,] matrixMergeIterAsc);

            ArrayExtensions.SortHalfLog(Sort.MergeRecAscVer1, matrixMergeRecAscVer1, logger);
            ArrayExtensions.SortHalfLog(Sort.MergeRecAscVer2, matrixMergeRecAscVer2, logger);
            ArrayExtensions.SortHalfLog(Sort.MergeIterAsc, matrixMergeIterAsc, logger);
            string stats = ArrayExtensions.SortHalfLog(Sort.InsertionAsc, matrixInsertionAsc, logger);

            Console.WriteLine("\nOriginal matrix:");
            ArrayExtensions.PrintMatrix(Console.Write, matrix);
            Console.WriteLine("\nMergesort with recursion ver 1:");
            ArrayExtensions.PrintMatrix(Console.Write, matrixMergeRecAscVer1);
            Console.WriteLine("\nMergesort with recursion ver 2:");
            ArrayExtensions.PrintMatrix(Console.Write, matrixMergeRecAscVer2);
            Console.WriteLine("\nMergesort with iteration:");
            ArrayExtensions.PrintMatrix(Console.Write, matrixMergeIterAsc);
            Console.WriteLine("\nInsertion sort (iteration):");
            ArrayExtensions.PrintMatrix(Console.Write, matrixInsertionAsc);

            Console.WriteLine($"Stats:\n{stats}");

            Console.WriteLine("Identity check tests (with first sorted variant):");
            Console.WriteLine($"Mergesort with recursion ver 1: True - first sorted");
            Console.WriteLine($"Mergesort with recursion ver 2: {ArrayExtensions
                .CheckMatrixIdentity(matrixMergeRecAscVer1, matrixMergeRecAscVer2)}");
            Console.WriteLine($"Mergesort with iteration: {ArrayExtensions
                .CheckMatrixIdentity(matrixMergeRecAscVer1, matrixMergeIterAsc)}");
            Console.WriteLine($"Insertion sort (iteration): {ArrayExtensions
                .CheckMatrixIdentity(matrixMergeRecAscVer1, matrixInsertionAsc)}");

            
            Console.WriteLine("\nTo exit the program type 'exit'");
            userInput = Console.ReadLine() ?? string.Empty;
            if (userInput.ToLower().Contains("exit"))
            {
                break;
            }
            logger.Clear();
            Console.Clear();
        }
    }
}