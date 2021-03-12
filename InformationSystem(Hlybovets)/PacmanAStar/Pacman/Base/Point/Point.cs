using Pacman.Base.Interfaces;

namespace Pacman.Base.Point
{
    public class Point : IPoint
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        
    }
}