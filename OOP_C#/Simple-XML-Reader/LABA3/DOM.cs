using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LABA3
{
    class DOM : IParse
    {
        XmlDocument doc = new XmlDocument();
        //private List<Search> find = null;
        public List<Search> AnalyzeFile(Search mySearch, string path)
        {
            doc.Load(path);
            List<List<Search>> info = new List<List<Search>>();

            if (mySearch.country == null && mySearch.company == null && mySearch.year == null && mySearch.rate == null&& mySearch.nato == null && mySearch.ga == null)
            {
                return ErrorCatch(doc);
            }

            //if (mySearch.name != null) info.Add(SearchByAttribute("Game", "NAME", mySearch.name, doc, 1));
            if (mySearch.country != null) info.Add(SearchByAttribute("Game", "COUNTRY", mySearch.country, doc));
            if (mySearch.company != null) info.Add(SearchByAttribute("Game", "COMPANY", mySearch.company, doc));
            if (mySearch.year != null) info.Add(SearchByAttribute("Game", "YEAR", mySearch.year, doc));
            if (mySearch.rate != null) info.Add(SearchByAttribute("Game", "RATE", mySearch.rate, doc));
            if (mySearch.nato != null) info.Add(SearchByAttribute("Game", "NATO", mySearch.nato, doc));
            if (mySearch.ga != null) info.Add(SearchByAttribute("Game", "GA", mySearch.ga, doc));

            return Cross(info, mySearch);
        }

        
        public List<Search> SearchByAttribute(string nodeName, string attribute, string myTemplate, XmlDocument doc)
        {
            List<Search> find = new List<Search>();

            if(myTemplate != null)
            {
                XmlNodeList lst = doc.SelectNodes("//" + nodeName + "[@" + attribute + "=\"" + myTemplate + "\"]");
                foreach(XmlNode e in lst){
                    find.Add(Info(e));
                }
            }
            return find;
        }
        public List<Search> ErrorCatch(XmlDocument doc)
        {
            List<Search> result = new List<Search>();
            XmlNodeList lst = doc.SelectNodes("//" + "Game");
            foreach(XmlNode elem in lst)
            {
                result.Add(Info(elem));
            }
            return result;

        }
        public Search Info(XmlNode node)
        {
            Search search = new Search();

            search.name = node.Attributes.GetNamedItem("NAME").Value;
            search.country = node.Attributes.GetNamedItem("COUNTRY").Value;
            search.company = node.Attributes.GetNamedItem("COMPANY").Value;
            search.year = node.Attributes.GetNamedItem("YEAR").Value;
            search.rate = node.Attributes.GetNamedItem("RATE").Value;
            search.nato = node.Attributes.GetNamedItem("NATO").Value;
            search.ga = node.Attributes.GetNamedItem("GA").Value;
            return search;
        }
        public List<Search> Cross(List<List<Search>> list, Search myTemplate)
        {


            List<Search> result = new List<Search>();

            List<Search> clear = CheckNodes(list, myTemplate);

            
           foreach(Search elem in clear)
            {
                bool isIn = false;
                foreach(Search s in result)
                {
                    if (s.Compare(elem))
                    {
                        isIn = true;
                    }
                }

                if (!isIn)
                {
                    result.Add(elem);
                }
            }

            return result;
        }

        public List<Search> CheckNodes(List<List<Search>> list, Search myTemplate)
        {
            List<Search> newResult = new List<Search>();
            foreach (List<Search> elem in list)
            {
                foreach (Search s in elem)
                {
                    if ((myTemplate.country == s.country || myTemplate.country == null) &&
                        (myTemplate.company == s.company || myTemplate.company == null) &&
                        (myTemplate.year == s.year || myTemplate.year == null) &&
                        (myTemplate.rate == s.rate || myTemplate.rate == null) && 
                        (myTemplate.nato == s.nato || myTemplate.nato == null) &&
                        (myTemplate.ga == s.ga || myTemplate.ga == null)
                        )
                    {
                        newResult.Add(s);
                    }
                }
            }
            return newResult;
        }
    }
}
