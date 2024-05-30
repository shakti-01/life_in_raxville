using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickSound : MonoBehaviour
{
    public AudioManager audioManager;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainUI")
        {
            StartCoroutine(DelayAction(0.1f, 1));
        }
        else
        {
            StartCoroutine(DelayAction(0.1f, 2));
        }
    }
    
    public void OnButtonClick()
    {
        audioManager.PlayOneShot("Click");
    }
    public void NextDialogueSound() { audioManager.PlayOneShot("Big alert close"); }
    IEnumerator DelayAction(float delayTime,int task)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        if (task == 1)
        {
            if (audioManager == null) { audioManager = FindObjectOfType<AudioManager>(); }
            audioManager.Stop("Day Birds");
            audioManager.Stop("Night Noise");
            audioManager.Stop("Thunder1");
            audioManager.Stop("Thunder2");
            audioManager.Stop("Thunder3");
            audioManager.Stop("Inside Water");
        }
        else if(task == 2)
        {
            if (audioManager == null) { audioManager = FindObjectOfType<AudioManager>(); }
            Logger.Log("Audio manag was null in CLICK SOUND");
        }
    }
}
