using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PhaticDialogue
{
    public class Database : IDatabase
    {
        static Random _rand = new Random();
        public AnswerTypes AnswerTypes { get; set; }
        
        public Dictionary<Persons,List<string>> Answers { get; set; }

        private static void IsExist()
        {
            if (File.Exists("database.bin")) return;
            using var fs = File.Create("database.bin");
            fs.Close();
        }

        public Database(AnswerTypes answerType, string answer, Persons person)
        {
            AnswerTypes = answerType;
            Answers = new Dictionary<Persons, List<string>> {{person, new List<string> {answer}}};
        }

        private static void AddData(AnswerTypes answerType, string answer)
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
                if (deserialized.Any(value => value.Answers.Values.Any(x => x.Contains(answer))))
                {
                    return;
                }

                if (deserialized.Any(x => x.AnswerTypes.Equals(answerType)))
                {
                    //
                    //foreach (var database in deserialized.Where(x=>x.answerTypes.Equals(answerType)))
                    var database = deserialized.First(x => x.AnswerTypes.Equals(answerType));
                    {
                        if (database.Answers.Values.Any(x=>x.Contains(answer)))
                            return;
                        if(!database.Answers.ContainsKey(person))
                            database.Answers.Add(person,new List<string>{answer});
                        else
                        {
                            database.Answers[person].Add(answer);
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
            if(chunks.Any(x=> x.Equals(Enum.GetName(Persons.I)) || x.Equals(Enum.GetName(Persons.Me))))
                return Persons.Me;
            return chunks.Any(x => x.Equals(Enum.GetName(Persons.You))) ? Persons.You : Persons.Gen;
        }

        public static string PerformDialogue(string answer)
        {
            if (string.IsNullOrEmpty(answer))
            {
                return "Are you gonna ignore me forever???";
            }
            var deserializedDatabase = JsonConvert.DeserializeObject<List<Database>>(File.ReadAllText("database.bin"));
            var answerType = ManageAnswerType(answer, deserializedDatabase);
            
            if (answerType != null && answerType.Item1.Equals(AnswerTypes.Hello))
            {
                return GetRandomValue(deserializedDatabase
                    .Where(x => x.AnswerTypes
                        .Equals(answerType.Item1))
                    .Select(x => x.Answers).ToList());
                /*var returned = deserializedDatabase
                    .Where(x => x.answerTypes
                        .Equals(answerType.Item1))
                    .Select(x => x.answers).ToList();*/
            }
            if (answerType != null && answerType.Item1.Equals(AnswerTypes.Bye))
            {
                return GetRandomValue(deserializedDatabase
                    .Where(x => x.AnswerTypes
                        .Equals(AnswerTypes.Bye))
                    .Select(x => x.Answers).ToList());
            }
            var result = deserializedDatabase
                .Where(x => x.AnswerTypes.Equals(answerType.Item1) 
                            && x.Answers.Keys.Contains(answerType.Item2))
                .Select(x=> x.Answers).ToList();
            var output = GetRandomValue(result);
            if (output == null)
            {
                return GetRandomValue(deserializedDatabase
                    .Where(x => x.AnswerTypes
                        .Equals(AnswerTypes.Question))
                    .Select(x => x.Answers).ToList());
            }
            return output;
        }

        private static string GetRandomValue(IReadOnlyList<Dictionary<Persons, List<string>>> result)
        {
            var rand = new Random();
            try
            {
                foreach (var (_, value) in result[rand.Next(result.Count)])
                {
                    var fileIndex = rand.Next(value.Count);
                    return value[fileIndex];
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        private static Tuple<AnswerTypes, Persons> ManageAnswerType(string answer,
            IEnumerable<Database> deserializedDatabase)
        {
            var chunks = answer.Split(" ");
            var personType = CheckPerson(answer);
            foreach (var database in deserializedDatabase)
            {
                if(database.AnswerTypes == AnswerTypes.Question)
                    foreach (var (key, value) in database.Answers)
                    {
                        
                        if ((answer.Contains("do") ||
                             Enum.GetNames(typeof(QuestionWords)).Any(x => chunks[0].Equals(x))) &&
                            key.Equals(personType))
                        {
                            return Tuple.Create(AnswerTypes.AnswerToQuestion, key);
                        }
                    }

                //if (!database.answers.Values.Any(x => x.Contains(answer))) continue;
                if (database.Answers.Values.All(x => x.FindIndex(y => y.Equals(answer, StringComparison.OrdinalIgnoreCase)) == -1))
                    continue;
                var getPerson = (from t in database.Answers
                    where t.Value.Contains(answer)
                    select t.Key).FirstOrDefault();
                return Tuple.Create(database.AnswerTypes, getPerson);
            }
            var randomized = new Random().Next(1, 3); 
            GetUnknownAnswerToFile(chunks);
            return randomized switch
        {
            1 => Tuple.Create(AnswerTypes.Question, Persons.Gen),
            _ => Tuple.Create(AnswerTypes.GeneralAnswer, Persons.Gen)
        };
        }

        private static void GetUnknownAnswerToFile(string[] chunks)
        {
            Console.WriteLine("Wow! I didn't know that.\n Thanks for making me smarter");
            if (chunks.Contains("bye"))
            {
                AddData(AnswerTypes.Bye,string.Join(" ",chunks));
                return;
            }
            AddData(
                Enum.GetNames(typeof(QuestionWords)).Any(x => chunks[0].Equals(x))
                    ? AnswerTypes.Question
                    : AnswerTypes.GeneralAnswer, string.Join(" ", chunks));
            
        }
    }
}