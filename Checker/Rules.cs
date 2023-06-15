namespace Checker
{
	public class Rules
	{
		public bool IsLegalMoves(Position target)
		{
			int row = target.GetRow();
			int column = target.GetColumn();

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

		public bool IsCaptureMove(Dictionary<IPlayer, List<IPiece>> playerPieceSet, Position target)
		{
			foreach(var opponentPieces in playerPieceSet.Values)
			{
	
				if(opponentPieces.Exists(piece => piece.GetPosition() == target))
				{
					return true;
				}
				
			}

			return false;
		}


	}
}