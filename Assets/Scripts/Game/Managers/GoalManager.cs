using Game.Core.Enums;
using Game.Core.LevelBase;
using Game.Managers;
using UnityEngine;

public class GoalManager : IProvidable
{
    public Goal[] Goals;
    public GoalManager(Goal[] goals)
    {
        Goals = goals;
    }

    public bool onObjectInteracted(InteractableObjectType obj)
    {
        for (int i = 0; i < Goals.Length; i++)
        {
            if (obj == Goals[i].GoalType)
            {
                Goals[i].CollectedGoalCount++;
                Debug.Log(Goals[i].CollectedGoalCount);
                if (Goals[i].CollectedGoalCount == Goals[i].TargetGoalCount)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }
}
