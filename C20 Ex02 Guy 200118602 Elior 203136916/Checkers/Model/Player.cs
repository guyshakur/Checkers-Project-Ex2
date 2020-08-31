using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    class Player
    {
        private e_PlayerID iD;
        private String name;

        public e_PlayerID ID { get; set; }
        //public String Name { get; set; }
        public double Score { get; set; }

        public Player(e_PlayerID i_PlayerID)
        {
            ID = i_PlayerID;

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
    }
}
