namespace TICTACTOE
{
    using LANGUAGE;
    using MAINMENU;
    using SETTINGS;
    class Game
    {
        public static bool hotSeat = false;
        public static void Main(String[] args)
        {
            TicTacToe game = new TicTacToe(hotSeat);
            //MainMenu.CreateMainMenu();
        }

    }
}