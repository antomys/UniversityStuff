using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.AI
{
    static class Utilities
    {
        public static int GetDicrection(Point p1, Point p2)
        {
            if(p1.X - p2.X < 0) { return 2; }
            else if(p1.X - p2.X > 0) { return 4; }
            else if(p1.Y - p2.Y < 0) { return 3; }
            else if(p1.Y - p2.Y > 0) { return 1; }
            return 0;
        }

        public static void PrintTree(Node root)
        {
            root.PrintPretty("", true);
        }

        public static int Heuristic(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static int Cost(Point a, Point b)
        {
            return Heuristic(a, b);   //A star algorithm uses distance between two points (because in pacman there is only this option), cost could be any other function in different games etc.
                                      // (for example cost of going through forest is higher than to goo through plain)
        }

        public static int ChooseRandomly(int from, int to)
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);

                int n = from + (Math.Abs(BitConverter.ToInt32(rno, 0)) % to);
                return n;
            }
        }
    }
}
