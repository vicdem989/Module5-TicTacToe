namespace TicTacToe
{

    using MAINMENU;
    using SETTINGS;
    class Game
    {
        public static void Main(String[] args)
        {
            Settings.ReadFile();
            Console.WriteLine("Do you want a new language added?");
            string input = Console.ReadLine().ToLower();
            if(input != string.Empty) 
                Settings.OutputToFile(input);
            Console.WriteLine(Settings.languageSettings);
            //MainMenu.CreateMainMenu();
        }

    }
}