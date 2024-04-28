using Logger = TimeLogger.TimeLogger;

namespace Lab6;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        MatrixExtensions.ChangeColor = () =>
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
        };
        MatrixExtensions.ResetColor = Console.ResetColor;
        MatrixExtensions.PrintAction = Console.Write;

        int action = 0;
        int[,] matrix = null!;
        int[,] tempMatrix = null!;
        bool sorted = false, continuePorgram = true;
        var logger = new Logger();

        using (var zmatrix = new ZeroBasedMatrix<int>()) // this isn't proper using of dispose pattern, but it looks nice
        {
            while (continuePorgram)
            {
                do
                {
                    Console.Clear();
                    Console.Write("1. Init matrix.\n2. Show zipped list" +
                                  "\n3. Show original and zipped.\n4. Sort zipped matrix.\n5. Matrix multiplication." +
                                  "\nAny number to exit.\nAction -> ");
                } while (!int.TryParse(Console.ReadLine(), out action));


                switch (action)
                {
                    case 1:
                    {
                        int dimLenght = 0;
                        do
                        {
                            Console.Write("\nEnter dimension lenght -> ");
                        } while (!int.TryParse(Console.ReadLine(), out dimLenght) || dimLenght < 1);

                        int NZpercent = 0;
                        do
                        {
                            Console.Write("\nEnter percent of non-zeroes integer [1..50) -> ");
                        } while (!int.TryParse(Console.ReadLine(), out NZpercent) || NZpercent >= 50
                                 || NZpercent < 1);

                        matrix = MatrixExtensions.RandomInit(dimLenght, NZpercent / 100d)!;
                        tempMatrix = new int[dimLenght, dimLenght];
                        zmatrix.Dispose();
                        sorted = false;
                        logger.SetMessage("\nParsing matrix").Start();
                        var isParsed = zmatrix.ParseMatrix(matrix);
                        logger.Stop();
                        Console.Write($"\nMatrix[{dimLenght},{dimLenght}] initialized with random values.");
                        Console.Write($"\nZero-based matrix parsing result: {isParsed}. ");
                        if (isParsed)
                        {
                            Console.Write("Matrix is zero-based.");
                        }

                        Console.WriteLine(logger.ToString());
                        logger.Clear();
                    }
                        break;
                    case 2:
                    {
                        if (matrix is not null)
                        {
                            foreach (var item in zmatrix)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else
                        {
                            Console.Write("\nZero-based matrix is empty.");
                        }
                    }
                        break;
                    case 3:
                    {
                        if (matrix is not null)
                        {
                            Console.WriteLine("\nOriginal:");
                            matrix.Print();
                            Console.WriteLine("\n\nZipped:");
                            tempMatrix.Clear();
                            MatrixExtensions.ParseZMatrix(tempMatrix, zmatrix);
                            tempMatrix.Print();
                        }
                        else
                        {
                            Console.Write("\nThere is nothing to show.");
                        }
                    }
                        break;
                    case 4:
                    {
                        if (matrix is not null)
                        {
                            logger.SetMessage("\nSorting zero-based matrix").Start();
                            zmatrix.Sort();
                            logger.Stop();
                            sorted = true;
                            Console.Write("\nZero-based matrix sorted.");
                            Console.Write(logger.ToString());
                            logger.Clear();
                        }
                        else
                        {
                            Console.Write("\nThere is nothing to sort.");
                        }
                    }
                        break;
                    case 5:
                    {
                        if (matrix is not null && sorted)
                        {
                            var newZmatrix = new ZeroBasedMatrix<int>();
                            newZmatrix.ParseMatrix(matrix);
                            Console.WriteLine("\nMatrix A:");
                            tempMatrix.Clear();
                            MatrixExtensions.ParseZMatrix(tempMatrix, zmatrix);
                            tempMatrix.Print();
                            Console.WriteLine("\nMatrix B:");
                            tempMatrix.Clear();
                            MatrixExtensions.ParseZMatrix(tempMatrix, newZmatrix);
                            tempMatrix.Print();
                            Console.WriteLine("\nMatrix A * B:");
                            logger.SetMessage("\nA * B multiplication").Start();
                            var tempZmatrix = zmatrix.Multiply(newZmatrix);
                            logger.Stop();
                            tempMatrix.Clear();
                            MatrixExtensions.ParseZMatrix(tempMatrix, tempZmatrix);
                            tempMatrix.Print();
                            logger.SetMessage("B * A multiplication").Start();
                            Console.WriteLine("\nMatrix B * A:");
                            tempZmatrix = newZmatrix.Multiply(zmatrix);
                            logger.Stop();
                            tempMatrix.Clear();
                            MatrixExtensions.ParseZMatrix(tempMatrix, tempZmatrix);
                            tempMatrix.Print();
                            Console.Write(logger.ToString());
                            logger.Clear();
                        }
                        else
                        {
                            Console.Write("\n There is nothing to multiply. Try to sort first.");
                        }
                    }
                        break;
                    default:
                        continuePorgram = false;
                        break;
                }

                if (continuePorgram)
                {
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Exiting...");
        }
    }
}