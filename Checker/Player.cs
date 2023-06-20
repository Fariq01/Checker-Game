namespace Checker
{
    public class Player : IPlayer
    {
        private string _name;
        private int _score;

        public Player(string _name)
        {
            this._name = _name;
            _score = 0;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public int GetScore()
        {
            return _score;
        }
    }
}