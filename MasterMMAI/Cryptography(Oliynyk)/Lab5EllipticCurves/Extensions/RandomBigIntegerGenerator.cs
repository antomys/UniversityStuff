using System.Numerics;
using System.Security.Cryptography;

namespace Lab5EllipticCurves.Extensions
{
    internal class RandomBigIntegerGenerator : IDisposable
    {
        private readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public void Dispose()
        {
            ((IDisposable) _randomNumberGenerator).Dispose();
        }

        public BigInteger GetBigInteger(BigInteger max, BigInteger min)
        {
            var bytes = max.ToByteArray();
            BigInteger res;

            do
            {
                _randomNumberGenerator.GetBytes(bytes);
                bytes[^1] &= 0x7F;
                res = new BigInteger(bytes);
            } while (res >= max || res <= min);

            return res;
        }

        public BigInteger GetBigInteger(BigInteger max)
        {
            var bytes = max.ToByteArray();
            BigInteger res;

            do
            {
                _randomNumberGenerator.GetBytes(bytes);
                bytes[^1] &= 0x7F;
                res = new BigInteger(bytes);
            } while (res >= max);

            return res;
        }

        public BigInteger GetBigInteger(int length)
        {
            var bytes = new byte[length];
            _randomNumberGenerator.GetBytes(bytes);
            bytes[^1] &= 0x7F;
            return new BigInteger(bytes);
        }
    }
}