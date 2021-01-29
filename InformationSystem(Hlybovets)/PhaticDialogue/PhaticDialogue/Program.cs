using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PhaticDialogue
{
    internal static class Program
    {
        private static void Main()
        {
            var r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            while (true)
            {
                Console.Write("You: ");
                var input = Console.ReadLine()?.ToLower();
                var fixedInput = r.Replace(input!, string.Empty);
                var answer = Database.PerformDialogue(fixedInput);
                Console.Write($"Bot: {answer}\n");
            }
            /*var strings = File.ReadAllLines("greetings2.txt");
            foreach (var VARIABLE in strings)
            {
                Database.AddData(AnswerTypes.Hello,VARIABLE.Trim());
            }*/
        }
    }
}
