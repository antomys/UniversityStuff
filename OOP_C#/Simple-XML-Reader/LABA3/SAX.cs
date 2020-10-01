using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LABA3
{
    class SAX : IParse
    {
        private List<Search> lastResult = null;
        public List<Search> AnalyzeFile(Search mySearch, string path)
        {
            XmlReader reader = XmlReader.Create(path);

            List<Search> result = new List<Search>();

            Search find = null;
            //string _name = null;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "Game")
                        {
                            find = new Search();
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name == "NAME")
                                {
                                    find.name = reader.Value;
                                }
                                if (reader.Name == "COUNTRY")
                                {
                                    find.country = reader.Value;
                                }
                                if (reader.Name == "COMPANY")
                                {
                                    find.company = reader.Value;
                                }
                                if (reader.Name == "YEAR")
                                {
                                    find.year = reader.Value;
                                }
                                if (reader.Name == "RATE")
                                {
                                    find.rate = reader.Value;
                                }
                                if (reader.Name == "NATO")
                                {
                                    find.nato = reader.Value;
                                }
                                if (reader.Name == "GA")
                                {
                                    find.ga = reader.Value;
                                }
                            }
                            result.Add(find);


                        }
                        break;
                }


            }
            lastResult = Filter(result, mySearch);
            return lastResult;
        }

        private List<Search> Filter(List<Search> allRes, Search myTemplate)
        {
            List<Search> newResult = new List<Search>();
            if (allRes != null)
            {
                foreach (Search i in allRes)
                {
                    if ((myTemplate.country == i.country || myTemplate.country == null) &&
                        (myTemplate.company == i.company || myTemplate.company == null) &&
                        (myTemplate.year == i.year || myTemplate.year == null) &&
                        (myTemplate.rate == i.rate || myTemplate.rate == null)&&
                        (myTemplate.nato == i.nato || myTemplate.nato == null) &&
                        (myTemplate.ga == i.ga || myTemplate.ga == null)
                        )
                    {
                        newResult.Add(i);
                    }
                }
            }
            return newResult;
        }
    }
}
