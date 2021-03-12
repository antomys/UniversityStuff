using System;
using Pacman.Base;
using Pacman.Base.Interfaces;
using Pacman.Base.Point;
using Pacman.Entity.AStar;
using Pacman.Game;

namespace Pacman.Entity
{
    public class Player : EntityBase
    {
        public override string Name { get; set; } = "Player";
        public override char Character { get; set; } = Constant.PlayerChar;
        private FindPaths _findPaths;

        public override void Start(GameScene scene, IPoint point)
        {
            base.Start(scene, point);
            Pixel.BackgroundColor = ConsoleColor.DarkYellow;
            _findPaths = new FindPaths(this);
        }

        public override void Update()
        {
            //var inputs = _findPaths.GetDirections();
            //var input = _findPaths.GetMove();
            //var direction = GetInputs().Direction;
            var direction = _findPaths.GetMove();
            var destX = Point.X;
            var destY = Point.Y;
            switch (direction)
            {
                case Direction.Right:
                    destX++;
                    break;
                case Direction.Left:
                    destX--;
                    break;
                case Direction.Up:
                    destY--;
                    break;
                case Direction.Down:
                    destY++;
                    break;
                case Direction.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var destEntity = GetEntityInDirection(direction, 1);

            if (destEntity.EntityType != EntityType.Score && destEntity.EntityType != EntityType.Space)
                return;

            if (destEntity.EntityType == EntityType.Score)
                destEntity.Destroy();

            if (destX != Point.X || destY != Point.Y)
                Move(new Point(destX, destY));
        }
    }
}
