using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static TimetableCSP.Utilities;

namespace TimetableCSP
{
    internal class Timetable
    {
        public enum FilteringHeuristic
        {
            NONE,
            ForwardChecking,
            ConstraintPropagation
        }

        public enum ValuePickingHeuristic
        {
            NONE,
            LCV // least constraining value
        }

        public enum VarPickingHeuristic
        {
            NONE,
            MRV, // Minimum remaining values
            LDH // Largest degree heuristic
        }

        private const bool RANDOM_VARS_ORDER = false;

        private const VarPickingHeuristic _pickingVarHeuristic = VarPickingHeuristic.NONE;
        private const ValuePickingHeuristic _pickingValueHeuristic = ValuePickingHeuristic.NONE;
        private const FilteringHeuristic _filteringHeuristic = FilteringHeuristic.ForwardChecking;
        private int _allSteps;
        private int _backSteps;
        private readonly List<int> _sequenceNumbers;

        private readonly List<Variable> _staticVarSequence = Hard1Sequence();

        private readonly Dictionary<Variable, DomainSet> _variables;

        private JsonElement root;


        public Timetable(string dataFilename)
        {
            _variables = new Dictionary<Variable, DomainSet>();
            _sequenceNumbers = new List<int>();

            GenerateeTimetable(dataFilename);
            DomainSet.LoadStaticLimitations(root);
            FillVariables();
        }

        public override string ToString()
        {
            //String toString = "";
            //foreach(var spec in _timetableRandom)
            //{
            //    toString += String.Concat(spec.Key, " :\n");
            //    foreach (var day in spec.Value._week)
            //    {
            //        toString += String.Concat(" ", day.Key, " :\n");
            //        foreach (var hours in day.Value._day)
            //        {
            //            toString += String.Concat("  ", hours.Key, " : ", hours.Value.ToString(), "\n");
            //        }
            //    }
            //}
            return "";
        }


        private void GenerateeTimetable(string dataFilename)
        {
            if (File.Exists(dataFilename))
            {
                var text = File.ReadAllText(dataFilename);
                using var doc = JsonDocument.Parse(text);
                root = doc.RootElement.Clone();
            }
            else
            {
                throw new FileNotFoundException(string.Concat(dataFilename, " doesn't exist!"));
            }
        }


        public void FillVariables() //required for checking amount of specific lectures/practices
        {
            var allLessons = _staticVarSequence;
            if (_staticVarSequence == null)
            {
                allLessons = new List<Variable>();
                var specialties = GetAsObjectJSON<string[]>(root, "Specialty");
                for (var i = 0; i < specialties.Length; i++)
                {
                    var groups = GetAsObjectJSON<string[]>(root, string.Concat(specialties[i], "_", "groups"));
                    for (var j = 0; j < groups.Length + 1; j++)
                    {
                        var group = j == groups.Length ? string.Join(", ", groups) : groups[j];
                        var type = j == groups.Length ? LessonType.Lecture : LessonType.Practice;
                        var subjects = GetAsObjectJSON<string[]>(root, string.Concat(specialties[i], "_", "subjects"));
                        for (var k = 0; k < subjects.Length; k++)
                            allLessons.Add(new Variable(type, subjects[k], group));
                    }
                }
            }


            var n = allLessons.Count;
            for (var i = 0; i < n; i++)
            {
                var number = 0;
                if (RANDOM_VARS_ORDER) number = ChooseRandomly(0, allLessons.Count);
                _variables.Add(allLessons[number],
                    new DomainSet(root, allLessons[number].Subject, allLessons[number].LessonType,
                        _pickingValueHeuristic, _filteringHeuristic));
                allLessons.RemoveAt(number);
            }
        }

        public bool SolveByBacktracking()
        {
            int counter = 0, end = _variables.Count;
            var couldSolve = true;
            while (counter != end)
            {
                ++_allSteps;
                var pair = _variables.ElementAt(HeuristicVariablePick(counter));
                if (pair.Value.Value.Empty)
                {
                    pair.Value.InitValue();
                }
                else
                {
                    if (pair.Value.TriedWholeDomain(_pickingValueHeuristic, _filteringHeuristic))
                    {
                        if (counter == 0) // in case of cant create timetable without conflicts
                        {
                            couldSolve = false;
                            break;
                        }

                        pair.Value.Value.Empty = true;
                        --counter;
                        ++_backSteps;
                        continue;
                    }

                    pair.Value.NextValue(_pickingValueHeuristic, _filteringHeuristic,
                        _variables.Where(p => p.Value.Value.Empty).ToDictionary(e => e.Key, e => e.Value));
                }

                while (true)
                {
                    if (CheckIfFeets(pair.Value.Value, pair.Key.Group,
                        _variables.Where(x => x.Key != pair.Key).ToDictionary(p => p.Key, p => p.Value)))
                    {
                        ++counter;
                        break;
                    }

                    if (pair.Value.TriedWholeDomain(_pickingValueHeuristic, _filteringHeuristic))
                    {
                        pair.Value.Value.Empty = true;
                        --counter;
                        ++_backSteps;
                        break;
                    }

                    pair.Value.NextValue(_pickingValueHeuristic, _filteringHeuristic,
                        _variables.Where(p => p.Value.Value.Empty).ToDictionary(e => e.Key, e => e.Value));
                }
            }

            Console.WriteLine(StringFromVars());
            return couldSolve;
        }

        private bool
            CheckIfFeets(Value value, string group,
                Dictionary<Variable, DomainSet> other) //return: 0 - no conflicts, 1 - teacher conflict, 2 - audience conflict  
        {
            other = other.Where(x => x.Value.Value.DayValue == value.DayValue &&
                                     x.Value.Value.TimeValue == value.TimeValue &&
                                     !x.Value.Value.Empty).ToDictionary(p => p.Key, p => p.Value);

            if (other.Where(x => x.Value.Value.TeacherValue == value.TeacherValue)
                    .ToDictionary(p => p.Key, p => p.Value).Count != 0 ||
                other.Where(x => x.Value.Value.AudienceValue == value.AudienceValue)
                    .ToDictionary(p => p.Key, p => p.Value).Count != 0 ||
                other.Where(x => x.Key.Group == group || x.Key.Group.Contains(group) ||
                                 group.Contains(x.Key.Group)).ToDictionary(p => p.Key, p => p.Value).Count != 0)
                return false;
            return true;
        }

        private int CountConflictsNumber(DomainSet set, Dictionary<Variable, DomainSet> other)
        {
            var conflictsN = 0;
            other = other.Where(x => !x.Value.Value.Empty).ToDictionary(p => p.Key, p => p.Value);
            foreach (var p in other)
                if (set.CanConstruct(p.Value.Value))
                    ++conflictsN;
            return conflictsN;
        }


        private int HeuristicVariablePick(int counter)
        {
            switch (_pickingVarHeuristic)
            {
                case VarPickingHeuristic.MRV:
                    return MRVHeuristic(counter);
                case VarPickingHeuristic.LDH:
                    return LDHeuristic(counter);
                default:
                    return counter;
            }
        }

        private int MRVHeuristic(int counter)
        {
            var _emptyVars = _variables.Where(x => x.Value.Value.Empty).ToDictionary(p => p.Key, p => p.Value);
            if (counter >= _sequenceNumbers.Count)
            {
                var minPossiblePair = _emptyVars.First();
                var possibleValues = minPossiblePair.Value.PossibleDomainVariationsNumber() -
                                     CountConflictsNumber(minPossiblePair.Value, _variables);

                foreach (var p in _emptyVars)
                {
                    var newPossibleValues = p.Value.PossibleDomainVariationsNumber() -
                                            CountConflictsNumber(p.Value, _variables);
                    if (newPossibleValues < possibleValues)
                    {
                        possibleValues = newPossibleValues;
                        minPossiblePair = p;
                    }
                }

                _sequenceNumbers.Add(_variables.Keys.ToList().IndexOf(minPossiblePair.Key));
            }

            return _sequenceNumbers.ElementAt(counter);
        }

        private int LDHeuristic(int counter)
        {
            var _emptyVars = _variables.Where(x => x.Value.Value.Empty).ToDictionary(p => p.Key, p => p.Value);
            if (counter >= _sequenceNumbers.Count)
            {
                var minPossiblePair = _emptyVars.First();
                foreach (var p in _emptyVars)
                    if (p.Key.LessonType == LessonType.Lecture ||
                        _emptyVars.Keys.ToList().IndexOf(p.Key) == _emptyVars.Count - 1)
                        minPossiblePair = p;
                _sequenceNumbers.Add(_variables.Keys.ToList().IndexOf(minPossiblePair.Key));
            }

            return _sequenceNumbers.ElementAt(counter);
        }

        private string StringFromVars()
        {
            var res = "";
            foreach (var v in _variables) res = string.Concat(res, v.Key, " : ", v.Value, " \n");
            res = string.Concat(res, "Steps number: ", _allSteps, "\nBack steps done: ", _backSteps, "\n");
            return res;
        }
    }
}