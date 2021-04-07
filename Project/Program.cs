using System;
using System.Threading;
namespace Project
{
    class Program
    {
        public static Game game = new Game();
        public static Lobby lobby = new Lobby();
        public static void Main() 
            =>game.Run();
    }
}
