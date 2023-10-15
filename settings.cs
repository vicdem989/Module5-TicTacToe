namespace SETTINGS
{
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using LANGUAGE;
    using MAINMENU;
    using Utils;

    public class Settings
    {

        /*

        Check if config.txt exists
            if !exists
                create new file
                dfault language to EeN
        
            else 
                get language from config.txt

        if currentLanguage == changed
            output new currentLanguage to config.txt
        



        */

        private static List<string> settings = new List<string>();
        private static string path = "config.txt";
        Settings()
        {
            CheckConfig();
        }

        public static void CreateSettings()
        {
            int choice = MainMenu.MultipleChoice(true, "English", "Norwegian");
            if (choice == 0)
            {
                Language.currentLanguage = "en";
            }
            else if (choice == 1)
            {
                Language.currentLanguage = "no";

            }
            OutputToFIle(ChangeLanguage());

            MainMenu.CreateMainMenu();
        }

        public static void CheckConfig()
        {
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
                Language.SetDefaultLanguage();
                CreateConfigContent();
            }
            else
            {
                GetLanguageFromFile();
            }
        }

        private static string ChangeLanguage() {
            return ("Language = " + Language.currentLanguage);
        }

        public static void CreateConfigContent()
        {
            OutputToFIle("CONFIG FILE");
            OutputToFIle(ChangeLanguage());
        }

        public static void OutputToFIle([Optional] string text)
        {
            StreamWriter sw = new StreamWriter(path);
            if (text != null)
                sw.Write(text);
            sw.Close();
        }
        public static void GetLanguageFromFile()
        {
            StreamReader sr = new StreamReader(path);
            String line = string.Empty;
            string[] desiredLanguage;
            line = sr.ReadLine() ?? string.Empty;
            if (line.Contains("Language"))
            {
                desiredLanguage = line.Split("=");
                Language.currentLanguage = desiredLanguage[1];
            }
            sr.Close();
            Console.WriteLine(Language.currentLanguage);
        }
    }
}
