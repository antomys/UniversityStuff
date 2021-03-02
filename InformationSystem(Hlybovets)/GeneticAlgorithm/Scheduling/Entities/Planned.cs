namespace Scheduling.Entities
{
    internal class Planned
    {
        public Planned(TimeSlot _timeSlot, Subject _subject, Room _room, Group _group, Teacher _teacher)
        {
            timeSlot = _timeSlot;
            subject = _subject;
            room = _room;
            group = _group;
            teacher = _teacher;
        }

        public int Id { get; set; }
        public TimeSlot timeSlot { get; set; }
        public Subject subject { get; set; }
        public Room room { get; set; }
        public Group group { get; set; }
        public Teacher teacher { get; set; }
    }
}