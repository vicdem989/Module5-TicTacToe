namespace TicTacToe
{
    using LANGUAGE;
    using MAINMENU;
    using SETTINGS;
    class Game
    {
        public static void Main(String[] args)
        {
            //Settings.ReadFile();
            Settings.GetLanguageFromFile();
            Settings.CreateSettings();
            Settings.OutputToFIle();
            Console.WriteLine(Language.currentLanguage);
        }

    }
}