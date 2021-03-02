namespace TimetableCSP
{
    internal class Value
    {
        public Value()
        {
        }

        public Value(Value value)
        {
            DayValue = value.DayValue;
            TimeValue = value.TimeValue;
            AudienceValue = value.AudienceValue;
            TeacherValue = value.TeacherValue;
            Empty = false;
        }

        public bool Empty { get; set; } = true;

        public string DayValue { get; set; }

        public string TimeValue { get; set; }

        public int AudienceValue { get; set; }

        public string TeacherValue { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Value;

            if (item == null) return false;
            var sameDateTime = DayValue == item.DayValue && TimeValue == item.TimeValue;
            return sameDateTime && AudienceValue == item.AudienceValue && sameDateTime &&
                   TeacherValue == item.TeacherValue;
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }
    }
}