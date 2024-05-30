using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isComplete;

    public short questNumber;
    public string title;
    [TextArea(2,4)]
    public string description;
    public string rewardQuestCount;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        isComplete = true;
        Logger.Log(title + " was Completed");
    }
}
