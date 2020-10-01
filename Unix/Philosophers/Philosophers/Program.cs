using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    class Program
    {
        static int philisopherNumber = 5;
        static Task[] philosiphers;
        static SemaphoreSlim[] forks;
        static SemaphoreSlim waiter;

        static void Main(string[] args)
        {
            forks = new SemaphoreSlim[philisopherNumber];
            waiter = new SemaphoreSlim(1);
            philosiphers = new Task[philisopherNumber];

            for (int i = 0; i < philisopherNumber; ++i)
            {
                philosiphers[i] = genphil(i);
                forks[i] = new SemaphoreSlim(1);
            }

            Task.WaitAll(philosiphers);
            Console.WriteLine("Finished eating.");
            Console.ReadLine();
        }

        private static Task genphil(int philosipherIndex)
        {
            return Task.Run(() =>
            {

                 for (int i = 0; i < 5; ++i)
                {
                    Console.WriteLine($"Phil {philosipherIndex}: Calling the waiter");
                    waiter.Wait();
                    var fork1 = GetChopstickIndex(philosipherIndex);
                    var fork2 = GetChopstickIndex(philosipherIndex + 1);

                    Console.WriteLine($"Phil {philosipherIndex}: [1] {forks[fork1].CurrentCount} [2] {forks[fork2].CurrentCount}");
                    if (forks[fork1].CurrentCount ==1 && forks[fork2].CurrentCount ==1)
                    {
                        Console.WriteLine($"Phil {philosipherIndex}: Attempting to get forks");
                        forks[fork1].Wait();
                        Console.WriteLine("Trying to get {0}", fork1);
                        forks[fork2].Wait();
                        Console.WriteLine("Trying to get {0}", fork2);
                    }
                    else
                    {
                        Console.WriteLine($"Phil {philosipherIndex}: Failed to acquire forks");
                        i -= 1;
                        waiter.Release();
                        continue;
                    }
                    waiter.Release();

                    Console.WriteLine($"Phil {philosipherIndex}: Eating");
                    Task.Delay(100);

                    forks[fork1].Release();
                    Console.WriteLine("Releasing {0}", fork1);
                    forks[fork2].Release();
                    Console.WriteLine("Releasing {0}", fork2);
                    Console.WriteLine('\n');
                }
            });
        }

        private static int GetChopstickIndex(int index)
        {
            return index % philisopherNumber;
        }
    }
}