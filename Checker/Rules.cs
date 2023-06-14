namespace Checker
{
    public class Rules
    {
        public bool IsLegalMoves(Position destination)
        {
            int row = destination.GetRow();
            int column = destination.GetColumn();

            if((row - column) % 2 == 0)
            {
                return true;
            }

            return false;
        }

        public bool IsOccupied(Dictionary<IPlayer, List<IPiece>> playerPieceSet, Position position)
        {
            foreach(var playerPieces in playerPieceSet.Values)
            {
                if(playerPieces.Exists(piece => piece.GetPosition() == position))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCaptureMove(Piece sourcePiece, Piece destinationPiece)
        {
       
        }
    

    }
}