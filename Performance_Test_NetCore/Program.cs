using System;

namespace Performance_Test_NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Core");

            Console.WriteLine(System.Environment.OSVersion.Platform.ToString());
            

            Performance_Test.Core.QueueTest _QueueTest = new Performance_Test.Core.QueueTest();
            _QueueTest.Queue_100_000_000();

            Console.WriteLine("-------------------------------------------------------------------");

            Performance_Test.Core.ListTest _ListTest = new Performance_Test.Core.ListTest();
            _ListTest.List_100_000_000();

            Console.WriteLine("-------------------------------------------------------------------");

            Performance_Test.Core.LINQTest _LINQTest = new Performance_Test.Core.LINQTest();
            _LINQTest.LINQ_10_000_000();

            Console.WriteLine("-------------------------------------------------------------------");

            Performance_Test.Core.BigIntegerTest _BigIntegerTest = new Performance_Test.Core.BigIntegerTest();
            _BigIntegerTest.BigInteger_ModPow();

            
        }
    }
}
