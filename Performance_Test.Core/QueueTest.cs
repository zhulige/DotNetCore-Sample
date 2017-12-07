using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Performance_Test.Core
{
    public class QueueTest
    {
        public void Queue_100_000_000()
        {
            int n = 0;
            while (n <= 10)
            {
                var _Queue = new Queue<int>();
                var _Stopwatch = Stopwatch.StartNew();
                for (int i = 0; i < 100_000_000; i++)
                {
                    _Queue.Enqueue(i);
                    _Queue.Dequeue();
                }
                n++;
                Console.WriteLine("Queue_100_000_000 : " + _Stopwatch.Elapsed);
            }
        }
    }
}
