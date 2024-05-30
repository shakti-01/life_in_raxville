using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    public Slider volumeSlider;
    public TMP_Dropdown graphicsDropdown;

    Resolution[] resolutions;

    private int selectedResolution;
    private int selectedGraphics;
    private float selectedVolume;
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Logger.Log("current vsync count: " + QualitySettings.vSyncCount);
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("selectedGraphics"))
        {
            selectedGraphics = 1;
        }
        else
        {
            selectedGraphics = PlayerPrefs.GetInt("selectedGraphics");
            setQuality(selectedGraphics);
            Debug.Log("setting graphics to " + selectedGraphics);
            graphicsDropdown.value = selectedGraphics;
            graphicsDropdown.RefreshShownValue();
        }
        //----volume pref----------
        if (!PlayerPrefs.HasKey("selectedVolume"))
        {
            selectedVolume = 0;
        }
        else
        {
            selectedVolume = PlayerPrefs.GetFloat("selectedVolume");
            setVolume(selectedVolume);
            Debug.Log("settings volume to " + selectedVolume);
            volumeSlider.value = selectedVolume;
        }
        //--------------------------------
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //set 1600 x 900 to current res if its there
        for(int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].width == 1600 && resolutions[i].height == 900) { currentResolutionIndex = i; }
        }

        resolutionDropdown.AddOptions(options);
        //-----------resolution prefs--------------
        if (!PlayerPrefs.HasKey("selectedResolution"))
        {
            selectedResolution = currentResolutionIndex;
            resolutionDropdown.value = selectedResolution;
            resolutionDropdown.RefreshShownValue();
        }
        else
        {
            selectedResolution = PlayerPrefs.GetInt("selectedResolution");
            //setResolution(selectedResolution);
            resolutionDropdown.value = selectedResolution;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("selectedResolution", resolutionIndex);
    }
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("selectedVolume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        int qualityIndexNew;
        if(qualityIndex == 0) { qualityIndexNew = 3; }
        else if(qualityIndex == 1) { qualityIndexNew = 4; }
        else { qualityIndexNew = 5; }
        QualitySettings.SetQualityLevel(qualityIndexNew);
        PlayerPrefs.SetInt("selectedGraphics", qualityIndex);
    }

    public void ResetGameProgress()
    {
        //PlayerPrefs.DeleteKey("questCountString");
        //PlayerPrefs.DeleteKey("questCount");
        //PlayerPrefs.DeleteKey("questIsActive");
        PlayerPrefs.DeleteKey("tutorialDone");

        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths)
        {
            //Logger.Log(filePath);
            if (filePath == Application.persistentDataPath + "\\player.save" || filePath == Application.persistentDataPath + "\\questTimes.save") {
                File.Delete(filePath);
                Logger.Log("Game progress deleted for "+filePath);
            }
        }
    }
}
