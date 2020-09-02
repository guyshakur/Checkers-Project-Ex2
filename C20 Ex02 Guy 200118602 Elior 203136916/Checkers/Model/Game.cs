
namespace Checkers.Model
{
    class Game
    {

        
        
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player PlayerTurn { get; set; }

        public Player GetOpponent(Player player)
        {
            if(player.ID == e_PlayerID.FIRST )
            {
                return Player2;
            }
            else
            {
                return Player1;
            }
        }

        public Game(Player player1,Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this.PlayerTurn = player1;

        }

        public void GameLoop()
        {
            bool gameEnded = false;
            while (!gameEnded)
            {

            }
        }
        
    }
}
