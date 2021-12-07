using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lab4
{
    public static class MillerRabinTest
    {
        private static BigInteger two = new BigInteger(2);

		public static bool IsPrime(BigInteger n, BigInteger k)
		{
			if (n <= 1)
				return false;

			if (n <= 3)
				return true;

			if (n % 2 == 0)
				return false;

			BigInteger s = BigInteger.One;
			BigInteger t = (n - 1) / two;

			while (t % 2 == BigInteger.Zero)
			{
				t = BigInteger.Divide(t, two);
				s++;
			}

			for (BigInteger i = BigInteger.Zero; i < k; i++)
			{
				BigInteger a = BigInteger.Add(two, BigIntegerRandom.Generate(n - 4));
				var u = BigInteger.ModPow(a, t, n);

				if (u == 1 || u == n - 1)
					continue;

				var j = BigInteger.Zero;
				bool isNotPrime = false;

				while (j < s)
				{
					u = BigInteger.ModPow(u, two, n);
					j++;

					if (u.IsOne)
						return false;

					if (u == n - 1)
					{
						isNotPrime = true;
						break;
					}
				}

				if (isNotPrime)
					continue;

				return false;
			}
			return true;
		}
	}
}
