namespace TimetableCSP
{
    internal class CspMachine
    {
        private string _dataFilename;


        public CspMachine(string dataFilename)
        {
            _dataFilename = dataFilename;
            Timetable = new Timetable(dataFilename);
        }

        public Timetable Timetable { get; }
    }
}