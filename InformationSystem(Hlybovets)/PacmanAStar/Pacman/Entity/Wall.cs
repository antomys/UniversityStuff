using System;
using Pacman.Base;
using Pacman.Base.Interfaces;
using Pacman.Base.Point;
using Pacman.Game;

namespace Pacman.Entity
{
    public class Wall : EntityBase
    {
        public override string Name { get; set; } = "Wall";
        public override char Character { get; set; } = Constant.WallChar;

        public override void Start(GameScene scene, IPoint point)
        {
            base.Start(scene, point);
            Pixel.BackgroundColor = ConsoleColor.DarkBlue;
            SmoothRender = true;
        }
        public override void Update()
        {
        }
    }
}
