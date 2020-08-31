using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;
using Ex02.ConsoleUtils;


namespace Checkers
{
	class Program
	{
		private static int boardSize;
		private static int numOfPlayers;
		private static Player player1;
		private static Player player2;

		public static int NumOfPlayers 
		{ 
			get 
			{ 
				return numOfPlayers; 
			}
			set
			{
				if(value == 1 || value == 2)
				{
					numOfPlayers = value;
				}
				else
				{
					numOfPlayers = 1;
				}
			}
		}

		public static Player Player1
		{
			get
			{
				return player1;
			}
		}

		public static Player Player2
		{
			get
			{
				return player2;
			}
		}



		public static void Main()
		{
			checkers(); 
		}
		private static void checkers()
		{
			
			player1 = new Player(e_PlayerID.FIRST);
			player2 = new Player(e_PlayerID.SECOND);

			GameView.initiaizeGame();

			Game game = new Game();
			boardSize = Board.InitialFilledRowsForPlayer;

			PrintBoard(Player1);
			
		}
		private static void NextTurn(Player i_ThePlayerIsTurn)
		{
			String turn;
			bool checkIfReadGood;
			do
			{
				checkIfReadGood = true;
				turn = Console.ReadLine();
				if(turn.Contains("Q"))
				{
					i_ThePlayerIsTurn.Quit();   
				}
				if(turn.Length!=5 || 
					turn[0] < 'A' || turn[0] > (char)(boardSize + (int)'A' -1) ||
					turn[1] < 'a' || turn[1] > (char)(boardSize + (int)'a' -1) ||
					turn[2] != '>' ||
					turn[3] < 'A' || turn[3] > (char)(boardSize + (int)'A' -1)||
					turn[4] < 'a' || turn[4] > (char)(boardSize + (int)'a' -1))
				{
					checkIfReadGood = false;
					Console.WriteLine("The move is wrong please try again:");
				}
			} while (!checkIfReadGood);
			//i_ThePlayerIsTurn.
			///we need to call func in model to check if the move is good and move the soldier in board.
			///and in thr fun to call to PrintBoard after the soldier moved.
		}
		private static void PrintBoard(Player i_ThePlayerIsNextTurn)
		{
			Screen.Clear();
			Board model = new Board(boardSize, Player1, Player2);
			string boardHeader = "   A   B   C   D   E   F   G   H   I   J";
			StringBuilder paint = new StringBuilder("");

			paint.AppendLine(" " + boardHeader.Substring(0, boardSize * 4));
			//Console.WriteLine(" " + boardHeader.Substring(0, boardSize * 4));
			paint.AppendLine("  " + new String('=', boardSize * 4 + 1));
			//Console.WriteLine("  " + new String('=', boardSize * 4 + 1));


			string boardSideHeader = "abcdefghij";
			for (int y = 0; y < boardSize; y++)
			{
				paint.Append(boardSideHeader.Substring(y, 1) + " ");
				//Console.Write(boardSideHeader.Substring(y, 1) + " ");
				paint.Append("|");
				//Console.Write("|");
				for (int x = 0; x < boardSize; x++)
				{
					paint.Append(" " + getCellAsString(model.getCellContent(x, y)) + " ");
					//Console.Write(" " + getCellAsString(model.getCellContent(x, y)) + " ");
					paint.Append("|");
					//Console.Write("|");
				}
				if (y == 0)
				{
					paint.Append("  Player2: " + Player2.Name);
					//Console.Write("  Player2: " + Player2.Name); 
				}
				if (y == boardSize - 1)
				{
					paint.Append("  Player1: " + Player1.Name);
					//Console.Write("  Player1: " + Player1.Name);
				}
				paint.AppendLine();
				//Console.WriteLine();
				paint.AppendLine("  " + new String('=', boardSize * 4 + 1));
				//Console.WriteLine("  " + new String('=', boardSize * 4 + 1));
			}
			paint.AppendLine();
			//Console.WriteLine();
			paint.Append(i_ThePlayerIsNextTurn.Name + "'s turn :");
			//Console.Write(Player1.Name + "'s turn :");
			paint.AppendLine();
			//Console.WriteLine();
			Console.WriteLine(paint);
			NextTurn(i_ThePlayerIsNextTurn);
		}

		private static string getCellAsString(Piece i_GamePiece)
		{
			string stringReturn = " ";

			if(i_GamePiece==null)
			{
				stringReturn  = " ";
			}
			else if(i_GamePiece.Player.ID==e_PlayerID.FIRST)
			{
				stringReturn  = i_GamePiece.Rank==e_Rank.SOLDIER ? "X" : "K";
			}
			else if (i_GamePiece.Player.ID == e_PlayerID.SECOND)
			{
				stringReturn  = i_GamePiece.Rank == e_Rank.SOLDIER ? "O" : "U";
			}
			
			return stringReturn;
		}

		private static int getIntFromUser(string msg, params int[] validInputs)
		{
			int result = 0;
			bool validNum = false;
			while (!validNum)
			{
				Console.Write(msg);
				string numAsStr = Console.ReadLine();
				if (Int32.TryParse(numAsStr, out result))
				{
					foreach (int validInput in validInputs)
					{
						if (result == validInput)
						{
							validNum = true;
						}
					}

				}
			}
			return result;
		}
	}
}
