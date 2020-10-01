using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA3
{
    public interface IParse
    {
        List<Search> AnalyzeFile(Search mySearch, string path);
    }
}
