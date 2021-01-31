using System;
using System.Collections.Generic;
using System.Text;

namespace mmsLab4
{
    public class Vimoga
    {
        public double timeStart;//час, коли з'явилася в ММО
        public double timeNext;//час, коли має змінити стан (просунутися далі в ММО)
        public int status;//теперішнє розташування в ММО

        public Vimoga(double time)
        {
            timeStart = timeNext = time;
            status = 0;
        }

        public void ChangeStatus(double dt, int s)
        {
            timeNext += dt;
            status = s;
        }
    }
}
