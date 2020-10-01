using System;

namespace ChebyshevInterpolation
{
    class Series
    {
        double c;
        double[] a, b;
        int n;
        Func<double, double> f;

        private double g0(double x)
        {
            return f(x);
        }

        private double g1(double x)
        {
            return f(x) * Math.Cos(Math.PI * x / c);
        }

        private double g2(double x)
        {
            return f(x) * Math.Cos(2 * Math.PI * x / c);
        }

        private double g3(double x)
        {
            return f(x) * Math.Cos(3 * Math.PI * x / c);
        }

        private double g4(double x)
        {
            return f(x) * Math.Cos(4 * Math.PI * x / c);
        }

        private double g5(double x)
        {
            return f(x) * Math.Cos(5 * Math.PI * x / c);
        }

        private double g6(double x)
        {
            return f(x) * Math.Cos(6 * Math.PI * x / c);
        }

        private double g7(double x)
        {
            return f(x) * Math.Cos(7 * Math.PI * x / c);
        }

        private double g8(double x)
        {
            return f(x) * Math.Cos(8 * Math.PI * x / c);
        }

        private double g9(double x)
        {
            return f(x) * Math.Cos(9 * Math.PI * x / c);
        }

        private double g10(double x)
        {
            return f(x) * Math.Cos(10 * Math.PI * x / c);
        }

        private double g11(double x)
        {
            return f(x) * Math.Cos(11 * Math.PI * x / c);
        }

        private double h1(double x)
        {
            return f(x) * Math.Sin(Math.PI * x / c);
        }

        private double h2(double x)
        {
            return f(x) * Math.Sin(2 * Math.PI * x / c);
        }

        private double h3(double x)
        {
            return f(x) * Math.Sin(3 * Math.PI * x / c);
        }

        private double h4(double x)
        {
            return f(x) * Math.Sin(4 * Math.PI * x / c);
        }

        private double h5(double x)
        {
            return f(x) * Math.Sin(5 * Math.PI * x / c);
        }

        private double h6(double x)
        {
            return f(x) * Math.Sin(6 * Math.PI * x / c);
        }

        private double h7(double x)
        {
            return f(x) * Math.Sin(7 * Math.PI * x / c);
        }

        private double h8(double x)
        {
            return f(x) * Math.Sin(8 * Math.PI * x / c);
        }

        private double h9(double x)
        {
            return f(x) * Math.Sin(9 * Math.PI * x / c);
        }

        private double h10(double x)
        {
            return f(x) * Math.Sin(10 * Math.PI * x / c);
        }

        private double h11(double x)
        {
            return f(x) * Math.Sin(11 * Math.PI * x / c);
        }

        public Series(
            double c,
            int n,
            Func<double, double> f)
        {
            this.c = c;
            this.n = n;
            this.f = f;

            double[] e = new double[7];

            a = new double[n + 1];
            b = new double[n + 1];

            e[1] = 1.0e-15;
            e[2] = 1.0e-15;

            a[0] = integral(-c, c, g0, e, true, true);
            a[1] = integral(-c, c, g1, e, true, true);
            b[1] = integral(-c, c, h1, e, true, true);
            a[0] *= 0.5;

            if (n == 1)
                return;

            a[2] = integral(-c, c, g2, e, true, true);
            b[2] = integral(-c, c, h2, e, true, true);

            if (n == 2)
                return;

            a[3] = integral(-c, c, g3, e, true, true);
            b[3] = integral(-c, c, h3, e, true, true);

            if (n == 3)
                return;

            a[4] = integral(-c, c, g4, e, true, true);
            b[4] = integral(-c, c, h4, e, true, true);

            if (n == 4)
                return;

            a[5] = integral(-c, c, g5, e, true, true);
            b[5] = integral(-c, c, h5, e, true, true);

            if (n == 5)
                return;

            a[6] = integral(-c, c, g6, e, true, true);
            b[6] = integral(-c, c, h6, e, true, true);

            if (n == 6)
                return;

            a[7] = integral(-c, c, g7, e, true, true);
            b[7] = integral(-c, c, h7, e, true, true);

            if (n == 7)
                return;

            a[8] = integral(-c, c, g8, e, true, true);
            b[8] = integral(-c, c, h8, e, true, true);

            if (n == 8)
                return;

            a[9] = integral(-c, c, g9, e, true, true);
            b[9] = integral(-c, c, h9, e, true, true);

            if (n == 9)
                return;

            a[10] = integral(-c, c, g10, e, true, true);
            b[10] = integral(-c, c, h10, e, true, true);

            if (n == 10)
                return;

            a[11] = integral(-c, c, g11, e, true, true);
            b[11] = integral(-c, c, h11, e, true, true);

            if (n == 11)
                return;
        }

        // naieve computation of the Fourier series

        public double ComputeSeries(double x)
        {
            double csum = a[0], ssum = 0;

            for (int i = 1; i <= n; i++)
            {
                double arg = i * Math.PI * x / c;

                csum += a[i] * Math.Cos(arg);
                ssum += b[i] * Math.Sin(arg);
            }

            return csum + ssum;
        }

        // functions from "A Numerical Library in C for Scientists
        // and Engineers" by H.T. Lau, PhD pages 299-303

        private double integral(double a, double b, Func<double, double> fx,
            double[] e, bool ua, bool ub)
        {
            double x0, x1 = 0, x2, f0, f1 = 0, f2, re, ae, b1 = 0, x;

            re = e[1];
            if (ub)
                ae = e[2] * 180.0 / Math.Abs(b - a);
            else
                ae = e[2] * 90.0 / Math.Abs(b - a);
            if (ua)
            {
                e[3] = e[4] = 0.0;
                x = x0 = a;
                f0 = fx(x);
            }
            else
            {
                x = x0 = a = e[5];
                f0 = e[6];
            }
            e[5] = x = x2 = b;
            e[6] = f2 = fx(x);
            e[4] += integralqad(false, fx, e, ref x0, ref x1, ref x2, ref f0,
                ref f1, ref f2, re, ae, b1);
            if (!ub)
            {
                if (a < b)
                {
                    b1 = b - 1.0;
                    x0 = 1.0;
                }
                else
                {
                    b1 = b + 1.0;
                    x0 = -1.0;
                }
                f0 = e[6];
                e[5] = x2 = 0.0;
                e[6] = f2 = 0.0;
                ae = e[2] * 90.0;
                e[4] -= integralqad(true, fx, e, ref x0, ref x1, ref x2, ref f0,
                    ref f1, ref f2, re, ae, b1);
            }
            return e[4];
        }

        private double integralqad(bool transf, Func<double, double> fx, double[] e,
            ref double x0, ref double x1, ref double x2, ref double f0, ref double f1,
            ref double f2, double re, double ae, double b1)
        {
            /* this function is internally used by INTEGRAL */

            double sum, hmin, x, z;

            hmin = Math.Abs(x0 - x2) * re;
            x = x1 = (x0 + x2) * 0.5;
            if (transf)
            {
                z = 1.0 / x;
                x = z + b1;
                f1 = fx(x) * z * z;
            }
            else
                f1 = fx(x);
            sum = 0.0;
            integralint(transf, fx, e, ref x0, ref x1, ref x2, ref f0,
                ref f1, ref f2, ref sum, re, ae, b1, hmin);
            return sum / 180.0;
        }

        void integralint(bool transf, Func<double, double> fx, double[] e,
                    ref double x0, ref double x1, ref double x2, ref double f0, ref double f1,
                    ref double f2, ref double sum, double re, double ae, double b1,
                    double hmin)
        {
            /* this function is internally used by INTEGRALQAD of INTEGRAL */

            bool anew;
            double x3, x4, f3, f4, h, x, z, v, t;

            x4 = (x2);
            (x2) = (x1);
            f4 = (f2);
            (f2) = (f1);
            anew = true;
            while (anew)
            {
                anew = false;
                x = x1 = ((x0) + (x2)) * 0.5;
                if (transf)
                {
                    z = 1.0 / x;
                    x = z + b1;
                    f1 = fx(x) * z * z;
                }
                else
                    f1 = fx(x);
                x = x3 = (x2 + x4) * 0.5;
                if (transf)
                {
                    z = 1.0 / x;
                    x = z + b1;
                    f3 = fx(x) * z * z;
                }
                else
                    f3 = fx(x);
                h = x4 - (x0);
                v = (4.0 * ((f1) + f3) + 2.0 * (f2) + (f0) + f4) * 15.0;
                t = 6.0 * (f2) - 4.0 * ((f1) + f3) + (f0) + f4;
                if (Math.Abs(t) < Math.Abs(v) * re + ae)
                    sum += (v - t) * h;
                else if (Math.Abs(h) < hmin)
                    e[3] += 1.0;
                else
                {
                    integralint(transf, fx, e, ref x0, ref x1, ref x2, ref f0, ref f1,
                        ref f2, ref sum, re, ae, b1, hmin);
                    x2 = x3;
                    f2 = f3;
                    anew = true;
                }
                if (!anew)
                {
                    x0 = x4;
                    f0 = f4;
                }
            }
        }
    }
}
