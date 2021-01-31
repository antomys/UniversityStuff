using System;
using System.Collections.Generic;

namespace krKrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            //Схема RSA
            int m = 22;
            int p = 11;
            int q = 5;
            int e = 7;
            RSA(m, p, q, e);

            Console.WriteLine("\n.........................................................................................................");

            //a*x = 1 (mod n)
            int a = 823;
            int n = 594;
            NSD(a, n);
        }

        static void NSD(int a1, int b1)
        {
            Console.WriteLine($"\n\n2) Розв'язок порiвняння {a1}*x = 1 (mod {b1})\nДля цього представимо наше порiвняння у виглядi а*х + b*у = 1 i скористаємося розширеним алгоритмом Евклiда");
            Console.WriteLine($"\nПрямий хiд (пошук НСД({a1}, {b1})):");
            List<int> a = new List<int> { a1 };
            List<int> b = new List<int> { b1 };
            List<int> c = new List<int> { a1 / b1 };
            List<int> d = new List<int> { a1 % b1 };
            int k = 0;
            while (d[k] != 0) 
            {
                a.Add(b[k]);
                b.Add(d[k]);
                ++k;
                c.Add(a[k] / b[k]);
                d.Add(a[k] % b[k]);
            }
            for (int i = 0; i < a.Count; ++i) 
            {
                Console.WriteLine($"{a[i]} = {b[i]}*({c[i]}) + {d[i]}");
            }
            int nsd = d[k - 1];
            Console.WriteLine($"НСД({a1}, {b1}) = {nsd}");


            Console.WriteLine($"\nЗворотнiй хiд (пошук коефiцiєнтiв х та у):");
            --k;
            int x = a[k];
            int x1 = 1;
            int y = b[k];
            int y1 = c[k];
            Console.WriteLine($"{nsd} = {x}*({x1}) - {y}*({y1})");
            while (k > 0)
            {
                --k;
                if(x == d[k])
                {
                    Console.Write($"{nsd} = ({a[k]} - {b[k]}*({c[k]}))*({x1}) - {y}*({y1}) = ");
                    x = a[k];
                    y1 += c[k] * x1;
                    Console.WriteLine($"{x}*({x1}) - {y}*({y1})");
                }
                else
                {
                    Console.Write($"{nsd} = {x}*({x1}) - ({a[k]} - {b[k]}*({c[k]}))*({y1}) = ");
                    x1 += c[k] * y1;
                    y = a[k];
                    Console.WriteLine($"{x}*({x1}) - {y}*({y1})");
                }
            }
            int resx = x1;
            int resy = (-1) * y1;
            if (a1 != x)
            {
                resx = (-1) * y1;
                resy = x1;
            }
            Console.WriteLine($"Отримали розв'язок {a1}*({resx}) + {b1}*({resy}) = 1");
            Console.Write($"\nВiдповiдь: х = {resx} (mod {b1})");
            if (resx < 0)
            {
                resx += b1;
                Console.Write($" = {resx} (mod {b1})");
            }
            Console.WriteLine($"\n");
        }

        static void RSA(int m, int p, int q, int e)
        {
            Console.WriteLine("1) Схема RSA");
            Console.WriteLine($"m = {m}; p = {p}; q = {q}; e = {e};\n");
            int n = p * q;
            Console.WriteLine($"n = p*q = {p}*{q} = {n};");

            int ph_n = phi(n);
            if (prost(p) && prost(q))
            {
                Console.WriteLine($"ф(n) = ф(p)*ф(q) =  ф({p})*ф({q}) = ({p} - 1)*({q} - 1) = " + (p - 1) + "*" + (q - 1) + " = " + ((p - 1) * (q - 1)) + ";");
                if (ph_n != ((p - 1) * (q - 1)))
                {
                    Console.WriteLine("!!!!!!!!!!!!!! Щось не так із ф-цією Ейлера !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            }
            else
            {
                Console.WriteLine("p або q не просте");
            }
            Console.WriteLine("TEST...... ф(n) = " + phi(n) + ";\n");

            Console.Write($"d = e^(ф(n)-1) mod ф(n) = ");
            int d = step(e, ph_n - 1, ph_n);

            Console.Write($"c = m^e mod n = ");
            int c = step(m, e, n);

            Console.Write($"m = c^d mod n = ");
            int m1 = step(c, d, n);

            if (m == m1)
            {
                Console.WriteLine($"Отримали правильний результат");
            }
            else
            {
                Console.WriteLine($"!!!!!! Чот якась фігня. Не сходиться !!!!!!!!!!!!!!!!!!!!!!");
            }
        }

        static int step (int a, int b, int n)//a^b mod n
        {
            Console.WriteLine($"{a}^{b} mod {n} (=)");
            Console.WriteLine("-----------------");

            
            List<int> pow2 = new List<int>();
            for(int i = 1; i <= b; i *= 2)
            {
                pow2.Add(i);
            }

            Console.Write(b + " = ");
            int buf = b;
            for (int i = pow2.Count - 1; i >= 0; --i)
            {
                if(buf >= pow2[i])
                {
                    buf -= pow2[i];
                    Console.Write("+" + pow2[i]);
                }
            }
            Console.WriteLine();
            Console.WriteLine();

            buf = a;
            List<int> powa = new List<int>();
            powa.Add(buf);
            for (int i = 2; i <= b; i *= 2)
            {
                Console.Write($"{a}^{i} | ({buf})^2 mod {n} = ");
                buf = (buf * buf) % n;
                Console.Write(buf);
                if ((buf-n)*(-1) < buf)
                {
                    buf -= n;
                    Console.Write($" = {buf}");
                }
                powa.Add(buf);
                Console.WriteLine();
            }

            Console.Write("-----------------\n (=) ");
            buf = b;
            for (int i = pow2.Count - 1; i >= 0; --i)
            {
                if (buf >= pow2[i])
                {
                    buf -= pow2[i];
                    Console.Write($"*{a}^{pow2[i]}");
                }
            }
            Console.Write($" mod {n} = "); 
            buf = b;
            int res = 1;
            for (int i = pow2.Count - 1; i >= 0; --i)
            {
                if (buf >= pow2[i])
                {
                    buf -= pow2[i];
                    res = res * powa[i] % n;
                    Console.Write($"*({powa[i]})");
                }
            }
            Console.Write($" mod {n} = {res}");
            if (res < 0)
            {
                res += n;
                Console.Write($" = {res}");
            }
            Console.Write("\n\n\n");
            return res;
        }

        static bool prost (int x)
        {
            for(int i = 2; i < Math.Sqrt(x); ++i)
            {
                if (x % i == 0)
                    return false;
            }
            return true;
        }

        static int phi(int n)
        {
            int ret = 1;
            for (int i = 2; i * i <= n; ++i)
            {
                int p = 1;
                while (n % i == 0)
                {
                    p *= i;
                    n /= i;
                }
                if ((p /= i) >= 1) ret *= p * (i - 1);
            }
            return (--n > 0) ? n * ret : ret;
        }
    }
}
