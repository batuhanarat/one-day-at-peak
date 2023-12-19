using UnityEngine;
using Game.Core.GridBase;
using Game.Core.Enums;


namespace Game.Core.SnakeBase
{
    public class SnakeBodyPart : MonoBehaviour, IInteractable {
        public GridCell CurrentGridCell { get; set; }
        public CardinalDirection CurrentDirection { get; set; }
        public GridCell PreviousGridCell { get; set; }
        public CardinalDirection PreviousDirection { get; set; }

        public void PassData(SnakeBodyPart bodyPart)
        {
            bodyPart.setPosition(CurrentGridCell);
            bodyPart.setDirection(CurrentDirection);
        }

        public void setPosition(GridCell gridCell)
        {
            
            PreviousGridCell = CurrentGridCell;
            CurrentGridCell = gridCell;
            CurrentGridCell.currentObject = this;
            transform.position = gridCell.transform.position;
        }

        public void setDirection(CardinalDirection dir)
        {
            transform.rotation = dir.GetRotation();
            PreviousDirection = CurrentDirection;
            CurrentDirection = dir;
        }

        public void OnInterractedWithSnake(Snake snake) {
            snake.Died();
        }
    }
}