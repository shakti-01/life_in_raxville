using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public StarterAssets.StarterAssetsInputs input;

    public QuestGiver questGiver;

    public Player player;

    public TextMeshProUGUI nameText, dialogueText;
    public GameObject acceptQuestBtn;

    private Queue<string> sentences;

    public Animator animator;
    // Start is called before the first frame update
    private bool questAtEndOfDialogue;
    void Start()
    {
        sentences = new Queue<string>();
        acceptQuestBtn.SetActive(false);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        //make the dialogue box visible and disable cursor input for look
        animator.SetBool("IsOpen", true);
        input.cursorInputForLook = false;

        nameText.text = dialogue.name;
        questAtEndOfDialogue = dialogue.questAtEndOfDialogue;

        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        acceptQuestBtn.SetActive(false);
        //nameText.text == "Paladin"
        if (questAtEndOfDialogue)
        {
            //bool inQuest = false;
            //acceptQuestBtn.SetActive(false);//make it false everytime its not the last sentence
            if (sentences.Count == 1 && !player.quest.isActive && questGiver.questCount <= 3)
            {
                acceptQuestBtn.SetActive(true);
            }
        }
        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        //instead of displaying the sentence directly we ll display each letter one by one;
        // we let the player skip the sentence before it animates
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// instead of displaying the sentence directly we ll display each letter one by one
    /// </summary>
    /// <param name="sentence"></param>
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;//gap of 1 frame after a letter
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        input.cursorInputForLook = true;
        if (player.quest.goal.targetSubmitted && player.quest.isActive)
        {
            Logger.Log("quest complete() was executed");
            player.quest.Complete();//this needs to be called first as we need the isActive from this function in below function
            questGiver.QuestCompleted(player.quest);
        }
        Logger.Log("End dialogues() was executed");
    }
}
