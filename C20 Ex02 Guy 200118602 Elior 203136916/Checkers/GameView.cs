using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;
using Ex02.ConsoleUtils;

namespace Checkers
{
    class GameView
    {
        public static void initiaizeGame()
        {
            Screen.Clear();
            Console.WriteLine("         Checkers Game");
            Console.WriteLine();
            getUserInitialInput();
        }

        private static void getUserInitialInput()
        {
            Player Player1 = new Player(e_PlayerID.FIRST, getFromUser("Enter your name (Max size 20 without spaces): ", 20));
            int boardSize = int.Parse(getFromUser("Choose board size (6,8,10): ", 6, 8, 10));
            int numOfPlayers = int.Parse(getFromUser("Enter number of players (1,2): ", 1, 2));
            if (numOfPlayers == 2)
            {
                Player Player2 = new Player(e_PlayerID.SECOND, getFromUser("Enter your name (Max size 20 without spaces): ", 20));
            }
        }

		private static String getFromUser(string i_Msg, params int [] i_Numbers)
		{
            //if in numers have just 1 -> name of user.
            //if in number have 2 numbers -> numbers of players.
            //if in number have 3 numbers -> size of board.
            string result = "";
            int players = 0;
            do
            {
                Console.WriteLine(i_Msg);
                result = Console.ReadLine();
            } while ((i_Numbers.Length == 1 && 
                        (result.Length >= i_Numbers[0] || result.Contains(" ") || result.Length < 1)) ||
                    (i_Numbers.Length == 2 && 
                        (!int.TryParse(result, out players) || 
                            (players != i_Numbers[0] && players != i_Numbers[1]))) ||
                    (i_Numbers.Length == 3 && 
                        (!int.TryParse(result, out players) || 
                            (players != i_Numbers[0] && players != i_Numbers[1] && players != i_Numbers[2]))));
            bool elior = int.TryParse(result, out players);
            return result;
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
