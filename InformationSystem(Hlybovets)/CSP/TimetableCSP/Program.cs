using System;

namespace TimetableCSP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gen = new CspMachine("../../../middle_size_data_backtracking_test.json");
            Console.WriteLine(gen.Timetable.SolveByBacktracking());
        }
    }
}