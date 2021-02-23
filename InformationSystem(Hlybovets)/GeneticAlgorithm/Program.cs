using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling.Algorithm;
using Scheduling.Entities;

namespace Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();

            geneticAlgorithm.Solve();

            Console.ReadLine();

        }
    }
}
