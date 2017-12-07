using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Performance_Test.Core
{
    public class BigIntegerTest
    {
        public void BigInteger_ModPow()
        {
            var rand = new Random(42);
            BigInteger a = Create(rand, 8192);
            BigInteger b = Create(rand, 8192);
            BigInteger c = Create(rand, 8192);

            var _Stopwatch = Stopwatch.StartNew();
            BigInteger.ModPow(a, b, c);
            Console.WriteLine("BigInteger_ModPow : " + _Stopwatch.Elapsed);
        }

        private static BigInteger Create(Random rand, int bits)
        {
            var value = new byte[(bits + 7) / 8 + 1];
            rand.NextBytes(value);
            value[value.Length - 1] = 0;
            return new BigInteger(value);
        }
    }
}
