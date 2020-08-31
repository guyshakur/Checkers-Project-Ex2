using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player PlayerTurn { get; set; }

        public Player GetOpponent(Player player)
        {
            if(player.ID == e_PlayerID.FIRST )
            {
                return Player2;
            }
            else
            {
                return Player1;
            }
        }

        public void GameLoop()
        {
            bool gameEnded = false;
            while (!gameEnded)
            {

            }
        }
    }
}
