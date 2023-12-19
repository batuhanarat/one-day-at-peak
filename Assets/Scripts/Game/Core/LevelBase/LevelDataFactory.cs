using Game.Core.Enums;
using Game.Managers;
using UnityEngine;

namespace Game.Core.LevelBase
{
    public class LevelDataFactory : IProvidable
    {
        public LevelDataFactory()
        {
            ServiceProvider.Register(this);
        }

        public LevelData GetLevelData(LevelName levelName)
        {
            var levelData = Resources.Load<LevelData>($"LevelData/{levelName}");
            return Object.Instantiate(levelData);
        }
    }
}