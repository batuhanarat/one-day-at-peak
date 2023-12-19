using System.Collections.Generic;
using Game.Core.Enums;
using Game.Core.LevelBase;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoalUIController : MonoBehaviour
    {
        [SerializeField] private GoalEntryUI GoalObjectPrefab;
        [SerializeField] private VerticalLayoutGroup VerticalLayoutGroup;

        private Dictionary<InteractableObjectType, GoalEntryUI> _goalEntryDict = new();

        public void InitializeGoalUiController(Goal[] goals)
        {
            foreach (var goal in goals)
            {
                var goalObject = Instantiate(GoalObjectPrefab, VerticalLayoutGroup.transform);
                goalObject.InitializeGoalEntry(goal.GoalType, goal.TargetGoalCount);
                _goalEntryDict[goal.GoalType] = goalObject;
            }
        }

        public void GoalCountUpdated(InteractableObjectType objectType, int newCount)
        {
            if (!_goalEntryDict.ContainsKey(objectType))
            {
                return;
            }

            _goalEntryDict[objectType].UpdateGoalEntry(newCount);
        }
    }
}