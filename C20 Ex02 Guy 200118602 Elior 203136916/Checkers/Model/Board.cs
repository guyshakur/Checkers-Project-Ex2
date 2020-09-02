using Checkers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{

    class Board
    {
        public Piece[,] BoardGame { get; set; }

        public int BoardSize { get; set; }

        private static int initialFilledRowsForPlayer = 0;
        public static int InitialFilledRowsForPlayer
        {
            get
            {
                return initialFilledRowsForPlayer;
            }

            set
            {
                if (value == 10 || value == 8 || value == 6)
                {
                    initialFilledRowsForPlayer = value;
                }

                else
                {
                    initialFilledRowsForPlayer = 8;
                }
            }
        }
        public Board(int boardSize, Player player1, Player player2)
        {
            this.BoardSize = boardSize;
            BoardGame = new Piece[boardSize, boardSize];

            initialFilledRowsForPlayer = (boardSize - 2) / 2;

            bool fillEvenCells = true;

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    BoardGame[x, y] = null;
                }
            }

            for (int y = 0; y < initialFilledRowsForPlayer; y++)
            {
                fillEvenCells = (((y + 1) % 2) == 0);
                for (int x = 0; x < boardSize; x++)
                {
                    if (fillEvenCells)
                    {
                        BoardGame[x, y] = (x % 2 == 0) ? new Piece(player2) : null;
                    }
                    else
                    {
                        BoardGame[x, y] = (x % 2 == 0) ? null : new Piece(player2);
                    }
                }
            }


            for (int y = boardSize - 1; y > boardSize - initialFilledRowsForPlayer - 1; y--)
            {
                fillEvenCells = (boardSize - 1 - y) % 2 == 0;
                for (int x = 0; x < boardSize; x++)
                {
                    if (fillEvenCells)
                    {
                        BoardGame[x, y] = (x % 2 == 0) ? new Piece(player1) : null;
                    }
                    else
                    {
                        BoardGame[x, y] = (x % 2 == 0) ? null : new Piece(player1);
                    }
                }
            }


        }

        public Piece getCellContent(int x, int y)
        {

            return BoardGame[x, y];
        }

        public void updateBoardGame(Player player, int oldX, int oldY, int newX, int newY)
        {
            BoardGame[newX, newY] = getCellContent(oldX, oldY);
            BoardGame[oldX, oldY] = null;

            //if the player is on the "kingdom" row he should be a king now.
            //if ((player.ID == e_PlayerID.FIRST && newY == 0) || (player.ID == e_PlayerID.SECOND && newY == BoardSize - 1))
            //{
            //    BoardGame[newX, newY].Rank = e_Rank.KING;
           // }

            //update if it had eaten for player 2
           // if (oldX+1<BoardSize-1 && oldY+1<BoardSize-1 && getCellContent(newX+1,newY+1)!=null)
          //  {
           //     if(getCellContent(oldX+1,oldY+1).Player.ID!=player.ID)
            //    {
                //    BoardGame[oldX + 1, oldY + 1] = null;
              //  }
           // }

           // else if (oldX - 1 > 0  && oldY + 1 < BoardSize - 1 && getCellContent(oldX - 1, oldY + 1) != null)
           // {
             //   if (getCellContent(oldX - 1, oldY + 1).Player.ID != player.ID)
              //  {
              //      BoardGame[oldX - 1, oldY + 1] = null;
              //  }
            }

            //update if it had eaten for player 2
            //if (newX+1 < BoardSize-1 && newY-1 > 0 && getCellContent(newX - 1, newY - 1) != null)
            //{

            //    BoardGame[newX + 1, newY - 1] = null; 
            // }

        }


        



    }
}
