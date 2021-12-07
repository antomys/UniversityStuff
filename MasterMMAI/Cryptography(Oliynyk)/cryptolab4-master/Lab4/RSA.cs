using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lab4
{
	public class Key
	{
		public BigInteger Value { get; set; }

		public BigInteger N { get; set; }
	}

	public class RSA
    {
		public static int keyLength = 1024;

		private static BigInteger GenerateRandomPrime()
		{
			var primeBigInteger = BigIntegerRandom.GenerateRandomByLength(keyLength);

			while (!MillerRabinTest.IsPrime(primeBigInteger, 10))
				primeBigInteger++;

			return primeBigInteger;
		}

		public static (Key publicKey, Key privateKey) GenerateRSAPair()
		{
			var p = GenerateRandomPrime();
			var q = GenerateRandomPrime();
			var n = BigInteger.Multiply(p, q);
			var phi = EulerFunction(p, q);
			var e = FindE(phi);
			var d = FindD(e, phi);

			if (d < 0)
				d += phi;

			return (new Key() { Value = e, N = n }, new Key() { Value = d, N = n });
		}

		private static BigInteger FindE(BigInteger phi)
		{
			int[] staticE = new int[] { 3, 17, 65537 };

			foreach (var e in staticE)
			{
				if (ExtendedEuclid(phi, e).d == 1)
				{
					return e;
				}
			}

			BigInteger generatedE = BigIntegerRandom.Generate(phi);

			for (; ; generatedE++)
			{
				if (ExtendedEuclid(phi, generatedE).d == 1)
				{
					return generatedE;
				}
			}
		}

		private static BigInteger FindD(BigInteger phi, BigInteger e)
		{
			return ExtendedEuclid(phi, e).x;
		}

		private static (BigInteger d, BigInteger x, BigInteger y) ExtendedEuclid(BigInteger a, BigInteger b)
		{
			BigInteger prevx = 1, x = 0, prevy = 0, y = 1;

			while (b > 0)
			{
				var q = a / b;
				(x, prevx) = (prevx - q * x, x);
				(y, prevy) = (prevy - q * y, y);
				(a, b) = (b, a % b);
			}

			return (a, prevx, prevy);
		}

		private static BigInteger EulerFunction(BigInteger p, BigInteger q)
		{
			return BigInteger.Multiply(p - 1, q - 1);
		}

		public static byte[] EncryptRSA(byte[] text, Key key)
		{
			return BigInteger.ModPow(new BigInteger(text), key.Value, key.N).ToByteArray();
		}

		public static byte[] DecryptRSA(byte[] text, Key key)
		{
			return BigInteger.ModPow(new BigInteger(text), key.Value, key.N).ToByteArray();
		}
	}
}
