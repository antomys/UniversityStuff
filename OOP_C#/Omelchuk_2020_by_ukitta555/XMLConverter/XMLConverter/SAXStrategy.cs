using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLConverter
{
    class SAXStrategy : IStrategyParser
    {
        public List<ProgLanguage> Parse (ProgLanguage searchParams) 
        {
            List<ProgLanguage> languages = new List<ProgLanguage>();
            var xmlReader = new XmlTextReader(@"C:\Users\Vlad_2\repos\C#\XMLConverter\XMLConverter\ProgLanguages.xml");
            while (xmlReader.Read()) 
            {
                ProgLanguage languageObj = new ProgLanguage();
                if (xmlReader.HasAttributes) 
                {
                    while( xmlReader.MoveToNextAttribute() ) 
                    {
                        if (xmlReader.Name == "LanguageName" && (searchParams.LanguageName == xmlReader.Value || searchParams.LanguageName == ""))
                        {
                            languageObj.LanguageName = xmlReader.Value;
                            Console.WriteLine("Added lang type");
                            xmlReader.MoveToNextAttribute();
                        }
                        if (xmlReader.Name == "ReleaseYear" && (searchParams.ReleaseYear == xmlReader.Value || searchParams.ReleaseYear == ""))
                        {
                            languageObj.ReleaseYear = xmlReader.Value;
                            Console.WriteLine("Added release year");
                            xmlReader.MoveToNextAttribute();
                        }
                        if (xmlReader.Name == "Authors" && (searchParams.Authors == xmlReader.Value || searchParams.Authors == ""))
                        {
                            languageObj.Authors = xmlReader.Value;
                            Console.WriteLine("Added authors");
                            xmlReader.MoveToNextAttribute();
                        }
                        if (xmlReader.Name == "TypeOfLanguage" && (searchParams.TypeOfLanguage == xmlReader.Value || searchParams.TypeOfLanguage == ""))
                        {
                            languageObj.TypeOfLanguage = xmlReader.Value;
                            Console.WriteLine("Added type");
                            xmlReader.MoveToNextAttribute();
                        }
                        if (xmlReader.Name == "AbstractionLevel" && (searchParams.AbstractionLevel == xmlReader.Value || searchParams.AbstractionLevel == ""))
                        {
                            languageObj.AbstractionLevel = xmlReader.Value;
                            Console.WriteLine("Added level");
                            xmlReader.MoveToNextAttribute();
                        }
                        if (xmlReader.Name == "CommonlyUsedFor" && (searchParams.CommonlyUsedFor == xmlReader.Value || searchParams.CommonlyUsedFor == ""))
                        {
                            languageObj.CommonlyUsedFor = xmlReader.Value;
                            Console.WriteLine("Added usage");
                            xmlReader.MoveToNextAttribute();
                        }
                    }
                }
                if (!languageObj.HasSomeFieldsEmpty()) 
                {
                    languages.Add(languageObj);
                }
            }
            xmlReader.Close();
            return languages;
        }
    }
}
