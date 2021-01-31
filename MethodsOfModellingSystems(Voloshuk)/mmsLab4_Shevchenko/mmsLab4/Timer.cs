using System;
using System.Collections.Generic;
using System.Text;

namespace mmsLab4
{
    class Timer
    {
        double stopTime;//час зупинки
        public double curTime;//теперішній час
        double dt;//інтервал
        public Timer(double maxTime, double period)
        {
            stopTime = 0;
            stopTime = maxTime;
            dt = period;
        }

        public bool Next()
        {
            curTime += dt;
            if (curTime > stopTime) return false;
            return true;
        }
    }
}
