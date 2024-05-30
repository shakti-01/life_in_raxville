using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest1, quest2, quest3;
    [HideInInspector]
    public int questCount; 
    //public GameObject player;
    public Player player;
    public GameObject questWindow;
    public GameObject closeBtn;
    public TextMeshProUGUI titleText, descriptionText;
    [SerializeField] private TextMeshProUGUI questCountString;

    [Header("Display at quest end")]
    public GameObject bigAlertBox;
    public TextMeshProUGUI bigAlertText;

    [Tooltip("To refresh fire array")]
    [SerializeField]
    private TimeController timeController;

    [Tooltip("Produce sound from treasure box when u accept the quest 3")]
    [SerializeField] private AudioSource treasureBox;

    [Header("Objects to destroy as per quests done")]
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject excalibur;
    [SerializeField] private GameObject treasure;

    [Header("Player position change")]
    [Tooltip("To chnage player postion before saving if it is in swim mode")]
    [SerializeField] private Animator anim;
    private int swim;

    [Space]
    [SerializeField] private GameObject questActiveStateContainer;
    [SerializeField] private TextMeshProUGUI questActiveStateText;

    [Space]
    [SerializeField] private GameObject npcToTalkContainer;
    [SerializeField] private TextMeshProUGUI npcToTalkText;

    [Space]
    private float timeCounter;
    private bool timeCounterIsActive;
    public uint[] questTimes;

    private void Start()
    {
        questTimes = new uint[3];

        swim = Animator.StringToHash("swim");
        questCount = 1;
       
        LoadPlayer();
        LoadQuestTimes();
        StartCoroutine(DelayAction(0f, 1));
        StartCoroutine(DelayAction(0f, 2));
    }

    private void Update()
    {
        if(timeCounterIsActive) timeCounter += Time.deltaTime;
    }
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        switch (questCount)
        {
            case 1: 
                titleText.text = quest1.title;
                descriptionText.text = quest1.description;
                break;
            case 2:
                titleText.text = quest2.title;
                descriptionText.text = quest2.description;
                break;
            case 3:
                titleText.text = quest3.title;
                descriptionText.text = quest3.description;
                break;
            default:
                Logger.LogWarning("Invalid quest count = " + questCount);
                CloseQuestWindow();
                break;
        }
        //titleText.text = quest.title;
        //descriptionText.text = quest.description;
    }
    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        switch (questCount)
        {
            case 1:
                quest1.isActive = true;
                player.quest = quest1;timeCounterIsActive = true;
                break;
            case 2:
                quest2.isActive = true;
                player.quest = quest2;timeCounterIsActive = true;
                break;
            case 3:
                quest3.isActive = true;
                player.quest = quest3;timeCounterIsActive = true;
                treasureBox.Play();
                break;
            default:
                Logger.LogWarning("Invalid quest count = " + questCount);
                break;
        }
        //int isActive = player.quest.isActive ? 1 : 0;
        //PlayerPrefs.SetInt("questIsActive", isActive);
        
        StartCoroutine(DelayAction(3.5f, 1));
        npcToTalkContainer.SetActive(false);
        SaveSystem.SavePlayer(player);
    }
    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }
    /// <summary>
    /// display situation alert after quest ends
    /// </summary>
    /// <param name="quest"></param>
    public void QuestCompleted(Quest quest)
    {
        StopAllCoroutines();
        string questEndText;
        switch (quest.questNumber)
        {
            case 1: 
                bigAlertBox.SetActive(true);
                questEndText = "You gave the book to paladin. He was almost certain he wouldn't be able to find it, but now he is happy and gave it to the old lady.\n\n The lady was thankfull and sent her good wishes to you and paladin was happy he got one more request of the people of the Raxville fullfiled under his name";
                StartCoroutine(TypeSentence(questEndText));
                questTimes[0] = (uint)timeCounter;
                break;
            case 2:
                bigAlertBox.SetActive(true);
                questEndText = "You were accepted by the Excalibur. And as promised you lent it to Donquit. He was extremely happy and so were you.\n\n However, you denied the money out of goodwill. If you accepted it, it would soon have been used up. But your kind action earned you the respect of all the people for a lifetime.";
                StartCoroutine(TypeSentence(questEndText));
                questTimes[1] = (uint)timeCounter;
                break;
            case 3:
                bigAlertBox.SetActive(true);
                questEndText = "After lots of looking, you found the box and went to Megan to open it together. You are excited for the treasure..\n\n But it seems the treasure she was talking about was her pictures of childhood and few toys...So it turns out it indeed had a treasure \n...BUT only for her !!But that does not stop us in our quest to keep the people of the Land of Raxville contented";
                StartCoroutine(TypeSentence(questEndText));
                questTimes[2] = (uint)timeCounter;
                break;
            default:
                Logger.LogWarning("Invalid quest Number : " + quest.questNumber);
                break;
        }
        SaveSystem.SaveQuestTime(timeCounter, questTimes);
        timeCounterIsActive = false;
        timeCounter = 0;
        //destroy the item you found in the quest
        
        questCount += 1;
        //PlayerPrefs.SetInt("questCount", questCount);


        questCountString.text = quest.rewardQuestCount;
        //PlayerPrefs.SetString("questCountString", quest.rewardQuestCount);

        //int isActive = player.quest.isActive ? 1 : 0;
        //PlayerPrefs.SetInt("questIsActive", isActive);
        SaveSystem.SavePlayer(player);
        FindObjectOfType<AudioManager>().PlayOneShot("Quest done");
        StartCoroutine(DelayAction(4f, 1));
        StartCoroutine(DelayAction(3.4f, 2));
    }

    public void QuestTargetFound(Quest quest)
    {
        quest.goal.targetFound = true;
        string targetFoundText;
        StopAllCoroutines();
        switch (quest.questNumber)
        {
            case 1:
                bigAlertBox.SetActive(true);
                targetFoundText = "This book might be it...\n\nLets ask Paladin to confirm if this is the book he was talking about";
                StartCoroutine(TypeSentence(targetFoundText));
                StartCoroutine(DelayAction(3f, "a book"));
                break;
            case 2:
                bigAlertBox.SetActive(true);
                targetFoundText = "This seems to have a refined moonlit glow compared to other swords,\n its no doubt the holy sword Excalibur !\n\nTime to show it to the old man that we are not just anybody but someone special as choosen by Excalibur\n\nLets also give it a few swings on our way back !";
                StartCoroutine(TypeSentence(targetFoundText));
                StartCoroutine(DelayAction(3f, "the excalibur"));
                break;
            case 3:
                bigAlertBox.SetActive(true);
                targetFoundText = "We have surely have enough experience to know that this is indeed a treasure!\nAnd judging by the size of the box...\n\nBut lets not..\nlets open it together with Megan...we can share..\nWe should share !";
                StartCoroutine(TypeSentence(targetFoundText));
                StartCoroutine(DelayAction(3f, "the treasure"));
                break;
            default:
                Logger.LogWarning("Invalid quest Number : " + quest.questNumber);
                break;
        }
        SaveSystem.SavePlayer(player);
        GameObject target = GameObject.Find(quest.goal.targetToFind);
        timeController.RefreshFireArray(target);
        Destroy(target);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.TryLoadPlayer();
        if(data != null)
        {
            questCountString.text = data.questCountString;
            //-----------------------
            questCount = data.questCount;
            switch (questCount)
            {
                case 1: //no need to make quest 1 complete as player pref saves from 2nd quest and default = 1
                    break;
                case 2:
                    quest1.isComplete = true;
                    Destroy(book);
                    break;
                case 3:
                    quest1.isComplete = true;
                    quest2.isComplete = true;
                    timeController.RefreshFireArray(excalibur);
                    Destroy(book);
                    Destroy(excalibur);
                    break;
                case 4:
                    quest1.isComplete = true;
                    quest2.isComplete = true;
                    quest3.isComplete = true;
                    timeController.RefreshFireArray(excalibur);
                    Destroy(book);
                    Destroy(excalibur);
                    Destroy(treasure);
                    break;
                default: Logger.LogWarning("Invalid quest count: " + questCount); break;
            }
            //--------------------------
            if (data.questIsActive)
            {
                switch (questCount)
                {
                    case 1: quest1.isActive = true; player.quest = quest1; break;
                    case 2: quest2.isActive = true; player.quest = quest2; break;
                    case 3: quest3.isActive = true; player.quest = quest3; treasureBox.Play(); break;
                    default: Logger.LogWarning("Invalid quest count: " + questCount); break;
                }
                timeCounterIsActive = true;
            }
            player.quest.goal.targetFound = data.questTargetFound;

            //change position of player after a while as if u set it here sometimes unity bring it back to the manual last position
            StartCoroutine(DelayAction(0.05f, data.position));
            
        }
    }

    private void LoadQuestTimes()
    {
        QuestTimeData timeData = SaveSystem.TryLoadQuestTimes();
        if(timeData == null)
        {
            timeCounter = 0;
            Logger.Log("no time data");
            return;//terminate function with this if no data
        }
        questTimes[0] = timeData.quest1time;
        Logger.Log(timeData.quest1time+" time data while load");
        questTimes[1] = timeData.quest2time;
        questTimes[2] = timeData.quest3time;
        timeCounter = timeData.timeCounter;
    }

    IEnumerator DelayAction(float delayTime, float[] position)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSecondsRealtime(delayTime);
        //Do the action after the delay time has finished.
        Vector3 positionOfPlayer;
        positionOfPlayer.x = position[0];
        positionOfPlayer.y = position[1];
        positionOfPlayer.z = position[2];
        player.transform.position = positionOfPlayer;
        Logger.Log(positionOfPlayer.ToString());
    }

    IEnumerator DelayAction(float delayTime, int task)
    {
        if(task == 1)
        {
            questActiveStateContainer.SetActive(false);
            yield return new WaitForSeconds(delayTime);
            if (player.quest.isActive)
            {
                questActiveStateText.text = "In a quest";
            }
            else { questActiveStateText.text = "No active quests"; }
            questActiveStateContainer.SetActive(true);
        }
        else if(task == 2)
        {
           npcToTalkContainer.SetActive(false);
           yield return new WaitForSeconds(delayTime);
           if (!player.quest.isActive && questCount == 1)
           {
               npcToTalkText.text = "Lets talk to Paladin";
           }
           else if(!player.quest.isActive && questCount == 2)
           {
               npcToTalkText.text = "Lets talk to Donquit";
           }
           else if (!player.quest.isActive && questCount == 3)
           {
               npcToTalkText.text = "Lets talk to Megan";
           }
            npcToTalkContainer.SetActive(true);
            if (player.quest.isActive) { npcToTalkContainer.SetActive(false); }
            /*if (!player.quest.isActive && questCount > 3)
            {
                npcToTalkContainer.SetActive(false);
            }*/
        }
    }
    IEnumerator DelayAction(float delayTime, string target)
    {
        
        questActiveStateContainer.SetActive(false);
        yield return new WaitForSeconds(delayTime);
        
        questActiveStateText.text = "You now have "+target;
        questActiveStateContainer.SetActive(true);
        
    }
    IEnumerator TypeSentence(string sentence)
    {
        Time.timeScale = 0f;
        bigAlertText.text = "";
        WaitForSecondsRealtime time = new WaitForSecondsRealtime(0.02f);
        foreach (char letter in sentence.ToCharArray())
        {
            bigAlertText.text += letter;
            //yield return null;//gap of 1 frame after a letter
            yield return time;
        }
        //instead of closing it after sometime we ll close it when user presses any key after text is displayed.
        //StartCoroutine(DelayAction(5));
        CloseOnEvent.safeToClose = true;
    }

    public void SavePosition()
    {
        if (anim.GetBool(swim))
        {
            Vector3 newPosition = player.transform.position;
            newPosition.y += 13f;
            player.transform.position = newPosition;
            Logger.Log("position raised as swiming");
        }
        SaveSystem.SavePlayer(player);
        Logger.Log("position saved at " + player.transform.position.ToString());
    }

    /*bool WantsToQuit()
    {
        Logger.Log("Player prevented from quitting.");
        SaveSystem.SaveQuestTime(timeCounter, questTimes);
        return true;
    }

    [RuntimeInitializeOnLoadMethod]
    void RunOnStart()
    {
        Application.wantsToQuit += WantsToQuit;
    }*/
    private void OnApplicationQuit()
    {
        SaveSystem.SaveQuestTime(timeCounter, questTimes);
        Logger.Log("quest times saved");
    }
}
