namespace TICTACTOE
{
    using LANGUAGE;
    using MAINMENU;
    using SETTINGS;
    class Game
    {
        public static bool hotSeat = true;
        public static void Main(String[] args)
        {
            TicTacToe game = new TicTacToe(hotSeat);
            //MainMenu.CreateMainMenu();
        }

    }
}