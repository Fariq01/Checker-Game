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

		public bool IsCaptureMove(IPlayer currentPlayer, Dictionary<IPlayer, List<IPiece>> playerPieceSet, Position target)
		{
			IPlayer? opponentPlayer = null;
			List<IPiece>? opponentPieces = null;
			IPiece? capturedPiece = null;
			
			// cari opponentPlayer di dict playerpieceset
			foreach(var player in playerPieceSet.Keys)
			{
				if(player != currentPlayer)
				{
					
					opponentPlayer = player;
				
				}
			}
			// masukin opponentPieces
			if(opponentPlayer != null)
			
			{
				opponentPieces = playerPieceSet[opponentPlayer];
			}
			
			//cari piece di opponentPieces yang berada di posisi target
			foreach(var piece in opponentPieces)
			{
				if(piece.GetPosition() == target)
				
				{
					capturedPiece = piece;
				}
			}
			
			if(capturedPiece != null)
			
			{
				opponentPieces.Remove(capturedPiece);
				//ganti pake display
				Console.WriteLine($"{capturedPiece} is captured");	
				
				return true;
							
			}
			
			return false;
		}
	

	}
}