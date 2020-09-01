using System;

namespace Checkers.Model
{
    enum e_PlayerID { FIRST, SECOND }
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

        public Player(e_PlayerID i_PlayerID, Board i_Board)
        {
            ID = i_PlayerID;
            Board = i_Board;
        }
        
        public void movePiece(Board i_Board, int i_OldX, int i_OldY, int i_NewX, int i_NewY)
        {

            Board = i_Board;
            if (Board.ChecksIfLegalMove(this, i_OldX, i_OldY, i_NewX, i_NewY))
                Board.UpdateBoardGame(this, i_OldX, i_OldY, i_NewX, i_NewY);

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
