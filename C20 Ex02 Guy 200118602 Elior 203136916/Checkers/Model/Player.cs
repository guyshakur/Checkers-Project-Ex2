using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{

	public enum e_PlayerID { FIRST, SECOND }
	class Player
	{


		public int CountOfPiecesForPlayer { get; set; }

		private String name;
		public Board Board { get; set; }
		public e_PlayerID ID { get; set; }

		public double Score { get; set; }

		public Player(e_PlayerID i_PlayerID)
		{
			this.ID = i_PlayerID;
			//this.Board = board;
			//CountOfPiecesForPlayer = ((board.BoardSize - 2) / 2) * (board.BoardSize / 2);


		}

		public void movePiece(Board board, int oldX, int oldY, int newX, int newY)
		{

			this.Board = board;

			Board.updateBoardGame(this, oldX, oldY, newX, newY);

			//while(Board.ChecksIfLegalMove()==false)
			// {
			// Board.UnvalidMoveMessage("");
			//

			//}


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

		public e_PlayerID GetOpponent(Player player)
		{
			if (player.ID == e_PlayerID.FIRST)
			{
				return e_PlayerID.SECOND;
			}
			else
			{
				return e_PlayerID.SECOND;
			}
		}
		private bool haveAJump(out List<int> o_CurrentPlace, out List<int> o_NextPlace)
		{
			bool have = false;
			int moveSoldierUpOrDown = ID == e_PlayerID.FIRST ? 1 : -1;
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
							if (Board.getCellContent(j, i).Rank == e_Rank.SOLDIER)
							{
								if (Board.getCellContent(j + 1, i + moveSoldierUpOrDown) != null
									&& Board.getCellContent(j + 1, i + moveSoldierUpOrDown).Player.ID != ID)
								{
									o_CurrentPlace.Add(j);
									o_CurrentPlace.Add(i);
									o_NextPlace.Add(j + 2);
									o_NextPlace.Add(i + 2 * moveSoldierUpOrDown);
									have = true;
								}
								else if (Board.getCellContent(j - 1, i + moveSoldierUpOrDown) != null
									&& Board.getCellContent(j - 1, i + moveSoldierUpOrDown).Player.ID != ID)
								{
									o_CurrentPlace.Add(j);
									o_CurrentPlace.Add(i);
									o_NextPlace.Add(j - 2);
									o_NextPlace.Add(i - 2 * moveSoldierUpOrDown);
									have = true;
								}
							}
							else if (Board.getCellContent(j, i).Rank == e_Rank.KING)
							{
								if (Board.getCellContent(j + 1, i + 1) != null
									&& Board.getCellContent(j + 1, i + 1).Player.ID != ID)
								{
									o_CurrentPlace.Add(j);
									o_CurrentPlace.Add(i);
									o_NextPlace.Add(j + 2);
									o_NextPlace.Add(i + 2);
									have = true;
								}
								else if (Board.getCellContent(j - 1, i + 1) != null
									&& Board.getCellContent(j - 1, i + 1).Player.ID != ID)
								{
									o_CurrentPlace.Add(j);
									o_CurrentPlace.Add(i);
									o_NextPlace.Add(j - 2);
									o_NextPlace.Add(i + 2 );
									have = true;
								}
								else if (Board.getCellContent(j + 1, i - 1 ) != null
									&& Board.getCellContent(j + 1, i - 1).Player.ID != ID)
								{
									o_CurrentPlace.Add(j);
									o_CurrentPlace.Add(i);
									o_NextPlace.Add(j + 2);
									o_NextPlace.Add(i - 1);
									have = true;
								}
								else if (Board.getCellContent(j - 1, i - 1) != null
									&& Board.getCellContent(j - 1, i - 1).Player.ID != ID)
								{
									o_CurrentPlace.Add(j);
									o_CurrentPlace.Add(i);
									o_NextPlace.Add(j - 2);
									o_NextPlace.Add(i - 2 );
									have = true;
								}
							}
						}
					}
				}

			}
			
			return have;
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

	}
}
