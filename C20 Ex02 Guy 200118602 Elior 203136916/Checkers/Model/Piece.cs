using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;

namespace Checkers
{
	enum e_Rank { SOLDIER, KING }

	class Piece
	{
		private Player m_player;
		private e_Rank m_Rank;
		private int m_X;
		private int m_Y;

		public Player Player
		{
			get
			{
				return m_player;
			}
			set
			{
				m_player = value;
			}
		}
		public e_Rank Rank
		{
			get
			{
				return m_Rank;
			}
			set
			{
				m_Rank = value;
			}
		}
		public int X
		{
			get
			{
				return m_X;
			}
			set
			{
				m_X = value;
			}
		}
		public int Y
		{
			get
			{
				return m_Y;
			}
			set
			{
				m_Y = value;
			}
		}


		public Piece(Player i_Player, int i_X, int i_Y)
		{
			Player = i_Player;
			Rank = e_Rank.SOLDIER;
			m_X = i_X;
			m_Y = i_Y;
		}
	}
}
