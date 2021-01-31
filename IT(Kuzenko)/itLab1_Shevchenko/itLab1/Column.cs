using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itLab1
{
    class Column
    {
        public string cName;
        public bdType cType;
        public string typeName;

        public Column(string cname, string ctype)
        {
            cName = cname;
            typeName = ctype;

            switch (ctype)
            {
                case "Integer":
                    cType = new bdTypeInteger();
                    break;
                case "Real":
                    cType = new bdTypesReal();
                    break;
                case "Char":
                    cType = new bdTypeChar();
                    break;
                case "String":
                    cType = new bdTypeString();
                    break;
                case "Path":
                    cType = new bdTypePath();
                    break;
                case "IntegerInvl":
                    cType = new bdTypeIntegerInvl();
                    break;
                default:
                    cType = new bdTypeString();
                    break;
            }
        }
    }
}
