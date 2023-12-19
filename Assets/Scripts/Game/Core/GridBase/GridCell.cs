using Game.Core.Enums;
using UnityEngine;

namespace Game.Core.GridBase
{
    public class GridCell : MonoBehaviour
    {
        public Vector2Int TileCoord;

        public IInteractable currentObject;
    }
}