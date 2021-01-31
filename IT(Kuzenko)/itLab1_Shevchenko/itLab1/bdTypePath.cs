using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itLab1
{
    class bdTypePath : bdType
    {
        public override bool Validation(string value)
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(value);
                sr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
