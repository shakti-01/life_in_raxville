using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class OnClickTutorial : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private GameObject thisObject;
    [SerializeField] private GameObject tutorialPlatform;
    [SerializeField] private GameObject tutorialCage;
    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if (PlayerPrefs.HasKey("tutorialDone"))
        {
            Destroy(gameObject);
            Destroy(tutorialPlatform);
            Destroy(tutorialCage);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == gameObject.name)
                {
                    player.textCount += 1;
                    player.Tutorial();
                    Destroy(gameObject);
                    Destroy(tutorialPlatform);
                    Destroy(tutorialCage);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DelayAction(1.1f));
        }
    }
    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSecondsRealtime(delayTime);

        //Do the action after the delay time has finished.
        player.textCount += 1;
        player.Tutorial();
        Destroy(gameObject);

    }
}
