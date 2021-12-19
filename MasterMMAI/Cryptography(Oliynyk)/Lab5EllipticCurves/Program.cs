using System.Globalization;
using System.Numerics;
using System.Text;
using Lab5EllipticCurves.EllipticService;
using Lab5EllipticCurves.EllipticService.Interfaces;
using Lab5EllipticCurves.Kupyna;

namespace Lab5EllipticCurves
{
    internal class Program
    {
        private static void Main()
        {
            var p = BigInteger.Parse(
                "300613450595050653169853516389035139504087366260264943450533244356122755214669880763353471793250393988089774081");
            BigInteger a = 1;
            var b = BigInteger.Parse(
                "43FC8AD242B0B7A6F3D1627AD5654447556B47BF6AA4A64B0C2AFE42CADAB8F93D92394C79A79755437B56995136",
                NumberStyles.HexNumber);
            var n = BigInteger.Parse(
                "40000000000000000000000000000000000000000000009C300B75A3FA824F22428FD28CE8812245EF44049B2D49",
                NumberStyles.HexNumber);

            var curve = new EllipticCurve(a, b, p, n, 367);

            IEllipticCurveContext context = new EllipticCurveContext(curve);

            Console.WriteLine($"Generator point:\nX = {curve.BasePoint.X}\nY = {curve.BasePoint.Y}");
            var (privateKey, publicKey) = context.GenerateKeyPair();
            Console.WriteLine($"Private Key: d = {privateKey}");
            Console.WriteLine($"Public Key:\nX = {publicKey.X}\nY = {publicKey.Y}");
            Console.WriteLine($"Is valid public key: {context.IsValidPublicKey(publicKey)}");
            Console.WriteLine($"Is valid private key: {context.IsValidPrivateKey(privateKey, publicKey)}");
            Console.WriteLine('\n');
            
            using var hashFunction = new KupynaHash(256);
            
            const string msg = "Hello from true message";
            const string fake = "Hello from fake message";
            
            var msgBytes = Encoding.ASCII.GetBytes(msg);
            var fakeBytes = Encoding.ASCII.GetBytes(fake);
            
            Console.WriteLine('\n');
            Console.WriteLine($"Message to sign: {msg}");
            var (r, s) = context.SignMessage(msgBytes, privateKey, publicKey, hashFunction);

            Console.WriteLine($"R = {r}");
            Console.WriteLine($"S = {s}");
            hashFunction.Initialize();
            var result = context.VerifySign(msgBytes, publicKey, r, s, hashFunction);
            Console.WriteLine($"Verify <{msg}>: {result}");
            hashFunction.Initialize();
            result = context.VerifySign(fakeBytes, publicKey, r, s, hashFunction);
            Console.WriteLine($"Verify <{fake}>: {result}");
        }
    }
}