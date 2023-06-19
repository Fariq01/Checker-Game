namespace Checker
{
	public class GameRunner
	{
		private Dictionary<IPlayer, List<IPiece>> _playerPieceSet;
		private IBoard _board;
		private Rules _rules;
		private Position _positon;
		private IPlayer _currentPlayer;

		public GameRunner()
		{
			_rules = new Rules();
		}

		public void CreateBoard(int row, int column)
		{
			int[,] matrix = new int[row, column];
			_board = new Board(matrix);

		}

		public void SwitchTurn(Dictionary<IPlayer, List<IPiece>> playerPieceSet)
		{
			IPlayer[] players = playerPieceSet.Keys.ToArray();

			if (_currentPlayer == players[0])
			{
				_currentPlayer = players[1];
			}
			else
			{
				_currentPlayer = players[0];
			}
		}

		public void InitializePieceOnBoard()
		{
			_playerPieceSet = new Dictionary<IPlayer, List<IPiece>>();

			IPlayer player1 = new Player("Player 1");
			IPlayer player2 = new Player("Player 2");

			List<IPiece> player1Pieces = new List<IPiece>();
			List<IPiece> player2Pieces = new List<IPiece>();

			for (int row = 0; row <= 2; row++)
			{
				for (int col = 0; col <= 7; col += 2)
				{
					if (row % 2 == 0)
					{
						Position initialPos = new Position(row, col);
						IPiece piece = new Piece(PieceType.BM, initialPos);
						player1Pieces.Add(piece);

					}
					else
					{
						Position initialPos = new Position(row, col + 1);
						IPiece piece = new Piece(PieceType.BM, initialPos);
						player1Pieces.Add(piece);
					}
				}

			}


			for (int row = 5; row <= 7; row++)
			{
				for (int col = 0; col <= 7; col += 2)
				{
					if (row % 2 != 0)
					{
						Position initialPos = new Position(row, col + 1);
						IPiece piece = new Piece(PieceType.WM, initialPos);
						player2Pieces.Add(piece);

					}
					else
					{
						Position initialPos = new Position(row, col);
						IPiece piece = new Piece(PieceType.WM, initialPos);
						player2Pieces.Add(piece);
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

		public void MovePiece(Position earlyPos, Position targetPos)
		{

			IPiece earlyPiece = GetPieceOnPosition(earlyPos);

			if(earlyPiece != null)
			{
				if(GetPlayerFromPiece(earlyPiece) == _currentPlayer)
				{
					if (_rules.IsLegalMove(earlyPiece, targetPos))
					{
						earlyPiece.SetPosition(targetPos);
						UpdateBoard(earlyPos, targetPos);
						
					}else
					{
						Console.WriteLine("Not Legal Move !");
					}

					if (_rules.IsOccupied(_playerPieceSet, targetPos))
					{
						Console.WriteLine("Not Occupied");
					}
					
					if (_rules.IsCaptureMove(_playerPieceSet, earlyPos, targetPos))
					{
						int captureRow = (earlyPos.GetRow() + targetPos.GetRow()) / 2;
						int captureCol = (earlyPos.GetRow() + targetPos.GetColumn()) / 2;
						Position capturePos = new Position(captureRow, captureCol);

						IPiece capturedPiece = GetPieceOnPosition(capturePos);
						IPlayer capturedPiecePlayer = GetPlayerFromPiece(capturedPiece);

						if(capturedPiece != null && capturedPiecePlayer != null)
						{
							_playerPieceSet[capturedPiecePlayer].Remove(capturedPiece);
						}

						UpdateBoard(capturePos, targetPos);

					}else
					{
						Console.WriteLine("Not Capture Move !");
					}

					
			 	}else
				{
					Console.WriteLine("It's not your turn");
				}
				
			}else
			{
				Console.WriteLine("No Piece Found On The Specified Position !");
			}
			
			
		SwitchTurn(_playerPieceSet);
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
				Console.Write($"  {col} ");
			}
			Console.WriteLine();

			for (int row = rows; row >= 0; row--)
			{
				Console.Write($"{row}");
				for (int col = 0; col < columns; col++)
				{
					Position position = new Position(row, col);
					IPiece piece = GetPieceOnPosition(position);

					if (piece != null)
					{
						IPlayer piecePlayer = GetPlayerFromPiece(piece);
						Console.Write($"[ {piece.GetPieceType()}]");
					}
					else
					{
						Console.Write("[ - ]");
					}
				}

				Console.WriteLine();
			}

			Console.WriteLine("--------------------");

		}

		public void UpdateBoard(Position earlyPos, Position targetPos)
		{
			int[,] matrix = _board.GetMatrix();
			int row = earlyPos.GetRow();
			int col = earlyPos.GetColumn();
			matrix[row, col] = 0;

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
			CreateBoard(7, 7);
			InitializePieceOnBoard();
			bool gameOver = false;

			while (!gameOver)
			{
				DisplayBoard();

				IPlayer currentPlayer = _currentPlayer;
				Console.WriteLine($"Player {currentPlayer.GetName()}, it's your turn.");

				Console.WriteLine("Enter initial row and column :");
				int initialRow = Convert.ToInt32(Console.ReadLine());
				int initialColumn = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Enter target row and column :");
				int targetRow = Convert.ToInt32(Console.ReadLine());
				int targetColumn = Convert.ToInt32(Console.ReadLine());

				Position initialPosition = new Position(initialRow, initialColumn);
				Position targetPosition = new Position(targetRow, targetColumn);

				Console.WriteLine(" Debug Early row, col ");

				Console.WriteLine(initialPosition.GetRow());
				Console.WriteLine(initialPosition.GetColumn());

				Console.WriteLine(" Debug Target row, col ");

				Console.WriteLine(targetPosition.GetRow());
				Console.WriteLine(targetPosition.GetColumn());


				
				
				if (_playerPieceSet[_currentPlayer].Count == 0)
				{
					gameOver = true;
					Console.WriteLine("Game over!");
					Console.WriteLine($"Player {currentPlayer.GetName()} wins!");
				}
				else
				{
					MovePiece(initialPosition, targetPosition);
					
				}
			}

		}
	}






}
