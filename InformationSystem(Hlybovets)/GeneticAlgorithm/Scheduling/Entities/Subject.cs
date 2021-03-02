using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Entities
{
    class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimesPerWeek { get; set; }
        public bool IsLab { get; set; }

        public Subject(int _Id, string _Name, int _TimesPerWeek, bool _IsLab)
        {
            Id = _Id;
            Name = _Name;
            TimesPerWeek = _TimesPerWeek;
            IsLab = _IsLab;
        }
    }
}
