using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLConverter
{
    class ProgLanguage
    {

        public ProgLanguage() 
        {
        }
        public ProgLanguage(string languageName, string releaseYear, string authors, string typeOfLang, string abstractionLevel, string commonlyUsed) 
        {
            LanguageName = languageName;
            ReleaseYear = releaseYear;
            Authors = authors;
            TypeOfLanguage = typeOfLang;
            AbstractionLevel = abstractionLevel;
            CommonlyUsedFor = commonlyUsed;
        }
        public string LanguageName { get; set; } = "";
        public string ReleaseYear { get; set; } = "";
        public string Authors { get; set; } = "";
        public string TypeOfLanguage { get; set; } = "";
        public string AbstractionLevel { get; set; } = "";
        public string CommonlyUsedFor { get; set; } = "";

        public bool HasSomeFieldsEmpty()
        {
            if (
                   LanguageName == ""
                || ReleaseYear == ""
                || Authors == ""
                || TypeOfLanguage == ""
                || AbstractionLevel == ""
                || CommonlyUsedFor == ""
               )
            {
                return true;
            }
            return false;
        }
    }
}
