namespace Checker
{


	public interface IPiece
	{

		public void SetPieceType(PieceType pieceType);

		public PieceType GetPieceType();

//ganti bool, buat check
		 public void SetPosition(Position position);

		 public Position GetPosition();

	
	}
}