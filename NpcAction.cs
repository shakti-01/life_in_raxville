//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class NpcAction : MonoBehaviour
{
    public StarterAssets.StarterAssetsInputs input;

    [SerializeField] private DialogueManager dialogueManager;

    public GameObject actionBtn;
    public Animator DialogueBoxAnimator;
    public Transform playerTransform;
    [SerializeField]
    private float minDistance;
    private bool talking;
    void Start()
    {
        talking = false;
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    void Update()
    {
        if (minDistance >= Vector3.Distance(playerTransform.position, transform.position))
        {
            //view ui
            if (!actionBtn.activeSelf && !talking)
            {
                actionBtn.SetActive(true);
                input.cursorInputForLook = false;
                input.LookInput(Vector2.zero);
                talking = true;
            }
        }
        else
        {
            if (talking)
            {
                dialogueManager.EndDialogue();
                //DialogueBoxAnimator.SetBool("IsOpen", false);
                //input.cursorInputForLook = true;
            }
            talking = false;
            if (actionBtn.activeSelf)
            {
                actionBtn.SetActive(false);
            }
        }
    }
}
