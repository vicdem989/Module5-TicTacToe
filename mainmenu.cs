using Utils;
using TicTacToe;

namespace MAINMENU
{
    public class MainMenu
    {

        public static void CreateMainMenu()
        {
            int choice = MainMenu.MultipleChoice(true, "1P Game", "2P Game", "Settings", "Quit");
            if (choice == 0)
            {
                RenderGame();
            }
            else if (choice == 1)
            {
                RenderGame();
            }
            else if (choice == 2)
            {
                Console.WriteLine("Settings coming soon");
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private static void RenderGame()
        {
            GameLogic game = new GameLogic();
            game.TicTacToe();
        }
        private static int MultipleChoice(bool canCancel, params string[] options)
        {
            const int startX = 15;
            const int startY = 4;
            const int optionsPerLine = 1;
            const int spacingPerLine = 0;

            int currentSelection = 0;

            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                Console.Clear();
                //If there is a winner
                //Display winner
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(options[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % optionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < options.Length)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            if (canCancel)
                                return -1;
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return currentSelection;
        }
    }
}