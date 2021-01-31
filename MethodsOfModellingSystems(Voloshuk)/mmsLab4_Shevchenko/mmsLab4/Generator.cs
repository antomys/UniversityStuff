using System;
using System.Collections.Generic;
using System.Text;

namespace mmsLab4
{
    class Generator
    {
        public double timeNext;//час створення наступної вимоги
        double dt;//інтервал створення вимог
        public int id;

        public Generator(double a, int _id)
        {
            dt = a;
            timeNext = 0;
            id = _id;
        }
        public Vimoga GenerateNext()
        {
            Random rand = new Random();
            Vimoga v = new Vimoga(timeNext - (Math.Log(1 - rand.NextDouble()) % dt));
            timeNext += dt;
            return v;
        }
    }
}
