using System;
using System.Collections.Generic;
using System.Text;

namespace mmsLab4
{
    class Queue
    {
        int Nmax;//максимальна довжина черги
        public int Ncur;//теперішня довжина черги
        public bool Enabled;//false - якщо черга заповнена
        public int id;

        public Queue(int n, int _id)
        {
            Nmax = n;
            Ncur = 0;
            Enabled = true;
            id = _id;
        }

        public void Add()
        {
            ++Ncur;
            if (Ncur == Nmax)
            {
                Enabled = false;
            }
        }

        public void Remove()
        {
            --Ncur;
            if (Ncur < Nmax)
            {
                Enabled = true;
            }
        }
    }
}
