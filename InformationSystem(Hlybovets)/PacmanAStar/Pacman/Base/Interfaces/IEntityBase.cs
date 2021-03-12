using Pacman.Base.Point;
using Pacman.Game;

namespace Pacman.Base.Interfaces
{
    public interface IEntityBase
    {
        IPoint Point { get; set; }
        GameScene Scene { get; set; }
        bool IsDestroyed { get; }
        EntityType EntityType { get; }
    }
}