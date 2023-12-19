using Game.Core.Enums;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoalEntryUI : MonoBehaviour
    {
        [SerializeField] private Image GoalTypeImage;
        [SerializeField] private TextMeshProUGUI GoalCountText;

        private int _targetGoalCount;

        public void InitializeGoalEntry(InteractableObjectType objectType, int goalCount)
        {
            _targetGoalCount = goalCount;
            GoalTypeImage.sprite = ServiceProvider.SpriteFactory.GetTexture(objectType);
            UpdateGoalEntry(0);
        }

        public void UpdateGoalEntry(int goalCount)
        {
            GoalCountText.text = $"{goalCount}/{_targetGoalCount}";
        }
    }
}