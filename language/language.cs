namespace LANGUAGE
{

    using System.Dynamic;
    using System.Net.NetworkInformation;
    using ENGLISH;
    using NORWEGIAN;
    using SETTINGS;

    public class Language
    {

        public static string currentLanguage = string.Empty;

        public static ApplicationStrings appText = SetLanguage();
        public static List<ApplicationStrings> Languages = new List<ApplicationStrings>();

        public static ApplicationStrings SetDefaultLanguage()
        {
            currentLanguage = "en";
            return LangEN.appTextEN;
        }

        public static ApplicationStrings SetLanguage()
        {
            currentLanguage = Settings.GetLanguageFromFile();
            if (currentLanguage == "no")
            {
                appText = LangNO.appTextNO;
                return LangNO.appTextNO;
            }
            else if (currentLanguage == "en")
            {
                appText = LangEN.appTextEN;
                return LangEN.appTextEN;
            }
            return LangEN.appTextEN;
        }


    }

    public class ApplicationStrings
    {

        //TicTacToe
        public string? Welcome { get; set; }
        public string? Player1DefaultName { get; set; }
        public string? Restart { get; set; }
        public string? Player1Name { get; set; }
        public string? Player2Name { get; set; }
        public string? Turn { get; set; }
        public string? EnterRowColumn { get; set; }
        public string? InvalidInput { get; set; }
        public string? SpotTaken { get; set; }
        public string? Input { get; set; }
        public string? AIStole { get; set; }
        public string? Winner { get; set; }
        public string? Tie { get; set; }

        //MainMenu

        public string? OneP { get; set; }
        public string? TwoP { get; set; }
        public string? Settings { get; set; }
        public string? Quit { get; set; }

        //Langauge 
        public string? English { get; set; }
        public string? Norwegian { get; set; }



    }
}