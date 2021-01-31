using System;
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
                if(answer.ToLower().Equals("bye"))
                    return;
            }
        }
    }
}
