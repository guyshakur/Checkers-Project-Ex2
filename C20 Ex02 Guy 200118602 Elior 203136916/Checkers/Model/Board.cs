﻿using Checkers.Model;
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
        public Piece[,] BoardGame
        {
            get
            {
                return m_BoardGame;
            }
            set
            {
                m_BoardGame = value;
            }
        }
        private Piece[,] m_BoardGame;

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
            BoardSize = boardSize;
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
            if(Math.Abs(newX- oldX) == 2 && Math.Abs(newY - oldY) == 2)
			{
                BoardGame[(oldX + newX) /2 , (oldY + newY) / 2] = null;
            }



        }


        



   }
}
