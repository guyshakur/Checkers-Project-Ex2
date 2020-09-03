using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Checkers.Model
{
	public enum e_Eat { CanToEatButNot, EAT, NotCanEat }
	public enum e_PlayerID { FIRST, SECOND }

	
	class Player
	{
		public string SignOfPlayer { get; set; }
		public int CountOfPiecesForPlayer { get; set; }
		private String m_Name;
		private e_PlayerID m_ID;
		public Board Board { get; set; }
		public int Score { get; set; }
		public Player(e_PlayerID i_PlayerID)
		{
			this.ID = i_PlayerID;
			//this.Board = board;
			//CountOfPiecesForPlayer = ((board.BoardSize - 2) / 2) * (board.BoardSize / 2);
		}

		public void movePiece(Board board, int oldX, int oldY, int newX, int newY)
		{
			Board = board;
			Board.updateBoardGame(this, oldX, oldY, newX, newY);
		}
		public void Quit()
		{

		}

		public String Name
		{
			get
			{
				return m_Name;
			}

			set
			{
				if (m_Name == null)
				{
					m_Name = value;
				}
			}
		}
		public e_PlayerID ID 
		{
			get
			{
				return m_ID;
			}
			set
			{
				m_ID = value;
			}
		}

		public e_PlayerID GetOpponentID(Player player)
		{
			if (player.ID == e_PlayerID.FIRST)
			{
				return e_PlayerID.SECOND;
			}
			else
			{
				return e_PlayerID.FIRST;
			}
		}

        public void HasWonAndUpdateTheScore(Player i_OppPlayer)
        {
			Score=updatePlayerPoints() - i_OppPlayer.updatePlayerPoints();
        }

		//counting the piecsed for player and if it's king it's equals 4 points
        private int updatePlayerPoints()
        {
			int points = 0;
            for (int y=0; y < Board.BoardSize; y++)
            {
				for (int x=0; x < Board.BoardSize; x++)
                {
					if(Board.getCellContent(x,y)!=null)
                    {
						if(Board.getCellContent(x,y).Player.ID == ID && Board.getCellContent(x,y).Rank==e_Rank.SOLDIER)
                        {
							points++;
                        }
						else if(Board.getCellContent(x, y).Player.ID == ID && Board.getCellContent(x, y).Rank == e_Rank.KING)
                        {
							points+=4;

						}
                    }
                }
            }
			return points;
        }

        public void CheckInPieceIfCanTOEat(int i_X, int i_Y, ref List<int> io_CurrentPlace, ref List<int> io_NextPlace)
		{
			if (i_X > 1 && i_Y > 1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.FIRST))//i can eat up and left
			{//--
				if (Board.getCellContent(i_X - 1, i_Y - 1) != null
					&& Board.getCellContent(i_X - 1, i_Y - 1).Player.ID != ID
					&& Board.getCellContent(i_X - 2, i_Y - 2) == null)

				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 2);
					io_NextPlace.Add(i_Y - 2 );
				}
			}
			if (i_X > 1 && i_Y < Board.BoardSize -1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.SECOND))//i can eat down and left
			{//-+
				if (Board.getCellContent(i_X - 1, i_Y + 1) != null
					&& Board.getCellContent(i_X - 1, i_Y + 1).Player.ID != ID
					&& Board.getCellContent(i_X - 2, i_Y + 2) == null)

				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 2);
					io_NextPlace.Add(i_Y + 2);
				}
			}
			if (i_X < Board.BoardSize - 1 && i_Y > 1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.FIRST))//i can eat up and right
			{//+-
				if (Board.getCellContent(i_X + 1, i_Y - 1) != null
					&& Board.getCellContent(i_X + 1, i_Y - 1).Player.ID != ID
					&& Board.getCellContent(i_X + 2, i_Y - 2) == null)

				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X + 2);
					io_NextPlace.Add(i_Y - 2);
				}
			}
			if (i_X < Board.BoardSize - 1 && i_Y < Board.BoardSize - 1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.FIRST))//i can eat down and left
			{//++
				if (Board.getCellContent(i_X + 1, i_Y + 1) != null
					&& Board.getCellContent(i_X + 1, i_Y + 1).Player.ID != ID
					&& Board.getCellContent(i_X + 2, i_Y + 2) == null)

				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X + 2);
					io_NextPlace.Add(i_Y + 2);
				}
			}
			/*int moveSoldierUpOrDown = ID == e_PlayerID.FIRST ? -1 : 1;
			if (Board.getCellContent(i_X, i_Y).Rank == e_Rank.SOLDIER)
			{
				if (i_X < Board.BoardSize - 1 && i_Y< Board.BoardSize - 1 && i_Y > 1 
					&& Board.getCellContent(i_X + 1, i_Y + moveSoldierUpOrDown) != null
					&& Board.getCellContent(i_X + 1, i_Y + moveSoldierUpOrDown).Player.ID != ID
					&& Board.getCellContent(i_X + 2, i_Y + 2 * moveSoldierUpOrDown) == null)

				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X + 2);
					io_NextPlace.Add(i_Y + 2 * moveSoldierUpOrDown);
				}
				else if ( i_X > 1 &&  i_Y < Board.BoardSize - 1 && i_Y > 1 
					&& Board.getCellContent(i_X - 1, i_Y + moveSoldierUpOrDown) != null
					&& Board.getCellContent(i_X - 1, i_Y + moveSoldierUpOrDown).Player.ID != ID
					&& Board.getCellContent(i_X - 2, i_Y + 2 * moveSoldierUpOrDown) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 2);
					io_NextPlace.Add(i_Y + 2 * moveSoldierUpOrDown);
				}
			}
			else if (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING)
			{
				if (i_X < Board.BoardSize - 1 && i_Y < Board.BoardSize - 1 && i_Y > 1 
					&& Board.getCellContent(i_X + 1, i_Y + 1) != null
					&& Board.getCellContent(i_X + 1, i_Y + 1).Player.ID != ID
					&& Board.getCellContent(i_X + 2, i_Y + 2) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X + 2);
					io_NextPlace.Add(i_Y + 2);
				}
				else if (i_X > 1 && i_Y < Board.BoardSize - 1 && i_Y > 1 
					&& Board.getCellContent(i_X - 1, i_Y + 1) != null
					&& Board.getCellContent(i_X - 1, i_Y + 1).Player.ID != ID
					&& Board.getCellContent(i_X - 2, i_Y + 2) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 2);
					io_NextPlace.Add(i_Y + 2);
				}
				else if (i_X < Board.BoardSize - 1 && i_Y < Board.BoardSize - 1 && i_Y > 1 
					&& Board.getCellContent(i_X + 1, i_Y - 1) != null
					&& Board.getCellContent(i_X + 1, i_Y - 1).Player.ID != ID
					&& Board.getCellContent(i_X + 2, i_Y - 2) == null)
					{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X + 2);
					io_NextPlace.Add(i_Y - 1);
				}
				else if (i_X > 1 && i_Y < Board.BoardSize - 1 && i_Y > 1 
					&& Board.getCellContent(i_X - 1, i_Y - 1) != null
					&& Board.getCellContent(i_X - 1, i_Y - 1).Player.ID != ID
					&& Board.getCellContent(i_X - 2, i_Y - 2) == null)
					{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 2);
					io_NextPlace.Add(i_Y - 2);
				}
			}*/
		}
		public bool RunOnAllBoardGame(out List<int> o_CurrentPlace, out List<int> o_NextPlace)
		{
			o_CurrentPlace = new List<int>(0);
			o_NextPlace = new List<int>(0);
			

			for (int i = 0; i < Board.BoardSize; i++)
			{
				for (int j = 0; j < Board.BoardSize; j++)
				{
					if (Board.getCellContent(j, i) != null)
					{
						if (Board.getCellContent(j, i).Player.ID == ID)
						{
							CheckInPieceIfCanTOEat(j, i, ref o_CurrentPlace, ref o_NextPlace);
						}
					}
				}
			}
			return !(o_CurrentPlace.Count == 0);
		}
		public bool hasAnyMoves()
		{
			return true;
		}

		public bool isValidMove(Board i_Board, int i_OldX, int i_OldY, int i_NewX, int i_NewY)
		{
			bool checkIfTheMovIsGood = true;
			Board = i_Board;

			if (Board.getCellContent(i_OldX, i_OldY) == null || Board.getCellContent(i_OldX, i_OldY).Player.ID != ID)
			{
				checkIfTheMovIsGood = false;
			}
			else if (Board.getCellContent(i_NewX, i_NewY) != null)
			{

				checkIfTheMovIsGood = false;
			}

			else if (!checkTheMovGenericIsCorrect(Board.getCellContent(i_OldX, i_OldY).Rank, i_OldX, i_OldY, i_NewX, i_NewY))
			{
				checkIfTheMovIsGood = false;
			}


			return checkIfTheMovIsGood;
		}
		private bool checkTheMovGenericIsCorrect(e_Rank i_TypeSoldier, int i_OldX, int i_OldY, int i_NewX, int i_NewY)
		{
			bool answer = false;
			int moveSoldierUpOrDown = ID == e_PlayerID.FIRST ? 1 : -1; //('1' -> First, '-1' -> Second 
			bool isKing = i_TypeSoldier == e_Rank.KING;

			if (i_OldY == i_NewY + moveSoldierUpOrDown && (i_OldX == i_NewX + 1 || i_OldX == i_NewX - 1))
			///check if this is 1 step and the location is consecutive 
			{
				answer = true;
			}
			else if (i_OldY == i_NewY + 2 * moveSoldierUpOrDown && (i_OldX == i_NewX + 2 || i_OldX == i_NewX - 2) && // the ToX and ToY is distance with 2 from FromX and FromY.
				Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2) != null && Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2).Player.ID != ID) // check if in cell have enemy soldier
			///check if i can to jump on enemy soldier.
			{
				answer = true;
			}
			else if (isKing)
			{
				if (i_OldY == i_NewY - moveSoldierUpOrDown
					  && (i_OldX == i_NewX + 1 || i_OldX == i_NewX - 1))
				///check if this is 1 step and the location is consecutive 
				{
					answer = true;
				}
				else if (i_OldY == i_NewY - 2 * moveSoldierUpOrDown && (i_OldX == i_NewX + 2 || i_OldX == i_NewX - 2) && // the ToX and ToY is distance with 2 from FromX and FromY.
					Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2) != null && Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2).Player.ID != ID) // check if in cell have enemy soldier
				///check if i can to jump on enemy soldier.
				{
					answer = true;
				}
			}


			return answer;
		}
		public e_Eat isCanToEatAndTheMovIsCorrect(string i_MoveStrFromUser)
		{
			e_Eat checkIfTheMovInEating = e_Eat.CanToEatButNot;
			if (RunOnAllBoardGame(out List<int> currentPlace, out List<int> nextPlace))
			{
				for (int i = 0; i < currentPlace.Count && checkIfTheMovInEating == e_Eat.CanToEatButNot; i += 2)
				{
					if (currentPlace[i] == (int)i_MoveStrFromUser[0] - 'A' && currentPlace[i + 1] == (int)i_MoveStrFromUser[1] - 'a' &&
						nextPlace[i] == (int)i_MoveStrFromUser[3] - 'A' && nextPlace[i + 1] == (int)i_MoveStrFromUser[4] - 'a')
					{
						checkIfTheMovInEating = e_Eat.EAT;
					}
				}
			}
			else
			{
				checkIfTheMovInEating = e_Eat.NotCanEat;
			}
			return checkIfTheMovInEating;
		}

		public bool checkIfCanMoreEatAfterEat(string i_StrFromUser)
		{
			bool ret = true;
			List<int> currentPlace = new List<int>(0);
			List<int> nextPlace = new List<int>(0);
			CheckInPieceIfCanTOEat((int)i_StrFromUser[3] - 'A', (int)i_StrFromUser[4] - 'a', ref currentPlace, ref nextPlace);
			if (currentPlace.Count == 0)
			{
				ret = false;
			}
			return ret;
		}

		
	}
}
