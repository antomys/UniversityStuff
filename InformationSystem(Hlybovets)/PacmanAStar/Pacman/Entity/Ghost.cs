using System;
using System.Collections.Generic;
using Pacman.Base;
using Pacman.Base.Interfaces;
using Pacman.Base.Point;
using Pacman.Game;

namespace Pacman.Entity
{
    public class Ghost : EntityBase
    {
        public override string Name { get; set; } = "Ghost";
        public override char Character { get; set; } = Constant.GhostChar;
        private Direction _previousPosition = Direction.None;
        private const int ViewDistance = 5;

        public override void Start(GameScene scene, IPoint point)
        {
            base.Start(scene, point);
            Pixel.BackgroundColor = ConsoleColor.Magenta;
        }

        public override void Update()
        {
            var nextDirection = GetNextDirection();

            _previousPosition = nextDirection switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => Direction.None
            };

            var destX = Point.X;
            var destY = Point.Y;
            switch (nextDirection)
            {
                case Direction.Up:
                    destY--;
                    break;
                case Direction.Down:
                    destY++;
                    break;
                case Direction.Left:
                    destX--;
                    break;
                case Direction.Right:
                    destX++;
                    break;
            }

            var destEntity = GetEntityInDirection(nextDirection, 1);
            if (destEntity.EntityType == EntityType.Player)
                StartTransition(TransitionType.Dead);

            if (destX != Point.X || destY != Point.Y)
                Move(new Point(destX, destY));
        }

        private Direction GetNextDirection()
        {
            var directions = new List<DirectionInfo>()
            {
                GetDirectionInfo(Direction.Up),
                GetDirectionInfo(Direction.Down),
                GetDirectionInfo(Direction.Left),
                GetDirectionInfo(Direction.Right)
            };
            directions.RemoveAll(d => !d.IsAvailable);

            var playerDirection = directions.Find(d => d.IsPlayerVisible);
            if (playerDirection != null)
                return playerDirection.Direction;

            if (directions.Count > 1)
                directions.RemoveAll(d => d.Direction == _previousPosition);

            return directions.Count == 0 ? Direction.None : directions[Random.Next(0, directions.Count)].Direction;
        }

        private DirectionInfo GetDirectionInfo(Direction direction)
        {
            var info = new DirectionInfo
            {
                Direction = direction,
                IsAvailable = false,
                IsPlayerVisible = false
            };

            for (var i = 1; i <= ViewDistance; i++)
            {
                var entity = GetEntityInDirection(direction, i);
                if (entity == null)
                    break;

                if (entity.EntityType == EntityType.Wall || entity.EntityType == EntityType.Ghost)
                {
                    break;
                }

                if (entity.EntityType == EntityType.Player)
                {
                    info.IsPlayerVisible = true;
                }

                info.IsAvailable = true;
            }

            return info;
        }

        private class DirectionInfo
        {
            public Direction Direction { get; init; }
            public bool IsPlayerVisible { get; set; }
            public bool IsAvailable { get; set; }
        }
    }
}
