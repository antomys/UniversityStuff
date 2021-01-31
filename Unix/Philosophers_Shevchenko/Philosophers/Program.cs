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
        static SemaphoreSlim[] chopsticks;
        static SemaphoreSlim waiter;

        static void Main(string[] args)
        {
            philosiphers = new Task[philisopherNumber];
            chopsticks = new SemaphoreSlim[philisopherNumber];
            waiter = new SemaphoreSlim(1);

            for (int i = 0; i < philisopherNumber; ++i)
            {
                philosiphers[i] = GenerateNewPhilosopher(i);
                chopsticks[i] = new SemaphoreSlim(1);
            }

            Task.WaitAll(philosiphers);
            Console.WriteLine("Finished eating.");
            Console.ReadLine();
        }

        private static Task GenerateNewPhilosopher(int philosipherIndex)
        {
            return Task.Run(() =>
            {

                for (int i = 0; i < 5; ++i)
                {
                    Console.WriteLine($"Phil {philosipherIndex}: Calling the waiter");
                    waiter.Wait();
                    var chopstick1 = GetChopstickIndex(philosipherIndex);
                    var chopstick2 = GetChopstickIndex(philosipherIndex + 1);

                    Console.WriteLine($"Phil {philosipherIndex}: [1] {chopsticks[chopstick1].CurrentCount} [2] {chopsticks[chopstick2].CurrentCount}");
                    if (chopsticks[chopstick1].CurrentCount == 1 && chopsticks[chopstick2].CurrentCount == 1)
                    {
                        Console.WriteLine($"Phil {philosipherIndex}: Attempting to get chopsticks");
                        chopsticks[chopstick1].Wait();
                        Console.WriteLine("Trying to get {0}", chopstick1);
                        chopsticks[chopstick2].Wait();
                        Console.WriteLine("Trying to get {0}", chopstick2);
                    }
                    else
                    {
                        Console.WriteLine($"Phil {philosipherIndex}: Failed to acquire Chopsticks");
                        i -= 1;
                        waiter.Release();
                        continue;
                    }
                    waiter.Release();

                    Console.WriteLine($"Phil {philosipherIndex}: Eating");
                    Task.Delay(100);

                    chopsticks[chopstick1].Release();
                    Console.WriteLine("Releasing {0}", chopstick1);
                    chopsticks[chopstick2].Release();
                    Console.WriteLine("Releasing {0}", chopstick2);
                }
            });
        }

        private static int GetChopstickIndex(int index)
        {
            return index % philisopherNumber;
        }
    }
}