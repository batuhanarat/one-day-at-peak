using Game.Core.Enums;
using Game.Core.GridBase;
using Game.Core.SnakeBase;
using UnityEngine;

namespace Game.InteractableObjects
{
    public class WallInteractableObject : MonoBehaviour,IInteractable
    {
        public void OnInterractedWithSnake(Snake snake)
        {
            snake.Died();
        }

        public void setPosition(GridCell gridCell)
        {
            transform.position = gridCell.transform.position;
            gridCell.currentObject = this;
        }
    }
}