namespace SETTINGS
{
    using System.Runtime.CompilerServices;
    using System.Text;
    using LANGUAGE;
    using MAINMENU;
    public class Settings
    {

        /*

        Check if config.txt exists
            if !exists
                create new file
                default language to EN
        
            else 
                get language from config.txt

        if currentLanguage == changed
            output new currentLanguage to config.txt
        



        */

        private static List<string> settings = new List<string>();
        private static string path = "config.txt";

        public static void CreateSettings()
        {
            //StreamWriter sw = new StreamWriter("config.txt");
            //sw.Close();
            int choice = MainMenu.MultipleChoice(true, "English", "Norwegian");
            if (choice == 0)
            {
                Language.currentLanguage = "en";
                MainMenu.CreateMainMenu();
            }
            else if (choice == 1)
            {
                Language.currentLanguage = "no";
                MainMenu.CreateMainMenu();
            }
        }

        public static void OutputToFIle()
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("hei");
            sw.Write("Language = " + Language.currentLanguage);
            sw.Close();
        }


        public static void SetLanguage(string language)
        {
            Language.currentLanguage = language;
        }

        private static void CreateConfigFile()
        {
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
            }
        }

        public static void GetLanguageFromFile()
        {
            CreateConfigFile();
            StreamReader sr = new StreamReader(path);
            String line = string.Empty;
            string[] desiredLanguage;
            line = sr.ReadLine() ?? string.Empty;
            if (line.Contains("Language"))
            {
                desiredLanguage = line.Split("=");
                Language.currentLanguage = desiredLanguage[1];
            }
            /*while (line != null)
            {
                settings.Add(line);
                if (line.Contains("Language"))
                {
                    desiredLanguage = line.Split("=");
                    Language.currentLanguage = desiredLanguage[1];
                }
            }*/
            sr.Close();
        }
    }
}
