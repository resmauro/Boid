using OpenTK.Graphics.ES20;

class Program
{
    static void Main(string[] args)
    {
        using (Game game = new Game(800, 800, "Amo noi"))
        {
            
            game.Run();
        }
    }
}