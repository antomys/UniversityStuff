using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.AI
{
    public class MultiAgent
    {
        private int[,] _map;
        private int _width, _height;
        private MinimaxTree _tree;
        private Dictionary<int, List<Point>> _agentPoints;
        private bool treeIsExhausted;

        public Node GetTreeRoot()
        {
            return _tree.Root;
        }


        public MultiAgent(int[,] map, Point player, Point[] enemies)
        {
            _map = map;
            _width = _map.GetLength(0);
            _height = _map.GetLength(1);
            TreeIsExhausted = true;
        }

        public bool TreeIsExhausted { 
            get => treeIsExhausted; 
            set => treeIsExhausted = value; 
        }

        public void ConstructMinimaxTree(Point player, Point[] enemies)
        {
            _tree = new MinimaxTree(_map, player, enemies);
            _agentPoints = _tree.GetStepsForEachAgent();
            TreeIsExhausted = false;
        }

        public Point GetEnemyNextPoint(int n)
        {
            Point p = new Point(_agentPoints[n].ElementAt(0).X, _agentPoints[n].ElementAt(0).Y);
            _agentPoints[n].RemoveAt(0);
            if(_agentPoints[n].Count == 0)
            {
                TreeIsExhausted = true;
            }
            return p;
        }

        public Point GetPlayerNextPoint()
        {
            return GetEnemyNextPoint(0);
        }



    }
}
