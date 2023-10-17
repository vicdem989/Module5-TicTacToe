namespace TICTACTOE
{
    using Utils;
    using MAINMENU;
    using SETTINGS;
    using LANGUAGE;
    using System.Reflection.Metadata.Ecma335;
    using System.Diagnostics.Contracts;
    using System.Numerics;

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

        private static int row = 0;
        private static int col = 0;

        private static bool validRow = false;
        private static bool validCol = false;

        private static string inputRow = string.Empty;
        private static string inputCol = string.Empty;



        public TicTacToe()
        {

            while (isPlaying)
            {
                System.Console.Clear();
                DrawBoard(board);
                DisplayCurrentPlayer();

                string inputText = System.Console.ReadLine() ?? string.Empty;
                inputRow = inputText.Split(' ')[0];
                inputCol = inputText.Split(' ')[1];
                CheckInput(inputText);
                Console.Clear();

                CheckMarkPlacement(board, inputText);
                PlaceMarks(row, col);

                CheckGameState();

            }
            //Console.WriteLine(Language.currentLanguage);
            //MainMenu.CreateMainMenu();
        }

        private static int DisplayCorrectPlayer()
        {
            int currentPlayerOutput = currentPlayer;
            if (currentPlayerOutput < 0)
            {
                currentPlayerOutput = 2;
                return 2;
            }
            return 1;
        }

        private static string DisplayCorrectMark() {
            if(currentPlayer == 1) {
                return "X";
            }
            return "O";
        }

        private static void DisplayCurrentPlayer()
        {
            System.Console.WriteLine("\nPlayer " + DisplayCorrectPlayer() + "'s " + "(" + DisplayCorrectMark() + ") " + "turn");
            System.Console.Write("Enter row then column: ");

        }

        private static void CheckInput(string inputText)
        {
            inputRow = inputText.Split(' ')[0];
            inputCol = inputText.Split(' ')[1];


            validRow = int.TryParse(inputRow, out row);
            validCol = int.TryParse(inputCol, out col);
            int boardLength = board.Length / 3 - 1;
            Console.WriteLine(validRow);
            Console.WriteLine(validCol);
            while (!validCol || !validRow || row > boardLength || row < 0 || col > boardLength || col < 0)
            {
                Console.WriteLine("Invalid inputs, input new:");
                NewInputs(inputText, inputRow, inputCol);
            }
        }

        private static void NewInputs(string inputText, string inputRow, string inputCol)
        {
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
                Console.WriteLine("Spot taken, input new inputs");
                DrawBoard(board);
                Console.WriteLine("");
                NewInputs(inputText, inputRow, inputCol);
            }
            Console.WriteLine("Input: " + row + " " + col);
        }

        private void PlaceMarks(int row, int col)
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
        private void CheckGameState()
        {
            int gameState = CheckForWin(board);
            currentPlayer = currentPlayer * -1;
            if (gameState == 0)
                return;

            DrawBoard(board);
            int currentPlayerOutput = currentPlayer;
            if (currentPlayerOutput < 0)
            {
                currentPlayerOutput = 2;
            }
            System.Console.WriteLine("Player " + currentPlayerOutput + " wins!");
            isPlaying = false;

        }

        private int CheckForWin(string[,] board)
        {

            int winSum = board.GetLength(0);

            // rows
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    //sum += Int32.Parse(board[i, j]);
                    if (board[i, j] == player1Mark)
                    {
                        sum += 1;
                    }
                    else
                    {
                        sum += 0;
                    }
                }
                Console.WriteLine("SCORE    " + sum);
                if (sum == (winSum * PLAYER1) || sum == (winSum * PLAYER2))
                {
                    return sum / winSum;
                }
            }

            // columns
            for (int i = 0; i < board.GetLength(1); i++)
            {
                int sum = 0;
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == player1Mark)
                    {
                        sum += 1;
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

            return 0;
        }


        private static void DrawBoard(string[,] board)
        {
            Console.Write("     1");
            Console.Write("   2 ");
            Console.Write("  3 ");
            Console.WriteLine();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                string row = " | ";

                for (int j = 0; j < board.GetLength(1); j++)
                {

                    row += $"{board[i, j]} | ";
                }
                Console.Write(i + 1 + " ");
                System.Console.WriteLine(row);
            }

        }

    }

}