using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    class Player
    {
        public e_PlayerID ID { get; set; }
        public String Name { get; set; }
        public double Score { get; set; }

        public Player(e_PlayerID playerID, String playerName="no name")
        {
            this.ID = playerID;
            this.Name = playerName;
        }
        
        public void Quit()
        {

        }
    }
}
