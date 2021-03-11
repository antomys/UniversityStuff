import parcs.*;

import java.math.BigInteger;
import java.util.Scanner;

public class Main implements AM {

    public static void main(String[] args) {
        task curtask = new task();
        curtask.addJarFile("RhoPollard.jar");
        (new Main()).run(new AMInfo(curtask, (channel) null));
        curtask.end();
    }

    public void run(AMInfo amInfo) {
        System.out.print("Enter n for factorization: ");
        Scanner sc = new Scanner(System.in);
        BigInteger n = sc.nextBigInteger();
        System.out.println("N is: " + n);
        long startTime = System.nanoTime();
        try {
            point p = amInfo.createPoint();
            channel c = p.createChannel();
            p.execute("RhoPollard");
            c.write(n.toString());
            System.out.println("Waiting for result......");
            Result res = (Result) (c.readObject());
            for (BigInteger num : res.getList()) {
                System.out.print(num + " ");
            }
            System.out.println(" ");
            double estimatedTime = (double) (System.nanoTime() - startTime) / 1000000000;
            System.out.println("\nTime total: " + estimatedTime);
        } catch (Exception e) {
            e.printStackTrace();
            return;
        }
    }
}
