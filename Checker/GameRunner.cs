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
			int[,] matrix = new int[row,column];
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

		 public void InitializePieceOnBoard(IPlayer p1, IPlayer p2)
		 {
			_playerPieceSet = new Dictionary<IPlayer, List<IPiece>>();

            IPlayer player1 = p1;
            IPlayer player2 = p2;
			
			List<IPiece> player1Pieces = new List<IPiece>();
			List<IPiece> player2Pieces = new List<IPiece>();

			for(int row = 1; row <= 3; row++)
			{
				for(int col = 1; col <= 8; col += 2)
				{
					if(row % 2 != 0)
					{
						Position initialPosition = new Position(row, col);
						IPiece piece = new Piece(PieceType.BlackMen, initialPosition);
						player1Pieces.Add(piece);
						
					}
					else
					{
						Position initialPosition = new Position(row, col + 1);
						IPiece piece = new Piece(PieceType.BlackMen, initialPosition);
						player1Pieces.Add(piece);
					}
				}
		
			}
            

			for(int row = 6; row <= 8; row++)
			{
				for(int col = 1; col <= 8; col += 2)
				{
					if(row % 2 == 0)
					{
						Position initialPosition = new Position(row, col);
						IPiece piece = new Piece(PieceType.WhiteMen, initialPosition);
						player2Pieces.Add(piece);
						
					}
					else
					{
						Position initialPosition = new Position(row, col + 1);
						IPiece piece = new Piece(PieceType.WhiteMen, initialPosition);
						player2Pieces.Add(piece);
					}
				}
		
			}

			_playerPieceSet.Add(player1, player1Pieces);
			_playerPieceSet.Add(player2, player2Pieces);

            _currentPlayer = player1;
	
		 }
		 
		 
		 
		public IPiece CheckPieceOnPosition(Position target)
		{
			
			
			foreach(var piece in _playerPieceSet.Values)
			
			{
				if(piece.Exists(piece => piece.GetPosition() == target))
				
				{
					return (IPiece)piece; 
					
					
				}
			}
			
			return null;
            
		}

        public IPlayer GetPiecePlayer(IPiece piece)
        {
            foreach(var player in _playerPieceSet.Keys)
            {
                if(_playerPieceSet[player].Contains(piece))
                {
                    return player;
                }
            }

            return null;
        }

		 public void MovePiece(Position early, Position target)
		 {
			
            IPiece earlyPiece = CheckPieceOnPosition(early);
            IPiece targetPiece = CheckPieceOnPosition(target);

			if(_rules.IsLegalMoves(target))
			{
				if(_rules.IsOccupied(_playerPieceSet, target))
				{
					if(_rules.IsCaptureMove(_playerPieceSet, target))
					{
						if(targetPiece != null)
                        {
                            IPlayer targetPlayer = GetPiecePlayer(targetPiece);

                            if(targetPlayer != null)
                            {
                                _playerPieceSet[targetPlayer].Remove(targetPiece);
                            }
                            
                        }
					}

                    earlyPiece.SetPosition(target);
					
				}
				
			}
		 }

         public void DisplayBoard()
         {

            // IPlayer player1 = new Player(p1);
            // IPlayer player2 = new Player(p2);

            // InitializePieceOnBoard(player1, player2);

            int rows = _board.GetMatrix().GetLength(0);
            int columns = _board.GetMatrix().GetLength(1);

            Console.WriteLine("Current Board State:");
            Console.WriteLine("--------------------");

            Console.Write("   ");
            for (int col = 1; col <= columns; col++)
            {
                Console.Write($"  {col} ");
            }
            Console.WriteLine();

            for (int row = 0; row < rows; row++)
            {
                Console.Write($"{row + 1}  ");
                for (int col = 0; col < columns; col++)
                {
                    Position position = new Position(row, col);
                    IPiece piece = CheckPieceOnPosition(position);

                    if (piece != null)
                    {
                        IPlayer piecePlayer = GetPiecePlayer(piece);
                        Console.Write($"[{piece.GetPieceType()}({piecePlayer.GetName()})]");
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

         

         public void StartGame(String p1, string p2)
         {
             CreateBoard(8, 8);

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

                MovePiece(initialPosition, targetPosition);

                if (_playerPieceSet[player1].Count == 0 || _playerPieceSet[player2].Count == 0)
                {
                    gameOver = true;
                    Console.WriteLine("Game over!");
                    Console.WriteLine($"Player {currentPlayer.GetName()} wins!");
                }
                else
                {
                    // Switch turn to the other player
                    SwitchTurn(_playerPieceSet);
                }
            }

            }
         }
		 
		 
		 
		


	}
}