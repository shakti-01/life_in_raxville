using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
//using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Avatar change details")]
    public CharacterDatabase CharacterDB;
    [Tooltip("This is the fbx object/model that will be replaced with choosen new model as per starter assets")]
    public GameObject characterGeometry;
    public Animator characterAnimator;
    //public TextMeshProUGUI characterNameText;
    //public Image characterImage;

    private int selectedOption = 0;
    //--------------------------------------for quest related
    [Header("Quest Details")]
    public QuestGiver questGiver;

    public Quest quest;
    public TextMeshProUGUI questCountString;

    public GameObject alertBox;
    public TextMeshProUGUI alertText;
    [Header("Stop looking when needed")]
    [SerializeField]
    private StarterAssets.StarterAssetsInputs input;
    [Header("Tutorial section")]
    [SerializeField]
    private GameObject bigAlertBox;
    [SerializeField] private TextMeshProUGUI bigAlertText;
    [Tooltip("used in tutorial to keep count of which step you are at")]
    public int textCount = 0;
    //[SerializeField] private GameObject tutorialPlatform;
    //[SerializeField] private GameObject tutorialSphere;

    [Tooltip("to zoom player camera when mouse wheel scroll")]
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float _value;

    [Tooltip("Teleport to main shed area if tutorial not done !")]
    public Vector3 tutorialPos;
    public Quaternion tutorialRot;

    [Space]
    [Tooltip("objects to deactivate during tutorial")]
    [SerializeField] private GameObject sideNotifs;
    private void Awake()
    {
        //chnage vsync to don't sync
        QualitySettings.vSyncCount = 0;
        Logger.Log("current vsync count: " + QualitySettings.vSyncCount);
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 5;//change this number to set the default avatar//also need to change this in the game scene !!!
        }
        else
        {
            LoadCharacter();
        }

        UpdateCharacter(selectedOption);

        //else keep the text as default

        //check if player is playing first time, if then tutorial
        if (!PlayerPrefs.HasKey("tutorialDone"))
        {
            gameObject.transform.position = tutorialPos;
            gameObject.transform.rotation = tutorialRot;
            sideNotifs.SetActive(false);
            Tutorial();
        }
        //zoom by mouse wheel
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) { input.cursorInputForLook = false; input.LookInput(Vector2.zero); }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            if (!input.cursorInputForLook) { input.cursorInputForLook = true; }
        }

        //zoom screen with mouse wheel input !
        _value = (float)(Input.mouseScrollDelta.y * 0.5);
        if (cinemachineVirtualCamera.m_Lens.FieldOfView - _value > 53) { cinemachineVirtualCamera.m_Lens.FieldOfView = 53; }
        else if(cinemachineVirtualCamera.m_Lens.FieldOfView - _value < 12) { cinemachineVirtualCamera.m_Lens.FieldOfView = 12; }
        cinemachineVirtualCamera.m_Lens.FieldOfView -= _value;
    }
    public void FoundSomething(string nameOfObjectFound)
    {
        //FindObjectOfType<AudioManager>().PlayOneShot("Obj clicked");
        if (quest.isActive)
        {
            Debug.Log(nameOfObjectFound);
            int returnValueOfFunction = quest.goal.ObjectIsFound(nameOfObjectFound);
            if (returnValueOfFunction == 1)
            {
                questGiver.QuestTargetFound(quest);
                /*quest.Complete();
                questGiver.QuestCompleted(quest);//quest.complete() needs to be executed before questcompleted() as we need that isActive from it
                //questGiver.questCount += 1;doing this in questgiver.cs
                questCountString.text = quest.rewardQuestCount;
                PlayerPrefs.SetString("questCountString", quest.rewardQuestCount);*/
            }
            else if(returnValueOfFunction == 0)
            {
                //Logger.Log("display a alert ui saying thats not the one we are looking for i guess");
                alertBox.SetActive(true);
                alertText.text = "Thats not the one we are looking for I guess. . .";
                StartCoroutine(DelayAction(4,1));
            }
            else
            {
                Logger.Log("Quest type mis match");
            }
        }
        
    }
    IEnumerator DelayAction(float delayTime,int task)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSecondsRealtime(delayTime);

        //Do the action after the delay time has finished.
        if(task == 1) 
        { 
            alertBox.SetActive(false); 
        }
        else
        {
            ChangeAvatarOfChar(selectedOption);
        }
    }
    //----------------------------------avatar related-----------
    private void UpdateCharacter(int selectedOption)
    {
        Character character = CharacterDB.GetCharacter(selectedOption);
        //characterImage.sprite = character.characterSprite;

        if (characterGeometry.transform.GetChild(0) != null)
        {
            Destroy(characterGeometry.transform.GetChild(0).gameObject);
        }
        GameObject newModel = Instantiate(character.characterFbxModel,characterGeometry.transform);
        newModel.transform.parent = characterGeometry.transform;
        //--changing the avatar
        //characterAnimator.avatar = character.characterAvatar;
        //need to change ava later for it to take effect
        StartCoroutine(DelayAction(1, 2));
    }
    private void ChangeAvatarOfChar(int selectedOption)
    {
        Character character = CharacterDB.GetCharacter(selectedOption);
        characterAnimator.avatar = character.characterAvatar;
    }

    private void LoadCharacter()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    //-----------------tutorial 
    public void Tutorial()
    {
        Time.timeScale = 0.0f;
        //Debug.Log(textCount);
        if (textCount == 0)
        {
            bigAlertText.text = "Welcome to the lands of Raxville" +"\nFor long times this land has been in peace and keeps on that path by helping each other out. Lets explore this land"+ "\nI will guide you on how to move";
            bigAlertBox.SetActive(true);
            StartCoroutine(WaitTillKeyDown());
        }
        else if (textCount == 1)
        {
            bigAlertText.text = "Use W, A, S, D keys to move around.\nHold down \"Shift\" to sprint\n\n You can also use arrow keys to move instead\nNow, try moving towards the platform infront of you and get on it.\nTry to get comfortable with the movement.\n\n Use \"Space\" to jump on the platform.";
            bigAlertBox.SetActive(true);
            CloseOnEvent.safeToClose = true;
            //Time.timeScale = 1f;
        }
        else if (textCount == 2)
        {
            bigAlertText.text = "Good job!!\nNow, lets learn how to interact with something.\n\nTo interact with any items for quests, just click on it. For now, click on the sphere you see floating in air.\n\n\n Hold \"C\" to lock the camera position. That way you can click easily !";
            bigAlertBox.SetActive(true);
            CloseOnEvent.safeToClose = true;
            //Time.timeScale = 1f;
        }
        else
        {
            bigAlertText.text = "Nice!! You are all set now !\nThere are few NPCs lets talk to them and help them solve their problems if they have any\n\nNow, we need to walk down the road and interact with the people we find...\nLets explore the woods , yay!";
            bigAlertBox.SetActive(true);
            CloseOnEvent.safeToClose = true;
            //Time.timeScale = 1f;
            PlayerPrefs.SetInt("tutorialDone", 1);
            sideNotifs.SetActive(true);
        }
    }

    
    private IEnumerator WaitTillKeyDown()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        while (true)
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1f;
                textCount += 1;
                GameObject.FindObjectOfType<AudioManager>().PlayOneShot("Big alert close");
                Tutorial();
                break;
            }
            yield return null;
        }
    }

    
}
