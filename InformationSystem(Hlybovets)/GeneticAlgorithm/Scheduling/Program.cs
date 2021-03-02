using System;
using Scheduling.Algorithm;

namespace Scheduling
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var geneticAlgorithm = new GeneticAlgorithm();

            geneticAlgorithm.Solve();

            Console.ReadLine();
        }
    }
}