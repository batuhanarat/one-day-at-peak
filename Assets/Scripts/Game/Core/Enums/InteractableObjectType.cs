using Game.Core.GridBase;
using Game.Core.SnakeBase;

namespace Game.Core.Enums
{
    public enum InteractableObjectType
    {
        EMPTY = 0,
        WALL = 1,
        PORTAL = 2,
        APPLE = 3,
    }

    public interface IInteractable
    {
        void OnInterractedWithSnake(Snake snake);
        void setPosition(GridCell gridCell);
    }
}