using Pacman.Base;
using Pacman.Base.Point;

namespace Pacman.Entity
{
    public class Space : EntityBase
    {
        public override string Name { get; set; } = "Space";
        public override char Character { get; set; } = Constant.SpaceChar;

        public override void Update()
        {
        }
    }
}
