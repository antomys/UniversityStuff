using System.Numerics;
using Lab5EllipticCurves.EllipticService.Interfaces;
using Lab5EllipticCurves.Extensions;

namespace Lab5EllipticCurves.EllipticService
{
    public class EllipticCurve : IEllipticCurve
    {
        private EllipticCurvePoint _basePoint;

        public EllipticCurve(BigInteger a, BigInteger b, BigInteger p, BigInteger n, uint m,
            EllipticCurvePoint basePoint = null!)
        {
            A = a;
            B = b;
            P = p;
            N = n;
            M = m;

            _basePoint = basePoint;
        }

        public BigInteger A { get; }
        public BigInteger B { get; }
        public BigInteger P { get; }
        public BigInteger N { get; }
        private uint M { get; }

        public EllipticCurvePoint BasePoint
        {
            get => _basePoint;
            set
            {
                if (!IsOnCurve(value)) throw new ArgumentException("Point is not on elliptic curve", nameof(value));
                _basePoint = value;
            }
        }

        public bool IsOnCurve(EllipticCurvePoint point)
        {
            if (EllipticCurvePoint.IsInfinity(point)) return true;

            var y = point.Y;
            var x = point.X;

            var lhs = y.SquareGf(P).AddGf(x.MultGf(y, P));
            var rhs = x.SquareGf(P).MultGf(x, P).AddGf(x.SquareGf(P).MultGf(A, P)).AddGf(B);

            return lhs == rhs;
        }
    }
}