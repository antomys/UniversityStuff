using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itLab1
{
    class bdTypeIntegerInvl : bdType
    {
        public override bool Validation(string value)
        {
            int a;
            int b;
            string[] buf = value.Split(' ');

            if (buf.Length != 2) return false;
            if (!int.TryParse(buf[0], out a) || !int.TryParse(buf[1], out b) || a >= b) return false;
            return true;
        }
    }
}
