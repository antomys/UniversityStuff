using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itLab1
{
    class bdTypesReal : bdType
    {
        public override bool Validation(string value)
        {
            double buf;
            if (double.TryParse(value, out buf)) return true;
            return false;
        }
    }
}
