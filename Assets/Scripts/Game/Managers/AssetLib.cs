using Game.Core.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Managers
{
    public class AssetLib : MonoBehaviour, IProvidable
    {
        [SerializeField] private GameObject SnakeHeadPrefab;
        [SerializeField] private GameObject SnakeBodyPartPrefab;
        [SerializeField] private GameObject SnakeTailPrefab;

        [SerializeField] private GameObject AppleObjectPrefab;
        [SerializeField] private GameObject WallObjectPrefab;
        [SerializeField] private GameObject PortalObjectPrefab;

        [SerializeField] private GameObject StartScreenPrefab;
        [SerializeField] private GameObject FailScreenPrefab;
        [SerializeField] private GameObject SuccessScreenPrefab;
        [SerializeField] private GameObject GoalUiControllerPrefab;
        [SerializeField] private GameObject MobileInputUiPrefab;

        [SerializeField] private GameObject GridCellPrefab_Light;
        [SerializeField] private GameObject GridCellPrefab_Dark;

        private Transform _snakeRoot;
        private Transform _gridRoot;
        private Transform _interactableRoot;

        private void Awake()
        {
            ServiceProvider.Register(this);

            _snakeRoot = new GameObject("SnakeRoot").transform;
            _gridRoot = new GameObject("GridRoot").transform;
            _interactableRoot = new GameObject("InteractableRoot").transform;

            _snakeRoot.parent = transform;
            _gridRoot.parent = transform;
            _interactableRoot.parent = transform;
        }

        public T GetAsset<T>(AssetType assetType, string objectName) where T : MonoBehaviour
        {
            var asset = GetAsset<T>(assetType);
            if (asset == null)
            {
                return null;
            }

            asset.name = objectName;
            return asset;
        }

        public T GetAsset<T>(AssetType assetType) where T : class
        {
            var asset = GetAsset(assetType);
            return asset == null ? null : asset.GetComponent<T>();
        }

        private GameObject GetAsset(AssetType assetType)
        {
            return assetType switch
            {
                AssetType.SNAKE_HEAD => Instantiate(SnakeHeadPrefab, _snakeRoot),
                AssetType.SNAKE_BODY => Instantiate(SnakeBodyPartPrefab, _snakeRoot),
                AssetType.SNAKE_TAIL => Instantiate(SnakeTailPrefab, _snakeRoot),
                AssetType.APPLE => Instantiate(AppleObjectPrefab, _interactableRoot),
                AssetType.WALL => Instantiate(WallObjectPrefab, _interactableRoot),
                AssetType.PORTAL => Instantiate(PortalObjectPrefab, _interactableRoot),
                AssetType.START_SCREEN => Instantiate(StartScreenPrefab),
                AssetType.FAIL_SCREEN => Instantiate(FailScreenPrefab),
                AssetType.SUCCESS_SCREEN => Instantiate(SuccessScreenPrefab),
                AssetType.GOAL_UI_CONTROLLER => Instantiate(GoalUiControllerPrefab),
                AssetType.MOBILE_INPUT_UI => Instantiate(MobileInputUiPrefab),
                AssetType.GRID_CELL_LIGHT => Instantiate(GridCellPrefab_Light, _gridRoot),
                AssetType.GRID_CELL_DARK => Instantiate(GridCellPrefab_Dark, _gridRoot),
                _ => null
            };
        }
    }
}