namespace TICTACTOE
{
    using LANGUAGE;
    using MAINMENU;
    using SETTINGS;
    using Utils;
    class Game
    {

        public static bool hotSeat;
        public static void Main(String[] args)
        {
            Settings settings= new Settings();
            MainMenu.CreateMainMenu();
        }

    }
}