using System;

namespace Connect4Project
{
class counectfour
    {
        public abstract class Players{
            public string Name { get; set; }
            public char ID { get; internal set; }

            public virtual string PrintInfo()
            {
                return  Name ;
            }
            public override string ToString()
            {
                return PrintInfo();
            }

        }

        class Player1: Players
        {
            public new string  Name { get; set; }
            
            public override string ToString()
            {
                return PrintInfo();
            }
        }
        class Player2: Players
        {
            public new string Name { get; set; }
       
            public override string ToString() {
            return PrintInfo() ;
            }
        }   
        
        public static int PlayerDrop(char[,] board, Players activePlayer)
            {
                int dropChoice;

                Console.WriteLine("Player " + activePlayer.Name + "'s Turn ");
                do
                {
                    Console.WriteLine("Please enter a number between 1 and 7: ");
                    dropChoice = Convert.ToInt32(Console.ReadLine());
                } while (dropChoice < 1 || dropChoice > 7);

                while (board[1, dropChoice] == 'X' || board[1, dropChoice] == 'O')
                {
                    Console.WriteLine("That row is full, please enter a new row: ");
                    dropChoice = Convert.ToInt32(Console.ReadLine());
                }

                return dropChoice;
            }
        public static void CheckBellow(char[,] board, Players activePlayer, int dropChoice)
        {
            int length, turn;
            length = 6;
            turn = 0;

            do
            {
                if (board[length, dropChoice] != 'X' && board[length, dropChoice] != 'O')
                {
                    board[length, dropChoice] = activePlayer.ID;
                    turn = 1;
                }
                else
                    --length;
            } while (turn != 1);


        }

        public static void DisplayBoard(char[,] board)
        {
            int rows = 6, columns = 7, i, ix;

            for (i = 1; i <= rows; i++)
            {
                Console.Write("|");
                for (ix = 1; ix <= columns; ix++)
                {
                    if (board[i, ix] != 'X' && board[i, ix] != 'O')
                        board[i, ix] = '#';

                    Console.Write(board[i, ix]);

                }

                Console.Write("| \n");
            }

        }

        static int CheckFour(char[,] board, Players activePlayer)
        {
            char XO;
            int win;

            XO = activePlayer.ID;
            win = 0;

            for (int i = 8; i >= 1; --i)
            {

                for (int ix = 9; ix >= 1; --ix)
                {

                    if (board[i, ix] == XO &&
                        board[i - 1, ix - 1] == XO &&
                        board[i - 2, ix - 2] == XO &&
                        board[i - 3, ix - 3] == XO)
                    {
                        win = 1;
                    }


                    if (board[i, ix] == XO &&
                        board[i, ix - 1] == XO &&
                        board[i, ix - 2] == XO &&
                        board[i, ix - 3] == XO)
                    {
                        win = 1;
                    }

                    if (board[i, ix] == XO &&
                        board[i - 1, ix] == XO &&
                        board[i - 2, ix] == XO &&
                        board[i - 3, ix] == XO)
                    {
                        win = 1;
                    }

                    if (board[i, ix] == XO &&
                        board[i - 1, ix + 1] == XO &&
                        board[i - 2, ix + 2] == XO &&
                        board[i - 3, ix + 3] == XO)
                    {
                        win = 1;
                    }

                    if (board[i, ix] == XO &&
                         board[i, ix + 1] == XO &&
                         board[i, ix + 2] == XO &&
                         board[i, ix + 3] == XO)
                    {
                        win = 1;
                    }
                }

            }

            return win;
        }

       public static int FullBoard(char[,] board)
        {
            int full;
            full = 0;
            for (int i = 1; i <= 7; ++i)
            {
                if (board[1, i] != '#')
                    ++full;
            }

            return full;
        }

        public static void PlayerWin(Players activePlayer)
        {
            Console.WriteLine(activePlayer.Name + " Connected Four, You Win!");
        }


        public static int restart(char[,] board)
        {
            int restart;

            Console.WriteLine("Would you like to restart? Yes(1) No(2): ");
            restart = Convert.ToInt32(Console.ReadLine());
            if (restart == 1)
            {
                for (int i = 1; i <= 6; i++)
                {

                    for (int ix = 1; ix <= 7; ix++)
                    {
                        board[i, ix] = '#';
                           
                    }

                }
               
            }
            else
                Console.WriteLine("Thank you for playing this game!");
            Environment.Exit(0);
           return restart;
        }



        static void Main(string[] args)
        {
           Players Player1 = new Player1 { };
            Players Player2 = new Player2 { };

            char[,] board = new char[9, 10];
            int dropChoice, win, full, again;
          
            Console.WriteLine("Connect Four Game");
            Console.WriteLine("Player One please enter your name: ");
            Player1.Name = Console.ReadLine();
            Player1.ID = 'X';
            Console.WriteLine("Player Two please enter your name: ");
            Player2.Name = Console.ReadLine();
           Player2.ID = 'O';

            full = 0;
            again = 0;

           while (Player1.Name == Player2.Name)
            {
                      
              
            Console.WriteLine("Connect Four Game");
            Console.WriteLine("Player One please enter your name: ");
            Player1.Name = Console.ReadLine();
            Player1.ID = 'X';
            Console.WriteLine("Player Two please enter your name: ");
            Player2.Name = Console.ReadLine();
            Player2.ID = 'O';


            }
           

            do
            {
                dropChoice = PlayerDrop(board, Player1);
                CheckBellow(board, Player1, dropChoice);
                DisplayBoard(board);
                win = CheckFour(board, Player1);
                if (win == 1)
                {
                    PlayerWin(Player1);
                    again = restart(board);
                    if (again == 2)
                    {
                        break;
                    }
                }

                dropChoice = PlayerDrop(board, Player2);
                CheckBellow(board, Player2, dropChoice);
                DisplayBoard(board);
                win = CheckFour(board, Player2);
                if (win == 1)
                {
                    PlayerWin(Player2);
                    again = restart(board);
                    if (again == 2)
                    {
                        break;
                    }
                }
                full = FullBoard(board);
                if (full == 7)
                {
                    Console.WriteLine("The board is full, it is a draw!");
                    again = restart(board);
                }

            } while (again != 2);

            PlayerDrop(board, Player1);
            PlayerDrop(board, Player2); 
            CheckBellow(board, Player1, full);
            CheckBellow(board, Player2, full);
            DisplayBoard(board);
            CheckFour(board, Player1);  
            CheckFour(board, Player2);  
            FullBoard(board);
            PlayerWin(Player1);
            PlayerWin(Player2); 
            restart(board);
        }


    }
}
