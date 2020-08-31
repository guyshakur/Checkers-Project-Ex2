using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace Checkers
{
    class GameView
    {



        public void initiaizeGame()
        {
            Screen.Clear();
            Console.WriteLine("         Checkers Game");
            Console.WriteLine();
            getUserInitialInput();
        }

        public static void getUserInitialInput()
        {


            Player1.Name = getStringFromUser("Enter your name (Max size 20 without spaces): ", 20);


            boardSize = getIntFromUser("Choose board size (6,8,10): ", 6, 8, 10);

            numOfPlayers = getIntFromUser("Enter number of players (1,2): ", 1, 2);

            if (numOfPlayers == 2)
            {
                Player2.Name = getStringFromUser("Enter your name (Max size 20 without spaces): ", 20);
            }
        }
        private static string getStringFromUser(string msg, int maxSize)
        {
            string result = "";
            bool validStr = false;
            while (!validStr)
            {
                Console.Write(msg);
                result = Console.ReadLine();
                if (!(result.Contains(' ')) && result.Length <= maxSize)
                {
                    validStr = true;
                }

            }
            return result;
        }
    }


}
