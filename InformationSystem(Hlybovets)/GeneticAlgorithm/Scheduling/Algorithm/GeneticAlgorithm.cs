using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling.Entities;

namespace Scheduling.Algorithm
{
    class GeneticAlgorithm
    {
        private const int PopulationSize = 30;
        private const int TopForCrossOvering = 10;
        private const double MutationRate = 0.1;
        private const int IdealCount = 3;

        public Population currentPopulation { get; set; }
        private Data data;
        private Random rand;

        public GeneticAlgorithm()
        {
            data = new Data();
            rand = new Random();

            currentPopulation = new Population(data, PopulationSize, rand);
        }

        public void StupidSolve()
        {
            int step = 0;
            while (currentPopulation.shedules[0].Item2 != 0)
            {
                step++;
                Population newPopulation = new Population(data, PopulationSize, rand);
                currentPopulation = newPopulation;
                PrintTopPopulation(step);
            }
            Console.WriteLine(step);
        }

        public void Solve()
        {
            int step = 0;
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
            Population newPopulation = new Population();
            currentPopulation.Sort();

            for (int i=0; i<IdealCount; ++i)
            {
                newPopulation.shedules.Add(currentPopulation.shedules[i]);
            }

            for (int i=IdealCount; i<PopulationSize; ++i)
            {
                Shedule sh1 = currentPopulation.shedules[rand.Next(currentPopulation.shedules.Count()) % TopForCrossOvering].Item1;
                Shedule sh2 = currentPopulation.shedules[rand.Next(currentPopulation.shedules.Count()) % TopForCrossOvering].Item1;
                Shedule newSh = CrossShedules(sh1, sh2);
                newPopulation.shedules.Add(new Tuple<Shedule, int>(newSh, newSh.GetConflictsCount()));
            }

            currentPopulation = newPopulation;
        }

        private void MuatatePopulation()
        {
            currentPopulation.Sort();
            for (int i=IdealCount; i<PopulationSize; ++i)
            {
                Shedule sh = Mutate(currentPopulation.shedules[i].Item1);
                currentPopulation.shedules[i] = new Tuple<Shedule, int>(sh, sh.GetConflictsCount());
            }
        }

        private Shedule Mutate(Shedule sh)
        {
            Shedule k = new Shedule(data, rand);

            for (int i=0; i<k.plans.Count(); ++i)
            {
                if (rand.NextDouble()>MutationRate)
                {
                    k.plans[i] = sh.plans[i];
                }
            }

            return k;
        }

        private Shedule CrossShedules(Shedule shedule1, Shedule shedule2)
        {
            Shedule res = new Shedule(data, rand);
            for (int i=0; i<res.plans.Count(); ++i)
            {
                if (rand.NextDouble()>0.5)
                {
                    res.plans[i] = shedule1.plans[i];
                }
                else
                {
                    res.plans[i] = shedule2.plans[i];
                }
            }
            return res;
        }

        private void PrintTopPopulation(int step)
        {
            currentPopulation.Sort();
            Console.Write("Step {0}:", step);
            foreach (var p in currentPopulation.shedules)
            {
                Console.Write("{0}  ", p.Item2);
            }
            Console.WriteLine();
        }
    }
}
