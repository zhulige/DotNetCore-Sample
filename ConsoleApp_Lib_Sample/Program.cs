using System;

namespace ConsoleApp_Lib_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            LibWarp.LibWarp _LibWarp = new LibWarp.LibWarp();
            Console.WriteLine(_LibWarp.GetSuccess());
            //Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }
}
