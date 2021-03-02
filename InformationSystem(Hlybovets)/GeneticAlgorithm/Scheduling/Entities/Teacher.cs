namespace Scheduling
{
    internal class Teacher
    {
        public Teacher(int _Id, string _Name)
        {
            Id = _Id;
            Name = _Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}