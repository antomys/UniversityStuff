using System;
using Pacman.Game;

namespace Pacman
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            new GameController().Run();
        }
    }
}
