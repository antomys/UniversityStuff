using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Entities
{
    class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public Group(int _Id, string _Name, int _Size)
        {
            Id = _Id;
            Name = _Name;
            Size = _Size;
        }
    }
}
