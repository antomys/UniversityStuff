using System;
using System.Collections.Generic;
using System.Linq;

namespace TimetableGeneticGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticMachine gen = new GeneticMachine("../../../data.json", 15);
            gen.FindAnswer();
            
        }
    }
}
