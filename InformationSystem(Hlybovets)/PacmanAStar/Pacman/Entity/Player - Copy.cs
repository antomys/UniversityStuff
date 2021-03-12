using System;
using Pacman.Base;
using Pacman.Base.Interfaces;
using Pacman.Base.Point;
using Pacman.Game;

namespace Pacman.Entity
{
    public class PlayerCopy : EntityBase
    {
        public override string Name { get; set; } = "Player";
        public override char Character { get; set; } = Constant.PlayerChar;

        public override void Start(GameScene scene, IPoint point)
        {
            base.Start(scene, point);
            Pixel.BackgroundColor = ConsoleColor.DarkYellow;
        }

        public override void Update()
        {
            var direction = GetInputs().Direction;
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
