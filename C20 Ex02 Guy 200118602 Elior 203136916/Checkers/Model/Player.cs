using System;
using System.Collections.Generic;

namespace Checkers.Model
{
    public enum e_EatStatus { EATISPOSSIBALEBUTNOTDONE, EAT, EATISNOTPOSSIBLE }

	public enum e_PlayerID { 
		FIRST, SECOND, TIE 
	}

	public class Player
	{

		public bool HasQuitted { get; set; }

		public string SignOfPlayer { get; set; }

		public int CountOfPiecesForPlayer { get; set; }
		private String m_Name;
		private e_PlayerID m_ID;
		private static Board s_Board;

		public Board Board
		{
			get
			{
				return s_Board;
			}

			set
			{
				s_Board = value;
			}

		}

		public int Score { get; set; }

		public Player(e_PlayerID i_PlayerID)
		{
			this.ID = i_PlayerID;
		}

		public void movePiece(Board i_Board, int i_OldX, int i_Oldy, int i_NewX, int i_NewY)
		{
			Board = i_Board;
			Board.updateBoardGame(this, i_OldX, i_Oldy, i_NewX, i_NewY);
		}

		public void Quit()
		{
			HasQuitted = true;
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

		public e_PlayerID GetOpponentID(Player i_Player)
		{
			if (i_Player.ID == e_PlayerID.FIRST)
			{
				return e_PlayerID.SECOND;
			}
			else
			{
				return e_PlayerID.FIRST;
			}
		}

		public void HasWonAndUpdateTheScore(Player i_OppPlayer, Board i_Board)
		{
			Board = i_Board;
			////the minimum point of winning a gaming is 1 . if player quits with more or same amunt of pieces so it's still 1.
			Score += Math.Max(1, updatePlayerPoints(Board) - i_OppPlayer.updatePlayerPoints(Board));
		}

		////counting the piecsed for player and if it's king it's equals 4 points
		private int updatePlayerPoints(Board i_Board)
		{
			Board = i_Board;
			int points = 0;
			for (int y = 0; y < Board.BoardSize; y++)
			{
				for (int x = 0; x < Board.BoardSize; x++)
				{
					if (Board.getCellContent(x, y) != null)
					{
						if (Board.getCellContent(x, y).Player.ID == ID && Board.getCellContent(x, y).Rank == e_Rank.SOLDIER)
						{
							points++;
						}
						else if (Board.getCellContent(x, y).Player.ID == ID && Board.getCellContent(x, y).Rank == e_Rank.KING)
						{
							points += 4;
						}
					}
				}
			}

			return points;
		}

		private void checkIfThereAreEatableMoveAndAddToList(int i_X, int i_Y, ref List<int> io_CurrentPlace, ref List<int> io_NextPlace)
		{
			if (i_X > 1 && i_Y > 1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.FIRST))//i can eat up and left
			{
				if (Board.getCellContent(i_X - 1, i_Y - 1) != null
					&& Board.getCellContent(i_X - 1, i_Y - 1).Player.ID != ID
					&& Board.getCellContent(i_X - 2, i_Y - 2) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 2);
					io_NextPlace.Add(i_Y - 2);
				}
			}

			if (i_X > 1 && i_Y < Board.BoardSize - 2 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.SECOND))//i can eat down and left
			{
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

			if (i_X < Board.BoardSize - 2 && i_Y > 1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.FIRST))//i can eat up and right
			{
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

			if (i_X < Board.BoardSize - 2 && i_Y < Board.BoardSize - 2 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.SECOND))//i can eat down and left
			{
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
		}

		public bool HasAnyMovesLeft(out List<int> o_CurrentPlace, out List<int> o_NextPlace)
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
							checkIfThereAreEatableMoveAndAddToList(j, i, ref o_CurrentPlace, ref o_NextPlace);
						}
					}
				}
			}

			return !(o_CurrentPlace.Count == 0);
		}

		public string GetRandomMoveAsStr()
		{
			List<int> currentPlace;
			List<int> nextPlace;
			string move = "";
			Random random = new Random();
			int resultRandom;
			if (HasAnyMovesLeft(out currentPlace, out nextPlace))
			{

				move = string.Format("{0}{1}>{2}{3}",
					(char)(currentPlace[0] + (int)'A'), (char)(currentPlace[1] + (int)'a'),
					(char)(nextPlace[0] + (int)'A'), (char)(nextPlace[1] + (int)'a'));
			}
			else
			{
				if (AnyMoveLeft(out currentPlace, out nextPlace))
				{
					resultRandom = random.Next((currentPlace.Count) / 2);
					move = string.Format("{0}{1}>{2}{3}",
						(char)(currentPlace[resultRandom * 2] + (int)'A'), (char)(currentPlace[(resultRandom * 2) + 1] + (int)'a'),
						(char)(nextPlace[resultRandom * 2] + (int)'A'), (char)(nextPlace[(resultRandom * 2) + 1] + (int)'a'));
				}
			}

			return move;
		}

		public bool AnyMoveLeft(out List<int> o_CurrentPlace, out List<int> o_NextPlace)
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
							checksIfThereAreValidMoveInAPiece(j, i, ref o_CurrentPlace, ref o_NextPlace);
						}
					}
				}
			}

			return !(o_CurrentPlace.Count == 0);
		}

		private void checksIfThereAreValidMoveInAPiece(int i_X, int i_Y, ref List<int> io_CurrentPlace, ref List<int> io_NextPlace)
		{
			if (i_X > 0 && i_Y > 0 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.FIRST))
			{
				if (Board.getCellContent(i_X - 1, i_Y - 1) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 1);
					io_NextPlace.Add(i_Y - 1);
				}
			}

			if (i_X > 0 && i_Y < Board.BoardSize - 1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.SECOND))
			{
				if (Board.getCellContent(i_X - 1, i_Y + 1) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X - 1);
					io_NextPlace.Add(i_Y + 1);
				}
			}

			if (i_X < Board.BoardSize - 1 && i_Y > 0 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.FIRST))//i can eat up and right
			{
				if (Board.getCellContent(i_X + 1, i_Y - 1) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X + 1);
					io_NextPlace.Add(i_Y - 1);
				}
			}

			if (i_X < Board.BoardSize - 1 && i_Y < Board.BoardSize - 1 && (Board.getCellContent(i_X, i_Y).Rank == e_Rank.KING || ID == e_PlayerID.SECOND))//i can eat down and left
			{
				if (Board.getCellContent(i_X + 1, i_Y + 1) == null)
				{
					io_CurrentPlace.Add(i_X);
					io_CurrentPlace.Add(i_Y);
					io_NextPlace.Add(i_X + 1);
					io_NextPlace.Add(i_Y + 1);
				}
			}
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
			////check if this is 1 step and the location is consecutive 
			{
				answer = true;
			}
			else if (i_OldY == i_NewY + 2 * moveSoldierUpOrDown && (i_OldX == i_NewX + 2 || i_OldX == i_NewX - 2) && // the ToX and ToY is distance with 2 from FromX and FromY.
				Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2) != null && Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2).Player.ID != ID) // check if in cell have enemy soldier
			////check if i can to jump on enemy soldier.
			{
				answer = true;
			}
			else if (isKing)
			{
				if (i_OldY == i_NewY - moveSoldierUpOrDown
					  && (i_OldX == i_NewX + 1 || i_OldX == i_NewX - 1))
				////check if this is 1 step and the location is consecutive 
				{
					answer = true;
				}
				else if (i_OldY == i_NewY - 2 * moveSoldierUpOrDown && (i_OldX == i_NewX + 2 || i_OldX == i_NewX - 2) && // the ToX and ToY is distance with 2 from FromX and FromY.
					Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2) != null && Board.getCellContent((i_OldX + i_NewX) / 2, (i_OldY + i_NewY) / 2).Player.ID != ID) // check if in cell have enemy soldier
				{
					answer = true;
				}
			}

			return answer;
		}

		public e_EatStatus GetTheEatableStatusOfMove(string i_MoveStrFromUser)
		{
			e_EatStatus checkIfTheMovInEating = e_EatStatus.EATISPOSSIBALEBUTNOTDONE;
			if (HasAnyMovesLeft(out List<int> currentPlace, out List<int> nextPlace))
			{
				for (int i = 0; i < currentPlace.Count && checkIfTheMovInEating == e_EatStatus.EATISPOSSIBALEBUTNOTDONE; i += 2)
				{
					if (currentPlace[i] == (int)i_MoveStrFromUser[0] - 'A' && currentPlace[i + 1] == (int)i_MoveStrFromUser[1] - 'a' &&
						nextPlace[i] == (int)i_MoveStrFromUser[3] - 'A' && nextPlace[i + 1] == (int)i_MoveStrFromUser[4] - 'a')
					{
						checkIfTheMovInEating = e_EatStatus.EAT;
					}
				}
			}
			else
			{
				checkIfTheMovInEating = e_EatStatus.EATISNOTPOSSIBLE;
			}

			return checkIfTheMovInEating;
		}

		public bool ChecksIfThereAreAnyEatableInStright(string i_StrFromUser)
		{
			bool ret = true;
			List<int> currentPlace = new List<int>(0);
			List<int> nextPlace = new List<int>(0);
			checkIfThereAreEatableMoveAndAddToList((int)i_StrFromUser[3] - 'A', (int)i_StrFromUser[4] - 'a', ref currentPlace, ref nextPlace);
			if (currentPlace.Count == 0)
			{
				ret = false;
			}

			return ret;
		}
	}
}
