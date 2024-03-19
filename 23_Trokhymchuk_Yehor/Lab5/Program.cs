using System;
using System.Numerics;
using TimeLogger;

namespace Lab5;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        string userNumber,
               secondUserNumber = "",
               strForCheck;
        
        MyBigInteger myInteger1,
                     myInteger2;

        BigInteger integer1, // only for check comparisons
                   integer2;
        
        TimeLogger.TimeLogger logger = new();
        
        while (true)
        {
            do
            { 
                Console.Write("\nEnter a (first number): ");
                userNumber = Console.ReadLine() ?? "";
            } while (!MyBigInteger.TryParseLog(userNumber, out myInteger1, logger));

            logger.SetMessage("System.Numerics.BigInteger a (parsing)").Start();
            integer1 = BigInteger.Parse(userNumber);
            logger.Stop();
            
            do
            { 
                Console.Write("\nEnter b (second number): ");
                userNumber = Console.ReadLine() ?? "";
            } while (!MyBigInteger.TryParseLog(userNumber, out myInteger2, logger));

            logger.SetMessage("System.Numerics.BigInteger b (parsing)").Start();
            integer2 = BigInteger.Parse(userNumber);
            logger.Stop();

            Console.Write("\nYehor's result:           ");
            logger.SetMessage("My multiply").Start();
            userNumber = MyBigInteger.Multiply(myInteger1, myInteger2).ToString();
            logger.Stop();
            Console.WriteLine(userNumber);
            
            Console.Write("\nYehor's optimized result: ");
            logger.SetMessage("My optimized multiply").Start();
            secondUserNumber = MyBigInteger.MultiplyOptimized(myInteger1, myInteger2).ToString();
            logger.Stop();
            Console.WriteLine(secondUserNumber);
            
            Console.Write("\nThe result form God:      ");
            logger.SetMessage("System.Numerics.BigInteger.Multiply").Start();
            strForCheck = BigInteger.Multiply(integer1, integer2).ToString();
            logger.Stop();
            
            Console.WriteLine(strForCheck);

            Console.Write("\nIdentity check (str_a == str_b == str_c): " 
                          + (string.Compare(userNumber, strForCheck) == 0
                              && string.Compare(secondUserNumber, strForCheck) == 0));
            
            Console.Write("\n\nStats: \n" + logger);
            
            Console.Write("\n\nTo exit type 'exit' ");
            
            if (Console.ReadLine().ToLower().Contains("exit"))
            {
                Console.WriteLine("Exiting...");
                break;
            }
            logger.Clear();
            
            Console.Clear();
        }
    
    }
}