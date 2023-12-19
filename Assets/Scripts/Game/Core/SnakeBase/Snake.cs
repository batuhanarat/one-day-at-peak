using System.Collections.Generic;
using Game.Core.Enums;
using Game.Managers;
using Game.UI;
using UnityEngine;

namespace Game.Core.SnakeBase
{
    public class Snake
    {
        private LinkedList<SnakeBodyPart> _snakeList = new LinkedList<SnakeBodyPart>();

        public Snake()
        {
            var gridDimensions = ServiceProvider.GameGrid.GridDimensions;
            var cell = ServiceProvider.GameGrid.GridCellList[gridDimensions.x / 2 + 1, gridDimensions.y / 2 + 1];

            _snakeList.AddFirst(ServiceProvider.AssetLib.GetAsset<SnakeBodyPart>(AssetType.SNAKE_HEAD));
            _snakeList.AddLast(ServiceProvider.AssetLib.GetAsset<SnakeBodyPart>(AssetType.SNAKE_BODY));
            _snakeList.AddLast(ServiceProvider.AssetLib.GetAsset<SnakeBodyPart>(AssetType.SNAKE_BODY));
            _snakeList.AddLast(ServiceProvider.AssetLib.GetAsset<SnakeBodyPart>(AssetType.SNAKE_TAIL));

            placeToGrid(cell);


            
        }

        private void placeToGrid(GridBase.GridCell cell)
        {
            foreach(var snakePart in _snakeList)
            {
                snakePart.setPosition(cell);
                cell = ServiceProvider.GameGrid.GetTileInDirection(cell, CardinalDirection.BACK);
            }
        }

        public void addPart()
        {
            var addedPart = ServiceProvider.AssetLib.GetAsset<SnakeBodyPart>(AssetType.SNAKE_BODY);
            var tailPart = _snakeList.Last.Value;
            addedPart.setDirection(tailPart.CurrentDirection);
            addedPart.setPosition(tailPart.CurrentGridCell);
            _snakeList.AddBefore(_snakeList.Last , addedPart);
            tailPart.setPosition(tailPart.PreviousGridCell);
            tailPart.setDirection(tailPart.PreviousDirection);


        }

        public void Advance(CardinalDirection dir)
        {
            var targetCell = ServiceProvider.GameGrid.GetTileInDirection(_snakeList.First.Value.CurrentGridCell, dir);
            if (targetCell == null)
            {
                return;
            }
            var interactableObject = targetCell.currentObject;
            var node = _snakeList.Last;
            node.Value.CurrentGridCell.currentObject = null;
            while (node != _snakeList.First)
            {
                var previousNode = node.Previous;
                previousNode.Value.PassData(node.Value);
                node = previousNode;
            }
            _snakeList.First.Value.setPosition(targetCell);
            _snakeList.First.Value.setDirection(dir);
            if(interactableObject != null) {
                interactableObject.OnInterractedWithSnake(this);
            }
        }
        public void Died()
        {
            ServiceProvider.LevelManager.OnLevelFailed();
            
        }
    }
}