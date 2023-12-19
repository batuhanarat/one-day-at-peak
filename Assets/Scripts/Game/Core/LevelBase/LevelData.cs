using System;
using Game.Core.Enums;
using UnityEngine;

namespace Game.Core.LevelBase
{
    [Serializable]
    public struct Goal
    {
        public InteractableObjectType GoalType;
        public int TargetGoalCount;
        [NonSerialized] public int CollectedGoalCount;
    }

    [CreateAssetMenu(fileName = "GameGridData", menuName = "SnakeData/GameGridData", order = 1)]
    public class LevelData : ScriptableObject, ISerializationCallbackReceiver
    {
        public Vector2Int GridDimensions;
        public Goal[] Goals;

        public InteractableObjectType[] GridItemSerializedData;
        public InteractableObjectType[,] GridItems;

        public void OnAfterDeserialize()
        {
            GridItems = new InteractableObjectType[GridDimensions.x, GridDimensions.y];
            for (var y = 0; y < GridDimensions.y; y++)
            {
                for (var x = 0; x < GridDimensions.x; x++)
                {
                    GridItems[x, y] = GridItemSerializedData[y * GridDimensions.x + x];
                }
            }
        }

        public void OnBeforeSerialize() { }
    }
}