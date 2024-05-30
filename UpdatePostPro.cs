using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class UpdatePostPro : MonoBehaviour
{
    [Header("Increase brightness at night")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Tonemapper toneMapperForNight;
    [SerializeField] private Tonemapper toneMapperForDay;
    private ColorGrading colorGradingLayer;
    private Bloom bloomLayer;
    [SerializeField] private PostProcessProfile profile;
    //private PostProcessVolume volume;
    void Start()
    {
        //volume = GetComponent<PostProcessVolume>();
        profile.TryGetSettings(out colorGradingLayer);
        profile.TryGetSettings(out bloomLayer);
        //start by this
        colorGradingLayer.tonemapper.Override(toneMapperForDay);
        colorGradingLayer.postExposure.Override(0.5f);
        colorGradingLayer.mixerBlueOutBlueIn.Override(110f);
        if (profile.name == "post pro water Profile")
        {
            bloomLayer.threshold.Override(0.8f);
        }
    }

    void Update()
    {
        if(timeText.text == "18:30")
        {
            colorGradingLayer.tonemapper.Override(toneMapperForNight);
            colorGradingLayer.postExposure.Override(1.3f);
            colorGradingLayer.mixerBlueOutBlueIn.Override(125f);
            colorGradingLayer.contrast.Override(12f);
            if(profile.name =="post pro water Profile")
            {
                bloomLayer.threshold.Override(0.18f);//was0.13
            }
        }
        else if(timeText.text == "19:00")
        {
            colorGradingLayer.postExposure.Override(1.5f);
            colorGradingLayer.mixerBlueOutBlueIn.Override(140f);
            colorGradingLayer.contrast.Override(10f);
        }
        else if(timeText.text == "03:10")
        {
            colorGradingLayer.contrast.Override(18f);
        }
        else if(timeText.text == "05:45")
        {
            colorGradingLayer.tonemapper.Override(toneMapperForDay);
            colorGradingLayer.postExposure.Override(0.5f);
            colorGradingLayer.mixerBlueOutBlueIn.Override(110f);
            colorGradingLayer.contrast.Override(35f);
            if (profile.name == "post pro water Profile")
            {
                bloomLayer.threshold.Override(0.8f);
            }
        }
    }
    private void OnDestroy()
    {
        colorGradingLayer.tonemapper.Override(toneMapperForDay);
        colorGradingLayer.postExposure.Override(0.5f);
        colorGradingLayer.mixerBlueOutBlueIn.Override(110f);
        if (profile.name == "post pro water Profile")
        {
            bloomLayer.threshold.Override(0.8f);
        }
    }
}
