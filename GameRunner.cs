using Loggerc;
namespace Checker
{
	public class GameRunner
	{
		private Dictionary<IPlayer, List<IPiece>> _playerPieceSet;
		private IBoard _board;
		private readonly Rules _rules;
		private IPlayer _currentPlayer;
		// private ILogger logger;

		public GameRunner()
		{
			_rules = new Rules();
			Logger.Configure();


		}

		public void CreateBoard(int row, int column)
		{
			int[,] matrix = new int[row, column];
			_board = new Board(matrix);

		}

		//langsung ngambil dari variabel
		public void SwitchTurn()
		{
			IPlayer[] players = _playerPieceSet.Keys.ToArray();

			if (_currentPlayer == players[0])
			{
				_currentPlayer = players[1];
			}
			else
			{
				_currentPlayer = players[0];
			}
		}


   		private static void InitialPiece(List<IPiece> player1Pieces, int row, int col, PieceType pieceType)
		{
			Position initialPos = new Position(row, col);
			IPiece piece = new Piece(pieceType, initialPos);
			player1Pieces.Add(piece);
		}
		
		public void InitializePieceOnBoard(string player1Name, string player2Name)
		{
			_playerPieceSet = new Dictionary<IPlayer, List<IPiece>>();
			
			IPlayer player1 = new Player(player1Name);
			IPlayer player2 = new Player(player2Name);

			List<IPiece> player1Pieces = new List<IPiece>();
			List<IPiece> player2Pieces = new List<IPiece>();

			for (int row = 0; row <= 2; row++)
			{
				for (int col = 0; col <= _board.GetMatrix().Length; col += 2)
				{
					if (row % 2 == 0)
					{///refactor
						InitialPiece(player1Pieces, row, col, PieceType.BM);
						
					}
					else
					{
						InitialPiece(player1Pieces, row, col + 1, PieceType.BM);
					}
				}

			}


			for (int row = 5; row <= _board.GetMatrix().Length; row++)
			{
				for (int col = 0; col <= _board.GetMatrix().Length; col += 2)
				{
					if (row % 2 != 0)
					{
						InitialPiece(player2Pieces, row, col + 1, PieceType.WM);

					}
					else
					{
						InitialPiece(player2Pieces, row, col, PieceType.WM);
					}
				}

			}


			_playerPieceSet.Add(player1, player1Pieces);
			_playerPieceSet.Add(player2, player2Pieces);

			_currentPlayer = player1;

		}

	 


		public IPiece GetPieceOnPosition(Position earlyPos)
		{

			foreach (var pieces in _playerPieceSet.Values)
			{
				foreach (var piece in pieces)
				{
					if (piece.GetPosition().GetRow() == earlyPos.GetRow() && piece.GetPosition().GetColumn() == earlyPos.GetColumn())
					{
						return piece;
					}
				}
			}

			return null;

		}

		public IPlayer GetPlayerFromPiece(IPiece piece)
		{
			foreach (var player in _playerPieceSet.Keys)
			{
				if (_playerPieceSet[player].Contains(piece))
				{
					return player;
				}
			}

			return null;
		}
		
		public IPlayer GetPlayer()
		
		{
			return _currentPlayer ;
		}
		public Position GetCapturePosition(IPiece piece, Position currentPosition)
		{
			int rowDiff = Math.Abs(currentPosition.GetRow() - piece.GetPosition().GetRow());
			int colDiff = Math.Abs(currentPosition.GetColumn() - piece.GetPosition().GetColumn());

			if (rowDiff == 2 && colDiff == 2)
			{
				int captureRow = (currentPosition.GetRow() + piece.GetPosition().GetRow()) / 2;
				int captureCol = (currentPosition.GetColumn() + piece.GetPosition().GetColumn()) / 2;
				return new Position(captureRow, captureCol);
			}

			return null;
		}

		public Position GetNextCapturePosition(IPiece piece, Position earlyPos)
		{
			// List<Position> capturePos = new List<Position>();

			int rowDiff = Math.Abs(earlyPos.GetRow() - piece.GetPosition().GetRow());
			int colDiff = Math.Abs(earlyPos.GetColumn() - piece.GetPosition().GetColumn());

			if(rowDiff == 2 & colDiff == 2)
			{
				int captureRow = (earlyPos.GetRow() + piece.GetPosition().GetRow()) / 2;
				int captureCol = (earlyPos.GetColumn() + piece.GetPosition().GetColumn()) / 2;
				return new Position(captureRow, captureCol);
			}

			return null;
		}

		public IPiece CapturedPiece(Position earlyPos, Position nextCapturePos)
		{
			int captureRow = (earlyPos.GetRow() + nextCapturePos.GetRow()) /2;
			int captureCol = (earlyPos.GetColumn() + nextCapturePos.GetColumn()) /2;
			Position capturePos = new Position(captureRow, captureCol);

			IPiece capturedPiece = GetPieceOnPosition(capturePos);
			if(capturedPiece != null)
			{
				IPlayer capturedPiecePlayer = GetPlayerFromPiece(capturedPiece);
				if(capturedPiecePlayer != null)
				{
					_playerPieceSet[capturedPiecePlayer].Remove(capturedPiece);
				}

			}

			return capturedPiece;
		}	

		public void MovePiece(Position earlyPos, Position targetPos)
		{

			IPiece earlyPiece = GetPieceOnPosition(earlyPos);

			if(earlyPiece != null)
			{
				if(GetPlayerFromPiece(earlyPiece) == _currentPlayer)
				{
				
					if (_rules.IsLegalMove(earlyPiece, targetPos) || _rules.IsOccupied(_playerPieceSet, targetPos) ||
                     (_rules.IsCaptureMove(_playerPieceSet, earlyPos, targetPos) && earlyPos != targetPos) || _rules.IsWorthyToBeKing(earlyPiece.GetPieceType(),targetPos, _board) )
					{
						if (_rules.IsOccupied(_playerPieceSet, targetPos))
						{
							Logger.Debug("It's  Occupied");
							
						} else
						{
							earlyPiece.SetPosition(targetPos);
							_playerPieceSet[_currentPlayer].ForEach(piece => 
							{ 
								if (piece == earlyPiece)
								 piece.SetPosition(targetPos);
							});
						
							Logger.Debug("it's Legal Move !");
						}

						if (_rules.IsCaptureMove(_playerPieceSet, earlyPos, targetPos))
						{
							int captureRow = (earlyPos.GetRow() + targetPos.GetRow()) / 2;
							int captureCol = (earlyPos.GetColumn() + targetPos.GetColumn()) / 2;
							Position capturePos = new(captureRow, captureCol);
			
							IPiece capturedPiece = GetPieceOnPosition(capturePos);
							IPlayer capturedPiecePlayer = GetPlayerFromPiece(capturedPiece);
						
							if(capturedPiece != null && capturedPiecePlayer != null)
							{
								_playerPieceSet[capturedPiecePlayer].Remove(capturedPiece);
							}

							earlyPiece.SetPosition(targetPos);
							_playerPieceSet[_currentPlayer].ForEach(piece => {if (piece == earlyPiece) piece.SetPosition(targetPos); });

							Logger.Debug("Enter Next Target Position : ");
							// string input = Console.ReadLine();

							// string[] coordinates = input.Split(' ');
							// int row = int.Parse(coordinates[0]);
							// int col = int.Parse(coordinates[1]);

							int row = Convert.ToInt32(Console.ReadLine());
							int col= Convert.ToInt32(Console.ReadLine());
							Position nextTargetPos = new Position(row, col);
							MovePiece(targetPos, nextTargetPos);
				
						}else
						{
							Logger.Debug("Not Capture Move !");
						}

						

						if(_rules.IsWorthyToBeKing(earlyPiece.GetPieceType(),targetPos, _board))
						{
							if(earlyPiece.GetPieceType() == PieceType.BM)
							{
								earlyPiece.SetPieceType(PieceType.BK);
								Logger.Debug("Promoted to Black King !");
								
							}
							else if(earlyPiece.GetPieceType() == PieceType.WM)
							{
								earlyPiece.SetPieceType(PieceType.WK);
								Logger.Debug("Promoted to White King !");
							}
							// UpdateBoard(earlyPos, targetPos);
						}
						
					UpdateBoard(targetPos);
					SwitchTurn();
					}else
					{
						Logger.Debug("Not Legal Move !");
					}
					
			 	}else
				{
					Logger.Debug("It's not your turn");
				}
				
			}else
			{
				Logger.Debug("No Piece Found On The Specified Position !");
			}
		}


		public void DisplayBoard()
		{
			// IPlayer player1 = new Player(p1);
			// IPlayer player2 = new Player(p2);

			//array matrix board dari 0
			int rows = _board.GetMatrix().GetLength(0);
			int columns = _board.GetMatrix().GetLength(1);

			Console.WriteLine("Checker:");
			Console.WriteLine("--------------------");

			Console.Write("   ");
			for (int col = 0; col <= columns; col++)
			{
				Console.Write($"{col}    ");
			}
			Console.WriteLine();

			for (int row = rows; row >= 0; row--)
			{
				Console.Write($"{row}");
				for (int col = 0; col <= columns; col++)
				{
					Position position = new Position(row, col);
					IPiece piece = GetPieceOnPosition(position);

					if (piece != null)
					{
						// IPlayer piecePlayer = GetPlayerFromPiece(piece);
						Console.Write($"[ {piece.GetPieceType()}]");
					}
					else
					{
						Console.Write("[   ]");
					}
				}

				Console.WriteLine();
			}

			Console.WriteLine("--------------------");

		}

		public void UpdateBoard(Position targetPos)
		{
			// int[,] matrix = _board.GetMatrix();
			int[,] matrix = new int[8,8];
			
			// int row = earlyPos.GetRow();
			// int col = earlyPos.GetColumn();
			// matrix[row + 1, col + 1] = 0;

			if(targetPos != null)
			{
				int targetRow = targetPos.GetRow();
				int targetCol = targetPos.GetColumn();

				IPiece piece = GetPieceOnPosition(targetPos);
				int piecePlayer = 0;

				if(piece != null)
				{
					if(GetPlayerFromPiece(piece) == _playerPieceSet.Keys.ElementAt(0))
					{
						piecePlayer = 1;
					}else
					{
						piecePlayer = 2;
					}
				}

				matrix[targetRow, targetCol] = piecePlayer;

			}

		}



		public void StartGame()
		{
			// CreateBoard(7, 7);
			// InitializePieceOnBoard();
			bool gameOver = false;

			while (!gameOver)
			{
				DisplayBoard();

				Console.WriteLine($"Player {_currentPlayer.GetName()}, it's your turn.");

				Console.WriteLine("Enter initial row and column :");
				int initialRow = Convert.ToInt32(Console.ReadLine());
				int initialColumn = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Enter target row and column :");
				int targetRow = Convert.ToInt32(Console.ReadLine());
				int targetColumn = Convert.ToInt32(Console.ReadLine());

				Position initialPosition = new Position(initialRow, initialColumn);
				Position targetPosition = new Position(targetRow, targetColumn);


			if (_playerPieceSet[_currentPlayer].Count == 0)
				{
					gameOver = true;
					Console.WriteLine("Game over!");
					Console.WriteLine($"Player {_currentPlayer.GetName()} wins!");
				}
				else
				{
					MovePiece(initialPosition, targetPosition);
					Console.WriteLine($"Debug {_currentPlayer.GetName()}");
					
				}
			}

		}
	}
}
