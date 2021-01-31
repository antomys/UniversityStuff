using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mmsLab3
{
    class Program
    {
        const int n = 4; //3+1
        
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //ініціалізація
            double[,] p = new double[n, n] { { 0, 1, 0, 0}, { 0, 0.2, 0.8, 0 }, { 0, 0, 0.4, 0.6 }, { 0, 0, 0.2, 0.1 } };//матриця ймовірностей слідування
            double[] m = new double[n] { 0, 0.5, 0.6, 0.7 };//інтенсивність обслуговування кожним каналом (1/mi)
            double[] r = new double[n] { 0, 1, 3, 2 };//к-сть каналів у СМОi
            double[] e = FindEi(p);//коефіцієнти передачі (ai=ei*a0)
            double[] a = new double[n];//надходження до СМОi (ai=ei*a0)
            a[0] = 0.8;
            Console.WriteLine("Розрахунок надходжень до СМОi");
            for (int i = 1; i < n; ++i)
            {
                a[i] = e[i] * a[0];
                Console.WriteLine("\ta" + i + " = " + a[i]);
            }

            //перевірка роботи СМОі в сталих режимах (ai<mi*ri) (a0<mi*ri/ei)
            Console.WriteLine("Перевiрка роботи СМОi в сталих режимах");
            for (int i = 1; i < n; ++i)
            {
                if (a[0] < r[i] / (e[i] * m[i]))
                {
                    Console.WriteLine("\tCMO" + i + " працює в сталомy режимi");
                }
                else
                {
                    Console.WriteLine("\tCMO" + i + " працює HE в сталомy режимi");
                }
            }

            double[] C = FindCi(a, e, m, r);//нормуючі множники

            //обчислення основних показників ефективності функціонування розімкнутої мережі МО
            Console.WriteLine("\n\nOбчислення основних показникiв ефективностi функцiонування розiмкнутої мережi МО\n");
            double[] L = FindLi(a, e, m, r, C);//середня к-сть вимог у СМОi (Li=sum(k, ri+1, inf, (k-ri)*pi(k))) (Li=sum(k, ri+1, inf, (k-ri)*((ei*a0*mi)^k)*Ci/(ri!*ri^(k-ri))))
            double[] R = FindRi(a, e, m);//середня к-сть зайнятих пристроїв у СМОі (Ri=ei*a0*mi)
            double[] M = FindMi(L, R);//середня к-сть вимог, які перебувають у СМОі (Mi=Li+Ri)
            double[] Q = FindQi(a, e, L);//середній час очікування в СМОі (Qi=Li/(ei*a0)) (друга формула Літтла)
            double[] T = FindTi(a, e, M);//середній час перебування вимог у СМОі (Ti=Mi/(ei*a0)) (перша формула Літтла)
            double Ttotal = FindT(e, T);//середній час перебування вимог у мережі МО (T=sum(ei*Ti))

            Console.ReadKey();
        }

        static double[] FindEi(double[,] p)//знаходить еі методом Гауса по матриці ймовірностей слідування
        {
            Console.WriteLine("Розрахунок коефiцiєнтiв передачi");
            double[,] ematr = new double[n, n];

            //заповнення матриці
            for (int i = 0; i < n; ++i)
            {
                ematr[0, i] = (-1) * p[0, i];
            }
            for (int i = 1; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ematr[i, j] = p[i, j];
                    if (i == j) --ematr[i, j];
                }
            }

            //прямий хід
            for (int j = 1; j < n - 1; ++j)
            {
                if (ematr[j, j + 1] != 0)
                {
                    double buf = ematr[j, j + 1] / ematr[j, j];
                    for (int i = 0; i < n; ++i)
                    {
                        ematr[i, j + 1] -= (ematr[i, j] * buf);
                    }
                }
            }

            //зворотній хід
            for (int j = n - 1; j > 1; --j)
            {
                if (ematr[j, j - 1] != 0)
                {
                    double buf = ematr[j, j - 1] / ematr[j, j];
                    for (int i = 0; i < n; ++i)
                    {
                        ematr[i, j - 1] -= (ematr[i, j] * buf);
                    }
                }
            }

            //результат
            double[] e = new double[n];
            e[0] = 0;
            for (int i = 1; i < n; ++i)
            {
                e[i] = ematr[0, i] / ematr[i, i];
                Console.WriteLine("\te" + i + " = " + e[i]);
            }
            return e;
        }

        static double[] FindCi(double[] a, double[] e, double[] m, double[] r)//пошук Сі - нормуючих множників
        {
            Console.WriteLine("Пошук нормуючих множникiв");
            double[] c = new double[n];
            c[0] = 0;

            for (int i = 1; i < n; ++i)
            {
                //пошук Сі
                string buf = "(" + e[i] + "*" + a[0] + "*" + m[i] + ")";
                string exp = "1 / ((" + buf + " ^ " + r[i] + ") / ((1 - " + buf + " / " + r[i] + ") * (" + r[i] + "!))+sum(k, 0, (" + r[i] + " - 1), (" + buf + " ^ k) / (k!)))";
                Expression expi = new Expression(exp);
                c[i] = expi.calculate();

                //перевірка правильності Сі
                string test = "sum(k,0,"+r[i]+",(("+buf+"^k)*"+c[i]+")/(k!))+sum(k,("+r[i]+"+1),999,(("+buf+"^k)*"+c[i]+")/(("+r[i]+"!)*("+r[i]+"^(k-"+r[i]+"))))";
                Expression expt = new Expression(test);
                double t = expt.calculate();
                if (t == 1)
                {
                    Console.WriteLine("\tC"+i+" = "+c[i]+ " розрахованo правильно (SUM = " + t + ")");
                }
                else
                {
                    Console.WriteLine("\tC" + i + " = " + c[i] + " розрахованo iз похибкою (SUM = "+t+")");
                }
            }

            return c;
        }

        static double[] FindLi(double[] a, double[] e, double[] m, double[] r, double[] c)//пошук Lі - середньої к-сті вимог у СМОі (Li=sum(k, ri+1, inf, (k-ri)*pi(k))) (Li=sum(k, ri+1, inf, (k-ri)*((ei*a0*mi)^k)*Ci/(ri!*ri^(k-ri))))
        {
            Console.WriteLine("Пошук середньої к-стi вимог у СМОi (мат. сподiвання к-стi вимог у черзi)");
            double[] l = new double[n];
            l[0] = 0;

            for (int i = 1; i < n; ++i)
            {
                string buf = "(" + e[i] + "*" + a[0] + "*" + m[i] + ")";
                string exp = "sum(k,(" + r[i] + "+1),999,((" + buf + "^k)*(k-" + r[i] + ")*" + c[i] + ")/((" + r[i] + "!)*(" + r[i] + "^(k-" + r[i] + "))))";
                Expression expi = new Expression(exp);
                l[i] = expi.calculate();
                Console.WriteLine("\tL" + i + " = " + l[i]);
            }

            return l;
        }

        static double[] FindRi(double[] a, double[] e, double[] m)//пошук Rі - середньої к-сті зайнятих пристроїв у СМОі (Ri=ei*a0*mi)
        {
            Console.WriteLine("Пошук середньої к-стi зайнятих пристроїв у СМОi");
            double[] r = new double[n];
            r[0] = 0;

            for (int i = 1; i < n; ++i)
            {
                r[i] = e[i] * a[0] * m[i];
                Console.WriteLine("\tR" + i + " = " + r[i]);
            }

            return r;
        }

        static double[] FindMi(double[] l, double[] r)//пошук Mі - середньої к-сті вимог, які перебувають у СМОі (Mi=Li+Ri)
        {
            Console.WriteLine("Пошук середньої к-стi вимог, якi перебувають у СМОi");
            double[] M = new double[n];
            M[0] = 0;

            for (int i = 1; i < n; ++i)
            {
                M[i] = l[i] + r[i];
                Console.WriteLine("\tM" + i + " = " + M[i]);
            }

            return M;
        }

        static double[] FindQi(double[] a, double[] e, double[] L)//пошук Qі - середнього часу очікування в СМОі (Qi=Li/(ei*a0)) (друга формула Літтла)
        {
            Console.WriteLine("Пошук середнього часу очiкування в СМОi");
            double[] Q = new double[n];
            Q[0] = 0;

            for (int i = 1; i < n; ++i)
            {
                Q[i] = L[i] / (e[i] * a[0]);
                Console.WriteLine("\tQ" + i + " = " + Q[i]);
            }

            return Q;
        }

        static double[] FindTi(double[] a, double[] e, double[] M)//пошук Tі - середнього часу перебування вимог у СМОі (Ti=Mi/(ei*a0)) (перша формула Літтла)
        {
            Console.WriteLine("Пошук середнього часу перебування вимог у СМОi");
            double[] T = new double[n];
            T[0] = 0;

            for (int i = 1; i < n; ++i)
            {
                T[i] = M[i] / (e[i] * a[0]);
                Console.WriteLine("\tT" + i + " = " + T[i]);
            }

            return T;
        }

        static double FindT(double[] e, double[] T)//пошук T - середнього часу перебування вимог у мережі МО (T=sum(ei*Ti))
        {
            Console.WriteLine("Пошук середнього часу перебування вимог у мережi МО");
            double Ttotal = 0.0;

            for (int i = 1; i < n; ++i)
            {
                Ttotal += (e[i] * T[i]);
            }
            Console.WriteLine("\tT = " + Ttotal);

            return Ttotal;
        }
    }
}
