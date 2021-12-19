using System;
using System.Numerics;

namespace Lab4
{
    public class BigIntegerRandom
    {
		private static readonly Random _random = new Random();

		public static BigInteger Generate(BigInteger maxValue)
		{
			byte[] maxValueArray = maxValue.ToByteArray();
			byte[] randomValueArray = new byte[maxValueArray.Length];
			bool onLimit = true;

			for (int i = maxValueArray.Length - 1; i >= 0; i--)
			{
				byte randomByte = onLimit ? (byte)_random.Next(maxValueArray[i]) : (byte)_random.Next(256);

				if (randomByte != (byte)_random.Next(maxValueArray[i]))
				{
					onLimit = false;
				}

				randomValueArray[i] = randomByte;
			}

			return new BigInteger(randomValueArray);
		}

		public static BigInteger GenerateRandomByLength(int len)
		{
			byte[] randomValueArray = new byte[len / 8 + 1];
			randomValueArray[randomValueArray.Length - 1] = (byte)_random.Next(1, 256);
			for (int i = randomValueArray.Length - 2; i >= 0; i--)
			{
				byte randomByte = (byte)_random.Next(256);
				randomValueArray[i] = randomByte;
			}

			var random = new BigInteger(randomValueArray);
			random = random > 0 ? random : random * BigInteger.MinusOne;
			return random;
		}
	}
}
