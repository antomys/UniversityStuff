using System;
using System.Collections.Generic;
using System.Linq;
using Scheduling.Entities;

namespace Scheduling.Algorithm
{
    internal class Population
    {
        public Population()
        {
            shedules = new List<Tuple<Timetable, int>>();
        }

        public Population(Data data, int PopulationSize, Random rand)
        {
            shedules = new List<Tuple<Timetable, int>>();

            for (var i = 0; i < PopulationSize; ++i)
            {
                var s = new Timetable(data, rand);
                shedules.Add(new Tuple<Timetable, int>(s, s.GetConflictsCount()));
            }
        }

        public List<Tuple<Timetable, int>> shedules { get; set; }

        public void Sort()
        {
            shedules = shedules.OrderBy(x => x.Item2).ToList();
        }
    }
}