using System;
using Ex02.ConsoleUtils;
using Checkers.Model;
using System.Text;

namespace Checkers
{
	class GameView
	{
		private static int s_NumOfPlayers;
		private static Game s_Game;
		private static string s_LastMoveStr = null;
		private static string s_SignOfPlayerPiece = null;
		private static bool s_MoreEating = false;
		public static Board Board { get; set; }
		public static int BoardSize { get; set; }
		public static int NumOfPlayers
		{
			get
			{
				return s_NumOfPlayers;
			}
			set
			{
				if (value == 1 || value == 2)
				{
					s_NumOfPlayers = value;
				}
				else
				{
					s_NumOfPlayers = 1;
				}
			}
		}
		public static Player Player1 { get; set; }
		public static Player Player2 { get; set; }
		public static void InitiaizeGame()
		{
			Screen.Clear();
			Console.WriteLine("         Checkers Game");
			Console.WriteLine();
			getUserInitialInput();
		}
		
		private static void getUserInitialInput()
		{
			Player1.Name = getFromUser("Enter your name (Max size 20 without spaces): ", 20);
			Board.InitialFilledRowsForPlayer = int.Parse(getFromUser("Choose board size (6,8,10): ", 6, 8, 10));
			NumOfPlayers = int.Parse(getFromUser("Enter number of players (1,2): ", 1, 2));
			if (NumOfPlayers == 2)
			{
				Player2.Name = getFromUser("Enter your name (Max size 20 without spaces): ", 20);
			}
			else
			{
				Player2.Name = "Computer";
			}
		}
		private static String getFromUser(string i_Msg, params int[] i_Numbers)
		{
			//if in numers have just 1 -> name of user.
			//if in number have 2 numbers -> numbers of players.
			//if in number have 3 numbers -> size of board.
			string result = "";
			int players = 0;
			do
			{
				Console.WriteLine(i_Msg);
				result = Console.ReadLine();
			} while ((i_Numbers.Length == 1 &&
						(result.Length >= i_Numbers[0] || result.Contains(" ") || result.Length < 1)) ||
					(i_Numbers.Length == 2 &&
						(!int.TryParse(result, out players) ||
							(players != i_Numbers[0] && players != i_Numbers[1]))) ||
					(i_Numbers.Length == 3 &&
						(!int.TryParse(result, out players) ||
							(players != i_Numbers[0] && players != i_Numbers[1] && players != i_Numbers[2]))));
			return result;
		}
		public static void startCheckerGame()
		{
			bool matchIsOver = false;
			Player1 = new Player(e_PlayerID.FIRST);
			Player2 = new Player(e_PlayerID.SECOND);
			Player1.SignOfPlayer = "X";
			Player2.SignOfPlayer = "O";
			InitiaizeGame();
			BoardSize = Board.InitialFilledRowsForPlayer;
			Board = new Board(BoardSize, Player1, Player2);
			s_Game = new Game(Player1, Player2, Board);

			while (!matchIsOver)
			{
				//running the loop of game 
				while (!s_Game.GameLoop())
				{
					
					PrintBoard();
					playerMoveView(s_Game.PlayerTurn);
				}
				printScoreAndAskForMoreGame();
				Console.WriteLine();
				Console.WriteLine("Do you want to have a rematch? Y / N");
				bool invalidInput = false;
				do
				{
					string userInput = Console.ReadLine();
					if(userInput=="Y" || userInput=="y")
                    {
						//s_Game.RefreshBoardGame();
						Game.GameEnded = false;
						invalidInput = true;
						s_Game.PlayerTurn = Player1;
						Board = new Board(BoardSize, Player1, Player2);
						PrintBoard();
						//playerMoveView(s_Game.PlayerTurn);

					}
					
				} while (!invalidInput);
			}
		}


		
		private static void printScoreAndAskForMoreGame()
		{
			Console.WriteLine();
			Console.WriteLine();
			if (s_Game.WinnerID == e_PlayerID.FIRST)
			{
				Console.WriteLine("The Winner is: " + Player1.Name);
			}
			else if (s_Game.WinnerID == e_PlayerID.SECOND)
			{
				Console.WriteLine("The Winner is: " + Player2.Name);
			}
			else
			{
				Console.WriteLine("It's A Tie!");
			}
			Console.WriteLine("The Score is: " + Player1.Name + ": " + Player1.Score);
			Console.WriteLine("The Score is: " + Player2.Name + ": " + Player2.Score);
		}
		private static void playerMoveView(Player i_ThePlayerIsTurn)
		{
			bool isExit = false;///change
			if (i_ThePlayerIsTurn.ID == e_PlayerID.FIRST)
			{
				s_SignOfPlayerPiece = "X";
			}
			else
			{
				s_SignOfPlayerPiece = "O";

			}
			if (s_LastMoveStr != null)
			{
				if (s_MoreEating)
				{
					Console.WriteLine(s_Game.PlayerTurn.Name + " Move's was " + "(" + s_Game.PlayerTurn.SignOfPlayer + "): " + s_LastMoveStr);
					s_MoreEating = false;
				}
				else
				{
					Console.WriteLine(s_Game.GetOpponent(s_Game.PlayerTurn).Name + " Move's was " + "(" + s_Game.GetOpponent(s_Game.PlayerTurn).SignOfPlayer + "): " + s_LastMoveStr);
				}
			}
			Console.WriteLine(i_ThePlayerIsTurn.Name + "'s Turn " + "(" + s_Game.PlayerTurn.SignOfPlayer + "):");
			Console.WriteLine();
			Console.WriteLine("Press Q to quit");
			String MoveStrFromUser;
			bool checkIfReadGood;
			e_Eat theMoveIsEating = e_Eat.NotCanEat;
			if (s_NumOfPlayers == 1 && i_ThePlayerIsTurn.ID == e_PlayerID.SECOND)
			{
				MoveStrFromUser = i_ThePlayerIsTurn.RandomMove();
				if (MoveStrFromUser == "")
				{
					/// no more move for computer player.
					Console.WriteLine("No more moves. the turn move the the Enemy player.");
				}
			}
			else
			{
				do
				{
					checkIfReadGood = true;
					theMoveIsEating = e_Eat.NotCanEat;
					MoveStrFromUser = Console.ReadLine();

					if (MoveStrFromUser.Contains("Q"))
					{
						s_Game.IsQuited = true;
						i_ThePlayerIsTurn.Quit();
						//s_Game.PlayerQuited(i_ThePlayerIsTurn.ID);
						isExit = true;
						checkIfReadGood = true;
					}
					if (!isExit)
					{ ///change
						if (MoveStrFromUser.Length != 5 ||
							MoveStrFromUser[0] < 'A' || MoveStrFromUser[0] > (char)(BoardSize + (int)'A' - 1) ||
							MoveStrFromUser[1] < 'a' || MoveStrFromUser[1] > (char)(BoardSize + (int)'a' - 1) ||
							MoveStrFromUser[2] != '>' ||
							MoveStrFromUser[3] < 'A' || MoveStrFromUser[3] > (char)(BoardSize + (int)'A' - 1) ||
							MoveStrFromUser[4] < 'a' || MoveStrFromUser[4] > (char)(BoardSize + (int)'a' - 1)
							|| (!i_ThePlayerIsTurn.isValidMove(Board, (int)MoveStrFromUser[0] - (int)'A', (int)MoveStrFromUser[1] - (int)'a', (int)MoveStrFromUser[3] - 'A', (int)MoveStrFromUser[4] - 'a')))
						{
							checkIfReadGood = false;
							Console.WriteLine("The move is wrong please try again:");
						}
						else
						{
							theMoveIsEating = i_ThePlayerIsTurn.isCanToEatAndTheMovIsCorrect(MoveStrFromUser);
							if (theMoveIsEating == e_Eat.CanToEatButNot)
							{
								checkIfReadGood = false;
								Console.WriteLine("You need to eat please try again:");
								//theMoveIsEating = e_Eat.NotCanEat;
							}
						}
						///change
					}
				} while (!checkIfReadGood);

				//i_ThePlayerIsTurn.
				///we need to call func in model to check if the move is good and move the soldier in board.
				///and in thr fun to call to PrintBoard after the soldier moved.
				///
			}
			if (!isExit)///change
			{///change
				i_ThePlayerIsTurn.movePiece(Board, (int)MoveStrFromUser[0] - (int)'A', (int)MoveStrFromUser[1] - (int)'a', (int)MoveStrFromUser[3] - 'A', (int)MoveStrFromUser[4] - 'a');
				if (!((theMoveIsEating == e_Eat.EAT) && (i_ThePlayerIsTurn.checkIfCanMoreEatAfterEat(MoveStrFromUser))))
				{
					s_Game.PlayerTurn = s_Game.GetOpponent(i_ThePlayerIsTurn);

				}
				else
				{
					s_MoreEating = true;
				}

				s_LastMoveStr = MoveStrFromUser;
				PrintBoard();
			}///change
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
				if (s_LastMoveStr != null)
				{
					Console.WriteLine(s_Game.PlayerTurn.Name + "Move's was : " + s_LastMoveStr);
					s_MoreEating = false;
				}
				Console.WriteLine(s_Game.PlayerTurn.Name + " Turn:");
				playerMoveView(s_Game.PlayerTurn);
			}
		}
		private static string getCellAsString(Piece i_GamePiece)
		{
			string stringReturn = " ";
			if (i_GamePiece == null)
			{
				stringReturn = " ";
			}
			else if (i_GamePiece.Player.ID == e_PlayerID.FIRST)
			{
				stringReturn = i_GamePiece.Rank == e_Rank.SOLDIER ? "X" : "K";

			}
			else if (i_GamePiece.Player.ID == e_PlayerID.SECOND)
			{
				stringReturn = i_GamePiece.Rank == e_Rank.SOLDIER ? "O" : "U";
			}
			return stringReturn;
		}
	}
}
