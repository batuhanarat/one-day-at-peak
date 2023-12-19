using System;
using Game.Core.Enums;
using Game.Core.GridBase;
using Game.Core.SnakeBase;
using Game.InteractableObjects;
using Game.Managers;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core.LevelBase
{
    public class LevelManager : MonoBehaviour,IProvidable
    {
        [HideInInspector] public LevelName StartLevel;
        private GoalManager _goalManager;

        [SerializeField] private PlayerController.PlayerController PlayerController;
        private GameGrid _gameGrid;
        private Snake _snake;

        public void Awake()
        {
            ServiceProvider.Register(this);

        }

        public void Start()
        {
            var levelData = ServiceProvider.LevelDataFactory.GetLevelData(ServiceProvider.GameState.TargetLevelName);

            _goalManager = new GoalManager(levelData.Goals);
            _gameGrid = new GameGrid(levelData.GridDimensions);
            _snake = new Snake();

            PopulateGridItems(levelData);
            PlayerController.Initialize(_snake);
            ServiceProvider.CameraManager.Initialize(_gameGrid.GridBounds);
        }

        public void OnInteractableCollected(InteractableObjectType obj)
        {
            bool success = _goalManager.onObjectInteracted(obj);
            if (success)
            {
                OnLevelSuccess();
            }
        }

        public void StartNextLevel()
        {
            switch (ServiceProvider.GameState.TargetLevelName)
            {
                case (LevelName.LEVEL0):
                    ServiceProvider.GameState.TargetLevelName = LevelName.LEVEL1;
                    break;
                case (LevelName.LEVEL1):
                    ServiceProvider.GameState.TargetLevelName = LevelName.LEVEL2;
                    break;
                case (LevelName.LEVEL2):
                    ServiceProvider.GameState.TargetLevelName = LevelName.LEVEL0;
                    break;
            }
            
            SceneManager.LoadScene(GameConfig.GameSceneName);
        }

        private void PopulateGridItems(LevelData levelData )
        {
            var gridDimension = levelData.GridDimensions;
            int rowsCount = gridDimension.x;
            int colsCount = gridDimension.y;

            bool appleExists = false;
            for (int  i = 0; i < colsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    InteractableObjectType item = levelData.GridItems[i, j];
                    switch (item) 
                    {
                        case InteractableObjectType.APPLE:
                            var apple = ServiceProvider.AssetLib.GetAsset<AppleInteractableObject>(AssetType.APPLE);
                            apple.setPosition(_gameGrid.GridCellList[i,j]);
                            appleExists = true;
                            break;
                        case InteractableObjectType.WALL:
                            var wall = ServiceProvider.AssetLib.GetAsset<WallInteractableObject>(AssetType.WALL);
                            wall.setPosition(_gameGrid.GridCellList[i,j]);
                            break;
                        case InteractableObjectType.PORTAL:
                          //  var apple = ServiceProvider.AssetLib.GetAsset<AppleInteractableObject>(AssetType.APPLE);
                            //apple.setPosition(_gameGrid.GridCellList[i,j]);
                            break;
                    }

                }

            }
            if (!appleExists)
            {
                GridCell cell = ServiceProvider.GameGrid.GetRandomAvailableGridCell();
                ServiceProvider.AssetLib.GetAsset<AppleInteractableObject>(AssetType.APPLE).setPosition(cell);
            }
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(GameConfig.GameSceneName);
        }

        public void OnLevelFailed()
        {
            PlayerController.StopController();
            ServiceProvider.UiManager.ShowFailScreen(TryAgain);
        }

        public void OnLevelSuccess()
        {
            PlayerController.StopController();
            ServiceProvider.UiManager.ShowSuccessScreen(StartNextLevel);
        }
    }
}