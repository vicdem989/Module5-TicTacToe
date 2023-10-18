namespace TICTACTOE
{
    using Utils;
    using MAINMENU;
    using SETTINGS;
    using LANGUAGE;
    using System.Threading.Tasks.Dataflow;
    using System.Security.Cryptography;
    using Microsoft.VisualBasic;
    using System.Collections;
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
        private static string player1Name = "Your";
        private static string player2Name = "Player2";
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



        public TicTacToe(bool hotSeat)
        {
            //Console.Clear();
            if (hotSeat)
                SetPlayerAttributes();
            while (isPlaying)
            {

                //System.Console.Clear();
                DrawBoard(board);
                DisplayCurrentPlayer();
                string inputText = System.Console.ReadLine() ?? string.Empty;
                inputRow = inputText.Split(' ')[0];
                inputCol = inputText.Split(' ')[1];
                CheckInput(inputText);
                //Console.Clear();

                CheckMarkPlacement(board, inputText);

                PlaceMarks(row, col);
                DrawBoard(board);
                if (!Game.hotSeat)
                    PlaceAIMark();
                DrawBoard(board);
                CheckGameState();
                if (!Game.hotSeat)
                    PlaceAIMark();

            }
            //Console.WriteLine(Language.currentLanguage);
            //MainMenu.CreateMainMenu();
        }


        private static void SetPlayerAttributes()
        {
            output.Write("Input player1's name (X): ");
            player1Name = Console.ReadLine() ?? String.Empty;

            /*output.Write("Input player1's color (X): ");
            player1Color = Console.ReadLine() ?? String.Empty;*/


            output.Write("Input player2's name (O): ");
            player2Name = Console.ReadLine() ?? String.Empty;

           /* output.Write("Input player2's color (X): ");
            player2Color = Console.ReadLine() ?? String.Empty;*/
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
                System.Console.WriteLine("\n" + DisplayCorrectPlayer() + " (" + DisplayCorrectMark() + ") " + "turn");
                System.Console.Write("Enter row then column: ");
                return;
            }
            System.Console.WriteLine("\n" + DisplayCorrectPlayer() + "'s " + "(" + DisplayCorrectMark() + ") " + "turn");
            System.Console.Write("Enter row then column: ");
        }

        private static void CheckInput(string inputText)
        {
            inputRow = inputText.Split(' ')[0];
            inputCol = inputText.Split(' ')[1];


            validRow = int.TryParse(inputRow, out row);
            validCol = int.TryParse(inputCol, out col);

            while (!validCol || !validRow || row > boardLengthInput || row < 0 || col > boardLengthInput || col < 0)
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

        private static void PlaceAIMark()
        {
            if (currentPlayer != 1 && !Game.hotSeat)
            {
                Random random = new Random();
                int rowAI = random.Next(boardLengthInput);
                int colAI = random.Next(boardLengthInput);
                board[rowAI, colAI] = player2Mark;
                //Checking if AI places on empty or marked spot doesn't work
                /*
                while (board[rowAI, colAI] == " ")
                {
                    rowAI = random.Next(boardLengthInput);
                    colAI = random.Next(boardLengthInput);
                    board[rowAI, colAI] = player2Mark;
                    DrawBoard(board);
                }*/


                Console.WriteLine("Input: " + row + " " + col);
                currentPlayer = 1;
            }
        }

        private void CheckGameState()
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
            isPlaying = false;

        }

        private int CheckForWin(string[,] board)
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
                Console.WriteLine("SCORE    " + sum);
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