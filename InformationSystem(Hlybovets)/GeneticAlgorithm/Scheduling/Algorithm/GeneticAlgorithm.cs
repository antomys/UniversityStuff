using System;
using System.Linq;
using Scheduling.Entities;

namespace Scheduling.Algorithm
{
    internal class GeneticAlgorithm
    {
        private const int PopulationSize = 30;
        private const int TopForCrossOvering = 10;
        private const double MutationRate = 0.1;
        private const int IdealCount = 3;
        private readonly Data data;
        private readonly Random rand;

        public GeneticAlgorithm()
        {
            data = new Data();
            rand = new Random();

            currentPopulation = new Population(data, PopulationSize, rand);
        }

        public Population currentPopulation { get; set; }

        public void StupidSolve()
        {
            var step = 0;
            while (currentPopulation.shedules[0].Item2 != 0)
            {
                step++;
                var newPopulation = new Population(data, PopulationSize, rand);
                currentPopulation = newPopulation;
                PrintTopPopulation(step);
            }

            Console.WriteLine(step);
        }

        public void Solve()
        {
            var step = 0;
            while (currentPopulation.shedules[0].Item2 != 0)
            {
                step++;
                CrossOverPopulation();
                MuatatePopulation();
                PrintTopPopulation(step);
            }

            Console.WriteLine("Result schedule:");
            currentPopulation.shedules[0].Item1.OutPut();
        }

        private void CrossOverPopulation()
        {
            var newPopulation = new Population();
            currentPopulation.Sort();

            for (var i = 0; i < IdealCount; ++i) newPopulation.shedules.Add(currentPopulation.shedules[i]);

            for (var i = IdealCount; i < PopulationSize; ++i)
            {
                var sh1 = currentPopulation.shedules[rand.Next(currentPopulation.shedules.Count()) % TopForCrossOvering]
                    .Item1;
                var sh2 = currentPopulation.shedules[rand.Next(currentPopulation.shedules.Count()) % TopForCrossOvering]
                    .Item1;
                var newSh = CrossShedules(sh1, sh2);
                newPopulation.shedules.Add(new Tuple<Timetable, int>(newSh, newSh.GetConflictsCount()));
            }

            currentPopulation = newPopulation;
        }

        private void MuatatePopulation()
        {
            currentPopulation.Sort();
            for (var i = IdealCount; i < PopulationSize; ++i)
            {
                var sh = Mutate(currentPopulation.shedules[i].Item1);
                currentPopulation.shedules[i] = new Tuple<Timetable, int>(sh, sh.GetConflictsCount());
            }
        }

        private Timetable Mutate(Timetable sh)
        {
            var k = new Timetable(data, rand);

            for (var i = 0; i < k.plans.Count(); ++i)
                if (rand.NextDouble() > MutationRate)
                    k.plans[i] = sh.plans[i];

            return k;
        }

        private Timetable CrossShedules(Timetable shedule1, Timetable shedule2)
        {
            var res = new Timetable(data, rand);
            for (var i = 0; i < res.plans.Count(); ++i)
                if (rand.NextDouble() > 0.5)
                    res.plans[i] = shedule1.plans[i];
                else
                    res.plans[i] = shedule2.plans[i];
            return res;
        }

        private void PrintTopPopulation(int step)
        {
            currentPopulation.Sort();
            Console.Write("Step {0}:", step);
            foreach (var p in currentPopulation.shedules) Console.Write("{0}  ", p.Item2);
            Console.WriteLine();
        }
    }
}