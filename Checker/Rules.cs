namespace Checker
{
	public class Rules
	{
		public bool IsLegalMove(IPiece piece, Position target)
		{
			int rowDiff = Math.Abs(piece.GetPosition().GetRow() - target.GetRow());
			int colDiff = Math.Abs(piece.GetPosition().GetColumn() - target.GetColumn());

			if(rowDiff == 1 && colDiff == 1)
			{
				return true;
			}

			return false;
		}
		

		public bool IsOccupied(Dictionary<IPlayer, List<IPiece>> playerPieceSet, Position targetPos)
		{
			foreach(var playerPieces in playerPieceSet.Values)
			{
				if(playerPieces.Exists(piece => piece.GetPosition() == targetPos))
				{
					return true;
				}
			}

			return false;

			// int[,] matrix = board.GetMatrix();
			// int value = matrix[targetPos.GetRow(), targetPos.GetColumn()];
		
			// if(value == 1)
			// {
			// 	return true;
			// }

			// return false;

		}

		public bool IsCaptureMove( Dictionary<IPlayer, List<IPiece>> playerPieceSet, Position earlyPos, Position targetPos)
		{
			
			int rowDiff = Math.Abs(earlyPos.GetRow() - targetPos.GetRow());
			int colDiff = Math.Abs(earlyPos.GetColumn() - targetPos.GetColumn());

			if (rowDiff == 2 && colDiff == 2)
			{
				int captureRow = (earlyPos.GetRow() + targetPos.GetRow()) / 2;
				int captureCol = (earlyPos.GetColumn() + targetPos.GetColumn()) / 2;
				Position capturePos = new Position(captureRow, captureCol);

					foreach(var pieces in playerPieceSet.Values)
					{
						if(pieces.Exists(piece => piece.GetPosition().Equals(capturePos)))
						{
							return true;
						}
					}			
			}

			return false;

			// int rowDiff = Math.Abs(piece.GetPosition().GetRow() - targetPos.GetRow());
			// int colDiff = Math.Abs(piece.GetPosition().GetColumn() - targetPos.GetColumn());

			// if(rowDiff == 2 && colDiff == 2)
			// {
			// 	int captureRow = (piece.GetPosition().GetRow() + targetPos.GetRow()) / 2;
			// 	int captureCol = (piece.GetPosition().GetColumn() + targetPos.GetColumn()) / 2;
			// 	Position capturePos = new Position(captureRow, captureCol);

			// 	if(IsOccupied(board, capturePos))
			// 	{
			// 		return true;
			// 	}
			// }

			// return false;
		}


	}
}