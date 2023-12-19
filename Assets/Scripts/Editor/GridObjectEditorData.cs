using Game.Core.Enums;
using UnityEngine;

namespace Editor {
    [System.Serializable]
    public struct GridObjectData
    {
        public InteractableObjectType ObjectType;
        public Texture2D ObjectEditorTexture;
    }

    [CreateAssetMenu(fileName = "GridObjectEditorData", menuName = "EditorObjects/GridObjectData", order = 1)]
    public class GridObjectEditorData : ScriptableObject
    {
        public GridObjectData[] GridObjectData;
    
        public Texture2D GetTextureForType(InteractableObjectType objectType)
        {
            for (var i = 0; i < GridObjectData.Length; i++)
            {
                if (GridObjectData[i].ObjectType == objectType)
                {
                    return GridObjectData[i].ObjectEditorTexture;
                }
            }

            return null;
        }
    
    }
}