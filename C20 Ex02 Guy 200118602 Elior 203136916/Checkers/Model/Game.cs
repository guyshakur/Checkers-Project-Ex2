
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Checkers.Model
{
	public enum e_StatusOFGame { NOTFINISH, WIN, TIE };
	class Game
	{

		public e_StatusOFGame e_GameStatus { get; set; }
		public static bool GameEnded { get; set; }
		public bool IsQuited { get; set; }
		public e_PlayerID WinnerID { get; set; }
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

		public Game(Player player1, Player player2, Board board)
		{
			Player1 = player1;
			Player2 = player2;
			PlayerTurn = player1;
			Board = board;

			player1.CountOfPiecesForPlayer = ((board.BoardSize - 2) / 2) * (board.BoardSize / 2);
			player2.CountOfPiecesForPlayer = ((board.BoardSize - 2) / 2) * (board.BoardSize / 2);
		}

		public bool GameLoop()
		{
			GameEnded = false;
			List<int> currentPlace;
			List<int> nextPlace;
			if (!GameEnded)
			{
				//checkes if player 2 don't have any pieces and player 1 wins
				if (Player2.CountOfPiecesForPlayer == 0)
				{
					Player1.HasWonAndUpdateTheScore(Player2, Board);
					WinnerID = Player1.ID;
					GameEnded = true;
				}
				//checkes if player 1 don't have any pieces and player 2 wins
				else if (Player1.CountOfPiecesForPlayer == 0)
				{
					Player2.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player2.ID;
					GameEnded = true;
				}
				//checks if player 1 quit the game so player 2 wins
				else if (Player1.HasQuitted)
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player2.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player2.ID;
					GameEnded = true;
				}
				//checks if player 2 quit the game so player 1 wins
				else if (Player2.HasQuitted)
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player1.HasWonAndUpdateTheScore(Player2, Board);
					WinnerID = Player1.ID;
					GameEnded = true;
				}
				//checkes if player 2 don't have any moves but player 1 have so he player 1 wins
				else if (Player2.RunOnAllBoardGame(out currentPlace, out nextPlace) && Player2.allMoves(out currentPlace, out nextPlace) && !(Player1.RunOnAllBoardGame(out currentPlace, out nextPlace)) && !(Player1.allMoves(out currentPlace, out nextPlace)))
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player1.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player1.ID;
					GameEnded = true;
				}
				//checks if player 1 don't have any moves but player to haves so player 2 wins.
				else if (Player2.RunOnAllBoardGame(out currentPlace, out nextPlace) && Player2.allMoves(out currentPlace, out nextPlace) && !(Player1.RunOnAllBoardGame(out currentPlace, out nextPlace)) && !(Player1.allMoves(out currentPlace, out nextPlace)))
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player2.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player2.ID;
					GameEnded = true;
				}


				//if any of this moves don't happend and there are no moves for anone it's a tie
				else
				{
					e_GameStatus = e_StatusOFGame.TIE;
				}
			}
			return GameEnded;
		}


		//public void PlayerQuited(e_PlayerID i_ID)
		//{

		//	if (Player1.ID == i_ID)
		//	{
		//		Player2.HasWonAndUpdateTheScore(Player1, Board);
		//	}
		//	else
		//	{
		//		Player1.HasWonAndUpdateTheScore(Player2, Board);
		//	}
		//}

		public void RefreshBoardGame()
		{
			Board = new Board(Board.BoardSize, Player1, Player2);
			Player1.CountOfPiecesForPlayer = ((Board.BoardSize - 2) / 2) * (Board.BoardSize / 2);
			Player2.CountOfPiecesForPlayer = ((Board.BoardSize - 2) / 2) * (Board.BoardSize / 2);
			Player1.HasQuitted = false;
			Player2.HasQuitted = false;

		}
	}
}
