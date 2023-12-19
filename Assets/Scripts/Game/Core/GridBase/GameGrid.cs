using Game.Core.Enums;
using Game.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.GridBase
{
    //  p1 - p2
    //  |    |
    //  p0 - p3
    public struct GridBounds
    {
        public Vector3 P0;
        public Vector3 P1;
        public Vector3 P2;
        public Vector3 P3;
    }

    public class GameGrid : IProvidable
    {
        public GridCell[,] GridCellList { get; }
        public Vector2Int GridDimensions { get; }
        public GridBounds GridBounds { get; private set; }

        public GameGrid(Vector2Int gridDimension)
        {
            GridDimensions = gridDimension;

            GridCellList = new GridCell[gridDimension.x, gridDimension.y];
            for (var y = 0; y < gridDimension.y; y++)
            {
                for (var x = 0; x < gridDimension.x; x++)
                {
                    var targetCellType = ((x + y) % 2) == 1 ? AssetType.GRID_CELL_LIGHT : AssetType.GRID_CELL_DARK;
                    var gridCell = ServiceProvider.AssetLib.GetAsset<GridCell>(targetCellType, $"Cell-Y:{y}-X:{x}");
                    gridCell.TileCoord = new Vector2Int(x, y);
                    gridCell.transform.position = new Vector3(x, 0f, y);

                    GridCellList[x, y] = gridCell;
                }
            }

            GridBounds = new GridBounds
            {
                P0 = GridCellList[0, 0].transform.position,
                P1 = GridCellList[0, gridDimension.y - 1].transform.position,
                P2 = GridCellList[gridDimension.x - 1, gridDimension.y - 1].transform.position,
                P3 = GridCellList[gridDimension.x - 1, 0].transform.position,
            };

            ServiceProvider.Register(this);
        }

        public bool IsGridCoordValid(Vector2Int gridCoord)
        {
            return gridCoord.x >= 0
                   && gridCoord.x < GridDimensions.x
                   && gridCoord.y >= 0
                   && gridCoord.y < GridDimensions.y;
        }

        public GridCell GetTileInDirection(GridCell cell, CardinalDirection direction)
        {
            var targetCoord = cell.TileCoord + direction.ToVector2Int();
            return IsGridCoordValid(targetCoord) ? GridCellList[targetCoord.x, targetCoord.y] : null;
        }

        public GridCell GetRandomAvailableGridCell() {
            var emptyCellList = new List<GridCell>();
            foreach(var cell in GridCellList) {
                if(cell.currentObject == null) {
                    emptyCellList.Add(cell);
                }
            }
            return emptyCellList[Random.Range(0,emptyCellList.Count)];
    }
    }
}