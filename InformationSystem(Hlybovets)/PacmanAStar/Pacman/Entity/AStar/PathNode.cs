using Pacman.Base.Point;

namespace Pacman.Entity.AStar
{
    public class PathNode
    {
       
        public Point Position { get; set; }
        // G
        public int PathLengthFromStart { get; set; }
        public PathNode CameFrom { get; set; }
        // H
        public int HeuristicEstimatePathLength { get; set; }
        // F
        public int EstimateFullPathLength => PathLengthFromStart + HeuristicEstimatePathLength;

        public EntityType EntityType { get; set; }
    }
}