namespace Scheduling.Entities
{
    internal class TimeSlot
    {
        public TimeSlot(int _Id, string _Day, string _Time)
        {
            Id = _Id;
            Day = _Day;
            Time = _Time;
        }

        public int Id { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
    }
}