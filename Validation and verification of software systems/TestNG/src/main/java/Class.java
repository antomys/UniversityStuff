public class Class {


    public int sum(int a, int b){
        return a + b;
    }

    public static int F_compute(int n) {
        int result = 0;

        if (n <= 1) {
            result = n;
        } else {
            result = F_compute(n - 1) + F_compute(n - 2);
        }

        return result;
    }

}
