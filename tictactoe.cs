namespace TicTacToe
{
    using Utils;
    using MAINMENU;
    using SETTINGS;


    class GameLogic
    {
        private const int EMPTY = 0;
        private const int PLAYER1 = 1;
        private const int PLAYER2 = -1;
        private string[,] board = {
            { string.Empty, string.Empty, string.Empty },
            { string.Empty, string.Empty, string.Empty },
            { string.Empty, string.Empty, string.Empty }
        };

        private static int currentPlayer = PLAYER1;
        private static bool isPlaying = true;

        private static string player1Mark = "X";
        private static string player2Mark = "O";


        public void TicTacToe()
        {

            while (isPlaying)
            {
                //System.Console.Clear();
                DrawBoard(board);
                System.Console.WriteLine("Player " + currentPlayer + "'s turn");
                string input = System.Console.ReadLine() ?? string.Empty;
                //Check if input is already placed or not
                int row = int.Parse(input.Split(' ')[0]);
                int col = int.Parse(input.Split(' ')[1]);
                if (board[row, col] != string.Empty)
                {
                    Console.WriteLine(row + " " + col);
                } else {
                    Console.WriteLine("Spot already taken");
                }
                

                if (currentPlayer == 1)
                {
                    board[row, col] = player1Mark;
                }
                else
                {
                    board[row, col] = player2Mark;
                }

                int gameState = CheckForWin(board);
                currentPlayer = currentPlayer * -1;

                if (gameState != 0)
                {

                    DrawBoard(board);
                    System.Console.WriteLine("Player " + currentPlayer + " wins!");
                    isPlaying = false;
                }

            }
            MainMenu.CreateMainMenu();
        }

        int CheckForWin(string[,] board)
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


        void DrawBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                string row = "| ";
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