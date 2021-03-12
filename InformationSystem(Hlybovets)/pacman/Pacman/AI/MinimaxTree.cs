using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.AI
{
    class MinimaxTree
    {
        private int[,] _map;
        private const int _treeDepth = 1;
        private int _oneStep;
        private Node _root;
        private Stack<Node> _nodes;

        private Point _startPlayerPosition;
        private Point[] _startEnemiesPositions;

        public Node Root { 
            get => _root; 
            set => _root = value; 
        }

        public MinimaxTree(int[,] map, Point player, Point[] enemies)
        {
            _map = map;
            _nodes = new Stack<Node>();
            _oneStep = 1 + enemies.Length;  // Player step + all enemies steps

            _startPlayerPosition = player;
            _startEnemiesPositions = enemies;
            ConstructTree();
        }
        

        private void ConstructTree()
        {
            Root = new Node(_startPlayerPosition, 0);
            _nodes.Push(Root);

            while (!(_nodes.Count == 1 && _nodes.Peek().AllNextVisited()))
            {
                Node peek = _nodes.Peek();
                if (!peek.Full)
                {
                    if(peek.NextNodes.Count == 0)  //means that not built yet
                    {
                        BuildNextNodes(peek);
                    }
                    else  
                    {
                        Node next = peek.FindFirstEmpty();
                        if(next == null)  // just visited all next nodes
                        {
                            peek.Full = true;
                        }
                        else // built but not all is visited yet
                        {
                            _nodes.Push(next);
                        }
                    }
                }
                else  // visited all next nodes -> go back
                {
                    if(peek.AgentIndex != 0)  // it is min agent
                    {
                        peek.Benefits = FindMaximumBenefit(peek);
                    }
                    else // now max agent must make his decision
                    {
                        peek.Benefits = FindMinimumBenefit(peek);
                    }
                    _nodes.Pop();
                }
            }
            Root.Benefits = FindMinimumBenefit(Root);  //last action for root (root is always max agent)

        }

        public Dictionary<int, List<Point>> GetStepsForEachAgent()
        {
            Dictionary<int, List<Point>> agentPoints = new Dictionary<int, List<Point>>();
            agentPoints.Add(0, new List<Point>());
            for(int i = 1; i<= _startEnemiesPositions.Length; i++)
            {
                agentPoints.Add(i, new List<Point>());
            }

            Node iteratingNode = Root;
         
            while(iteratingNode.NextNodes.Count != 0)
            {
                iteratingNode = iteratingNode.NextNodes.Find(x => x.Benefits.SequenceEqual(iteratingNode.Benefits));
                agentPoints[iteratingNode.AgentIndex].Add(iteratingNode.Coordinate);     
            }

            return agentPoints;
        }

        private List<double> FindMinimumBenefit(Node peek)
        {
            int index = 0;
            double minimum = peek.NextNodes[0].Benefits[peek.AgentIndex];
            
            for (int i = 0; i < peek.NextNodes.Count; i++)
            {
                double maybeMin = peek.NextNodes[i].Benefits[peek.AgentIndex];
                if (maybeMin < minimum)
                {
                    minimum = maybeMin;
                    index = i;
                }
            }
           return new List<double>(peek.NextNodes[index].Benefits);
        }

        private List<double> FindMaximumBenefit(Node peek)
        {
            int index = 0;
            double maximum = peek.NextNodes[0].Benefits[peek.AgentIndex];

            for (int i = 0; i < peek.NextNodes.Count; i++)
            {
                double maybeMax = peek.NextNodes[i].Benefits[peek.AgentIndex];
                if (maybeMax > maximum)
                {
                    maximum = maybeMax;
                    index = i;
                }
            }
            return new List<double>(peek.NextNodes[index].Benefits);
        }

        private void BuildNextNodes(Node peek)
        {
            int currentAgent = peek.AgentIndex;
            int nextAgent = currentAgent == _oneStep - 1 ? 0 : currentAgent + 1;
            if(nextAgent == 0)
            {
                Point[] previousEnemiesPositions = _nodes.ToList().GetRange(0, _startEnemiesPositions.Length).ConvertAll(x => x.Coordinate).ToArray().Reverse().ToArray();
                Point previousPlayerPosition = _nodes.ToList()[_startEnemiesPositions.Length].Coordinate;
                Point[] possibleSteps = GetPossiblePlayerSteps(previousPlayerPosition, previousEnemiesPositions);
                AddPlayerNodes(peek, possibleSteps, previousEnemiesPositions);
            }
            else
            {
                Point previousEnemyPosition = _nodes.Count <= (1 + _startEnemiesPositions.Length) ? _startEnemiesPositions[nextAgent - 1] : _nodes.ToArray()[_startEnemiesPositions.Length].Coordinate;
                Point[] possibleSteps = GetPossibleSteps(previousEnemyPosition);
                AddEnemyNodes(peek, nextAgent, possibleSteps);
            }

        }

        private void AddEnemyNodes(Node peek, int nextAgent, Point[] possibleSteps)
        {
            foreach (var p in possibleSteps)
            {
                Node newNode = new Node(p, nextAgent);
                peek.NextNodes.Add(newNode);
            }
        }

        private void AddPlayerNodes(Node peek, Point[] possibleSteps, Point[] previousEnemiesPositions)
        {
            foreach(var p in possibleSteps)
            {
                Node newNode = new Node(p, 0);

                newNode.Benefits.Add(BenefitForPlayer(p, previousEnemiesPositions));
                BenefitsForEnemies(newNode, p, previousEnemiesPositions);

                if(_nodes.Count == _treeDepth * _oneStep)
                {
                    newNode.Full = true;
                }
                peek.NextNodes.Add(newNode);
            }
        }

        private void BenefitsForEnemies(Node newNode, Point p, Point[] previousEnemiesPositions)
        {
            foreach(var en in previousEnemiesPositions)
            {
                newNode.Benefits.Add((new InformedAlgorithms(en.X, en.Y, p , _map).AStarAlgorithm().Count));
            }
        }

        private double BenefitForPlayer(Point p, Point[] previousEnemiesPositions)
        {
            double ben = 0;
            double nearestFood = (new InformedAlgorithms(p.X, p.Y, FindFood(p), _map).AStarAlgorithm().Count);

            List<double> enemiesDistance = new List<double>();
            foreach(var en in previousEnemiesPositions)
            {
                enemiesDistance.Add(new InformedAlgorithms(p.X, p.Y, en, _map).AStarAlgorithm().Count);
            }
            ben = enemiesDistance.Max();

            return ben > nearestFood ? ben : -nearestFood;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private Point[] GetPossiblePlayerSteps(Point p, Point[] enemies)
        {
            Point[] result = GetPossibleSteps(p);
            return result.Where(x => !enemies.Contains(x)).ToArray();
        }

        private Point[] GetPossibleSteps(Point p)
        {
            List<Point> result = new List<Point>();
            if (p.X > 0 && _map[p.Y, p.X - 1] != 10) result.Add(new Point(p.X - 1, p.Y));
            if (p.X < _map.GetLength(1) - 1 && _map[p.Y, p.X + 1] != 10) result.Add(new Point(p.X + 1, p.Y));
            if (p.Y > 0 && _map[p.Y - 1, p.X] != 10) result.Add(new Point(p.X, p.Y - 1));
            if (p.Y < _map.GetLength(0) - 1 && _map[p.Y + 1, p.X] != 10) result.Add(new Point(p.X, p.Y + 1));
            return result.ToArray();

        }

        private Point FindFood(Point p)
        {
            Point nearest = new Point(-1, -1);
            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    if (_map[y, x] == 1)
                    {
                        if (nearest.X == -1) { nearest = new Point(x, y); }
                        if (Distance(new Point(x, y), p) < Distance(nearest, p))
                        {
                            nearest = new Point(x, y);
                        }
                    }
                }
            }
            return nearest;   //TO DO: do normal win
        }
    }
}
