namespace Scheduling.Entities
{
    internal class Room
    {
        public Room(int _Id, string _Name, int _Capacity)
        {
            Id = _Id;
            Name = _Name;
            Capacity = _Capacity;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}