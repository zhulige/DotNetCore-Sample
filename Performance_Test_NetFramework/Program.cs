using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Test_NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Framework");

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
