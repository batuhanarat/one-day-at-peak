using System;
using System.Collections.Generic;
using Game.Core.Enums;
using Game.Core.GridBase;
using Game.Core.LevelBase;
using Game.UI;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public static class ServiceProvider
    {
        public static GameState GameState;
        private static readonly Dictionary<Type, IProvidable> _registerDictionary = new();
        public static GameGrid GameGrid => GetManager<GameGrid>();
        public static AssetLib AssetLib => GetManager<AssetLib>();
        public static UIManager UiManager => GetManager<UIManager>();
        public static CameraManager CameraManager => GetManager<CameraManager>();
        public static LevelManager LevelManager => GetManager<LevelManager>();

        public static SpriteFactory SpriteFactory => GetManager<SpriteFactory>();
        public static LevelDataFactory LevelDataFactory => GetManager<LevelDataFactory>();
        

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeServiceProvider()
        {
            SceneManager.sceneLoaded += (_, _) =>
            {
                //Self registered.
                _ = new UIManager();
                _ = new LevelDataFactory();

                if (GameState == null)
                {
                    var StartLevel = GameObject.FindObjectOfType<LevelManager>().StartLevel;
                    GameState = new GameState { TargetLevelName = StartLevel };
                }
            };
        }

        private static T GetManager<T>() where T : class, IProvidable
        {
            if (_registerDictionary.ContainsKey(typeof(T)))
            {
                return (T)_registerDictionary[typeof(T)];
            }

            return null;
        }

        public static T Register<T>(T target) where T : class, IProvidable
        {
            _registerDictionary[typeof(T)] = target;
            return target;
        }
    }
}