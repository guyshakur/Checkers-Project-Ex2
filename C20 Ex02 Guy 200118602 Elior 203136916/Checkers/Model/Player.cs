using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    class Player
    {
        private String name;
        public Board Board { get; set; }
        public e_PlayerID ID { get; set; }
        //public String Name { get; set; }
        public double Score { get; set; }

        public Player(e_PlayerID i_PlayerID,Board board)
        {
            this.ID = i_PlayerID;
            this.Board = board;
        }
        
        public void movePiece(Board board,int oldX,int oldY,int newX,int newY)
        {

            this.Board = board;
            if (isValidMove)
                Board.updateBoardGame(this, oldX, oldY, newX, newY);

            else
            {
                
            }
        }
        public void Quit()
        {

        }

        public String Name
        {
            get 
            { 
                return name; 
            }

            set
            {
                if (name == null)
                {
                    name = value;
                }
            }
        }
    }
}
