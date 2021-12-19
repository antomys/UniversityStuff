using System.Numerics;
using System.Security.Cryptography;

namespace Lab5EllipticCurves
{
    public class EllipticCurveContext
    {
        private readonly EllipticCurve _curve;
        private readonly RandomBigIntegerGenerator _randomNumberGenerator = new();

        public EllipticCurveContext(EllipticCurve curve, bool initIfNonInitialized = true)
        {
            _curve = curve ?? throw new ArgumentNullException(nameof(curve));
            if (!Initialized && initIfNonInitialized) InitializeBasePoint();
        }

        private bool Initialized => !EllipticCurvePoint.IsInfinity(_curve.BasePoint);

        private EllipticCurvePoint GenerateBasePoint()
        {
            EllipticCurvePoint result;
            do
            {
                result = GeneratePoint();
            } while (!EllipticCurvePoint.IsInfinity(ScalarMult(_curve.N, result)));

            CheckOnCurve(result);

            return result;
        }

        private EllipticCurvePoint GeneratePoint()
        {
            BigInteger u, z;
            int k;

            do
            {
                u = GetRandomIntegerFromField();
                var w = u.SquareGf(_curve.P).MultGf(u, _curve.P).AddGf(u.SquareGf(_curve.P).MultGf(_curve.A, _curve.P))
                    .AddGf(_curve.B);
                k = SolveQuadraticEquation(u, w, out z);
            } while (k == 0);

            var result = new EllipticCurvePoint(u, z);
            CheckOnCurve(result);
            return result;
        }

        private int SolveQuadraticEquation(BigInteger u, BigInteger w, out BigInteger z1)
        {
            // z^2 + u*z = w
            if (u == 0)
            {
                z1 = w.SqrtGf();
                return 1;
            }

            if (w == 0)
            {
                z1 = 0;
                return 2;
            }

            var u2 = u.InverseGf(_curve.P).SquareGf(_curve.P);

            var v = w.MultGf(u2, _curve.P);

            if (v.Trace(_curve.P) == 1)
            {
                z1 = 0;
                return 0;
            }

            var t = v.HalfTrace(_curve.P);
            z1 = t.MultGf(u, _curve.P);
            return 2;
        }

        private BigInteger GetRandomIntegerFromField()
        {
            return _randomNumberGenerator.GetBigInteger(_curve.P);
        }

        private void InitializeBasePoint()
        {
            var basePoint = GenerateBasePoint();
            _curve.BasePoint = basePoint;
        }

        public (BigInteger, EllipticCurvePoint) GenerateKeyPair()
        {
            if (!Initialized) throw new Exception("Elliptic curve wasn't initialized");
            var d = GetRandomIntegerFromField();

            var p = NegativePoint(ScalarMult(d, _curve.BasePoint));
            return (d, p);
        }

        private EllipticCurvePoint Add(EllipticCurvePoint a, EllipticCurvePoint b)
        {
            if (EllipticCurvePoint.IsInfinity(a))
            {
                CheckOnCurve(b);
                return b;
            }

            if (EllipticCurvePoint.IsInfinity(b))
            {
                CheckOnCurve(a);
                return a;
            }

            CheckOnCurve(a);
            CheckOnCurve(b);

            if (a.X == b.X)
            {
                if (a.Y != b.Y || a.X == BigInteger.Zero) return EllipticCurvePoint.InfinityPoint;

                return Twice(a);
            }

            var lambda = a.X.AddGf(b.X).InverseGf(_curve.P).MultGf(a.Y.AddGf(b.Y), _curve.P);
            var x = lambda.SquareGf(_curve.P).AddGf(lambda).AddGf(a.X).AddGf(b.X).AddGf(_curve.A);
            var y = a.X.AddGf(x).MultGf(lambda, _curve.P).AddGf(x).AddGf(a.Y);

            var result = new EllipticCurvePoint(x, y);
            CheckOnCurve(result);
            return result;
        }

        private EllipticCurvePoint Twice(EllipticCurvePoint a)
        {
            if (EllipticCurvePoint.IsInfinity(a)) return a;

            var sigma = a.X.InverseGf(_curve.P).MultGf(a.Y, _curve.P).AddGf(a.X);
            var x = sigma.SquareGf(_curve.P).AddGf(sigma).AddGf(_curve.A);
            var y = a.X.SquareGf(_curve.P).AddGf(sigma.AddGf(BigInteger.One).MultGf(x, _curve.P));

            var result = new EllipticCurvePoint(x, y);
            
            return result;
        }

        private EllipticCurvePoint ScalarMult(BigInteger k, EllipticCurvePoint a)
        {
            while (true)
            {
                CheckOnCurve(a);

                if (k.ModulusGf(_curve.N) == 0 || EllipticCurvePoint.IsInfinity(a)) return EllipticCurvePoint.InfinityPoint;

                if (k < 0)
                {
                    k = -k;
                    a = NegativePoint(a);
                    continue;
                }

                var res = EllipticCurvePoint.InfinityPoint;
                var temp = a;

                while (k != 0)
                {
                    if ((k & 1) == 1) res = Add(res, temp);

                    temp = Twice(temp);

                    k >>= 1;
                }

                return res;
            }
        }

        private EllipticCurvePoint NegativePoint(EllipticCurvePoint a)
        {
            CheckOnCurve(a);

            if (EllipticCurvePoint.IsInfinity(a)) return a;

            var result = new EllipticCurvePoint(a.X, a.X.AddGf(a.Y));

            CheckOnCurve(a);
            return result;
        }

        public bool IsValidPublicKey(EllipticCurvePoint publicKey)
        {
            if (EllipticCurvePoint.IsInfinity(publicKey)) return false;

            if (publicKey.X >= _curve.P || publicKey.Y >= _curve.P || publicKey.X * publicKey.Y == 0) return false;

            return _curve.IsOnCurve(publicKey) && EllipticCurvePoint.IsInfinity(ScalarMult(_curve.N, publicKey));
        }

        public bool IsValidPrivateKey(BigInteger privateKey, EllipticCurvePoint publicKey)
        {
            var p = NegativePoint(ScalarMult(privateKey, _curve.BasePoint));

            if (EllipticCurvePoint.IsInfinity(p)) return false;

            return p.X == publicKey.X && p.Y == publicKey.Y;
        }

        private (BigInteger, BigInteger) PreComputedSign()
        {
            if (EllipticCurvePoint.IsInfinity(_curve.BasePoint)) throw new Exception("Base point uninitialized");

            while (true)
            {
                var e = GetRandomIntegerFromField();
                var r = ScalarMult(e, _curve.BasePoint);

                if (!EllipticCurvePoint.IsInfinity(r) && r.X != 0) return (e, r.X);
            }
        }

        private BigInteger TransformHash(byte[] hash)
        {
            var hashInt = new BigInteger(hash);
            if (hashInt < 0) hashInt = -hashInt;

            return hashInt.Seek(_curve.N.GetBitCount() - 1);
        }

        public (BigInteger, BigInteger) SignMessage(byte[] message, BigInteger privateKey, EllipticCurvePoint publicKey,
            HashAlgorithm hashAlgorithm)
        {
            if (!IsValidPrivateKey(privateKey, publicKey)) throw new Exception("Private key is invalid");

            var hash = hashAlgorithm.ComputeHash(message);
            var hashInt = TransformHash(hash);

            BigInteger r, s;
            do
            {
                BigInteger e;
                BigInteger fe;
                (e, fe) = PreComputedSign();
                r = fe.MultGf(hashInt, _curve.P).Seek(_curve.N.GetBitCount() - 1);
                s = (r * privateKey + e) % _curve.N; //r.MultGF(privateKey, curve.P).AddGF(e).ModulusGF(curve.N);
            } while (r == 0 || s == 0);

            return (r, s);
        }

        public bool VerifySign(byte[] message, EllipticCurvePoint publicKey, BigInteger r, BigInteger s,
            HashAlgorithm hashAlgorithm)
        {
            if (!IsValidPublicKey(publicKey)) throw new Exception("Public key is invalid");

            if (r >= _curve.N || s >= _curve.N || r <= 0 || s <= 0) return false;

            var hash = hashAlgorithm.ComputeHash(message);
            var hashInt = TransformHash(hash);

            var ellipticCurvePoint = Add(ScalarMult(s, _curve.BasePoint), ScalarMult(r, publicKey));
            var y = ellipticCurvePoint.X.MultGf(hashInt, _curve.P);
            var r1 = y.Seek(_curve.N.GetBitCount() - 1);

            return r1 == r;
        }


        private void CheckOnCurve(EllipticCurvePoint a)
        {
            if (!_curve.IsOnCurve(a)) throw new ArgumentException("Point not on curve");
        }
    }
}