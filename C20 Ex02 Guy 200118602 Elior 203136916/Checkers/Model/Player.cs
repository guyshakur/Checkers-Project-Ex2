using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{

    public enum e_PlayerID { FIRST, SECOND }
    class Player
    {


        public int CountOfPiecesForPlayer { get; set; }

        private String name;
        public Board Board { get; set; }
        public e_PlayerID ID { get; set; }

        public double Score { get; set; }

        public Player(e_PlayerID i_PlayerID)
        {
            this.ID = i_PlayerID;
            //this.Board = board;
            //CountOfPiecesForPlayer = ((board.BoardSize - 2) / 2) * (board.BoardSize / 2);


        }

        public void movePiece(Board board, int oldX, int oldY, int newX, int newY)
        {

            this.Board = board;

            Board.updateBoardGame(this, oldX, oldY, newX, newY);

            //while(Board.ChecksIfLegalMove()==false)
            // {
            // Board.UnvalidMoveMessage("");
            //

            //}


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

        public e_PlayerID GetOpponent(Player player)
        {
            if (player.ID == e_PlayerID.FIRST)
            {
                return e_PlayerID.SECOND;
            }
            else
            {
                return e_PlayerID.SECOND;
            }
        }

        public bool hasAnyMoves()
        {
            return true;
        }

        public bool isValidMove(Board i_Board, int i_OldX, int i_OldY, int i_NewX, int i_NewY)
        {

            this.Board = i_Board;
            if (Board.getCellContent(i_OldX, i_OldY) == null || Board.getCellContent(i_OldX, i_OldY).Player.ID != this.ID)
            {
                return false;
            }
            if (Board.getCellContent(i_NewX, i_NewY) != null)
            {

                return false;
            }

            if (Board.getCellContent(i_OldX, i_OldY).Rank == e_Rank.SOLDIER)
            {
                if ((ID == e_PlayerID.FIRST && i_NewY != i_OldY - 1 || ID == e_PlayerID.SECOND && i_NewY != i_OldY + 1) && (i_NewX != i_OldX + 1 || i_NewX != i_OldX - 1))
                {

                    return false;
                }
            }
            else if ((i_NewY != i_OldY - 1 || i_NewY != i_OldY + 1) && (i_NewX != i_OldX + 1 || i_NewX != i_OldX - 1))
            {

                return false;
            }

            return true;
        }
    }
}
