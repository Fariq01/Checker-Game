namespace Checker
{


    public interface IPiece
    {

        public void SetPieceType(PieceType pieceType);

        public PieceType GetPieceType();

         public void SetPosition(Position position);

         public Position GetPosition();

    
    }
}