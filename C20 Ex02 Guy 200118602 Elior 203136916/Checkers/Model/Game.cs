
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

        public void GameLoop(Player i_Player1,Player i_player2)
        {
            bool gameEnded = false;
            while (!gameEnded)
            {
                for(int y = 0; y < Board.BoardSize; y++ )
                {
                    for(int x = 0; x<Board.BoardSize; x++)
                    {
                        if(Board.getCellContent(x,y)!= null)
                        {
                            if(Board.getCellContent(x, y).Player.ID==e_PlayerID.FIRST)
                            {

                            }
                        }
                    }
                }

            }
        }

        public void updateCountOfPiecesOfTheOther(e_PlayerID ID)
        {
            if(Player1.ID==ID)
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
