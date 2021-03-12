using Pacman.Base;
using Pacman.Base.Point;

namespace Pacman.Entity
{
    public class Score : EntityBase
    {
        public override string Name { get; set; } = "Score";
        public override char Character { get; set; } = Constant.ScoreChar;

        public override void Update()
        {
        }
    }
}
