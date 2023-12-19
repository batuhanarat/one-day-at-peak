using Game.Core.Enums;
using Game.UI;
using UnityEngine;

namespace Game.Managers
{
    public class InputManager : MonoBehaviour
    {
        private InputKeyType _latestInputKeyType;
        private MobileInputUI _mobileInputUi;

        private void Start()
        {
            _mobileInputUi = ServiceProvider.AssetLib.GetAsset<MobileInputUI>(AssetType.MOBILE_INPUT_UI);
            _mobileInputUi.OnDirectionClicked += OnDirectionClicked;
            _mobileInputUi.SetActive(true);
        }

        void Update()
        {
            UpdateInputStandalone();
        }

        private void UpdateInputStandalone()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnDirectionClicked(InputKeyType.FORWARD);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                OnDirectionClicked(InputKeyType.BACKWARD);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                OnDirectionClicked(InputKeyType.LEFT);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                OnDirectionClicked(InputKeyType.RIGHT);
            }
        }

        private void OnDirectionClicked(InputKeyType keyType)
        {
            _latestInputKeyType = keyType;
        }

        public InputKeyType GetAndConsumeInput()
        {
            var keyType = _latestInputKeyType;
            _latestInputKeyType = InputKeyType.NONE;
            return keyType;
        }

        public void DisableInputUi()
        {
            _mobileInputUi.SetActive(false);
        }
    }
}