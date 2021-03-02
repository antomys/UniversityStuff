using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TimetableGeneticGeneration
{
    class WorkingDay
    {
        public Dictionary<String, Lesson> _day;

        public bool IsFull()
        {
            foreach (var hour in _day)
            {
                if (hour.Value.IsFree)
                {
                    return false;
                }
            }
            return true;
        }


        public WorkingDay(JsonElement root)
        {
            string[] workingHours = Utilities.GetAsObjectJSON<string[]>(root, "Lessons_time");
            _day = new Dictionary<string, Lesson>();
            foreach (var hours in workingHours)
            {
                _day.Add(hours, new Lesson());
            }
        }

        public void FillForDay(String group, String subject, Utilities.LessonType type, JsonElement root)
        {
            string[] workingHours = Utilities.GetAsObjectJSON<string[]>(root, "Lessons_time");
            bool successfullyAdded = false;
            while (!successfullyAdded)
            {
                int choosedHour = Utilities.ChooseRandomly(0, workingHours.Length);
                if (_day[workingHours[choosedHour]].IsFree)
                {
                    _day[workingHours[choosedHour]].FillForHour(group, subject, type, root);
                    successfullyAdded = true;
                }
            }

        }
    }
}
