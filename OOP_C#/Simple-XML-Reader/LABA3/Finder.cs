using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA3
{
    public class Search
    {
        public string name = null;
        public string country = null;
        public string company = null;
        public string year = null;
        public string rate = null;
        public string nato = null;
        public string ga = null;

        public Search()
        {
        }
        public bool Compare(Search obj)
        {
            if ((this.country == obj.country) &&
                (this.company == obj.company) &&
                (this.year == obj.year) &&
                (this.rate == obj.rate)&&
                (this.nato == obj.nato) &&
                (this.ga == obj.ga)
                )
            {
                return true;
            }
            else return false;
        }
    }
}
