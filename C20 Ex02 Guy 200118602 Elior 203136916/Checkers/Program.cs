using System;
using System.Text;
using Checkers.Model;
using Ex02.ConsoleUtils;


namespace Checkers
{
	class Program
	{
		private static Board s_Board;
		private static int s_BoardSize;
		private static int s_NumOfPlayers;
		private static Player s_Player1;
		private static Player s_Player2;

		public static Board Board
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
		public static int BoardSize
		{
			get
			{
				return s_BoardSize;
			}
			set
			{
				s_BoardSize = value;
			}

		}
		public static int NumOfPlayers 
		{ 
			get 
			{ 
				return s_NumOfPlayers; 
			}
			set
			{
				if(value == 1 || value == 2)
				{
					s_NumOfPlayers = value;
				}
				else
				{
					s_NumOfPlayers = 1;
				}
			}
		}

		public static Player Player1
		{
			get
			{
				return s_Player1;
			}
			set
			{
				s_Player1 = value;
			}
		}

		public static Player Player2
		{
			get
			{
				return s_Player2;
			}
			set
			{
				s_Player2 = value;
			}
		}



		public static void Main()
		{
			checkers(); 
		}
		private static void checkers()
		{
			
			Player1 = new Player(e_PlayerID.FIRST,Board);
			Player2 = new Player(e_PlayerID.SECOND,Board);

			GameView.InitiaizeGame();

			Game game = new Game();
			BoardSize = Board.InitialFilledRowsForPlayer;
			Board = new Board(BoardSize, Player1, Player2);
			PrintBoard(Player1);
			
		}
		private static void nextTurn(Player i_ThePlayerIsTurn)
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
					turn[0] < 'A' || turn[0] > (char)(BoardSize + (int)'A' -1) ||
					turn[1] < 'a' || turn[1] > (char)(BoardSize + (int)'a' -1) ||
					turn[2] != '>' ||
					turn[3] < 'A' || turn[3] > (char)(BoardSize + (int)'A' -1)||
					turn[4] < 'a' || turn[4] > (char)(BoardSize + (int)'a' -1))
				{
					checkIfReadGood = false;
					Console.WriteLine("The move is wrong please try again:");
				}
			} while (!checkIfReadGood);
			//i_ThePlayerIsTurn.
			///we need to call func in model to check if the move is good and move the soldier in board.
			///and in thr fun to call to PrintBoard after the soldier moved.
			///
			i_ThePlayerIsTurn.movePiece(Board, (int)turn[0] - 'A' + 1, (int)turn[1] - 'a' + 1, (int)turn[3] - 'A' + 1, (int)turn[4] - 'a' + 1);
		}
		private static void PrintBoard(Player i_ThePlayerIsNextTurn)
		{
			Screen.Clear();			
			string boardHeader = "   A   B   C   D   E   F   G   H   I   J";
			StringBuilder paint = new StringBuilder("");
			paint.AppendLine(" " + boardHeader.Substring(0, BoardSize * 4));
			paint.AppendLine("  " + new String('=', BoardSize * 4 + 1));

			string boardSideHeader = "abcdefghij";
			for (int y = 0; y < BoardSize; y++)
			{
				paint.Append(boardSideHeader.Substring(y, 1) + " ");
				paint.Append("|");
				for (int x = 0; x < BoardSize; x++)
				{
					paint.Append(" " + getCellAsString(Board.getCellContent(x, y)) + " ");
					paint.Append("|");
				}
				if (y == 0)
				{
					paint.Append("  Player2: " + Player2.Name);
				}
				if (y == BoardSize - 1)
				{
					paint.Append("  Player1: " + Player1.Name);
				}
				paint.AppendLine();
				paint.AppendLine("  " + new String('=', BoardSize * 4 + 1));
			}
			paint.AppendLine();
			paint.Append(i_ThePlayerIsNextTurn.Name + "'s turn :");
			paint.AppendLine();
			Console.WriteLine(paint);
			nextTurn(i_ThePlayerIsNextTurn);
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
	}
}
