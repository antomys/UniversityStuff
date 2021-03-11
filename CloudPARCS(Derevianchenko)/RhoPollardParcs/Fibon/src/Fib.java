import parcs.*;

public class Fib implements AM{
    public void run(AMInfo info){
        long n,r1,r2,r;
        n = info.parent.readLong();

        //System.out.println("n="+n);
        if (n<2) r=n;
        else {
            point p1 = info.createPoint();
            channel c1 = p1.createChannel();

            p1.execute("Fib");
            c1.write(n-2);
            point p2 = info.createPoint();
            channel c2 = p2.createChannel();
            p2.execute("Fib");
            c2.write(n-1);

            r1=c1.readLong();
            r2=c2.readLong();
            //if ((r1==0)||(r2==0)) System.out.println("n="+n+" r1="+r1+" r2="+r2);
            r=r1+r2;
        }
        info.parent.write(r);
    }
}
