using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace TimetableGeneticGeneration
{
    class WorkingWeek
    {
        public Dictionary<String, WorkingDay> _week;
        bool _extraDay = false;


        public WorkingWeek(String specialty, JsonElement root)
        {
            string[] workingDays = Utilities.GetAsObjectJSON<string[]>(root, "WorkingDays");
            _week = new Dictionary<string, WorkingDay>();
            foreach (var day in workingDays)
            {
                _week.Add(day, new WorkingDay(root));
            }
            FillForWeek(specialty, root);
        }

        public WorkingWeek()
        {
            _week = new Dictionary<string, WorkingDay>();
        }

        public WorkingWeek(WorkingWeek week)
        {
            _week = new Dictionary<string, WorkingDay>(week._week);
        }

        private void FillForWeek(string specialty, JsonElement root)
        {
            string[] workingDays = Utilities.GetAsObjectJSON<string[]>(root, "WorkingDays");
            string[] specialtySubjects = Utilities.GetAsObjectJSON<string[]>(root, String.Concat(specialty, "_subjects"));
            string[] specialtyGroups = Utilities.GetAsObjectJSON<string[]>(root, String.Concat(specialty, "_groups"));

            foreach (var subject in specialtySubjects)
            {
                foreach (var group in specialtyGroups)
                {
                    _week[chooseRandomDay(workingDays, root)].FillForDay(group, subject, Utilities.LessonType.Practice, root);   //for practices
                }
                int choosedDayLecture = Utilities.ChooseRandomly(0, workingDays.Length);
                _week[chooseRandomDay(workingDays, root)].FillForDay(String.Join(", ", specialtyGroups), subject, Utilities.LessonType.Lecture, root);  //for lectures
            }
        }

        private String chooseRandomDay(string[] workingDays, JsonElement root)
        {
            while (workingDays.Length != 0)
            {
                int choosedDayNum = Utilities.ChooseRandomly(0, workingDays.Length);
                String choosedDay = workingDays[choosedDayNum];
                if (!_week[choosedDay].IsFull())
                {
                    return choosedDay;
                }
                workingDays = workingDays.Where(x => x != choosedDay).ToArray();
            }
            String extra = "Extra lesson (Didn't fit in the working week)";
            if (!_extraDay)
            {
                _week.Add(extra, new WorkingDay(root));
                _extraDay = true;
            }
            return extra;
        }


    }
}
