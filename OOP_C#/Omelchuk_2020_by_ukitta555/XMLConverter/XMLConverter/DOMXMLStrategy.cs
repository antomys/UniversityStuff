using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMLConverter
{
    class DOMXMLStrategy : IStrategyParser
    {
        private bool queryHasAttributes = false;
        public List<ProgLanguage> Parse(ProgLanguage searchParams) 
        {
            List<ProgLanguage> languages = new List<ProgLanguage>();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Vlad_2\repos\C#\XMLConverter\XMLConverter\ProgLanguages.xml");

            XmlNode root = doc.DocumentElement;
            string query = "";

            // query construction
            query = @"ProgLanguage[";
            query += addSearchParamToQuery("@LanguageName", searchParams.LanguageName);
            query += addSearchParamToQuery("@ReleaseYear", searchParams.ReleaseYear);

            query += addSearchParamToQuery("@Authors", searchParams.Authors);
            query += addSearchParamToQuery("@TypeOfLanguage", searchParams.TypeOfLanguage);

            query += addSearchParamToQuery("@AbstractionLevel", searchParams.AbstractionLevel);
            query += addSearchParamToQuery("@CommonlyUsedFor", searchParams.CommonlyUsedFor);

            query += "]";

            Console.WriteLine(query);
            XmlNodeList childNodes = root.SelectNodes(query);

            foreach (XmlNode node in childNodes) 
            {
                ProgLanguage pl = new ProgLanguage(
                                                    node.Attributes.GetNamedItem("LanguageName").Value,
                                                    node.Attributes.GetNamedItem("ReleaseYear").Value,
                                                    node.Attributes.GetNamedItem("Authors").Value,
                                                    node.Attributes.GetNamedItem("TypeOfLanguage").Value,
                                                    node.Attributes.GetNamedItem("AbstractionLevel").Value,
                                                    node.Attributes.GetNamedItem("CommonlyUsedFor").Value
                                                  );

                languages.Add(pl);
            }
            return languages;
        }
        private string addSearchParamToQuery(string attribute, string searchParameter)
        {
            string result = "";
            if (searchParameter != "")
            {
                if (queryHasAttributes)
                {
                    result += " and ";
                }
                result += String.Format("{0} = '{1}'", attribute, searchParameter);
                queryHasAttributes = true;
            }
            return result;
        }
    }
}
