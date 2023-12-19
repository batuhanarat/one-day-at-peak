using Game.Core.Enums;
using Game.Core.GridBase;
using Game.Core.SnakeBase;
using Game.Managers;
using UnityEngine;

namespace Game.InteractableObjects
{
    public class AppleInteractableObject : MonoBehaviour, IInteractable
    {
        public void OnInterractedWithSnake(Snake snake) {
            snake.addPart();
            setPosition(ServiceProvider.GameGrid.GetRandomAvailableGridCell());
            ServiceProvider.LevelManager.OnInteractableCollected(InteractableObjectType.APPLE);
        }

        public void setPosition(GridCell gridCell)
        {
            transform.position = gridCell.transform.position;
            gridCell.currentObject = this;
        }
    }
}