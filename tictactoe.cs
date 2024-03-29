﻿namespace TICTACTOE
{
    using Utils;
    using MAINMENU;
    using LANGUAGE;

    class TicTacToe
    {
        private const int PLAYER1 = 1;
        private const int PLAYER2 = -1;
        private static string[,] board = {
        { " ", " ", " " },
        { " ", " ", " " },
        { " ", " ", " " }
        };

        private static int currentPlayer = PLAYER1;

        private static string player1Mark = "X";
        private static string player2Mark = "O";
        private static string player1Name = "Your";
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

        public static Output output = new Output();

        private static bool gameOver = false;

        private static bool hotSeat = false;

        private static string currentPlayerOutput = String.Empty;
        private static string outputColor = ANSI_COLORS.WHITE;
        private static string inputText = String.Empty;



        public TicTacToe(bool chosenMode)
        {
            inputText = string.Empty;
            gameOver = false;
            player1Name = Language.appText.Player1DefaultName;
            hotSeat = chosenMode;
            //do
            //{
                Console.Clear();
                if (hotSeat)
                    SetPlayerAttributes();
                Run();
                output.WriteLine(Language.appText.Restart);
                if (Console.ReadLine() == "y" && hotSeat)
                {
                    MainMenu.RenderGame(hotSeat);
                }
                else
                {
                    Environment.Exit(0);
                }
            //} while (Console.ReadKey().Key != ConsoleKey.Q || Console.ReadKey().Key != ConsoleKey.Enter);
        }

        private static void Run()
        {
            currentPlayerOutput = String.Empty;
            output.WriteLine(Language.appText.Welcome + "\n");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    board[i, j] = " ";
                }
            }
            //do
            //{
                while (!gameOver)
                {
                    Console.Clear();
                    DrawBoard(board);
                    DisplayCurrentPlayer();
                    string inputText = System.Console.ReadLine() ?? string.Empty;
                    output.MoveCursor();
                    CheckInput(inputText);
                    CheckMarkPlacement(board, inputText);
                    PlaceMarks(row, col);

                    CheckGameState();
                    if (!hotSeat)
                        PlaceAIMark();
                    Console.Clear();
                    if (gameOver == true)
                    {
                        output.AddColor(currentPlayerOutput + Language.appText.Winner, outputColor);
                    }
                }
            //} while (Console.ReadKey().Key != ConsoleKey.R || Console.ReadKey().Key != ConsoleKey.Enter && gameOver == false);
        }

        private static void SetPlayerAttributes()
        {
            output.Write(Language.appText.Player1Name);
            player1Name = Console.ReadLine() ?? String.Empty;
            while (player1Name == string.Empty)
            {
                output.Write(Language.appText.Player1Name);
                player1Name = Console.ReadLine() ?? String.Empty;
            }

            output.Write(Language.appText.Player2Name);
            player2Name = Console.ReadLine() ?? String.Empty;
            while (player2Name == string.Empty)
            {
                output.Write(Language.appText.Player2Name);
                player2Name = Console.ReadLine() ?? String.Empty;
            }
        }

        private static string DisplayCorrectPlayer()
        {
            int currentPlayerOutput = currentPlayer;
            if (currentPlayer < 0 && hotSeat)
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
            if (hotSeat)
            {
                output.WriteLine(" ");
                output.WriteLine(DisplayCorrectPlayer() + " (" + DisplayCorrectMark() + ") " + Language.appText.Turn);
                output.Write(Language.appText.EnterRowColumn);
                return;
            }
            output.WriteLine(DisplayCorrectPlayer() + "'s " + "(" + DisplayCorrectMark() + ") " + Language.appText.Turn);
            output.Write(Language.appText.EnterRowColumn);
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
                output.WriteLine(Language.appText.InvalidInput);
                NewInputs(inputText, inputRow, inputCol);
            }
        }

        private static void NewInputs(string inputText, string inputRow, string inputCol)
        {
            Console.Write(" \n");
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
                output.WriteLine(Language.appText.SpotTaken);
                DrawBoard(board);
                Console.WriteLine("");
                NewInputs(inputText, inputRow, inputCol);
            }
            Console.WriteLine(Language.appText.Input + row + " " + col);
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
            if (currentPlayer != PLAYER1 && hotSeat)
            {
                Random random = new Random();
                int rowAI = random.Next(board.GetLength(0));
                int colAI = random.Next(board.GetLength(0));
                board[rowAI, colAI] = player2Mark;
                DrawBoard(board);

                if (board[rowAI, colAI] == player1Mark)
                {
                    output.AddColor(Language.appText.AIStole, ANSI_COLORS.RED);
                }

                output.WriteLine(Language.appText.Input + row + " " + col);
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
            currentPlayerOutput = currentPlayer.ToString();
            outputColor = player1Color;
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
            output.AddColor(currentPlayerOutput + Language.appText.Winner, outputColor);
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
                output.WriteLine(Language.appText.Tie);
                Thread.Sleep(200);
                MainMenu.CreateMainMenu();
            }

            output.Write(" 1   2   3");
            output.WriteLine(" ");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                string row = " | ";

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == "X")
                    {
                        row += $"{board[i, j]} | ";
                    }
                    else
                    {
                        row += $"{board[i, j]} | ";
                    }
                }
                output.WriteLine(i + 1 + " " + row + "\n");
                Console.ResetColor();

            }
        }
    }
}