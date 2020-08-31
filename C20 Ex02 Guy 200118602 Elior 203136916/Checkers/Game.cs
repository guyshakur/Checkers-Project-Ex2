using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;


namespace Checkers
{
    class Game
    {
        private static string playerName;
        private static int boardSize;
        private static int numOfPlayers;

        public static void Main()
        {

            Console.WriteLine("         Checkers Game");
            Console.WriteLine();

            getUserInitialInput();
            Screen.Clear();

            CheckersModel model = new CheckersModel(boardSize);
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

                    Console.Write(" " + model.getCell(x, y) + " ");
                    Console.Write("|");
                }
                Console.WriteLine();
                Console.WriteLine("  " + new String('=', boardSize * 4 + 1));
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

        public static void getUserInitialInput()
        {


            playerName = getStringFromUser("Enter your name (20 characters and no spaces): ");


            boardSize = getIntFromUser("Choose board size (6,8,10): ", 6, 8, 10);

            numOfPlayers = getIntFromUser("Enter number of players (1,2): ", 1, 2);

        }
        private static string getStringFromUser(string msg)
        {
            string result = "";
            bool validStr = false;
            while (!validStr)
            {
                Console.Write(msg);
                result = Console.ReadLine();
                if (!(result.Contains(' ')) && result.Length <= 20)
                {
                    validStr = true;
                }

            }
            return result;
        }
    }
}
