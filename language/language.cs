

using System.Dynamic;
using ENGLISH;

namespace LANGUAGE {
    public class Language {

        public static string currentLanguage = string.Empty;

        public static ApplicationStrings appText = SetDefaultLanguage();

        public static ApplicationStrings SetDefaultLanguage() 
        {
            currentLanguage = "EN";
            return LangEN.appTextEN;
        }

    }

    public class ApplicationStrings {

    }
}