using static TimetableCSP.Utilities;

namespace TimetableCSP
{
    internal class Variable
    {
        public Variable(Lesson les)
        {
            LessonType = les.LessonType;
            Subject = les.Subject;
            Group = les.Group;
        }

        public Variable(LessonType type, string subject, string group)
        {
            LessonType = type;
            Subject = subject;
            Group = group;
        }

        public LessonType LessonType { get; }

        public string Subject { get; }

        public string Group { get; }


        public override string ToString()
        {
            return string.Concat(LessonType == LessonType.Lecture ? "Lecture" : "Practice", " , ", Subject, " , ",
                Group);
        }
    }
}