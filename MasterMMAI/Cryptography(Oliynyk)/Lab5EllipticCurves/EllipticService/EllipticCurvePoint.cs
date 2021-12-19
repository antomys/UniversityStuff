using System.Numerics;

namespace Lab5EllipticCurves.EllipticService
{
    public class EllipticCurvePoint
    {
        public static readonly EllipticCurvePoint InfinityPoint = null!;

        public EllipticCurvePoint(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }

        public BigInteger X { get; }
        public BigInteger Y { get; }

        public static bool IsInfinity(EllipticCurvePoint point)
        {
            return point == InfinityPoint;
        }
    }
}