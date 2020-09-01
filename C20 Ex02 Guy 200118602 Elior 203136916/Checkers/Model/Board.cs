using Checkers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{

	class Board
	{
		public static Piece[,] BoardGame { get; set; }

		private static int initialFilledRowsForPlayer = 0;
		public static int InitialFilledRowsForPlayer
		{
			get
			{
				return initialFilledRowsForPlayer;
			}

			set
			{
				if (value == 10 || value == 8 || value == 6)
				{
					initialFilledRowsForPlayer = value;
				}

				else
				{
					initialFilledRowsForPlayer = 8;
				}
			}
		}
		public Board(int boardSize, Player player1, Player player2)
		{
			BoardGame = new Piece[boardSize, boardSize];

			initialFilledRowsForPlayer = (boardSize - 2) / 2;

			bool fillEvenCells = true;

			for (int y = 0; y < boardSize; y++)
			{
				for (int x = 0; x < boardSize; x++)
				{
					BoardGame[x, y] = null;
				}
			}

			for (int y = 0; y < initialFilledRowsForPlayer; y++)
			{
				fillEvenCells = (((y + 1) % 2) == 0);
				for (int x = 0; x < boardSize; x++)
				{
					if (fillEvenCells)
					{
						BoardGame[x, y] = (x % 2 == 0) ? new Piece(player2, x, y) : null;
					}
					else
					{
						BoardGame[x, y] = (x % 2 == 0) ? null : new Piece(player2, x, y);
					}
				}
			}


			for (int y = boardSize - 1; y > boardSize - initialFilledRowsForPlayer - 1; y--)
			{
				fillEvenCells = (boardSize - 1 - y) % 2 == 0;
				for (int x = 0; x < boardSize; x++)
				{
					if (fillEvenCells)
					{
						BoardGame[x, y] = (x % 2 == 0) ? new Piece(player1, x, y) : null;
					}
					else
					{
						BoardGame[x, y] = (x % 2 == 0) ? null : new Piece(player1, x, y);
					}
				}
			}


		}

		public Piece getCellContent(int x, int y)
		{

			return BoardGame[x, y];
		}

		public void updateBoardGame(Player player, int oldX, int oldY, int newX, int newY)
		{
			BoardGame[oldX, oldY] = null;
			BoardGame[newX, newY] = new Piece(player, newX, newY);
		}


		public bool ChecksIfLegalMove(Player i_PlayerMoved, int i_FromX, int i_FromY, int i_ToX, int i_ToY)
		{
			bool checkIfTheMovIsGood = true;
			int move = i_PlayerMoved.ID == e_PlayerID.FIRST ? 1 : -1;
			if (BoardGame[i_FromY, i_FromX] == null)// check if in fromCell is not empty
			{
				checkIfTheMovIsGood = false;
				Console.WriteLine("The soldier in a cell you want moved is empty. please try again");

			}
			else if (BoardGame[i_FromY, i_FromX].Player.ID != i_PlayerMoved.ID)// check if in the cell, have a soldier of player 
			{
				checkIfTheMovIsGood = false;
				Console.WriteLine("The soldier in a cell you want moved is not your soldier. please try again");
			}
			else if (BoardGame[i_ToY, i_ToX] != null) // check if in the toCell is Empty
			{
				checkIfTheMovIsGood = false;
				Console.WriteLine("In cell you want to move the soldier is not Empty. please try again");
			}
			//CHECK IF THE MOV IS CORRECT
			else if (BoardGame[i_FromY, i_FromX].Rank == e_Rank.SOLDIER)//the soldier is SOLDIER (Just 1 side)
			{
				if (!(i_FromY == i_ToY + move/*(1 -> First, -1 -> Second*/ && (i_FromX == i_ToX + 1 || i_FromX == i_ToX - 1)))//the next cell is not correct
				{
					checkIfTheMovIsGood = false;
					Console.WriteLine("The next cell is not correct for the soldier. please try again");
				}
			}
			else if (BoardGame[i_FromY, i_FromX].Rank == e_Rank.KING)//the soldier is King (UP and DOWN)
			{
				if (!((i_FromY == i_ToY + 1 || i_FromY == i_ToY - 1) && (i_FromX == i_ToX + 1 || i_FromX == i_ToX - 1)))//the next cell is not correct
				{
					checkIfTheMovIsGood = false;
					Console.WriteLine("The next cell is not correct for the King. please try again");
				}
			}

			//else if()

			return checkIfTheMovIsGood;
		}
	}
}

