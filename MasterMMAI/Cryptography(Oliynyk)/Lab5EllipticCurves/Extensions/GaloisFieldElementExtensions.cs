using System.Numerics;

namespace Lab5EllipticCurves.Extensions
{
    public static class GaloisFieldElementExtensions
    {
        public static BigInteger AddGf(this BigInteger a, BigInteger b)
        {
            return a ^ b;
        }

        public static BigInteger MultGf(this BigInteger a, BigInteger b, BigInteger modulus)
        {
            BigInteger valC = 0;
            var range = a.GetBitCount();
            for (var j = 0; j < range; j++)
            {
                var t = a & (BigInteger.One << j);
                if (t > 0) valC ^= b;

                b <<= 1;
            }

            return valC.ModulusGf(modulus);
        }
        
        public static BigInteger ModulusGf(this BigInteger val, BigInteger modulus)
        {
            if (val <= modulus) return val;

            var rv = val;
            var bitmL = modulus.GetBitCount();
            while (rv.GetBitCount() >= bitmL)
            {
                var mask = modulus << (rv.GetBitCount() - bitmL);
                rv ^= mask;
            }

            return rv;
        }

        public static BigInteger SquareGf(this BigInteger value, BigInteger modulus)
        {
            return value.MultGf(value, modulus);
        }

        public static BigInteger Trace(this BigInteger value, BigInteger modulus)
        {
            var result = value;
            var bitLength = modulus.GetBitCount();
            for (var i = 1; i < bitLength - 1; i++) result = result.SquareGf(modulus).AddGf(value);

            return result;
        }

        public static BigInteger HalfTrace(this BigInteger value, BigInteger modulus)
        {
            var result = value;
            var bitLength = modulus.GetBitCount();
            for (var i = 1; i <= (bitLength - 1) / 2; i++)
                result = result.SquareGf(modulus).SquareGf(modulus).AddGf(value);

            return result;
        }

        public static BigInteger InverseGf(this BigInteger value, BigInteger modulus)
        {
            BigInteger b = 1, c = 0, u = value, v = modulus;

            while (u.GetBitCount() > 1)
            {
                var j = u.GetBitCount() - v.GetBitCount();
                if (j < 0)
                {
                    (u, v) = (v, u);
                    (c, b) = (b, c);
                    j = -j;
                }

                u = u.AddGf(v << j);
                b = b.AddGf(c << j);
            }

            return b;
        }

        public static BigInteger SqrtGf(this BigInteger value, BigInteger modulus)
        {
            throw new NotImplementedException();
        }
    }
}