using System;
using System.Runtime.CompilerServices;
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
		private static Game s_Game;
		private static string lastMoveStr=null;
		private static string signOfPlayerPiece = null;

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
			checkersGame(); 
		}
		private static void checkersGame()
		{
			
			Player1 = new Player(e_PlayerID.FIRST);
			Player2 = new Player(e_PlayerID.SECOND);

			GameView.InitiaizeGame();

			s_Game = new Game(Player1,Player2);
			BoardSize = Board.InitialFilledRowsForPlayer;
			Board = new Board(BoardSize, Player1, Player2);
			
			while(s_Game.PlayerTurn.hasAnyMoves())
            {
				PrintBoard();
				playerMoveView(s_Game.PlayerTurn);
			}

			

		}
		private static void playerMoveView(Player i_ThePlayerIsTurn)
		{
			if(i_ThePlayerIsTurn.ID== e_PlayerID.FIRST)
            {
				signOfPlayerPiece = "X";
            }
            else
            {
				signOfPlayerPiece = "O";

			}
			if (lastMoveStr != null)
			{
				Console.WriteLine(s_Game.GetOpponent(s_Game.PlayerTurn).Name + " Move's was " +"("+ signOfPlayerPiece+"): "+ lastMoveStr);
			}

			Console.WriteLine(i_ThePlayerIsTurn.Name + "'s Turn: "+"("+signOfPlayerPiece+")");
			String MoveStrFromUser;
			bool checkIfReadGood;
			do
			{
				checkIfReadGood = true;
				MoveStrFromUser = Console.ReadLine();

				if(MoveStrFromUser.Contains("Q"))
				{
					i_ThePlayerIsTurn.Quit();   
				}
				if(MoveStrFromUser.Length!=5 || 
					MoveStrFromUser[0] < 'A' || MoveStrFromUser[0] > (char)(BoardSize + (int)'A' -1) ||
					MoveStrFromUser[1] < 'a' || MoveStrFromUser[1] > (char)(BoardSize + (int)'a' -1) ||
					MoveStrFromUser[2] != '>' ||
					MoveStrFromUser[3] < 'A' || MoveStrFromUser[3] > (char)(BoardSize + (int)'A' -1)||
					MoveStrFromUser[4] < 'a' || MoveStrFromUser[4] > (char)(BoardSize + (int)'a' -1)
					|| (!i_ThePlayerIsTurn.isValidMove(Board, (int)MoveStrFromUser[0]-(int)'A', (int)MoveStrFromUser[1] - (int)'a', (int)MoveStrFromUser[3] - 'A', (int)MoveStrFromUser[4] - 'a')))

				{
					
					checkIfReadGood = false;
					Console.WriteLine("The move is wrong please try again:");
				}
				
			} while (!checkIfReadGood);
			//i_ThePlayerIsTurn.
			///we need to call func in model to check if the move is good and move the soldier in board.
			///and in thr fun to call to PrintBoard after the soldier moved.
			///
			i_ThePlayerIsTurn.movePiece(Board, (int)MoveStrFromUser[0]-(int)'A',(int)MoveStrFromUser[1]-(int)'a',(int)MoveStrFromUser[3]-'A',(int)MoveStrFromUser[4]-'a') ;
			s_Game.PlayerTurn = s_Game.GetOpponent(i_ThePlayerIsTurn);
			lastMoveStr = MoveStrFromUser;
			PrintBoard();
		}
		private static void PrintBoard()
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
			paint.AppendLine();
			Console.WriteLine(paint);
			


		}

		public static void PrintTheNextMoveOfPlayer()
        {
			
			while (s_Game.PlayerTurn.hasAnyMoves())
            {
				//s_Game.PlayerTurn = s_Game.GetOpponent(s_Game.PlayerTurn);
				//nextTurn(s_Game.PlayerTurn);
				if (lastMoveStr != null)
				{
					Console.WriteLine(s_Game.PlayerTurn.Name + "Move's was : " + lastMoveStr);

				}
				Console.WriteLine(s_Game.PlayerTurn.Name + " Turn:");
				
				playerMoveView(s_Game.PlayerTurn);
				
			}
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
