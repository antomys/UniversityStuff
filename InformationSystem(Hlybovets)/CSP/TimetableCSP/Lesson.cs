using System.Text.Json;

namespace TimetableCSP
{
    internal class Lesson
    {
        private bool _free;

        public Lesson()
        {
            _free = true;
        }

        public Lesson(Lesson les)
        {
            _free = false;
            LessonType = les.LessonType;
            Subject = les.Subject;
            Teacher = les.Teacher;
            Group = les.Group;
            Audience = les.Audience;
        }


        public Utilities.LessonType LessonType { get; private set; }

        public string Subject { get; private set; }

        public string Teacher { get; private set; }

        public string Group { get; private set; }

        public int Audience { get; private set; }

        public bool IsFree
        {
            get => _free;
            set { }
        }

        public override string ToString()
        {
            var type = LessonType == Utilities.LessonType.Lecture ? "Lecture" : "Practice";
            return _free
                ? "free"
                : string.Concat(LessonType, ", ", Subject, ", ", Teacher, ", Audience: ", Audience, ", Group(s): ",
                    Group);
        }

        public void FillForHour(string group, string subject, Utilities.LessonType type, JsonElement root)
        {
            LessonType = type;
            var _subjectLecturer = Utilities.GetAsObjectJSON(root, "Subject_lecturer", "Subject");
            var teachers = Utilities.GetAsObjectJSON<string[]>(root, "Teacher");
            var audiences = Utilities.GetAsObjectJSON<int[]>(root, "Audience");
            if (type == Utilities.LessonType.Lecture)
            {
                Teacher = _subjectLecturer[subject];
            }
            else
            {
                var choosedTeacher = Utilities.ChooseRandomly(0, teachers.Length);
                Teacher = teachers[choosedTeacher];
            }

            Subject = subject;
            Group = group;
            Audience = audiences[Utilities.ChooseRandomly(0, audiences.Length)];
            _free = false;
        }

        public override bool Equals(object obj)
        {
            return obj is Lesson lesson &&
                   LessonType == lesson.LessonType &&
                   Subject == lesson.Subject &&
                   Group == lesson.Group &&
                   IsFree == lesson.IsFree;
        }
    }
}