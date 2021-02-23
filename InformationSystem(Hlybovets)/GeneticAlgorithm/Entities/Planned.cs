using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Entities
{
    class Planned
    {
        public int Id { get; set; }
        public TimeSlot timeSlot { get; set; }
        public Subject subject { get; set; }
        public Room room { get; set; }
        public Group group { get; set; }
        public Teacher teacher { get; set; }

        public Planned(TimeSlot _timeSlot, Subject _subject, Room _room, Group _group, Teacher _teacher)
        {
            timeSlot = _timeSlot;
            subject = _subject;
            room = _room;
            group = _group;
            teacher = _teacher;
        }
    }
}
