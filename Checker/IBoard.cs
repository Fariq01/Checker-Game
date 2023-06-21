namespace Checker
{
    public interface IBoard
    {
        public void SetMatrix(int[,] matrix);
        public int[,] GetMatrix();
    }
}