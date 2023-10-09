using System.ComponentModel.Design;
using MAINMENU;

namespace TicTacToe
{

    class Game
    {
        public static void Main()
        {
            
            //TicTacToe game = new TicTacToe();
            int choice = ConsoleHelper.MultipleChoice(true, "Start Game", "Choose a Language", "Exit Game", "More?");
            if(choice == 0) {
                Console.WriteLine("Game has started");
            } else if (choice == 2) {
                Console.WriteLine("yes");
            } else {
                Console.WriteLine("kaa");
            }
        }
    }
}