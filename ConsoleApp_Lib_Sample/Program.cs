using System;

namespace ConsoleApp_Lib_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //LibWarp.LibWarp _LibWarp = new LibWarp.LibWarp();
            Console.WriteLine(LibWarp.LibWarp.GSuccess());

            object obj = LibWarp.LibWarp.NewInterferDetector();
            LibWarp.LibWarp.SetParams();

            for (int i = 0; i < 120; i++)
            {
                char _char = System.Convert.ToChar(i);
                LibWarp.LibWarp.PushSample(obj, _char);
            }

            Console.WriteLine(LibWarp.LibWarp.GetInterfer(obj));

            Console.ReadLine();
        }
    }
}
