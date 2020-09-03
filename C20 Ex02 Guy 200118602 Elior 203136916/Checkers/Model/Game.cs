
using System.Runtime.InteropServices;

namespace Checkers.Model
{
    class Game
    {



        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player PlayerTurn { get; set; }
        public Board Board { get; set; }
        public Player GetOpponent(Player player)
        {
            if (player.ID == e_PlayerID.FIRST)
            {
                return Player2;
            }
            else
            {
                return Player1;
            }
        }

        public Game(Player player1,  Player player2, Board board)
        {
            Player1 = player1;
            Player2 = player2;
            PlayerTurn = player1;
            Board = board;

            player1.CountOfPiecesForPlayer = ((board.BoardSize - 2) / 2) * (board.BoardSize / 2);
            player2.CountOfPiecesForPlayer = ((board.BoardSize - 2) / 2) * (board.BoardSize / 2);
        }

        public bool GameLoop(Player i_Player1,Player i_Player2)
        {
            bool gameEnded = false;
            while (!gameEnded)
            {
                
                if(i_Player2.CountOfPiecesForPlayer == 0)
                {
                    i_Player1.HasWonAndUpdateTheScore(i_Player2);
                    gameEnded = true;
                    
                }
                else if(i_Player1.CountOfPiecesForPlayer == 0)
                {
                    i_Player2.HasWonAndUpdateTheScore(i_Player1);
                    gameEnded = true;
                }

            }

            return gameEnded;
        }

     

    }
}
