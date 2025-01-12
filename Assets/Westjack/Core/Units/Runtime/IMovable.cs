using Core.World;

namespace Core.UnitEntities
{
    public interface IMovable
    {
        void StartMove(Tile tile);
        void EndMove(Tile tile);
    }
}
