using TwoDArrOps;

namespace  Lab1;

internal class Program
{
    public static void Main(string[] args)
    {
        TwoDArr arr = new(Console.Write);
        int dimYLength, dimXLength;
        
        while (true)
        {
            do
            {
                Console.Write("Enter dimension-y lenght: ");
            }
            while (!int.TryParse(Console.ReadLine(), null, out dimYLength) || dimYLength < 1);

            do
            {
                Console.Write("Enter dimension-x length: ");
            }
            while (!int.TryParse(Console.ReadLine(), null, out dimXLength) || dimXLength < 1);

            Console.Clear();

            Console.WriteLine($"Initialized Array [{dimYLength}, {dimXLength}]:\n");

            arr.Init(dimXLength, dimYLength);
            arr.Print();

            Console.WriteLine("\nAfter flipping columns:\n");
            arr.FlipColumns();
            arr.Print();

            Console.WriteLine("\nAfter moving rows:\n");
            arr.MoveRows();
            arr.Print();

            Console.WriteLine("\n-------Stats-------\n");
            Console.WriteLine(arr.GetTimeInfo());
            Console.WriteLine("-------------------\n");

            Console.WriteLine("\nPress ESC to exit program. Press any button to continue.");

            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                break;
            }

            arr.ClearLog();
            Console.Clear();
        }
    }
}

