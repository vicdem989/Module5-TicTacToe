

using System.Dynamic;
using System.Net.NetworkInformation;
using ENGLISH;
using NORWEGIAN;

namespace LANGUAGE {
    public class Language {

        public static string currentLanguage = string.Empty;

        public static ApplicationStrings appText = SetDefaultLanguage();
        public static List<ApplicationStrings> Languages = new List<ApplicationStrings>();

        public static ApplicationStrings SetDefaultLanguage() 
        {
            currentLanguage = "EN";
            return LangEN.appTextEN;
        }

    }

    public class ApplicationStrings {
        public string? Welcome { get; set; }
    }
}