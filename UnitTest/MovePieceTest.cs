using Xunit;
using Checker;

namespace Checker_Game.UnitTest
{
   
    public class MovePieceTest
    {
        [Fact]
        public void MovePosition()
        {
            var game = new GameRunner();
            game.CreateBoard(7,7);
            game.InitializePieceOnBoard("Budi","Ilham");

            var initialRow = 2;
            var initialCol = 0;
            var targetRow = 3;
            var targetCol = 1;

            var initialPos = new Position(initialRow, initialCol);
            var targetPos = new Position(targetRow, targetCol);

            game.MovePiece(initialPos, targetPos);

            var piece = game.GetPieceOnPosition(targetPos);
            Assert.NotNull(piece);
            Assert.Equal(targetPos, piece.GetPosition());

        }
        [Fact]
        public void IsCaptureMove()
        {
           var rules = new Rules();
           var playerPieceSet = new Dictionary<IPlayer, List<IPiece>>();

           var player1 = new Player("1");
           var player2 = new Player("2");

            //inisisai piece pos
           var piece1 = new Piece(PieceType.BM, new Position(2, 2));
           var piece2 = new Piece(PieceType.WM, new Position(3, 3)); 

           playerPieceSet.Add(player1, new List<IPiece>() {piece1});
           playerPieceSet.Add(player2, new List<IPiece>() {piece2});

           var earlyPos = new Position(2, 2);
           var targetPos = new Position(4,4);

           var IsCaptureMove = rules.IsCaptureMove(playerPieceSet, earlyPos, targetPos);

           Assert.True(IsCaptureMove);

        }


    }
}