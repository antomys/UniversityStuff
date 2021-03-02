using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace TimetableCSP
{
    internal class WorkingWeek
    {
        private bool _extraDay;
        public Dictionary<string, WorkingDay> _week;


        public WorkingWeek(string specialty, JsonElement root)
        {
            var workingDays = Utilities.GetAsObjectJSON<string[]>(root, "WorkingDays");
            _week = new Dictionary<string, WorkingDay>();
            foreach (var day in workingDays) _week.Add(day, new WorkingDay(root));
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
            var workingDays = Utilities.GetAsObjectJSON<string[]>(root, "WorkingDays");
            var specialtySubjects = Utilities.GetAsObjectJSON<string[]>(root, string.Concat(specialty, "_subjects"));
            var specialtyGroups = Utilities.GetAsObjectJSON<string[]>(root, string.Concat(specialty, "_groups"));

            foreach (var subject in specialtySubjects)
            {
                foreach (var group in specialtyGroups)
                    _week[chooseRandomDay(workingDays, root)]
                        .FillForDay(@group, subject, Utilities.LessonType.Practice, root); //for practices
                var choosedDayLecture = Utilities.ChooseRandomly(0, workingDays.Length);
                _week[chooseRandomDay(workingDays, root)].FillForDay(string.Join(", ", specialtyGroups), subject,
                    Utilities.LessonType.Lecture, root); //for lectures
            }
        }

        private string chooseRandomDay(string[] workingDays, JsonElement root)
        {
            while (workingDays.Length != 0)
            {
                var choosedDayNum = Utilities.ChooseRandomly(0, workingDays.Length);
                var choosedDay = workingDays[choosedDayNum];
                if (!_week[choosedDay].IsFull()) return choosedDay;
                workingDays = workingDays.Where(x => x != choosedDay).ToArray();
            }

            var extra = "Extra lesson (Didn't fit in the working week)";
            if (!_extraDay)
            {
                _week.Add(extra, new WorkingDay(root));
                _extraDay = true;
            }

            return extra;
        }
    }
}