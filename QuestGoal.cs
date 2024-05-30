//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount, currentAmount;
    public string targetToFind;
    public bool targetFound;
    public bool targetSubmitted;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public int ObjectIsFound(string nameOfObjectFound)
    {
        if(goalType == GoalType.Find)
        {
            if(nameOfObjectFound == targetToFind)
            {
                //currentAmount += 1;
                return 1;
            }
            else
            {
                return 0;
            }
        }
        return -1;
    }
}

public enum GoalType
{
    Find
}

