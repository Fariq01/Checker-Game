using Checker;

    

static class Program
{

    static void Main()
    {

        Console.WriteLine("Enter the name for Player 1:");
    	string player1Name = Console.ReadLine();

        Console.WriteLine("Enter the name for Player 2:");
    	string player2Name = Console.ReadLine();

        GameRunner game = new GameRunner(); 
        game.CreateBoard(7,7);
        game.InitializePieceOnBoard(player1Name, player2Name);
        game.StartGame();

    }   
}