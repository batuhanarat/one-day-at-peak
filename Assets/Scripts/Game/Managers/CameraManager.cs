using Game.Core.GridBase;
using Settings;
using UnityEngine;

namespace Game.Managers
{
    public class CameraManager : MonoBehaviour, IProvidable
    {
        [SerializeField] private Camera _currentCamera;
        private float _defaultOrthographicSize;

        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        public void Initialize(GridBounds gridBounds)
        {
            var gridCenter = (gridBounds.P0 + gridBounds.P2) / 2f;

            var gridLenFirstDiagonal = gridBounds.P0 - gridBounds.P2;
            var gridLenSecondDiagonal = gridBounds.P1 - gridBounds.P3;

            var desiredFrustumRectWidth = Mathf.Max(Mathf.Abs(Vector3.Dot(gridLenFirstDiagonal, transform.right)),
                                                    Mathf.Abs(Vector3.Dot(gridLenSecondDiagonal, transform.right)));
            var desiredFrustumRectHeight = Mathf.Max(Mathf.Abs(Vector3.Dot(gridLenFirstDiagonal, transform.up)),
                                                     Mathf.Abs(Vector3.Dot(gridLenSecondDiagonal, transform.up)));

            _defaultOrthographicSize =
                Mathf.Max(desiredFrustumRectWidth / _currentCamera.aspect, desiredFrustumRectHeight) / 2f
                + GameConfig.OrthographicCameraSizeOffset;

            const float someBigDistance = 100f;
            _currentCamera.orthographicSize = _defaultOrthographicSize;
            _currentCamera.transform.position = gridCenter - transform.forward * someBigDistance;
        }
    }
}