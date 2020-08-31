using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{

    class CheckersModel
    {
        private char[,] board = null;

        private int initialFilledRowsForPlayer = 0;
        private int InitialFilledRowsForPlayer
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
                    initialFilledRowsForPlayer = 10;
                }
            }
        }
        public CheckersModel(int boardSize)
        {
            board = new char[boardSize, boardSize];

            initialFilledRowsForPlayer = (boardSize - 2) / 2;

            bool fillEvenCells = true;

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    board[x, y] = ' ';
                }
            }

            for (int y = 0; y < initialFilledRowsForPlayer; y++)
            {
                fillEvenCells = (((y + 1) % 2) == 0);
                for (int x = 0; x < boardSize; x++)
                {
                    if (fillEvenCells)
                    {
                        board[x, y] = (x % 2 == 0) ? 'O' : ' ';
                    }
                    else
                    {
                        board[x, y] = (x % 2 == 0) ? ' ' : 'O';
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
                        board[x, y] = (x % 2 == 0) ? 'X' : ' ';
                    }
                    else
                    {
                        board[x, y] = (x % 2 == 0) ? ' ' : 'X';
                    }
                }
            }


        }

        public char getCell(int x, int y)
        {

            return board[x, y];
        }



    }
}
