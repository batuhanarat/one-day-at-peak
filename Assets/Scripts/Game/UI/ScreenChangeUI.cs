using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ScreenChangeUI : MonoBehaviour, IUIObject
    {
        [SerializeField] private Button ContinueButton;

        private void Awake()
        {
            ContinueButton.onClick.AddListener(() => { OnContinueClicked?.Invoke(); });
        }

        public event Action OnContinueClicked;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}