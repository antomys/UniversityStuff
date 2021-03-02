using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static TimetableCSP.Timetable;
using static TimetableCSP.Utilities;

namespace TimetableCSP
{
    internal class DomainSet
    {
        //static data and limitations
        private static Dictionary<string, string> _subjectLecturer;
        private static List<int> _lectureAudiences;
        private static DomainSet _fullDomain;


        //domains

        //actual value

        public DomainSet(JsonElement element)
        {
            Days = new List<string>(GetAsObjectJSON<string[]>(element, "WorkingDays"));
            Times = new List<string>(GetAsObjectJSON<string[]>(element, "Lessons_time"));
            Audiences = new List<int>(GetAsObjectJSON<int[]>(element, "Audience"));
            Teachers = new List<string>(GetAsObjectJSON<string[]>(element, "Teacher"));

            Value = new Value();
        }

        public DomainSet(JsonElement element, string subject, LessonType type, ValuePickingHeuristic heuristic,
            FilteringHeuristic filtering) : this(element)
        {
            CutOffStaticLimitations(subject, type);

            if (heuristic != ValuePickingHeuristic.NONE || filtering != FilteringHeuristic.NONE) FillPossibleValues();
        }


        public Value Value { get; set; }


        public List<string> Days { get; set; }

        public List<string> Times { get; set; }

        public List<int> Audiences { get; set; }

        public List<string> Teachers { get; set; }

        public List<Value> PossibleValues { get; set; }


        public static void LoadStaticLimitations(JsonElement element)
        {
            _subjectLecturer = GetAsObjectJSON(element, "Subject_lecturer", "Subject");
            _lectureAudiences = new List<int>(GetAsObjectJSON<int[]>(element, "AudienceForLectures"));
            _fullDomain = new DomainSet(element);
        }

        public int PossibleDomainVariationsNumber()
        {
            return Days.Count * Times.Count * Audiences.Count * Teachers.Count;
        }

        public bool OutOfDomain()
        {
            return Days.Count == 0 && Times.Count == 0 && Audiences.Count == 0 && Teachers.Count == 0;
        }

        public bool TriedWholeDomain(ValuePickingHeuristic heuristic, FilteringHeuristic filtering) //TO DO: reduce code
        {
            if (filtering != FilteringHeuristic.NONE)
            {
                if (PossibleValues.Count == 0)
                {
                    FillPossibleValues();
                    return true;
                }

                return false;
            }

            switch (heuristic)
            {
                case ValuePickingHeuristic.LCV:
                    if (PossibleValues.Count == 0)
                    {
                        FillPossibleValues();
                        return true;
                    }

                    return false;
                default:
                    var lastDayTime = Value.DayValue == Days.Last() && Value.TimeValue == Times.Last();
                    return lastDayTime && Value.AudienceValue == Audiences.Last() &&
                           Value.TeacherValue == Teachers.Last();
            }
        }

        public void InitValue()
        {
            Value.DayValue = Days.First();
            Value.TimeValue = Times.First();
            Value.AudienceValue = Audiences.First();
            Value.TeacherValue = Teachers.First();
            Value.Empty = false;
        }

        public bool CanConstruct(Value val)
        {
            return Days.Contains(val.DayValue) && Times.Contains(val.TimeValue) &&
                   Teachers.Contains(val.TeacherValue) && Audiences.Contains(val.AudienceValue);
        }

        public void NextValue(ValuePickingHeuristic heuristic, FilteringHeuristic filtering,
            Dictionary<Variable, DomainSet> emptyVars)
        {
            switch (heuristic)
            {
                case ValuePickingHeuristic.LCV:
                    LCVPicking(emptyVars);
                    break;
                default:
                    StepByStepPicking(filtering);
                    break;
            }

            switch (filtering)
            {
                case FilteringHeuristic.ForwardChecking:
                    foreach (var k in new List<Variable>(emptyVars.Keys))
                        emptyVars[k].PossibleValues = emptyVars[k].PossibleValues.Where(x => !x.Equals(Value)).ToList();
                    break;
            }
        }

        private void LCVPicking(Dictionary<Variable, DomainSet> emptyVars) //TO DO optimize and check for bugs
        {
            if (PossibleValues.Count == 1)
            {
                Value = new Value(PossibleValues.ElementAt(0));
                Value.Empty = false;
                PossibleValues.Remove(Value);
            }
            else
            {
                var intersectCount = -1;
                foreach (var p in PossibleValues)
                {
                    var newIntersectCount = 0;
                    foreach (var k in emptyVars) newIntersectCount += k.Value.PossibleValues.Count(el => el.Equals(p));

                    if (intersectCount == -1)
                    {
                        Value = new Value(p);
                        intersectCount = newIntersectCount;
                    }
                    else if (newIntersectCount < intersectCount)
                    {
                        Value = new Value(p);
                        Value.Empty = false;
                        intersectCount = newIntersectCount;
                    }
                    else if (newIntersectCount == 0)
                    {
                        Value = new Value(p);
                    }
                }

                PossibleValues.Remove(Value);
            }
        }

        private void
            StepByStepPicking(
                FilteringHeuristic filtering) //choosing next value (tries all possible variations of parametrs, if doesnt fit - backtracking will solve the issue)
        {
            if (filtering == FilteringHeuristic.NONE)
            {
                if (Value.TeacherValue != Teachers.Last())
                {
                    Value.TeacherValue = Teachers.ElementAt(Teachers.IndexOf(Value.TeacherValue) + 1);
                }
                else if (Value.AudienceValue != Audiences.Last())
                {
                    Value.TeacherValue = Teachers.First(); // resetting previous data element
                    Value.AudienceValue = Audiences.ElementAt(Audiences.IndexOf(Value.AudienceValue) + 1);
                }
                else if (Value.TimeValue != Times.Last())
                {
                    Value.AudienceValue = Audiences.First(); // resetting previous data element
                    Value.TimeValue = Times.ElementAt(Times.IndexOf(Value.TimeValue) + 1);
                }
                else if (Value.DayValue != Days.Last())
                {
                    Value.TimeValue = Times.First(); // resetting previous data element
                    Value.DayValue = Days.ElementAt(Days.IndexOf(Value.DayValue) + 1);
                }
            }
            else
            {
                Value = new Value(PossibleValues.First());
                PossibleValues.RemoveAt(0);
            }
        }

        public void SetDomainDefault(string subject, LessonType type) //set default domain (all possible values)
        {
            Days = new List<string>(_fullDomain.Days);
            Times = new List<string>(_fullDomain.Times);
            Audiences = new List<int>(_fullDomain.Audiences);
            Teachers = new List<string>(_fullDomain.Teachers);
            CutOffStaticLimitations(subject, type);
        }

        private void FillPossibleValues()
        {
            PossibleValues = new List<Value>();
            InitValue();
            PossibleValues.Add(new Value(Value));
            while (!TriedWholeDomain(ValuePickingHeuristic.NONE, FilteringHeuristic.NONE))
            {
                StepByStepPicking(FilteringHeuristic.NONE);
                PossibleValues.Add(new Value(Value));
            }

            Value.Empty = true;
        }

        private void CutOffStaticLimitations(string subject, LessonType type)
        {
            if (type == LessonType.Lecture)
            {
                Audiences = Audiences.Where(x => _lectureAudiences.Contains(x)).ToList();
                Teachers = new List<string> {_subjectLecturer[subject]};
            }
        }

        public override string ToString()
        {
            return string.Concat(Value.DayValue, " , ", Value.TimeValue, " , ", Value.AudienceValue, " , ",
                Value.TeacherValue);
        }
    }
}