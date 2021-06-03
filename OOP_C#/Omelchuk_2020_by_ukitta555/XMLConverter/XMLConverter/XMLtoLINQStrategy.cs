using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLConverter
{
    class XMLtoLINQStrategy : IStrategyParser
    {
        public List<ProgLanguage> Parse(ProgLanguage searchParams)
        {
            List<ProgLanguage> languages = new List<ProgLanguage>();
            var doc = XDocument.Load(@"C:\Users\Vlad_2\repos\C#\XMLConverter\XMLConverter\ProgLanguages.xml");
            var queryResult = from obj in doc.Descendants("ProgLanguage")
                              where
                              (
                                 (obj.Attribute("LanguageName").Value == searchParams.LanguageName || searchParams.LanguageName == "")
                              && (obj.Attribute("Authors").Value == searchParams.Authors || searchParams.Authors == "")
                              && (obj.Attribute("ReleaseYear").Value == searchParams.ReleaseYear || searchParams.ReleaseYear == "")
                              && (obj.Attribute("TypeOfLanguage").Value == searchParams.TypeOfLanguage || searchParams.TypeOfLanguage == "")
                              && (obj.Attribute("AbstractionLevel").Value == searchParams.AbstractionLevel || searchParams.AbstractionLevel == "")
                              && (obj.Attribute("CommonlyUsedFor").Value == searchParams.CommonlyUsedFor || searchParams.CommonlyUsedFor == "")
                              )
                              select new ProgLanguage
                              (    
                                 obj.Attribute("LanguageName").Value,
                                 obj.Attribute("ReleaseYear").Value,
                                 obj.Attribute("Authors").Value,
                                 obj.Attribute("TypeOfLanguage").Value,
                                 obj.Attribute("AbstractionLevel").Value,
                                 obj.Attribute("CommonlyUsedFor").Value
                              );
            foreach (var obj in queryResult) 
            {
                if (!obj.HasSomeFieldsEmpty())
                {
                    languages.Add(obj);
                }
            }
            return languages;
        } 
    }
}
