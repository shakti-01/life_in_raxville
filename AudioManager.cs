using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixer;
    public Sound[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
            s.source.priority = 0;
            s.source.outputAudioMixerGroup = audioMixer;
        }
    }
    private void AddAudioSourceAgain(Sound s)
    {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.playOnAwake = false;
        //s.source.priority = 0;
        //s.source.outputAudioMixerGroup = audioMixer;
    }
    /*private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainUI")
        {
            Stop("Footsteps");
            Stop("Run");
            Stop("Day Birds");
            Stop("Night Noise");
            Stop("Inside Water");
        }
    }*/

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s.source == null) 
        { 
            //AddAudioSourceAgain(s);
            Logger.Log("Audio sources are absent for "+s.name);
        }//not needed if we do return
        if (s == null || s.source == null)
        {
            Logger.LogWarning("Sound : " + name + " not found !!");
            return;
        }
        if (Time.timeScale == 0.0f)
        {
            return;
        }
        if (!s.source.isPlaying)
        {
            s.source.Play();
            //Debug.Log(s.name);
        }
        //FindObjectOfType<AudioManager>().Play("playerdeath");
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !! or "+s.source.ToString()+ " not found");
            return;
        }
        s.source.Stop();
    }
    public void PlayDelayed(string name, float seconds)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !! or " + s.source.ToString() + " not found");
            return;
        }
        s.source.PlayDelayed(seconds);
    }
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !!");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
    public void PlayOneShot(string name,int condition)
    {
        if(Time.timeScale == 0f && condition == 1) { return; } 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !!");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
}
