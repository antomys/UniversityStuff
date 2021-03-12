using System;
using System.Collections.Generic;
using Pacman.Base.Interfaces;
using Pacman.Base.Point;
using Pacman.Game;

namespace Pacman.Base
{
    public abstract class EntityBase : IEntityBase
    {
        /*public int X { get; private set; }
        public int Y { get; private set; }*/
        
        public IPoint Point { get; set; }
        public GameScene Scene { get; set; }
        
        public bool SmoothRender = false;
        public Pixel Pixel { get; private set; } = new()
        {
            BackgroundColor = ConsoleColor.Black,
            ForegroundColor = ConsoleColor.Gray
        };


        public bool IsDestroyed { get; private set; }
        private LinkedList<EntityBase>[,] GridView => (LinkedList<EntityBase>[,])Scene.Grid.Clone();
        public EntityType EntityType => GetEntityByChar(Character);

        protected static Random Random => Util.Random;

        public abstract string Name { get; set; }
        public abstract char Character { get; set; }

        public virtual void Start(GameScene scene, IPoint point)
        {
            this.Scene = scene;
            /*this.X = X;
            this.Y = Y;*/
            Point = new Point.Point(point.X, point.Y);
        }

        public abstract void Update();

        protected void Move(IPoint point)
        {
            Scene.Grid[Point.Y, Point.X].RemoveFirst();
            Scene.Grid[point.Y, point.X].AddFirst(this);
            Point.X = point.X;
            Point.Y = point.Y;
        }

        public void Destroy()
        {
            IsDestroyed = true;
            Scene.DestroyEntity(this);
        }

        private static EntityType GetEntityByChar(char c)
        {
            return c switch
            {
                Constant.PlayerChar => EntityType.Player,
                Constant.GhostChar => EntityType.Ghost,
                Constant.WallChar => EntityType.Wall,
                Constant.ScoreChar => EntityType.Score,
                Constant.SpaceChar => EntityType.Space,
                _ => EntityType.None
            };
        }

        public EntityType GetEntityByPoint(int x, int y)
        {
            return GridView[y, x].First.Value.EntityType;
        }

        protected static InputStatus GetInputs()
        {
            return Input.GetInput();
        }

        protected EntityBase GetEntityInDirection(Direction direction, int distance)
        {
            var destX = Point.X;
            var destY = Point.Y;
            switch (direction)
            {
                case Direction.Left:
                    destX -= distance;
                    break;
                case Direction.Right:
                    destX += distance;
                    break;
                case Direction.Up:
                    destY -= distance;
                    break;
                case Direction.Down:
                    destY += distance;
                    break;
            }

            //Out of range
            if (destY < 0 || destX < 0 || destY >= Scene.YSize || destX >= Scene.XSize) //todo:changed
                return null;

            return GridView[destY, destX].First.Value; // Do not refactor, it won't work again.
        }

        protected void StartTransition(TransitionType type)
        {
            Scene.StartTransition(type);
        }
    }
}
