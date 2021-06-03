using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLConverter
{
    interface IStrategyParser
    {
        List<ProgLanguage> Parse(ProgLanguage searchParams);
    }
}
