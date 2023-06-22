namespace Checker
{
	public class Rules
	{

		public bool IsWorthyToBeKing(PieceType pieceType, Position targetPos, IBoard board)
		{
			int row = board.GetMatrix().GetLength(0);
			int col = board.GetMatrix().GetLength(1) - 1;

            if(pieceType == PieceType.BM && targetPos.GetRow() == row)
			{
				return true;
			}

			if(pieceType == PieceType.WM && targetPos.GetRow() == 0)
			{
				return true;
			}

			return false;
        }

		public bool IsCanDoKingMove(IPiece piece, Position targetPos)
		{
			int rowDiff = Math.Abs(piece.GetPosition().GetRow() - targetPos.GetRow());
			int colDiff = Math.Abs(piece.GetPosition().GetColumn() - targetPos.GetColumn());

			// sama kek if, if cek dan return boolean
            return rowDiff == 1 && colDiff == 1;
		}

		public bool IsLegalMove(IPiece piece, Position targetPos)
		{
			int rowDiff = Math.Abs(piece.GetPosition().GetRow() - targetPos.GetRow());
			int colDiff = Math.Abs(piece.GetPosition().GetColumn() - targetPos.GetColumn());


            // sama kek if, if cek dan return boolean

            return rowDiff == 1 && colDiff == 1;
        }
		

		public bool IsOccupied(Dictionary<IPlayer, List<IPiece>> playerPieceSet, Position targetPos)
		{
			foreach(var playerPieces in playerPieceSet.Values)
			{
				// if(playerPieces.Exists(piece => piece.GetPosition() == targetPos))
				// {
				// 	return true;
				// }

				if (playerPieces.Exists(piece => piece.GetPosition().GetRow() == targetPos.GetRow() && piece.GetPosition().GetColumn() == targetPos.GetColumn()))
				{
					return true;
				}
			}

			return false;
		}

		public bool IsCaptureMove(Dictionary<IPlayer, List<IPiece>> playerPieceSet, Position earlyPos, Position targetPos)
		{
			
			int rowDiff = Math.Abs(earlyPos.GetRow() - targetPos.GetRow());
			int colDiff = Math.Abs(earlyPos.GetColumn() - targetPos.GetColumn());

			if (rowDiff == 2 && colDiff == 2)
			{
				int captureRow = (earlyPos.GetRow() + targetPos.GetRow()) / 2;
				int captureCol = (earlyPos.GetColumn() + targetPos.GetColumn()) / 2;
				// Position capturePos = new(captureRow, captureCol);

					foreach(var pieces in playerPieceSet.Values)
					{
						if(pieces.Exists(piece => piece.GetPosition().GetRow() == captureRow && piece.GetPosition().GetColumn() == captureCol))
						{
							return true;
						}
					}
			}

			return false;
		}

	}
}