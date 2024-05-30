using System;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.Audio;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using System.Linq;

public class TimeController : MonoBehaviour
{
    public float timeMultiplier;

    [SerializeField] private float startHour;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Light sunLight;

    [SerializeField] private float sunriseHour;

    [SerializeField] private float sunsetHour;

    [SerializeField] private Color dayAmbientLight;

    [SerializeField] private Color nightAmbientLight;

    [SerializeField] private AnimationCurve lightChangeCurve;

    [SerializeField] private float maxSunLightIntensity;

    [SerializeField] private Light moonLight;

    [SerializeField] private float maxMoonLightIntensity;

    [SerializeField] private Material sunriseSkyMaterial, beforeNoonSkyMaterial, daySkyMaterial,  cloudySkyMaterial, eveningSkyMaterial, sunsetSkyMaterial, nightSkyMaterial, nightSky2Material, nightSky3Material, defaultSkyMaterial;

    [SerializeField] private Color sunriseFog, dayFog, cloudyFog, sunsetFog, sunsetYellowFog, nightFog, night2Fog, night3Fog;
    [Space]

    [SerializeField] public AudioManager audioManager;

    [SerializeField]private GameObject[] fire;

    private DateTime currentTime;

    private TimeSpan sunriseTime, sunsetTime;

    [Header("Random audios section")]
    [SerializeField] private int audioIntervalMultiplier;
    private byte isNight;

    private bool CR_fogColor_running;

    [Header("Lightmap data change")]
    [SerializeField] private CustomLightMap[] lightmapDayTextures;
    [SerializeField] private CustomLightMap[] lightmapNightTextures;

    ///<summary>check if day or night setting is already active</summary>
    private bool daySettingActive, nightSettingActive;
    private bool CR_sunColor_running;
    [SerializeField] private Color[] sunLightColors;

    [Tooltip("Time to lerp the fog color and sunlight color when time multiplier is 100")]
    ///<summary>Time to lerp the fog color and sunlight color when time multiplier is 100</summary>
    [SerializeField] private byte fogLerpDuration, sunlightLerpDuration;
    /// <summary>
    /// Check if time blocks are running
    /// </summary>
    private bool block1isActive, block2isActive, block3isActive, block4isActive, block5isActive, block6isActive, block7isActive, block8isActive, block9isActive, block10isActive, block11isActive;

    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        fire = GameObject.FindGameObjectsWithTag("Fire");
        isNight = 0;
        nightSettingActive = false;
        daySettingActive = true;

        foreach (GameObject obj in fire)
        {
            obj.SetActive(false);
        }
        block1isActive = block2isActive = block3isActive = block4isActive = block5isActive = block6isActive = block7isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;

        if (audioManager == null) { audioManager = FindObjectOfType<AudioManager>(); }
        StartCoroutine(DelayAction(0.1f));
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
        float sunLightRotation;
        
        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));

        //chanage skybox and fog as per currenr time
        if (timeText.text == "05:45" && !block1isActive)
        {
            Logger.Log("block 1");
            block1isActive = true;
            block2isActive = block3isActive = block4isActive = block5isActive = block6isActive = block7isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;
            isNight = 0;
            audioManager.Stop("Night Noise");
            audioManager.Play("Day Birds");
            RenderSettings.skybox = sunriseSkyMaterial;
            if (!CR_fogColor_running){ StartCoroutine(ChangeFogColor(sunsetFog, fogLerpDuration * 100/timeMultiplier)); }
            else { RenderSettings.fogColor = sunsetFog; }
            RenderSettings.fogDensity = 0.006f;
            foreach (GameObject obj in fire)
            {
                obj.SetActive(false);
            }
            if (!daySettingActive)
            {
                ChangeLight(0);
            }
            if (!CR_sunColor_running) { StartCoroutine(ChangeSunColor(sunLightColors[0], sunlightLerpDuration * 100/timeMultiplier)); }
            else { sunLight.color = sunLightColors[0]; }
        }
        else if (timeText.text == "05:50" && !block2isActive)
        {
            Logger.Log("block 2");
            block2isActive = true;
            block1isActive = block3isActive = block4isActive = block5isActive = block6isActive = block7isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(sunriseFog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = sunriseFog; }
            RenderSettings.fogDensity = 0.007f;
        }
        else if (timeText.text == "06:30" && !block3isActive)
        {
            Logger.Log("block 3");
            block3isActive = true;
            block1isActive = block2isActive = block4isActive = block5isActive = block6isActive = block7isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;
            RenderSettings.skybox = beforeNoonSkyMaterial;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(dayFog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = dayFog; }
            RenderSettings.fogDensity = 0.006f;
            if (!CR_sunColor_running) { StartCoroutine(ChangeSunColor(sunLightColors[1], sunlightLerpDuration * 100 / timeMultiplier)); }
            else { sunLight.color = sunLightColors[1]; }
        }
        else if(timeText.text == "10:30" && !block4isActive)
        {
            Logger.Log("block 4");
            block4isActive = true;
            block1isActive = block2isActive = block3isActive = block5isActive = block6isActive = block7isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;
            RenderSettings.skybox = daySkyMaterial;
            if (!CR_sunColor_running) { StartCoroutine(ChangeSunColor(sunLightColors[2], sunlightLerpDuration * 100 / timeMultiplier)); }
            else { sunLight.color = sunLightColors[2]; }
        }
        else if (timeText.text == "15:30" && !block5isActive)
        {
            Logger.Log("block 5");
            block5isActive = true;
            block1isActive = block2isActive = block3isActive = block4isActive = block6isActive = block7isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;
            audioManager.PlayDelayed("Thunder1", 2 * (100/timeMultiplier));
            audioManager.PlayDelayed("Thunder2", 10 * (100 / timeMultiplier));
            audioManager.PlayDelayed("Thunder3", 17 * (100 / timeMultiplier));
            RenderSettings.skybox = cloudySkyMaterial;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(cloudyFog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = cloudyFog; }
            RenderSettings.fogDensity = 0.011f;
            if (!CR_sunColor_running) { StartCoroutine(ChangeSunColor(sunLightColors[3], sunlightLerpDuration * 100 / timeMultiplier)); }
            else { sunLight.color = sunLightColors[3]; }
        }
        else if (timeText.text == "17:00" && !block6isActive)
        {
            Logger.Log("block 6");
            block6isActive = true;
            block1isActive = block2isActive = block3isActive = block4isActive = block5isActive = block7isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;
            RenderSettings.skybox = eveningSkyMaterial;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(sunsetFog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = sunsetFog; }
            RenderSettings.fogDensity = 0.006f;
            if (!CR_sunColor_running) { StartCoroutine(ChangeSunColor(sunLightColors[4], sunlightLerpDuration * 100 / timeMultiplier)); }
            else { sunLight.color = sunLightColors[4]; }
        }
        else if(timeText.text == "18:00" && !block7isActive)
        {
            Logger.Log("block 7");
            block7isActive = true;
            block1isActive = block2isActive = block3isActive = block4isActive = block5isActive = block6isActive = block8isActive = block9isActive = block10isActive = block11isActive = false;
            isNight = 2;
            RenderSettings.skybox = sunsetSkyMaterial;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(sunsetYellowFog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = sunsetYellowFog; }
            if (!CR_sunColor_running) { StartCoroutine(ChangeSunColor(sunLightColors[5],sunlightLerpDuration * 100 / timeMultiplier)); }
            else { sunLight.color = sunLightColors[5]; }
        }
        else if (timeText.text == "18:30" && !block8isActive)
        {
            block8isActive = true;
            block1isActive = block2isActive = block3isActive = block4isActive = block5isActive = block6isActive = block7isActive = block9isActive = block10isActive = block11isActive = false;
            RenderSettings.skybox = defaultSkyMaterial;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(nightFog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = nightFog; }
            RenderSettings.fogDensity = 0.008f;
            if (!nightSettingActive) { ChangeLight(1); }
            foreach (GameObject obj in fire)
            {
                obj.SetActive(true);
            }
        }
        else if (timeText.text == "19:00" && !block9isActive)
        {
            block9isActive = true;
            block1isActive = block2isActive = block3isActive = block4isActive = block5isActive = block6isActive = block7isActive = block8isActive = block10isActive = block11isActive = false;
            isNight = 1;
            RenderSettings.fogDensity = 0.015f;
            RenderSettings.skybox = nightSkyMaterial;
            audioManager.Stop("Day Birds");
            audioManager.Play("Night Noise");
        }
        else if(timeText.text == "23:00" && !block10isActive)
        {
            block10isActive = true;
            block1isActive = block2isActive = block3isActive = block4isActive = block5isActive = block6isActive = block7isActive = block8isActive = block9isActive = block11isActive = false;
            RenderSettings.skybox = nightSky2Material;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(night2Fog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = night2Fog; }
        }
        else if(timeText.text == "03:10" && !block11isActive)
        {
            block11isActive = true;
            block1isActive = block2isActive = block3isActive = block4isActive = block5isActive = block6isActive = block7isActive = block8isActive = block9isActive = block10isActive = false;
            RenderSettings.skybox = nightSky3Material;
            if (!CR_fogColor_running) { StartCoroutine(ChangeFogColor(night3Fog, fogLerpDuration * 100 / timeMultiplier)); }
            else { RenderSettings.fogColor = night3Fog; }
        }
        //random audios
        RandomIntervalAudio();
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }

    ///<summary>random audio generator</summary>
    private void RandomIntervalAudio()
    {
        if(isNight == 0)
        {
            int randomNum = Random.Range(1,10);
            randomNum *= audioIntervalMultiplier;
            if (Time.frameCount % randomNum == 0)
            {
                if (randomNum < 3*audioIntervalMultiplier) { audioManager.PlayOneShot("Bird1", 1); Logger.Log("1st day"); }
                else if (randomNum < 7*audioIntervalMultiplier) { audioManager.PlayOneShot("Bird2", 1); Logger.Log("2nd day"); }
                else { audioManager.PlayOneShot("Cricket", 1); Logger.Log("3rd day"); }
            }
        }
        else if(isNight == 1)
        {
            int randomNum = Random.Range(1,10);
            randomNum *= (audioIntervalMultiplier+250);
            if (Time.frameCount % randomNum == 0)
            {
                if (randomNum < 4*audioIntervalMultiplier) { audioManager.PlayOneShot("Wolf3", 1); Logger.Log("1st night"); }
                else if(randomNum < 7*audioIntervalMultiplier) { audioManager.PlayOneShot("Wolf2", 1); Logger.Log("2nd night"); }
                else { audioManager.PlayOneShot("Wolf1", 1); Logger.Log("3rd night"); }
            }
        }
    }
    ///<summary>after destroying the excalibur which is in list of fire[] then we set active it here it gives error
    ///so we will re add the objects with fire tag after it is destroyed by calling thus funtion</summary>
    public void RefreshFireArray(GameObject objToRemove)
    {
        fire = fire.Where(obj => obj != objToRemove).ToArray();
    }
    /// <summary>Changes color of fog to "color" over lerpDuration</summary>
    IEnumerator ChangeFogColor(Color color, float lerpDuration)
    {
        
        CR_fogColor_running = true;
        float timeElapsed = 0;
        float currentColorR = RenderSettings.fogColor.r;
        float currentColorG = RenderSettings.fogColor.g;
        float currentColorB = RenderSettings.fogColor.b;
        //float currentColorA = RenderSettings.fogColor.a;
        //no need of a as it doesnt affect fog

        while (timeElapsed < lerpDuration)
        {
            //valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            Color colorToAssign = RenderSettings.fogColor; 
            colorToAssign.r = Mathf.Lerp(currentColorR, color.r, timeElapsed / lerpDuration);
            colorToAssign.g = Mathf.Lerp(currentColorG, color.g, timeElapsed / lerpDuration);
            colorToAssign.b = Mathf.Lerp(currentColorB, color.b, timeElapsed / lerpDuration);
            RenderSettings.fogColor = colorToAssign;
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        RenderSettings.fogColor = color;
        CR_fogColor_running = false;
    }
    ///<summary>To change sun color over lerp duration</summary>
    IEnumerator ChangeSunColor(Color color, float lerpDuration)
    {

        CR_sunColor_running = true;
        float timeElapsed = 0;
        float currentColorR = sunLight.color.r;
        float currentColorG = sunLight.color.g;
        float currentColorB = sunLight.color.b;
        //float currentColorA = RenderSettings.fogColor.a;
        //no need of a as it doesnt affect fog

        while (timeElapsed < lerpDuration)
        {
            //valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            Color colorToAssign = sunLight.color;
            colorToAssign.r = Mathf.Lerp(currentColorR, color.r, timeElapsed / lerpDuration);
            colorToAssign.g = Mathf.Lerp(currentColorG, color.g, timeElapsed / lerpDuration);
            colorToAssign.b = Mathf.Lerp(currentColorB, color.b, timeElapsed / lerpDuration);
            sunLight.color = colorToAssign;
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        sunLight.color = color;
        CR_sunColor_running = false;
    }

    ///<summary>chaning light map for day and night</summary>
    private void ChangeLight(int forNight)
    {
        if (forNight == 1)
        {
            LightmapData[] lightmapData = new LightmapData[11];
            for (int i = 0; i < 11; i++)
            {
                lightmapData[i] = new LightmapData();
                lightmapData[i].lightmapColor = lightmapNightTextures[i].lightmapColor;
                lightmapData[i].lightmapDir = lightmapNightTextures[i].lightmapDir;
                lightmapData[i].shadowMask = lightmapNightTextures[i].shadowMask;
            }
            LightmapSettings.lightmaps = lightmapData;

            RenderSettings.reflectionIntensity = 0.3f;
            RenderSettings.ambientIntensity = 0.2f;
            //RenderSettings.ambientGroundColor = new Color(20, 20, 240);
            nightSettingActive = true;
            daySettingActive = false;
            Logger.Log("Using night settings");
        }
        else
        {
            LightmapData[] lightmapData = new LightmapData[11];
            for (int i = 0; i < 11; i++)
            {
                lightmapData[i] = new LightmapData();
                lightmapData[i].lightmapColor = lightmapDayTextures[i].lightmapColor;
                lightmapData[i].lightmapDir = lightmapDayTextures[i].lightmapDir;
                lightmapData[i].shadowMask = lightmapDayTextures[i].shadowMask;
            }
            LightmapSettings.lightmaps = lightmapData;

            RenderSettings.reflectionIntensity = 0.7f;
            RenderSettings.ambientIntensity = 0.5f;
            daySettingActive = true;
            nightSettingActive = false;
            Logger.Log("Using day settings");
        }
    }
    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        if (audioManager == null) { audioManager = FindObjectOfType<AudioManager>(); Logger.Log("TIME CONTROLLER audio manager was null now ok"); }
        audioManager.Play("Day Birds");
    }
}
