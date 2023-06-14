namespace Checker
{
    public class GameRunner
    {
         private Dictionary<IPlayer, List<IPiece>> _playerPieceSet;
         private IBoard _board;
         private Rules _rules;
         private IPlayer _currentPlayer;



         public void CreateBoard(int row, int column)
         {
            int[,] matrix = new int[row,column];
            _board = new Board(matrix);
         }

         public void CreatePlayerPieceSet(IPlayer player1, IPlayer player2)
         {
            _playerPieceSet = new Dictionary<IPlayer, List<IPiece>>();

            
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
    
         }

         public void MovePiece(IPlayer currentPlayer, Position early, Position target)
         {
            if(isCaptur)
            {

            }
         }

        //  public void CapturePiece(IPlayer currentPlayer, Position position)
        //  {
        //     IPlayer opponentPlayer = _playerPieceSet.Keys.FirstOrDefault(player => player != currentPlayer);

        //     if (opponentPlayer != null)
        //     {
        //         List<IPiece> opponentPieces = _playerPieceSet[opponentPlayer];
        //         IPiece capturedPiece = opponentPieces.Find(piece => piece.GetPosition() == position);

        //         if (capturedPiece != null)
        //         {
        //             opponentPieces.Remove(capturedPiece);
        //             Console.WriteLine($"Capture piece at ({position}) !");

                    
        //         }
        //         else
        //         {
        //             Console.WriteLine("No piece to capture at the position.");
        //         }
        //     }
        //  }

    }
}