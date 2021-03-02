using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Entities
{
    class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Room(int _Id, string _Name, int _Capacity)
        {
            Id = _Id;
            Name = _Name;
            Capacity = _Capacity;
        }
    }
}
