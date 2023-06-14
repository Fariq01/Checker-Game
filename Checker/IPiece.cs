namespace Checker
{


    public interface IPiece
    {

        public void SetPieceType(PieceType pieceType);

        public PieceType GetPieceType();

         public void SetPosition(Position position);

         public Position GetPosition();

         //eat piece enemy
        //  public void SetCaptured(bool isCaptured);

        //  public bool GetCaptured();
    }
}