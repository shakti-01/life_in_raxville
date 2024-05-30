using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class ScrrenShot : MonoBehaviour
{
    [SerializeField] private GameObject alertBox;
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private GameObject screenShotImage;
    [SerializeField] private GameObject screenShotImageContainer;

    [SerializeField] private GameObject canvasObject;
    private Canvas canvas;
    private RawImage SSimage;
    private Texture2D texture;
    private string path;
    private string SS_directoryPath;

    [Tooltip("Toogle btn's text to change when hight resolution is toogled")]
    [SerializeField] private TextMeshProUGUI screenShotToogleBtnText;
    [Tooltip("Note text to display a warning when enabling high resolution ss")]
    [SerializeField] private TextMeshProUGUI noteText;
    private int highResScreenShot;

    void Start()
    {
        SSimage = screenShotImage.GetComponent<RawImage>();
        canvas = canvasObject.GetComponent<Canvas>();
        if (PlayerPrefs.HasKey("highResScreenShot")) 
        { 
            highResScreenShot = PlayerPrefs.GetInt("highResScreenShot");
            screenShotToogleBtnText.text = (highResScreenShot == 1 ? "Disable" : "Enable");
        }
        else { highResScreenShot = 0; screenShotToogleBtnText.text = "Enable"; }
        noteText.text = "";
        SS_directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Pictures\\Life in Raxville shots";
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
        {
            ScreenShotBtnOnClick(0);
        }
    }
    public void ScreenShotBtnOnClick(int type)
    {
        Logger.Log("path: " + SS_directoryPath);

        if(!Directory.Exists(SS_directoryPath)) 
{
            Directory.CreateDirectory(SS_directoryPath);
        }
        //type = 1 inclucde UI type = 0 Dont't include UI
        StartCoroutine(CaptureScreenShot(type));
        //StartCoroutine(CaptureScreenShotAsTexture());
        alertText.text = "Took screenshot to: " + SS_directoryPath;
        
    }

    IEnumerator DelayAction(float delayTime,byte task)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSecondsRealtime(delayTime);

        //Do the action after the delay time has finished.
        if (task == 1)
        {
            alertBox.SetActive(false);
        }
        else if(task == 2)
        {
            FindObjectOfType<AudioManager>().PlayOneShot("Screenshot");
        }
        else if(task == 3) 
        {
            alertBox.SetActive(true);
            StartCoroutine(DelayAction(3f, 1));
            screenShotImageContainer.SetActive(false) ;
            Destroy(texture);
        }
        else if(task == 4)
        {
            SSimage.texture = LoadSprite(path);
            screenShotImageContainer.SetActive(true);
            StartCoroutine(DelayAction(0.2f, 2));//play ss sound
            StartCoroutine(DelayAction(3f, 3));//show alert showing location of ss
        }
        else if(task == 5)
        {
            canvas.enabled = true;
        }
    }
    IEnumerator CaptureScreenShot(int type)
    {
        int resolution = (highResScreenShot == 1 ? 2 : 1);
        if (type == 1)
        {
            yield return new WaitForEndOfFrame();
            path = SS_directoryPath + "/_gameShot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            ScreenCapture.CaptureScreenshot(path, resolution);
            if (resolution == 1) { StartCoroutine(DelayAction(1.4f, 4)); }
            else { StartCoroutine(DelayAction(1.9f, 4)); }
        }
        else
        {
            canvas.enabled = false;
            yield return new WaitForEndOfFrame();
            path = SS_directoryPath + "/_gameShot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            ScreenCapture.CaptureScreenshot(path , resolution);
            StartCoroutine(DelayAction(0.11f, 5));
            if (resolution == 1) { StartCoroutine(DelayAction(1.4f, 4)); }
            else { StartCoroutine(DelayAction(2f, 4)); }
        }
    }

    private Texture2D LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            Logger.Log("path exits to load ss");
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return texture;
            //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            //return sprite;
        }
        return null;
    }
    /// <summary>
    /// to chnage screen shot resolution
    /// </summary>
    public void ChangeScreenShotResolution()
    {
        if (highResScreenShot == 1)
        {
            highResScreenShot = 0;
            PlayerPrefs.SetInt("highResScreenShot", 0);
            screenShotToogleBtnText.text = "Enable";
            noteText.text = "";
            
        }
        else
        {
            highResScreenShot = 1;
            PlayerPrefs.SetInt("highResScreenShot", 1);
            screenShotToogleBtnText.text = "Disable";
            noteText.text = "High resolution screen shot takes 5 times more space. Disable it if you are memory bound";
        }
    }
}
