namespace Checker
{
    public class Piece :IPiece
    {
        private PieceType _pieceType;

        private Position _piecePosition;

      //   private bool _isCaptured;


         public Piece(PieceType pieceType, Position piecePosition)
         {
            _pieceType = pieceType;
            _piecePosition = piecePosition;
            // _isCaptured = false;
         }


         public void SetPieceType(PieceType pieceType)
         {
            _pieceType = pieceType;
         }
         public PieceType GetPieceType()
         {
            return _pieceType;
         }

         public void SetPosition(Position piecePosition)
         {
            _piecePosition = piecePosition;
         }

         public Position GetPosition()
         {
            return _piecePosition;
         }

       
    }
}