using System;
using Checkers.Model;

namespace Checkers
{
    public class Board
    {
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

        private static int m_BoardSize;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        private Piece[,] m_BoardGame;

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

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Board(int boardSize, Player player1, Player player2)
        {
            BoardSize = boardSize;
            BoardGame = new Piece[boardSize, boardSize];
            Player1 = player1;
            Player2 = player2;
            initialFilledRowsForPlayer = (boardSize - 2) / 2;
            player1.CountOfPiecesForPlayer = ((boardSize - 2) / 2) * (boardSize / 2);
            player2.CountOfPiecesForPlayer = ((boardSize - 2) / 2) * (boardSize / 2);
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
                fillEvenCells = ((y + 1) % 2) == 0;
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

        public void updateBoardGame(Player i_Player, int i_OldX, int i_OldY, int i_NewX, int i_newY)
        {
            BoardGame[i_NewX, i_newY] = getCellContent(i_OldX, i_OldY);
            BoardGame[i_OldX, i_OldY] = null;

            if ((i_Player.ID == e_PlayerID.FIRST && i_newY == 0) || (i_Player.ID == e_PlayerID.SECOND && i_newY == BoardSize - 1))
            {
                BoardGame[i_NewX, i_newY].Rank = e_Rank.KING;
            }

            if (Math.Abs(i_NewX - i_OldX) == 2 && Math.Abs(i_newY - i_OldY) == 2)
            {
                BoardGame[(i_OldX + i_NewX) / 2, (i_OldY + i_newY) / 2] = null;
                ////the opponent's pieces count is now iccreent by 1.
                updateCountOfPiecesForPlayerOpponentByID(i_Player.GetOpponentID(i_Player));
            }
        }

        private void updateCountOfPiecesForPlayerOpponentByID(e_PlayerID ID)
        {
            if (ID == e_PlayerID.FIRST)
            {
                Player1.CountOfPiecesForPlayer--;
            }
            else
            {
                Player2.CountOfPiecesForPlayer--;
            }
        }
    }
}
