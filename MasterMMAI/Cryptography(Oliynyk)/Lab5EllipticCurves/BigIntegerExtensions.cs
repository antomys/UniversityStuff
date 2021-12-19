using System.Numerics;

namespace Lab5EllipticCurves
{
    public static class BigIntegerExtensions
    {
        private static readonly BigInteger FastSqrtSmallNumber = 4503599761588224UL;

        private static BigInteger SqrtFast(BigInteger value)
        {
            if (value <= FastSqrtSmallNumber)
            {
                if (value.Sign < 0) throw new ArgumentException("Negative argument.");
                return (ulong) Math.Sqrt((ulong) value);
            }

            BigInteger root;
            var byteLen = value.ToByteArray().Length;
            if (byteLen < 128)
                root = (BigInteger) Math.Sqrt((double) value);
            else
                root = (BigInteger) Math.Sqrt((double) (value >> ((byteLen - 127) * 8))) << ((byteLen - 127) * 4);

            for (;;)
            {
                var root2 = (value / root + root) >> 1;
                if (root2 == root || root2 == root + 1) return root;
                root = (value / root2 + root2) >> 1;
                if (root == root2 || root == root2 + 1) return root2;
            }
        }

        public static BigInteger SqrtGf(this BigInteger value)
        {
            return SqrtFast(value);
        }

        public static int GetBitCount(this BigInteger a)
        {
            return (int) BigInteger.Log(a, 2) + 1;
        }

        public static int Lsb(this BigInteger a)
        {
            return a == 0 ? -1 : (int) BigInteger.Log(a & -a, 2);
        }

        public static BigInteger Seek(this BigInteger a, int bitLenght)
        {
            for (var j = a.GetBitCount() - 1; j >= bitLenght && j >= 0; j--) a &= ~(BigInteger.One << j);
            return a;
        }
    }
}