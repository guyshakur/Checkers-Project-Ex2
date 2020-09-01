using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    class Player
    {
        private String m_Name;
        private Board m_Board;
        private e_PlayerID m_ID;
        private double m_Score;

        public Board Board {
            get 
            {
                return m_Board;
            }
            set
			{
                m_Board = value;
			}
        }
        public e_PlayerID ID 
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
            }
        }
        public double Score 
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public Player(e_PlayerID i_PlayerID,Board i_Board)
        {
            ID = i_PlayerID;
            Board = i_Board;
        }
        
        public void movePiece(Board board,int oldX,int oldY,int newX,int newY)
        {

            this.Board = board;
            if (Board.ChecksIfLegalMove(this, oldX, oldY, newX, newY))
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
                return m_Name; 
            }

            set
            {
                if (m_Name == null)
                {
                    m_Name = value;
                }
            }
        }
    }
}
