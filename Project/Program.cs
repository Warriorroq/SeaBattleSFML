using System;
using System.Threading;
namespace Project
{
    class Program
    {
        public static Game game = null;
        public static void Main(string[] args)
        {
            game = new Game();
            game.Run();
        }
    }
}
