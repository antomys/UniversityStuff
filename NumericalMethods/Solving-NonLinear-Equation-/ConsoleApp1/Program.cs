using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Methods
    {
        static double func(double x)
        {
            return Math.Pow(x,2)+Math.Sin(x)-12.0*x-0.25;
        }

        static double df(double x)
        {
            return 2*x+Math.Cos(x)-12;
        }

        public void AposteriorDychotomy(double a, double b, double e)
        {
            Console.WriteLine("Aposterior Dychotomy\n");
            int i = 0;
            do
            {
                double x = (a + b) / 2;
                double fx = func(x);
                double fa = func(a);
                double fb = func(b);
                if (fb * fx < 0)
                {
                    a = x;
                }
                else
                    if (fa * fx < 0)
                {
                    b = x;
                }
                i++;
                Console.WriteLine($"N= {i} | x= {x}| f(x)= {func(x)}");
            }
            while (b - a >= 2 * e);
          
        }

        public void ApriorDychotomy(double a, double b,double e)
        {
            int Apriory = Convert.ToInt32(Math.Log((b - a) / e) / Math.Log(2)) + 1;
            Console.WriteLine($"ApriorDych= {Apriory}\n");
            
        }

        public void AposteriorRelaxation(double x, double r, double eps)
        {
            Console.WriteLine("AposteriorRelaxation");
            double x0 = x;
            int i = 0;
            while (r * func(x) > eps)
            {
                x = x + r * func(x);
                i++;
                Console.WriteLine($"N= {i} | x= {x} | r= {r}| f(x) = {func(x)} ");
            }

            Console.WriteLine("|||X = " + x + " N = " + i + "|||\n");
        }

        public void ApriorRelaxation(double x, double r, int n)
        {
            Console.WriteLine("Aprior Relaxation");
            double x0 = x;
            int i = 0;
            while (i < n)
            {
                x = x + r * func(x);
                i++;
                Console.WriteLine($"N= {i}| x= {x}| r={r}| f(x)={func(x)}");
            }

            Console.WriteLine("X = " + x + " Epsilon= " + Math.Abs(r * func(x)) + "\n");

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Methods calc = new Methods();
            Console.WriteLine("Dychotomy and Relaxation:\n ");


                calc.ApriorRelaxation(-4, 0.0001, 5);
                calc.AposteriorRelaxation(-5, 0.001, 0.000001);
                

                calc.ApriorDychotomy(-215, 125, 0.0001);
                calc.AposteriorDychotomy(-5, 7, 0.0001);

            Console.ReadKey();


        }
    }
}