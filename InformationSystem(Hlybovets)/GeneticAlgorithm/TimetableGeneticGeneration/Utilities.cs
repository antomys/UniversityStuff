using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TimetableGeneticGeneration
{
    class Utilities
    {
        public enum LessonType { Lecture, Practice };

        //-------------static limitations data here ------------------------
        public static int[] _lectureAudiences;
        public static Dictionary<String, List<Lesson>> _requiredLessonsSet;


        public static void LoadLectureAudiences(String dataFilename)
        {
            string text = File.ReadAllText(dataFilename);
            using JsonDocument doc = JsonDocument.Parse(text);
            JsonElement root = doc.RootElement;
            _lectureAudiences = Utilities.GetAsObjectJSON<int[]>(root, "AudienceForLectures");
        }


        public static void LoadRequiredLessonsSet(String dataFilename)
        {
            Chromosome amountSatisfying = new Chromosome(dataFilename);
            _requiredLessonsSet = amountSatisfying.GetAllLessonsSet();
        }

        public static int ChooseRandomly(int from, int to)
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);

                int n = from + (Math.Abs(BitConverter.ToInt32(rno, 0)) % to);
                return n;
            }
        }

        public static T GetAsObjectJSON<T>(JsonElement element, String property)
        {
            return JsonSerializer.Deserialize<T>(element.GetProperty(property).GetRawText());
        }

        public static Dictionary<String, String> GetAsObjectJSON(JsonElement element, String property, String ofProperties)
        {
            string[] props = GetAsObjectJSON<string[]>(element, ofProperties);
            JsonElement from = element.GetProperty(property);
            var result = new Dictionary<String, String>();

            foreach(var prop in props)
            {
                result[prop] = JsonSerializer.Deserialize<String>(from.GetProperty(prop).GetRawText());
            }

            return result;
        }

    }
}
