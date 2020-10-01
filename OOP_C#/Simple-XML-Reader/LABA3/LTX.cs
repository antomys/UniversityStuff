using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace LABA3
{
    class LinqToXML : IParse
    {

        private List<Search> find = null;
        XDocument doc = new XDocument();
        public List<Search> AnalyzeFile(Search mySearch, string path)
        {
            doc = XDocument.Load(@path);
            find = new List<Search>();
            List<XElement> matches = (from val in doc.Descendants("Weaponry")
                                      where ((mySearch.company == null || mySearch.company == val.Attribute("COMPANY").Value) &&
                                      (mySearch.country == null || mySearch.country == val.Attribute("COUNTRY").Value) &&
                                      (mySearch.year == null || mySearch.year == val.Attribute("YEAR").Value) &&
                                      (mySearch.rate == null || mySearch.rate == val.Attribute("RATE").Value)&&
                                      (mySearch.nato == null || mySearch.nato == val.Attribute("NATO").Value) &&
                                      (mySearch.ga == null || mySearch.ga == val.Attribute("GA").Value)
                                      )
                                      select val).ToList();

            foreach (XElement match in matches)
            {
                Search res = new Search();
                res.name = match.Attribute("NAME").Value;
                res.country = match.Attribute("COUNTRY").Value;
                res.company = match.Attribute("COMPANY").Value;
                res.year = match.Attribute("YEAR").Value;
                res.rate = match.Attribute("RATE").Value;
                res.nato = match.Attribute("NATO").Value;
                res.ga = match.Attribute("GA").Value;
                find.Add(res);
            }

            return find;
        }
    }
}
