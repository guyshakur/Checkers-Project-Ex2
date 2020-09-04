using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Checkers.Model
{
	public enum e_StatusOFGame
	{
		NOTFINISH, WIN, TIE
	}

	public class Game
	{
		public static bool GameEnded { get; set; }

		public e_StatusOFGame e_GameStatus { get; set; }

		public bool IsQuited { get; set; }

		public e_PlayerID WinnerID { get; set; }

		public Player Player1 { get; set; }

		public Player Player2 { get; set; }

		public Player PlayerTurn { get; set; }

		public Board Board { get; set; }

		public Player GetOpponent(Player i_Player)
		{
			if (i_Player.ID == e_PlayerID.FIRST)
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
				
				if (Player2.CountOfPiecesForPlayer == 0)
				{
					Player1.HasWonAndUpdateTheScore(Player2, Board);
					WinnerID = Player1.ID;
					GameEnded = true;
				}
				else if (Player1.CountOfPiecesForPlayer == 0)
				{
					Player2.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player2.ID;
					GameEnded = true;
				}
				else if (Player1.HasQuitted)
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player2.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player2.ID;
					GameEnded = true;
				}
				else if (Player2.HasQuitted)
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player1.HasWonAndUpdateTheScore(Player2, Board);
					WinnerID = Player1.ID;
					GameEnded = true;
				}
				else if (Player2.HasAnyMovesLeft(out currentPlace, out nextPlace) && Player2.AnyMoveLeft(out currentPlace, out nextPlace) && !(Player1.HasAnyMovesLeft(out currentPlace, out nextPlace)) && !(Player1.AnyMoveLeft(out currentPlace, out nextPlace)))
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player1.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player1.ID;
					GameEnded = true;
				}
				else if (Player2.HasAnyMovesLeft(out currentPlace, out nextPlace) && Player2.AnyMoveLeft(out currentPlace, out nextPlace) && !(Player1.HasAnyMovesLeft(out currentPlace, out nextPlace)) && !(Player1.AnyMoveLeft(out currentPlace, out nextPlace)))
				{
					e_GameStatus = e_StatusOFGame.WIN;
					Player2.HasWonAndUpdateTheScore(Player1, Board);
					WinnerID = Player2.ID;
					GameEnded = true;
				}
				else
				{
					e_GameStatus = e_StatusOFGame.TIE;
				}
			}

			return GameEnded;
		}

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
