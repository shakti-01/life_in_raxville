using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Tooltip("Change text when mute or unmute")]
    [SerializeField] private TextMeshProUGUI muteAudioBtnText;
    private int muteAllAudioToogle;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public StarterAssets.StarterAssetsInputs input;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private TextMeshProUGUI settingsToogleText;
    private bool settingsMenuIsOpen;

    [Tooltip("Starter assets sensitivity setting to chnage and save in player pref")]
    [SerializeField] private StarterAssets.StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private Slider sensitivitySlider;

    [Space]
    [SerializeField] private TimeController timeController;

    [Space]
    [SerializeField] private GameObject timeSpeedBtn;
    [SerializeField] private TextMeshProUGUI timeSpeedText;

    [Tooltip("Time after which to display the time speed animation")]
    [SerializeField] private float seconds;
    private WaitForSeconds wait;

    [Space]
    [SerializeField]private StarterAssets.ThirdPersonController controller;

    // Update is called once per frame
    private void Start()
    {
        GameIsPaused = false;
        muteAllAudioToogle = 1;
        AudioListener.volume = 1f;
        settingsMenuIsOpen = false;

        if (PlayerPrefs.HasKey("sensitivity"))
        {
            starterAssetsInputs.sensitivity = PlayerPrefs.GetFloat("sensitivity");
            sensitivitySlider.value = starterAssetsInputs.sensitivity;
        }
        else { starterAssetsInputs.sensitivity = 1f; }

        wait = new WaitForSeconds(seconds);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseBtnOnClick();
        }
    }

    public void PauseBtnOnClick()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Resume()
    {
        input.cursorInputForLook = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (settingsMenuIsOpen) { ToogleSettingsMenu(); }
    }
    void Pause()
    {
        input.cursorInputForLook = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;//make it false when u go to menu //made it false in start so same
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("quitting game...");
        Application.Quit();
    }

    //Save position is in QuestGives.cs
    
    public void MuteAllAudio()
    {
        if(muteAllAudioToogle == 1)
        {
            AudioListener.volume = 0f;
            muteAudioBtnText.text = "Unmute Audio";
            muteAllAudioToogle = 0;
        }
        else
        {
            AudioListener.volume = 1f;
            muteAudioBtnText.text = "Mute Audio";
            muteAllAudioToogle = 1;
        }
    }
    /// <summary>toogle settings window</summary>
    public void ToogleSettingsMenu()
    {
        if (!settingsMenuIsOpen)
        {
            settingsMenuIsOpen = true;
            settingsMenu.SetActive(true);
            settingsToogleText.text = "Close Settings";
        }
        else
        {
            settingsMenuIsOpen = false;
            settingsMenu.SetActive(false);
            settingsToogleText.text = "Open Settings";
        }
    }
    /// <summary>change starter assets sensitivity dynamically</summary>
    public void ChangeSensitivity(float value)
    {
        starterAssetsInputs.sensitivity = value;
        PlayerPrefs.SetFloat("sensitivity", value);
        //Logger.Log(value);
    }
    /// <summary>change time multiplier</summary>
    public void ChangeTimeMultiplier(float value)
    {   
        StopAllCoroutines();
        StartCoroutine(DelayAction(value));
    }
    IEnumerator DelayAction(float value)
    {
        timeSpeedBtn.SetActive(false);
        yield return wait;
        timeController.timeMultiplier = value;
        timeSpeedText.text = "Time Speed : " + value+"x";
        timeSpeedBtn.SetActive(true);
    }
    public void ChangeSprintSpeed(float value)
    {
        controller.SprintSpeed = value;
    }
}
