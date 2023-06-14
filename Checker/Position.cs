namespace Checker
{
    public class Position
    {
        private int _row;
        private int _column;

        public Position(int _row, int _column)
        {
            this._row = _row;
            this._column = _column;
        }
        public void SetPosition(int row, int column)
        {
            _row = row;
            _column = column;
        }

        // public int GetPosition()
        // {
        //     return _row;
        //     return _column;
        // }

        public int GetRow()
        {
            return _row;
        }

        public int GetColumn()
        {
            return _column;
        }

    }
}