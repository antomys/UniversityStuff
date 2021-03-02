namespace Scheduling.Entities
{
    internal class Group
    {
        public Group(int _Id, string _Name, int _Size)
        {
            Id = _Id;
            Name = _Name;
            Size = _Size;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}