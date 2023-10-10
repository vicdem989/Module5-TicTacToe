namespace SETTINGS
{
    public class Settings
    {

        public static string languageSettings = "EN";
        public static string test = "hei"; 

        public static void OutputToFile(string score)
        {
            File.AppendAllText(@"settings.txt", score.ToString() + Environment.NewLine);
        }

        public static void ReadFile()
        {
            StreamReader sr = new StreamReader("settings.txt");
            String line = string.Empty;
            line = sr.ReadLine() ?? string.Empty;
            while (line != string.Empty)
            {
                line = sr.ReadLine() ?? string.Empty;
                if(line.Contains(languageSettings)) {
                    test = languageSettings;
                }
            }
            sr.Close();
            Console.WriteLine(test);
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
