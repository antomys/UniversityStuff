import parcs.*;
import java.math.BigInteger;
import java.security.SecureRandom;

public class RhoPollard implements AM {

    private final static BigInteger ZERO = BigInteger.valueOf(0);
    private final static BigInteger ONE = BigInteger.valueOf(1);
    private final static BigInteger TWO = BigInteger.valueOf(2);
    private final static SecureRandom random = new SecureRandom();

    public static BigInteger pollard(BigInteger N) {
        BigInteger divisor;
        BigInteger c = new BigInteger(N.bitLength(), random);
        BigInteger x = new BigInteger(N.bitLength(), random);
        BigInteger x1 = x;
        if (N.mod(TWO).compareTo(ZERO) == 0) {
            return TWO;
        }
        do {
            x = x.multiply(x).mod(N).add(c).mod(N);
            x1 = x1.multiply(x1).mod(N).add(c).mod(N);
            x1 = x1.multiply(x1).mod(N).add(c).mod(N);
            divisor = x.subtract(x1).gcd(N);
        } while ((divisor.compareTo(ONE)) == 0);
        return divisor;
    }

    public void run(AMInfo amInfo) {
        System.out.println("\nRun Started!");
        Result result = new Result();
        String obj = amInfo.parent.readObject().toString();
        BigInteger N = new BigInteger(obj);
        if (N.isProbablePrime(1) || N.compareTo(ONE) == 0) result.add(N);
        else {
            BigInteger divisor = pollard(N);
            point p = amInfo.createPoint();
            channel c = p.createChannel();
            p.execute("RhoPollard");
            c.write(divisor.toString());
            point p1 = amInfo.createPoint();
            channel c1 = p1.createChannel();
            p1.execute("RhoPollard");
            c1.write(N.divide(divisor).toString());
            Result result1 = (Result) (c.readObject());
            Result result2 = (Result) (c1.readObject());
            for (BigInteger bi : result1.getList())
                result.add(bi);
            for (BigInteger bi : result2.getList())
                result.add(bi);
        }
        amInfo.parent.write(result);
    }
}
