using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itLab1
{
    class myType
    {
        private List<string> EnableTypes = new List<string> { "Integer", "Real", "Char", "string", "path", "IntInvl"};
        string curType;

        public myType(string type)
        {
            if (EnableTypes.Contains(type))
            {
                curType = type;
            }
            else
            {
                curType = EnableTypes[3];
            }
        }

        public bool Validation(string value)
        {
            switch (curType)
            {
                case "dg":
                    break;
            }

            return true;
        }
    }
}
