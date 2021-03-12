using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Pacman.Base;
using Pacman.Base.Interfaces;
using Pacman.Base.Point;
using Pacman.Entity.Internals;
using Pacman.Game;

namespace Pacman.Entity.AStar
{
    public class FindPaths
    {
        private readonly IEntityBase _entityBase;
        //private LinkedList<Point> _scorePoints;
        private List<Point> _scorePoints;
        private readonly Random _random = new();
        private static string _logFile = "log.txt";
        private Stopwatch _stopwatch;

        public FindPaths(IEntityBase entityBase)
        {
            _entityBase = entityBase;
            //_scorePoints = new LinkedList<Point>();
            _scorePoints = GetAllPoints();
            _stopwatch = new Stopwatch();
        }

        private List<Point> GetAllPoints()
        {
            //return (from LinkedList<EntityBase> listOfLists in _entityBase.Scene.Grid from type in listOfLists where type.EntityType.Equals(EntityType.Score) select (Point) type.Point).ToList();

            var pointsList = new List<Point>();
            foreach (var listOfLists in _entityBase.Scene.Grid)
            {
                if(listOfLists == null)
                    break;
                
                //Debug.Write($"list of Lists contains {listOfLists}\n");
                pointsList.AddRange(from type in listOfLists where type.EntityType.Equals(EntityType.Score) select (Point) type.Point);
            }
            return pointsList;
        }

        private LinkedList<Point> GetThisRowPoints()
        {
            //return (from type in _entityBase.Scene.Grid[_entityBase.Point.Y, _entityBase.Point.X] where type.EntityType.Equals(EntityType.Score) select (Point) type.Point).ToList();
            var pointsList = new LinkedList<Point>();
            var entityBases = new List<LinkedList<EntityBase>>();

            for (var i = _entityBase.Point.Y; i > _entityBase.Point.Y - 4; i--)
            {
                for (var j = 0; j < _entityBase.Point.X; j++)
                {
                    entityBases.Add(_entityBase.Scene.Grid[j, i]);
                }
            }
            foreach (var listOfLists in entityBases)
            {
                //pointsList.AddRange(from type in listOfLists where type.EntityType.Equals(EntityType.Score) select (Point) type.Point);
                foreach (var type in listOfLists)
                {
                    if (type.EntityType.Equals(EntityType.Score))
                    {
                        pointsList.AddLast((Point)type.Point);
                    }
                }
            }

            //pointsList.Reverse();
            return pointsList;
        }
        
        

        public List<Point> FindPath()
        {
            
            var field = _entityBase.Scene.Grid;
            var start = (Point) _entityBase.Point;
            //IPoint goal = new Point(1, 1); //todo:change to SCORE point

            //IPoint goal = new Point(5, 13);
            //_scorePoints = GetThisRowPoints();
            Debug.Write($"Total amount of points: {_scorePoints.Count}");

            IPoint goal = new Point(5, 5);//_scorePoints.ElementAt(_random.Next(0, _scorePoints.Count)); /TODO: make it dynamic
            
            Debug.Write($"Point start entity type is {GetEntityByPoint(start.X,start.Y)}, Coordinates: {start.X},{start.Y}\n");
            Debug.Write($"Point goal entity type is {GetEntityByPoint(goal.X,goal.Y)}, Coordinates: {goal.X},{goal.Y}\n");
            

            
            // Шаг 1.
           
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();
            // Шаг 2.
            var startNode = new PathNode
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, (Point) goal),
                EntityType = GetEntityByPoint(start.X,start.Y)
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                // Шаг 3.
                var currentNode = openSet.OrderBy(node => 
                    node.EstimateFullPathLength).First();
                // Шаг 4.
                //if (currentNode.Position == goal)
                if(currentNode.Position.X == goal.X && currentNode.Position.Y == goal.Y)
                    return GetPathForNode(currentNode);
                // Шаг 5.
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                // Шаг 6.

                var getNeighbours = GetNeighbours(currentNode, (Point) goal, field);
                foreach (var neighbourNode in getNeighbours)
                {
                    // Шаг 7.
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                        node.Position == neighbourNode.Position);
                    // Шаг 8.
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                    if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        openNode.EntityType = GetEntityByPoint(openNode.Position.Y, openNode.Position.X);
                    }
                }
            }
            // Шаг 10.
            return null;
        }
        
        private static int GetDistanceBetweenNeighbours()
        {
            return 1;
        }

        public List<Direction> GetDirections()
        {
            
            var result = new List<Direction>();
            
            var previousPoint = _entityBase.Point;
            var resultPoints = FindPath();

            foreach (var point in resultPoints)
            {
                if (point.X > previousPoint.X && point.Y == previousPoint.Y)
                    result.Add(Direction.Right);
                
                if (point.X < previousPoint.X && point.Y == previousPoint.Y)
                    result.Add(Direction.Left);
                
                if (point.X == previousPoint.X && point.Y > previousPoint.Y)
                    result.Add(Direction.Up);
                
                if (point.X == previousPoint.X && point.Y < previousPoint.Y)
                    result.Add(Direction.Down);
            }
            
            return result;
        }

        private void WriteEverythingToFile(long time)
        {
            Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();
            decimal percentFree = ((decimal)phav / (decimal)tot) * 100;
            decimal percentOccupied = 100 - percentFree;
            
            File.AppendAllText(_logFile,$"Free memory: {percentFree}% " +
                                        $"Occupied: {percentOccupied}% ElapsedTime: {time}ms \n");
        }

        public Direction GetMove()
        {
            _stopwatch.Start();
            var previousPoint = _entityBase.Point;
            Point point;
            try
            {
                point = FindPath()[1];
                
                _stopwatch.Stop();
                var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
                WriteEverythingToFile(elapsedMilliseconds);
            }
            catch (Exception)
            {
                return Direction.None;
            }
            
            if (point.X > previousPoint.X && point.Y == previousPoint.Y)
                return Direction.Right;
                
            if (point.X < previousPoint.X && point.Y == previousPoint.Y)
                return Direction.Left;
                
            if (point.X == previousPoint.X && point.Y > previousPoint.Y)
                return Direction.Down;
                
            if (point.X == previousPoint.X && point.Y < previousPoint.Y)
                return Direction.Up;

            return Direction.None;
        }
        
        private static int GetHeuristicPathLength(IPoint from, IPoint to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }
        
        private EntityType GetEntityByPoint(int x, int y)
        {
            return _entityBase.Scene.Grid[y, x].First.Value.EntityType;
        }
        
        private  Collection<PathNode> GetNeighbours(PathNode pathNode, 
            Point goal, LinkedList<EntityBase>[,] field)
        {
            var result = new Collection<PathNode>();
 
            // Соседними точками являются соседние по стороне клетки.
            var neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            
            neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);
 
            foreach (var point in neighbourPoints)
            {
                var getEntityType = GetEntityByPoint(point.X, point.Y);
                    
                    //Debug.Write($"\nNeighbour position: {point.X},{point.Y}; And entity is {getEntityType}\n");
                    // Проверяем, что не вышли за границы карты.
                    //if (point.X < 0 || point.X >= field.GetLength(0))
                    //if (destY < 0 || destX < 0 || destY >= Scene.YSize || destY >= Scene.XSize)
                    //Debug.Write($"_entityBase.Scene.YSize: {_entityBase.Scene.YSize}\n");
                    //if (point.Y < 0 || point.X < 0 || point.Y <= _entityBase.Scene.YSize || point.X <= _entityBase.Scene.XSize)
                    if (point.Y >= _entityBase.Scene.YSize || point.X >= _entityBase.Scene.XSize)
                        continue;
                    // Проверяем, что по клетке можно ходить.
                    //if ((field[point.X, point.Y] != 0) && (field[point.X, point.Y] != 1))
                    //if ((field[point.X, point.Y] != 0) && (field[point.X, point.Y] != 1))
                    if (getEntityType.Equals(EntityType.Wall) || 
                        getEntityType.Equals(EntityType.Ghost))
                        continue;
                    // Заполняем данные для точки маршрута.
                    var neighbourNode = new PathNode()
                    {
                        Position = point,
                        CameFrom = pathNode,
                        PathLengthFromStart = pathNode.PathLengthFromStart +
                                              GetDistanceBetweenNeighbours(),
                        HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal),
                        EntityType = GetEntityByPoint(point.X,point.Y)
                    };

                    result.Add(neighbourNode);
                    /*Debug.Write($"Neighbour is {neighbourNode.Position.X}, {neighbourNode.Position.Y}\n" +
                                $"Neighbour Type {neighbourNode.EntityType}\n");*/
            }
            return result;
        }
        
        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }

        public IPoint Point { get; set; }
        public GameScene Scene { get; set; }
        public bool IsDestroyed { get; }
        public EntityType EntityType { get; }
    }
}