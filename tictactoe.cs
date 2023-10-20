namespace TICTACTOE
{
    using Utils;
    using MAINMENU;
    using SETTINGS;
    using LANGUAGE;
    using System.Windows.Input;
    using System.Threading.Tasks.Dataflow;
    using System.Security.Cryptography;
    using Microsoft.VisualBasic;
    using System.Collections;
    using System.Numerics;
    using System.Reflection.Metadata;
    using System.Security.Cryptography.X509Certificates;

    class TicTacToe
    {
        private const int EMPTY = 0;
        private const int PLAYER1 = 1;
        private const int PLAYER2 = -1;
        private static string[,] board = {
        { " ", " ", " " },
        { " ", " ", " " },
        { " ", " ", " " }
        };

        private static int currentPlayer = PLAYER1;
        private static bool isPlaying = true;

        private static string player1Mark = "X";
        private static string player2Mark = "O";
        private static string player1Name = "You";
        private static string player2Name = "AI";
        private static string player1Color = ANSI_COLORS.MAGENTA;
        private static string player2Color = ANSI_COLORS.CYAN;

        private static int row = 0;
        private static int col = 0;

        private static bool validRow = false;
        private static bool validCol = false;

        private static string inputRow = string.Empty;
        private static string inputCol = string.Empty;

        private static int boardLengthInput = board.Length / 3 - 1;

        private static bool canCancel = true;

        public static Output output = new Output();

        private static bool gameOver = false;


        public TicTacToe(bool hotSeat)
        {
            output.WriteLine("Hei" + " = " + Convert.ToChar(0xB0));
            do
            {
                Console.Clear();
                if (hotSeat)
                    SetPlayerAttributes();
                Run();
                output.WriteLine("Do you want to restart the game? y/n");
                if (Console.ReadLine() == "y")
                    Run();

                //Console.WriteLine(Language.currentLanguage);
                //MainMenu.CreateMainMenu();
            } while (Console.ReadKey().Key != ConsoleKey.Q || Console.ReadKey().Key != ConsoleKey.Enter);
            output.AddColor("Game Over!", ANSI_COLORS.BLUE);
        }

        private static void Run()
        {
            output.WriteLine("Welcome to TicTacToe!");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    board[i, j] = " ";
                }
            }
            do
            {
                System.Console.Clear();
                DrawBoard(board);
                DisplayCurrentPlayer();
                string inputText = System.Console.ReadLine() ?? string.Empty;
                output.MoveCursor();
                inputRow = inputText.Split(' ')[0];
                inputCol = inputText.Split(' ')[1];
                CheckInput(inputText);
                Console.Clear();

                CheckMarkPlacement(board, inputText);

                PlaceMarks(row, col);
                DrawBoard(board);
                if (!Game.hotSeat)
                    PlaceAIMark();
                DrawBoard(board);
                CheckGameState();
                if (!Game.hotSeat)
                    PlaceAIMark();
            } while (Console.ReadKey().Key != ConsoleKey.R || Console.ReadKey().Key != ConsoleKey.Enter);
        }

        private static void SetPlayerAttributes()
        {
            output.Write("Input player1's name (X): ");
            player1Name = Console.ReadLine() ?? String.Empty;

            output.Write("Input player2's name (O): ");
            player2Name = Console.ReadLine() ?? String.Empty;
        }

        private static string DisplayCorrectPlayer()
        {
            int currentPlayerOutput = currentPlayer;
            if (currentPlayer < 0 && Game.hotSeat)
            {
                return player2Name;
            }
            return player1Name;
        }

        private static string DisplayCorrectMark()
        {
            if (currentPlayer == 1)
            {
                return "X";
            }
            return "O";
        }

        private static void DisplayCurrentPlayer()
        {
            if (!Game.hotSeat)
            {
                output.WriteLine(" ");
                output.WriteLine(DisplayCorrectPlayer() + " (" + DisplayCorrectMark() + ") " + "turn");
                output.Write("Enter row then column: ");
                return;
            }
            output.WriteLine(DisplayCorrectPlayer() + "'s " + "(" + DisplayCorrectMark() + ") " + "turn");
            output.Write("Enter row then column: ");
        }

        private static void CheckInput(string inputText)
        {
            Console.Clear();
            inputRow = inputText.Split(' ')[0];
            inputCol = inputText.Split(' ')[1];


            validRow = int.TryParse(inputRow, out row);
            validCol = int.TryParse(inputCol, out col);

            while (!validCol || !validRow || row > boardLengthInput || row < 0 || col > boardLengthInput || col < 0)
            {
                output.WriteLine("Invalid inputs, input new:");
                NewInputs(inputText, inputRow, inputCol);
            }
        }

        private static void NewInputs(string inputText, string inputRow, string inputCol)
        {
            DrawBoard(board);
            output.MoveCursor();
            inputText = System.Console.ReadLine() ?? string.Empty;
            if (inputText == "")
                NewInputs(inputText, inputRow, inputCol);
            inputRow = inputText.Split(' ')[0];
            inputCol = inputText.Split(' ')[1];
            CheckInput(inputText);
        }

        private static void CheckMarkPlacement(string[,] board, string inputText)
        {
            while (board[row, col] != " ")
            {
                Console.Clear();
                output.WriteLine("Spot taken, input new inputs");
                DrawBoard(board);
                Console.WriteLine("");
                NewInputs(inputText, inputRow, inputCol);
            }
            Console.WriteLine("Input: " + row + " " + col);
        }

        private static void PlaceMarks(int row, int col)
        {

            if (currentPlayer == 1)
            {
                board[row, col] = player1Mark;
            }
            else
            {
                board[row, col] = player2Mark;
            }

        }

        private static void PlaceAIMark()
        {
            if (currentPlayer != 1 && !Game.hotSeat)
            {
                Random random = new Random();
                int rowAI = random.Next(board.GetLength(0));
                int colAI = random.Next(board.GetLength(0));
                board[rowAI, colAI] = player2Mark;
                DrawBoard(board);
                //Checking if AI places on empty or marked spot doesn't work

                if (board[rowAI, colAI] == player1Mark)
                {
                    output.AddColor("The AI took your mark!", ANSI_COLORS.RED);
                } /*else if (board[rowAI, colAI] == player2Mark) {
                    output.AddColor("The AI missplaced, your turn", ANSI_COLORS.GREEN);
                }*/
                /*else
                {
                    PlaceAIMark();
                }*/


                output.WriteLine("Input: " + row + " " + col);
                currentPlayer = 1;
            }
        }

        private static void CheckGameState()
        {
            int gameState = CheckForWin(board);
            currentPlayer = currentPlayer * -1;
            if (gameState == 0)
                return;

            DrawBoard(board);
            string currentPlayerOutput = currentPlayer.ToString();
            string outputColor = player1Color;
            if (gameState == -1)
            {
                currentPlayerOutput = player2Name;
                outputColor = player2Color;
            }
            else
            {
                currentPlayerOutput = player1Name;
                outputColor = player1Color;
            }
            output.AddColor(currentPlayerOutput + " wins!", outputColor);
            gameOver = true;

        }

        private static int CheckForWin(string[,] board)
        {

            int winSum = board.GetLength(0);

            // rows
            for (int i = 0; i < board.GetLength(1); i++)
            {
                int sum = 0;
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == player1Mark)
                    {
                        sum++;
                    }
                    else if (board[i, j] == player2Mark)
                    {
                        sum--;
                    }
                    else
                    {
                        sum += 0;
                    }
                }
                output.WriteLine("SCORE    " + sum);
                if (sum == (winSum * PLAYER1) || sum == (winSum * PLAYER2))
                {
                    return sum / winSum;
                }
            }

            // columns
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < board.GetLength(1); j++)
                {

                    if (board[j, i] == player1Mark)
                    {
                        sum++;
                    }
                    else if (board[j, i] == player2Mark)
                    {
                        sum--;
                    }
                    else
                    {
                        sum += 0;
                    }
                }
                if (sum == (winSum * PLAYER1) || sum == (winSum * PLAYER2))
                {
                    return sum / winSum;
                }
            }

            if (board[1, 1] == " ")
                return 0;

            if (board[0, 0] == player1Mark && board[1, 1] == player1Mark && board[2, 2] == player1Mark)
            {
                return 1;
            }
            else if (board[0, 0] == player2Mark && board[1, 1] == player2Mark && board[2, 2] == player2Mark)
            {
                return -1;
            }
            else if (board[0, 2] == player1Mark && board[1, 1] == player1Mark && board[2, 0] == player1Mark)
            {
                return 1;
            }
            else if (board[0, 2] == player2Mark && board[1, 1] == player2Mark && board[2, 0] == player2Mark)
            {
                return -1;
            }

            return 0;
        }

        private static bool CheckBoardSize(string[,] board)
        {
            int count = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != " ")
                    {
                        count++;
                    }
                }
            }
            if (count == board.Length)
                return false;
            return true;
        }


        private static void DrawBoard(string[,] board)
        {
            if (!CheckBoardSize(board))
            {
                output.WriteLine("It's a tie!");
                Thread.Sleep(200);
                MainMenu.CreateMainMenu();
            }

            output.WriteLine(" ");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                string row = " | ";

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == "X")
                    {
                        row += $"{board[i, j]} | ";
                        //Console.ForegroundColor = ConsoleColor.DarkRed;
                        //Console.WriteLine(row);
                        //Console.ResetColor();
                    }
                    else
                    {
                        row += $"{board[i, j]} | ";
                    }
                }
                output.Write(i + 1 + " " + row + "\n");
                Console.ResetColor();

            }
        }
    }
}