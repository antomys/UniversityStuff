using System.Collections.Generic;

namespace Scheduling.Entities
{
    internal class Data
    {
        public Data()
        {
            Groups = new List<Group>();
            Rooms = new List<Room>();
            Subjects = new List<Subject>();
            Teachers = new List<Teacher>();
            TimeSlots = new List<TimeSlot>();

            Groups.Add(new Group(1, "TK", 15));
            Groups.Add(new Group(2, "TTP", 30));
            Groups.Add(new Group(3, "MI", 30));

            Subjects.Add(new Subject(1, "OOP-L", 1, false));
            Subjects.Add(new Subject(2, "OOP-P", 2, true));
            Subjects.Add(new Subject(3, "DM-L", 2, false));
            Subjects.Add(new Subject(4, "DM-P", 2, true));
            Subjects.Add(new Subject(5, "PARCS-L", 3, false));
            Subjects.Add(new Subject(6, "PARCS-P", 2, true));

            Rooms.Add(new Room(1, "228", 15));
            Rooms.Add(new Room(2, "322", 15));
            Rooms.Add(new Room(3, "69", 30));
            Rooms.Add(new Room(4, "96", 30));

            Teachers.Add(new Teacher(1, "T1"));
            Teachers.Add(new Teacher(2, "T2"));
            Teachers.Add(new Teacher(3, "T3"));
            Teachers.Add(new Teacher(4, "T4"));

            TimeSlots.Add(new TimeSlot(1, "Mon", "8:40-10:15"));
            TimeSlots.Add(new TimeSlot(2, "Mon", "10:35-12:10"));
            TimeSlots.Add(new TimeSlot(3, "Mon", "12:20-13:55"));
            TimeSlots.Add(new TimeSlot(4, "Mon", "14:05-15:45"));
            TimeSlots.Add(new TimeSlot(5, "Tue", "8:40-10:15"));
            TimeSlots.Add(new TimeSlot(6, "Tue", "10:35-12:10"));
            TimeSlots.Add(new TimeSlot(7, "Tue", "12:20-13:55"));
            TimeSlots.Add(new TimeSlot(8, "Tue", "14:05-15:45"));
            TimeSlots.Add(new TimeSlot(9, "Wed", "8:40-10:15"));
            TimeSlots.Add(new TimeSlot(10, "Wed", "10:35-12:10"));
            TimeSlots.Add(new TimeSlot(11, "Wed", "12:20-13:55"));
            TimeSlots.Add(new TimeSlot(12, "Wed", "14:05-15:45"));
            TimeSlots.Add(new TimeSlot(13, "Thu", "8:40-10:15"));
            TimeSlots.Add(new TimeSlot(14, "Thu", "10:35-12:10"));
            TimeSlots.Add(new TimeSlot(15, "Thu", "12:20-13:55"));
            TimeSlots.Add(new TimeSlot(16, "Thu", "14:05-15:45"));
        }

        public List<Group> Groups { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }
}