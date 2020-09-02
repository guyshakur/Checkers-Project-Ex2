using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;

namespace Checkers
{

    enum e_Rank { SOLDIER, KING }

    class Piece
    {
        public Player Player { get; set; }
        // public Board Board { get; set; }
        //public int X { get; set; }
        // public int Y { get; set; }
        public e_Rank Rank { get; set; }


        public Piece(Player player)
        {
            this.Player = player;
            // this.Board = board;
            // this.X = x;
            // this.Y = y;
            this.Rank = e_Rank.SOLDIER;
        }
    }
}
