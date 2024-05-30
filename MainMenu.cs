using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    private void Start()
    {
        AudioListener.volume = 1f;
    }
    public void PlayGame()
    {
        //SceneManager.LoadScene(1);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quiting ..");
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
