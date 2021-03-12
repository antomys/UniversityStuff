using Pacman.AI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Pacman.AI
{
    class InformedAlgorithms
    {
        private int[,] _map;
        private Dictionary<Point, Point> _cameFrom;
        private Dictionary<Point, int> _costSoFar;
        private List<KeyValuePair<int, Point>> _frontier;
        private Point _foodPoint;
        private Point _startPoint;

        public InformedAlgorithms(int startX, int startY, int[,] map)
        {
            _map = map;
            _foodPoint = FoodCoordinate();
            _startPoint = new Point(startX, startY);
            _cameFrom = new Dictionary<Point, Point>();
            _frontier = new List<KeyValuePair<int, Point>>();
            _costSoFar = new Dictionary<Point, int>();

            _frontier.Add(new KeyValuePair<int, Point>(0, new Point(startX, startY)));
            _costSoFar.Add(_startPoint, 0);
            _cameFrom.Add(_startPoint, new Point(-1, -1));
        }

        public InformedAlgorithms(int startX, int startY, Point target, int[,] map)
        {
            _map = map;
            _foodPoint = target;
            _startPoint = new Point(startX, startY);
            _cameFrom = new Dictionary<Point, Point>();
            _frontier = new List<KeyValuePair<int, Point>>();
            _costSoFar = new Dictionary<Point, int>();

            _frontier.Add(new KeyValuePair<int, Point>(0, new Point(startX, startY)));
            _costSoFar.Add(_startPoint, 0);
            _cameFrom.Add(_startPoint, new Point(-1, -1));
        }

        public List<Point> AStarAlgorithm()
        {
            while (_frontier.Count != 0)
            {
                var current = _frontier.Last();
                _frontier.RemoveAt(_frontier.Count - 1);

                if (new Point(current.Value.Y, current.Value.X) == _foodPoint)
                {
                    break;
                }
                foreach (var next in GetNeighbours(current.Value))
                {
                    int cost = _costSoFar[current.Value] + Utilities.Cost(current.Value, next);
                    if (!_costSoFar.ContainsKey(next) || cost < _costSoFar[next])
                    {
                        _costSoFar[next] = cost;

                        int priority = cost + Utilities.Heuristic(_foodPoint, next);
                        _frontier.Add(new KeyValuePair<int, Point>(priority, next));
                        
                        _cameFrom[next] = current.Value;

                        
                        _frontier.Sort(delegate (KeyValuePair<int, Point> p1, KeyValuePair<int, Point> p2) { return p1.Key > p2.Key ? -1 : 1; });
                    }
                }
            }
            List<Point> result = TrackFromEnd();
            return result;
        }

        public List<Point> GreedyAlgorithm()
        {
            while(_frontier.Count != 0)
            {
                var current = _frontier.Last();
                _frontier.RemoveAt(_frontier.Count - 1);

                if(_map[current.Value.Y, current.Value.X] == 2)
                {
                    break;
                }
                foreach(var next in GetNeighbours(current.Value))
                {
                    if (!_cameFrom.ContainsKey(next))
                    {
                        int priority = Utilities.Heuristic(_foodPoint, next);
                        _frontier.Add(new KeyValuePair<int, Point>(priority, next));
                        _cameFrom.Add(next, current.Value);
                        _frontier.Sort(delegate (KeyValuePair<int, Point> p1, KeyValuePair<int, Point> p2) { return p1.Key > p2.Key ? -1 : 1; });
                    }
                }
            }
            List<Point> result = TrackFromEnd();
            return result;
        }


        private List<Point> GetNeighbours(Point p)
        {
            List<Point> result = new List<Point>();
            if (p.X > 0 && _map[p.Y, p.X - 1] != 10) result.Add(new Point(p.X - 1, p.Y));
            if (p.X < _map.GetLength(1)-1 && _map[p.Y, p.X + 1] != 10) result.Add(new Point(p.X + 1, p.Y));
            if (p.Y > 0 && _map[p.Y-1, p.X] != 10) result.Add(new Point(p.X, p.Y-1));
            if (p.Y < _map.GetLength(0)-1 && _map[p.Y + 1, p.X] != 10) result.Add(new Point(p.X, p.Y + 1));
            return result;
        }


        private Point FoodCoordinate()
        {
            for(int i = 0; i< _map.GetLength(0); i++)
            {
                for(int j = 0; j < _map.GetLength(1); j++)
                {
                    if(_map[i, j] == 2 || _map[i, j] == 1)
                    {
                        return new Point(j, i);
                    }
                }
            }
            return new Point(0, 0);
        }

        private List<Point> TrackFromEnd()
        {
            List<Point> result = new List<Point>();
            result.Add(_foodPoint);
            while(result[result.Count - 1] != _startPoint)
            {
                if(_cameFrom.ContainsKey(result[result.Count - 1]))
                {
                    result.Add(_cameFrom[result[result.Count - 1]]);
                }
                else
                {
                    Console.WriteLine("The food doesn't exist");
                    break;
                }
            }
            result.Reverse();
            return result;
        }

        private void PrintResult(List<Point> result)
        {
            foreach(var p in result)
            {
                Console.WriteLine(String.Concat(p.X, " , ", p.Y));
            }
            
        }
    }
}
