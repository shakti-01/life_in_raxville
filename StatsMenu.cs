using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsMenu : MonoBehaviour
{
    [Header("Quest stats")]
    [SerializeField] private TextMeshProUGUI quest1Title, quest2Title, quest3Title;
    [SerializeField] private TextMeshProUGUI quest1Complete, quest2Complete, quest3Complete;
    [SerializeField] private GameObject statsWindow;
    [SerializeField] private QuestGiver questGiver;
    [SerializeField] private StarterAssets.StarterAssetsInputs input;
    static private byte toogleStatsWindow;
    [SerializeField] private Color red, green;

    [Header("Teleportation")]
    public TextMeshProUGUI teleportLockStatus;
    public TextMeshProUGUI teleportNote;
    public Player player;
    public Vector3[] areaPositions;

    [Space]
    [SerializeField] private TextMeshProUGUI quest1time;
    [SerializeField] private TextMeshProUGUI quest2time;
    [SerializeField] private TextMeshProUGUI quest3time;
    void Start()
    {
        toogleStatsWindow = 0;
    }

    public void StatsBtnOnClick()
    {
        if(toogleStatsWindow == 0)
        {
            toogleStatsWindow = 1;
            quest1Title.text = questGiver.quest1.title;
            quest2Title.text = questGiver.quest2.title;
            quest3Title.text = questGiver.quest3.title;
            if (questGiver.quest1.isComplete) { quest1Complete.text = "Completed"; quest1Complete.color = green; }
            else { quest1Complete.text = "X"; quest1Complete.color = red; }
            if (questGiver.quest2.isComplete) { quest2Complete.text = "Completed"; quest2Complete.color = green; }
            else { quest2Complete.text = "X"; quest2Complete.color = red; }
            if (questGiver.quest3.isComplete) { quest3Complete.text = "Completed"; quest3Complete.color = green; }
            else { quest3Complete.text = "X"; quest3Complete.color = red; }
            input.cursorInputForLook = false;
            statsWindow.SetActive(true);

            //for tp window
            if(questGiver.questCount > 3)
            {
                teleportNote.text = "* you can now teleport to the areas of interest";
                teleportLockStatus.text = "Teleportation : Unlocked";
            }
            else
            {
                teleportNote.text = "* complete all quests to unlock teleportation";
                teleportLockStatus.text = "Teleportation : Locked";
            }

            //for quest time update
            for(int i = 0; i < questGiver.questTimes.Length; i++)
            {
                if (i == 0)
                {
                    double secs = questGiver.questTimes[0];
                    int mins = (int)(secs / 60);
                    secs = (int)(secs % 60);
                    quest1time.text = "Completed in : "+mins+" mins : "+secs+" secs";
                }
                else if (i == 1)
                {
                    double secs = questGiver.questTimes[1];
                    int mins = (int)(secs / 60);
                    secs = (int)(secs % 60);
                    quest2time.text = "Completed in : " + mins + " mins : " + secs + " secs";
                }
                else if (i == 2)
                {
                    double secs = questGiver.questTimes[2];
                    int mins = (int)(secs / 60);
                    secs = (int)(secs % 60);
                    quest3time.text = "Completed in : " + mins + " mins : " + secs + " secs";
                }
            }
        }
        else
        {
            toogleStatsWindow = 0;
            input.cursorInputForLook = true;
            statsWindow.SetActive(false);
        }
    }

    /// <summary>
    /// to enable player tot tp after all quest done
    /// </summary>
    public void Teleport(int area) 
    {
        if(questGiver.questCount <= 3)
        {
            Logger.Log("can't tp");
            return;//only tp if all quest done
        }
        if(area == 0)
        {
            player.transform.position = areaPositions[0];
        }
        else if (area == 1) { player.transform.position = areaPositions[1]; }
        else if (area == 2) { player.transform.position = areaPositions[2]; }
        else if (area == 3) { player.transform.position = areaPositions[3]; }
        else if (area == 4) { player.transform.position = areaPositions[4]; }
        else if (area == 5) { player.transform.position = areaPositions[5]; }
        else if (area == 6) { player.transform.position = areaPositions[6]; }
        else if (area == 7) { player.transform.position = areaPositions[7]; }
        else if (area == 8) { player.transform.position = areaPositions[8]; }
        else
        {
            Logger.LogWarning("Invalid area : " + area);
        }
        StatsBtnOnClick();
    }

}
