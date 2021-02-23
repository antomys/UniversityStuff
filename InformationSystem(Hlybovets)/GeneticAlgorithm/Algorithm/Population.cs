using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling.Entities;

namespace Scheduling.Algorithm
{
    class Population
    {
        public List<Tuple<Shedule, int>> shedules { get; set; }

        public Population()
        {
            shedules = new List<Tuple<Shedule, int>>();
        }

        public Population(Data data, int PopulationSize, Random rand)
        {
            shedules = new List<Tuple<Shedule, int>>();

            for (int i = 0; i<PopulationSize; ++i)
            {
                Shedule s = new Shedule(data, rand);
                shedules.Add(new Tuple<Shedule, int>(s, s.GetConflictsCount()));
            }
        }

        public void Sort()
        {
            shedules = shedules.OrderBy(x => x.Item2).ToList();
        }
    }
}
