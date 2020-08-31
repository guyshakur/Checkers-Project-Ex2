using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;
using Ex02.ConsoleUtils;


namespace Checkers
{
    class GameMain
    {
        public static Player Player1 { get; set; }
        public static Player Player2 { get; set; }
        private static int boardSize;
        private static int numOfPlayers;

        public static void Main()
        {
            initiaizeGame();
            getUserInitialInput();
             


            Player1 = new Player(e_PlayerID.FIRST);
            Player2 = new Player(e_PlayerID.SECOND);
            Game game = new Game();

            

            Screen.Clear();
            Board model = new Board(boardSize,Player1,Player2);
            string boardHeader = "   A   B   C   D   E   F   G   H   I   J";
            Console.WriteLine(" " + boardHeader.Substring(0, boardSize * 4));
            Console.WriteLine("  " + new String('=', boardSize * 4 + 1));

            string boardSideHeader = "abcdefghij";
            for (int y = 0; y < boardSize; y++)
            {
                Console.Write(boardSideHeader.Substring(y, 1) + " ");
                Console.Write("|");
                for (int x = 0; x < boardSize; x++)
                {

                    Console.Write(" " + getCellAsString(model.getCellContent(x, y)) + " ");
                    Console.Write("|");
                }
                if(y==0)
                { 
                    Console.Write("  Player2: " + Player2.Name); 
                }
                if (y == boardSize-1)
                {
                    Console.Write("  Player1: " + Player1.Name);
                }
                Console.WriteLine();
                Console.WriteLine("  " + new String('=', boardSize * 4 + 1));
            }
            Console.WriteLine();
            Console.Write(Player1.Name + "'s turn :");
            Console.WriteLine();

        }

        private static string getCellAsString(Piece gamePiece)
        {
            if(gamePiece==null)
            {
                return " ";
            }
            else if(gamePiece.Player.ID==e_PlayerID.FIRST)
            {
                return gamePiece.Rank==e_Rank.SOLDIER ? "X" : "K";
            }
            else if (gamePiece.Player.ID == e_PlayerID.SECOND)
            {
                return gamePiece.Rank == e_Rank.SOLDIER ? "O" : "U";
            }
            else
            {
                return ".";
            }

        }

        private static int getIntFromUser(string msg, params int[] validInputs)
        {
            int result = 0;
            bool validNum = false;
            while (!validNum)
            {
                Console.Write(msg);
                string numAsStr = Console.ReadLine();

                if (Int32.TryParse(numAsStr, out result))
                {
                    foreach (int validInput in validInputs)
                    {
                        if (result == validInput)
                        {
                            validNum = true;
                        }
                    }

                }
            }
            return result;
        }

       



    }
}
