using System;

namespace ChebyshevInterpolation
{
    class Interpolate
    {
        public double[] ChebyshevCalculateCoefficients(int n, double[] u)
        {
            double[] v = new double[n + 2];

            for (int k = 0; k <= n; k++)
            {
                double sum = 0.0;
                double arg = k * Math.PI / n;

                for (int j = 0; j <= n; j++)
                {
                    double dj = 1;

                    if (j % 2 == 0)
                        dj = 2.0;

                    sum += Math.Cos(j * arg) * u[j] / dj;
                }

                double dk = 1.0;

                if (k % 2 == 0)
                    dk = 2.0;

                v[k] = 2.0 * sum / (n * dk);
            }

            return v;
        }












        public double Lagrange(int n, double z, double[] x, double[] y)
        {
            double Epsilon = 1.0e-8;
            double sum = 0.0;

            for (int k = 0; k < n; k++)
            {
                double numer = 1.0;
                double denom = 1.0;
                double prod = 1.0;

                for (int j = 0; j < k; j++)
                {
                    numer = z - x[j];
                    denom = x[k] - x[j];

                    if (Math.Abs(denom) > Epsilon)
                        prod *= numer / denom;
                }

                for (int j = k; j < n; j++)
                {
                    numer = z - x[j];
                    denom = x[k] - x[j];

                    if (Math.Abs(denom) > Epsilon)
                        prod *= numer / denom;
                }

                sum += y[k] * prod;
            }

            return sum;
        }

        public void Newton(int n, double[] x, double[] f)
        {
            int k, i, im1;
            double xim1, fim1;

            im1 = 0;
            for (i = 1; i <= n; i++)
            {
                fim1 = f[im1];
                xim1 = x[im1];
                for (k = i; k <= n; k++) f[k] = (f[k] - fim1) / (x[k] - xim1);
                im1 = i;
            }
        }
    }
}