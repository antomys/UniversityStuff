using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace PhaticDialogue
{
    public class Database : IDatabase
    {
        public AnswerTypes answerTypes { get; set; }
        
        public Dictionary<Persons,List<string>> answers { get; set; }

        private static void IsExist()
        {
            if (File.Exists("database.bin")) return;
            using var fs = File.Create("database.bin");
            fs.Close();
        }

        public Database(AnswerTypes answerType, string answer, Persons person)
        {
            answerTypes = answerType;
            answers = new Dictionary<Persons, List<string>> {{person, new List<string> {answer}}};
        }

        public static void AddData(AnswerTypes answerType, string answer)
        {
            IsExist();
            var file = File.ReadAllText("database.bin");
            var person = CheckPerson(answer);
            if (file.Length == 0)
            {
                var list = new List<Database>() {new(answerType, answer,person)};
                var serialize = JsonConvert.SerializeObject(list,Formatting.Indented);
                File.WriteAllText("database.bin",serialize);
            }
            else
            {
                var deserialized = JsonConvert.DeserializeObject<List<Database>>(file);
                //if (deserialized.Any(value => value.answers.Contains(answer)))
                if (deserialized.Any(value => value.answers.Values.Any(x => x.Contains(answer))))
                {
                    return;
                }

                if (deserialized.Any(x => x.answerTypes.Equals(answerType)))
                {
                    //
                    //foreach (var database in deserialized.Where(x=>x.answerTypes.Equals(answerType)))
                    var database = deserialized.First(x => x.answerTypes.Equals(answerType));
                    {
                        if (database.answers.Values.Any(x=>x.Contains(answer)))
                            return;
                        if(!database.answers.ContainsKey(person))
                            database.answers.Add(person,new List<string>{answer});
                        else
                        {
                            database.answers[person].Add(answer);
                        }
                    }
                }
                else
                {
                    deserialized.Add(new Database(answerType,answer,person));
                }
                var serializeAgain = JsonConvert.SerializeObject(deserialized,Formatting.Indented);
                File.WriteAllText("database.bin",serializeAgain);
            }
        }

        private static Persons CheckPerson(string answer)
        {
            var chunks = answer.Split(" ");
            if(chunks.Any(x=> x.Equals(Enum.GetName(Persons.i)) || x.Equals(Enum.GetName(Persons.me))))
                return Persons.me;
            return chunks.Any(x => x.Equals(Enum.GetName(Persons.you))) ? Persons.you : Persons.gen;
        }

        public static string PerformDialogue(string answer)
        {
            if (string.IsNullOrEmpty(answer))
            {
                return "Are you gonna ignore me forever???";
            }
            var deserializedDatabase = JsonConvert.DeserializeObject<List<Database>>(File.ReadAllText("database.bin"));
            var answerType = ManageAnswerType(answer, deserializedDatabase);
            
            int total = 0;
            if (answerType != null && answerType.Item1.Equals(AnswerTypes.Hello))
            {
                total = deserializedDatabase
                    .Where(x => x.answerTypes
                        .Equals(answerType.Item1))
                    .Select(x => x.answers.Values
                        .Sum(collection => collection.Count))
                    .FirstOrDefault();
            }

            var ztotal = deserializedDatabase.Where(x =>
                x.answerTypes.Equals(answerType.Item1)).Select(x=>x.answers.Keys.Where(x=>x == answerType.Item2)).ToList();

            var result = deserializedDatabase.Where(x => x.answerTypes.Equals(answerType.Item1) && x.answers.Keys.Contains(answerType.Item2)).Select(x=> x.answers).ToList();
            //return  result[0].ElementAt(rand.Next(result[0].Count));
            return null;
        }

        private static Tuple<AnswerTypes, Persons> ManageAnswerType(string answer,
            IEnumerable<Database> deserializedDatabase)
        {
            var chunks = answer.Split(" ");
            foreach (var database in deserializedDatabase)
            {
                if(database.answerTypes == AnswerTypes.Question)
                    foreach (var (key, value) in database.answers)
                    {
                        if (answer.Contains("do") || Enum.GetNames(typeof(QuestionWords)).Any(x => chunks[0].Equals(x)))
                        {
                            return Tuple.Create(AnswerTypes.AnswerToQuestion, key);
                        }
                    }

                //if (!database.answers.Values.Any(x => x.Contains(answer))) continue;
                if (database.answers.Values.All(x => x.FindIndex(y => y.Equals(answer, StringComparison.OrdinalIgnoreCase)) == -1))
                    continue;
                var getPerson = (from t in database.answers
                    where t.Value.Contains(answer)
                    select t.Key).FirstOrDefault();
                return Tuple.Create(database.answerTypes, getPerson);
            }
            var randomized = new Random().Next(1, 3); 
            GetUnknownAnswerToFile(chunks);
            return randomized switch
        {
            1 => Tuple.Create(AnswerTypes.Question, Persons.gen),
            _ => Tuple.Create(AnswerTypes.GeneralAnswer, Persons.gen)
        };
        }

        private static void GetUnknownAnswerToFile(string[] chunks)
        {
            Console.WriteLine("Wow! I didn't know that.\n Thanks for making me smarter");
            AddData(
                Enum.GetNames(typeof(QuestionWords)).Any(x => chunks[0].Equals(x))
                    ? AnswerTypes.Question
                    : AnswerTypes.GeneralAnswer, string.Join(" ", chunks));
        }
    }
}