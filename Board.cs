namespace Checker
{
	public class Board : IBoard
	{
		private int[,] _matrix;


		public Board(int[,] _matrix)
		{   
			this._matrix = _matrix;
		}

		//pake bool, cek matrix
		public void SetMatrix(int[,] matrix)
		{
			_matrix = matrix;
		}

		public int[,] GetMatrix()
		{
			return _matrix;
		}

		
	}
}