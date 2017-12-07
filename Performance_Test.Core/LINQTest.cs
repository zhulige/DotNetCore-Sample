using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Performance_Test.Core
{
    public class LINQTest
    {
        public void LINQ_10_000_000()
        {
            IEnumerable<int> tenMillionToZero = Enumerable.Range(0, 10_000_000).Reverse();
            int n = 0;
            while (n<=10)
            {
                var _Stopwatch = Stopwatch.StartNew();
                int fifth = tenMillionToZero.OrderBy(i => i).Skip(4).First();
                n++;
                Console.WriteLine("LINQ_10_000_000 : " + _Stopwatch.Elapsed);
            }
        }
    }
}
