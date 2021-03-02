using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;

namespace TimetableCSP
{
    internal class Utilities
    {
        public enum LessonType
        {
            Lecture,
            Practice
        }


        public static int ChooseRandomly(int from, int to)
        {
            using (var rg = new RNGCryptoServiceProvider())
            {
                var rno = new byte[5];
                rg.GetBytes(rno);

                var n = from + Math.Abs(BitConverter.ToInt32(rno, 0)) % to;
                return n;
            }
        }

        public static T GetAsObjectJSON<T>(JsonElement element, string property)
        {
            return JsonSerializer.Deserialize<T>(element.GetProperty(property).GetRawText());
        }

        public static Dictionary<string, string> GetAsObjectJSON(JsonElement element, string property,
            string ofProperties)
        {
            var props = GetAsObjectJSON<string[]>(element, ofProperties);
            var from = element.GetProperty(property);
            var result = new Dictionary<string, string>();

            foreach (var prop in props)
                result[prop] = JsonSerializer.Deserialize<string>(@from.GetProperty(prop).GetRawText());

            return result;
        }

        public static List<Variable> Hard1Sequence() // 10378 steps, 5183 back steps using pure backtracking
        {
            var hardSequence = new List<Variable>();
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "TK1"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "TK1"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "MI1"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "TK2"));
            hardSequence.Add(new Variable(LessonType.Lecture, "EEPM", "MI1, MI2"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "MI2"));
            hardSequence.Add(new Variable(LessonType.Lecture, "Methods of system modelling", "MI1, MI2"));
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "MI1"));
            hardSequence.Add(new Variable(LessonType.Lecture, "EEPM", "TK1, TK2"));
            hardSequence.Add(new Variable(LessonType.Lecture, "Methods of system modelling", "TK1, TK2"));
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "MI2"));
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "TK2"));
            return hardSequence;
        }

        public static List<Variable> Hard2Sequence() // 10378 steps, 5183 back steps using pure backtracking
        {
            var hardSequence = new List<Variable>();
            hardSequence.Add(new Variable(LessonType.Lecture, "Methods of system modelling", "TK1, TK2"));
            hardSequence.Add(new Variable(LessonType.Lecture, "EEPM", "MI1, MI2"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "MI2"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "TK1"));
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "TK2"));
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "MI2"));
            hardSequence.Add(new Variable(LessonType.Lecture, "EEPM", "TK1, TK2"));
            hardSequence.Add(new Variable(LessonType.Lecture, "Methods of system modelling", "MI1, MI2"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "TK2"));
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "TK1"));
            hardSequence.Add(new Variable(LessonType.Practice, "Methods of system modelling", "MI1"));
            hardSequence.Add(new Variable(LessonType.Practice, "EEPM", "MI1"));
            return hardSequence;
        }
    }
}