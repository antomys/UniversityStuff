using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Entities
{
    class TimeSlot
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        public TimeSlot(int _Id, string _Day, string _Time)
        {
            Id = _Id;
            Day = _Day;
            Time = _Time;
        }
    }
}
