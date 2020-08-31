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
        private Piece[,] board = null;

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
        public Board(int boardSize,Player player1,Player player2)
        {
            board = new Piece[boardSize, boardSize];

            initialFilledRowsForPlayer = (boardSize - 2) / 2;

            bool fillEvenCells = true;

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    board[x, y] = null;
                }
            }

            for (int y = 0; y < initialFilledRowsForPlayer; y++)
            {
                fillEvenCells = (((y + 1) % 2) == 0);
                for (int x = 0; x < boardSize; x++)
                {
                    if (fillEvenCells)
                    {
                        board[x, y] = (x % 2 == 0) ? new Piece(player2,this,x,y) : null;
                    }
                    else
                    {
                        board[x, y] = (x % 2 == 0) ? null : new Piece(player2, this, x, y);
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
                        board[x, y] = (x % 2 == 0) ? new Piece(player1, this, x, y) : null;
                    }
                    else
                    {
                        board[x, y] = (x % 2 == 0) ? null : new Piece(player1, this, x, y);
                    }
                }
            }


        }

        public Piece getCellContent(int x, int y)
        {

            return board[x, y];
        }



    }
}
