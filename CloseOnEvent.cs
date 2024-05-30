using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOnEvent : MonoBehaviour
{
    public static bool safeToClose = false;
    private void Start()
    {
        safeToClose = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //Time.timeScale = 0f;
        if(safeToClose && Input.anyKeyDown)
        {
            FindObjectOfType<AudioManager>().PlayOneShot("Big alert close");
            Time.timeScale = 1f;
            safeToClose = false;
            gameObject.SetActive(false);
        }
    }
}
