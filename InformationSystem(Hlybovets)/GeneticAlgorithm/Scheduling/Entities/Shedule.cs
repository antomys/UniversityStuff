using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Entities
{
    class Shedule
    {
        public Data data { get; set; }
        public List<Planned> plans { get; set; }

        public Shedule(Data _data, Random rand)
        {
            data = _data;
            plans = new List<Planned>();

            foreach (Group g in data.Groups)
            {
                foreach (Subject s in data.Subjects)
                {
                    for (int i=0; i<s.TimesPerWeek; ++i)
                    {
                        plans.Add(new Planned(
                            data.TimeSlots[rand.Next(data.TimeSlots.Count())],
                            s,
                            data.Rooms[rand.Next(data.Rooms.Count())],
                            g,
                            data.Teachers[rand.Next(data.Teachers.Count())]
                            ));
                    }
                }
            }
        }

        public int GetConflictsCount()
        {
            int res = 0;
            //rooms capacity
            res += plans.Where(p => p.room.Capacity < (p.subject.IsLab ? p.group.Size/2 : p.group.Size)).Count();

            //groups has 2 plans in 1 timeslot
            foreach (Group g in data.Groups)
            {
                foreach (TimeSlot t in data.TimeSlots)
                {
                    int i = plans.Where(p => p.timeSlot.Id == t.Id && p.group.Id == g.Id).Count();
                    res += (i > 1 ? i - 1 : 0);
                }
            }

            //teachers has 2 plans in 1 timeslot
            foreach (Teacher tc in data.Teachers)
            {
                foreach (TimeSlot t in data.TimeSlots)
                {
                    int i = plans.Where(p => p.timeSlot.Id == t.Id && p.teacher.Id == tc.Id).Count();
                    res += (i > 1 ? i - 1 : 0);
                }
            }
            // 2 plans in 1 room
            foreach (Room r in data.Rooms)
            {
                foreach (TimeSlot t in data.TimeSlots)
                {
                    int i = plans.Where(p => p.timeSlot.Id == t.Id && p.room.Id == r.Id).Count();
                    res += (i > 1 ? i - 1 : 0);
                }
            }

            //same lector check
            foreach (Group g in data.Groups)
            {
                foreach (Subject s in data.Subjects)
                {
                    int i = plans.Where(p => p.group.Id == g.Id && p.subject.Id == s.Id && !s.IsLab).Select(p => p.teacher.Id).Distinct().Count();
                    res += (i > 1 ? i - 1 : 0);
                }
            }

            return res;

        }

        public void OutPut()
        {
            foreach (Group g in data.Groups)
            {
                Console.WriteLine("{0}:", g.Name);
                var res = plans.Where(p => p.group.Id == g.Id).OrderBy(p => p.timeSlot.Id);
                foreach (var p in res)
                {
                    Console.WriteLine("\t{0} - {1} - {2} - {3} - {4}", p.timeSlot.Day, p.timeSlot.Time, p.subject.Name, p.teacher.Name, p.room.Name);
                }
            }
        }
    }
}
