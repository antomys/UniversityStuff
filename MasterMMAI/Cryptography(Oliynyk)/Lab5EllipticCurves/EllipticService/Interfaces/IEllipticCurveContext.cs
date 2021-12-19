using System.Numerics;
using System.Security.Cryptography;

namespace Lab5EllipticCurves.EllipticService.Interfaces;

public interface IEllipticCurveContext
{
    (BigInteger, BigInteger) SignMessage(byte[] message, BigInteger privateKey, EllipticCurvePoint publicKey,
        HashAlgorithm hashAlgorithm);
        
    bool VerifySign(byte[] message, EllipticCurvePoint publicKey, BigInteger r, BigInteger s,
        HashAlgorithm hashAlgorithm);

    (BigInteger, EllipticCurvePoint) GenerateKeyPair();
        
    bool IsValidPublicKey(EllipticCurvePoint publicKey);

    bool IsValidPrivateKey(BigInteger privateKey, EllipticCurvePoint publicKey);
}