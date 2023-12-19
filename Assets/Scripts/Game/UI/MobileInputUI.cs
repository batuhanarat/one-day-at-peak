using Game.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public delegate void OnDirectionClicked(InputKeyType keyType);

    public class MobileInputUI : MonoBehaviour
    {
        [SerializeField] private Button ForwardArrow;
        [SerializeField] private Button BackwardArrow;
        [SerializeField] private Button RightArrow;
        [SerializeField] private Button LeftArrow;

        private void Awake()
        {
            ForwardArrow.onClick.AddListener(() => { OnDirectionClicked?.Invoke(InputKeyType.FORWARD); });
            BackwardArrow.onClick.AddListener(() => { OnDirectionClicked?.Invoke(InputKeyType.BACKWARD); });
            RightArrow.onClick.AddListener(() => { OnDirectionClicked?.Invoke(InputKeyType.RIGHT); });
            LeftArrow.onClick.AddListener(() => { OnDirectionClicked?.Invoke(InputKeyType.LEFT); });
        }

        public event OnDirectionClicked OnDirectionClicked;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}