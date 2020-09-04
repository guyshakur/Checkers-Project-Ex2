using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;

namespace Checkers
{
	public enum e_Rank
	{ 
		SOLDIER, 
		KING 
	}

	public class Piece
	{
		public Player Player { get; set; }
		
		public e_Rank Rank { get; set; }

		public Piece(Player player)
		{
			this.Player = player;
			this.Rank = e_Rank.SOLDIER;
		}
	}
}
