using System.Net.Http.Headers;

namespace Lab2;

public class Program
{
    public static void Main(string[] args)
    {
        double lowerBound, upperBound, recRes, iterRes, secRecRes;

        Sum sum = new();
        
        while (true)
        {
            do
            {
                Console.Write("Enter lowerBound of arithmetic sum: ");
            } while (!double.TryParse(Console.ReadLine(), out lowerBound));

            do
            {
                Console.Write("Enter upperBound of arithmetic sum: ");
            } while (!double.TryParse(Console.ReadLine(), out upperBound) || upperBound - lowerBound < 1);

            Console.Clear();

            recRes = sum.RecursiveLog(lowerBound, upperBound);
            iterRes = sum.IterativeLog(lowerBound, upperBound);
            secRecRes = sum.SecondVerRecursiveLog(lowerBound, upperBound);

            Console.WriteLine($"Recursive result: {recRes, 30}");
            Console.WriteLine($"Iterative result: {iterRes, 30}");
            Console.WriteLine($"Second variant recursive: {secRecRes, 22}\n");

            Console.WriteLine(sum.GetLog());

            Console.WriteLine("\n\nPress ESC to exit.\n");

            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                break;
            }

            Console.Clear();
            sum.ClearLog();
        }
    }
}
