using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.AI
{
    public class Node
    {
        private int _agentIndex;

        private Point _point;
        private List<double> _benefits;

        private List<Node> _nextNodes;
        private bool _full = false;

        public Node(Point p, int agentIndex)
        {
            AgentIndex = agentIndex;
            Coordinate = new Point(p.X, p.Y);
            NextNodes = new List<Node>();
            Benefits = new List<double>();
        }

        public bool Full 
        { 
            get => _full; 
            set => _full = value; 
        }

        public List<Node> NextNodes 
        { 
            get => _nextNodes; 
            set => _nextNodes = value; 
        }

        public int AgentIndex 
        { 
            get => _agentIndex; 
            set => _agentIndex = value; 
        }
        public Point Coordinate 
        { 
            get => _point; 
            set => _point = value; 
        }
        public List<double> Benefits 
        { 
            get => _benefits; 
            set => _benefits = value; 
        }

        public bool AllNextVisited()
        {
            if(NextNodes.Count == 0)
            {
                return false;
            }
            foreach(var node in NextNodes)
            {
                if (!node.Full)
                {
                    return false;
                }
            }
            return true;
        }

        public Node FindFirstEmpty()
        {
            for(int i = 0; i<_nextNodes.Count; i++)
            {
                if (!_nextNodes[i].Full)
                {
                    return _nextNodes[i];
                }
            }
            return null;
        }


        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }

            Console.WriteLine(String.Concat("Ag: ", AgentIndex, " P(", _point.X, " , ", _point.Y, ")", " Benef: (", String.Join(" , ", Benefits.ConvertAll(x => Math.Round(x, 2))), " )"));

            for (int i = 0; i < NextNodes.Count; i++)
                NextNodes[i].PrintPretty(indent, i == NextNodes.Count - 1);
        }

    }
}
