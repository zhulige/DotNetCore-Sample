using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Performance_Test.Core
{
    public class ListTest
    {
        public void List_100_000_000()
        {
            int n = 0;
            while (n<=10)
            {
                var _List = new List<int>();
                var _Stopwatch = Stopwatch.StartNew();
                for (int i = 0; i < 100_000_000; i++)
                {
                    _List.Add(i);
                    _List.RemoveAt(0);
                }
                n++;
                Console.WriteLine("List_100_000_000 : " + _Stopwatch.Elapsed);
            }
        }
    }
}
