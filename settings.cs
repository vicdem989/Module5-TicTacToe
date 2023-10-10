namespace SETTINGS
{
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    public class Settings
    {

        public static string languageSettings = "EN";
        public static string test = "hei";
        
        private static string[] desiredLanguage;

        private static List<string> settings = new List<string>();

        public static void OutputToFile(string languageNEw)
        {
            //File.AppendAllText(@"settings.txt", score.ToString() + Environment.NewLine);

            //Remove current languageSettings
            //Add new

            foreach(string item in settings) {
                if(item.Contains("language")) {
                    Console.WriteLine("LMAO        " + item);
                    settings.Remove("KEKW");
                }
            }

            File.AppendAllText(@"settings.txt", "languageSettings = " + languageNEw + Environment.NewLine);

            /*Dictionary<string, string> configuration = new Dictionary<string, string>();

            Regex r = new Regex(@"\[\[(\w+)\]\]=\[\[(\w+)\]\]");

            string[] configArray = { "[[param1]]=[[Value1]]", "[[param2]]=[[Value2]]" };// File.ReadAllLines("some.txt");

            foreach (string config in configArray)
            {
                Match m = r.Match(config);
                configuration.Add(m.Groups[1].Value, m.Groups[2].Value);
            }
            Console.WriteLine(File.ReadAllLines("settings.txt"));*/

        }

        public static void ReadFile()
        {
            StreamReader sr = new StreamReader("settings.txt");
            String line = string.Empty;
            line = sr.ReadLine() ?? string.Empty;
            while (line != string.Empty)
            {
                line = sr.ReadLine() ?? string.Empty;
                settings.Add(line);
            }
            sr.Close();
            foreach(string item in settings) {
                if(item.Contains("languageSettings")) {
                    desiredLanguage = item.Split("=");
                    languageSettings = desiredLanguage[1];
                }
                Console.WriteLine(item);
            }
            Console.WriteLine("New language = " + languageSettings);           
        }

        /*public static void OutputFileContent()
        {
            StreamWriter sw = new StreamWriter("settings.txt");
            foreach (int score in totalHighscores)
            {
                sw.WriteLine(score);
            }
            sw.Close();
        }*/


    }
}
