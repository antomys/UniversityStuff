using System.Collections.Generic;
using System.Text.Json;

namespace TimetableCSP
{
    internal class WorkingDay
    {
        public Dictionary<string, Lesson> _day;


        public WorkingDay(JsonElement root)
        {
            var workingHours = Utilities.GetAsObjectJSON<string[]>(root, "Lessons_time");
            _day = new Dictionary<string, Lesson>();
            foreach (var hours in workingHours) _day.Add(hours, new Lesson());
        }

        public bool IsFull()
        {
            foreach (var hour in _day)
                if (hour.Value.IsFree)
                    return false;
            return true;
        }

        public void FillForDay(string group, string subject, Utilities.LessonType type, JsonElement root)
        {
            var workingHours = Utilities.GetAsObjectJSON<string[]>(root, "Lessons_time");
            var successfullyAdded = false;
            while (!successfullyAdded)
            {
                var choosedHour = Utilities.ChooseRandomly(0, workingHours.Length);
                if (_day[workingHours[choosedHour]].IsFree)
                {
                    _day[workingHours[choosedHour]].FillForHour(group, subject, type, root);
                    successfullyAdded = true;
                }
            }
        }
    }
}