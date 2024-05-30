using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestTimeData
{
    public float timeCounter;
    public uint quest1time, quest2time, quest3time;
    public QuestTimeData(float timeCounter, uint[] questTimes)
    {
        this.timeCounter = timeCounter;
        quest1time = questTimes[0];
        //Logger.Log("quest time data : " + quest1time);
        quest2time = questTimes[1];
        quest3time = questTimes[2];
    }

}