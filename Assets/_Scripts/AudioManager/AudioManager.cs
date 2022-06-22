using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] private AudioFile[] audioFiles;
    public AudioSource music;
    public AudioSource sfx;

    [Range(0, 1)] public float OverallVolume_Music;
    [Range(0, 1)] public float OverallVolume_SFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume()
    {

        Slider volume = GameObject.FindWithTag("MusicSlider").GetComponent<Slider>();
        if (volume != null)
        {
            instance.OverallVolume_Music = volume.value;
            music.volume = instance.OverallVolume_Music;
        }
    }
    public void SetSFXVolume()
    {
        Slider volume = GameObject.FindWithTag("SFXSlider").GetComponent<Slider>();
        if(volume != null)
        {
            instance.OverallVolume_SFX = volume.value;
            sfx.volume = instance.OverallVolume_SFX;
        }
    }



    public void PlayMusic(string audioName)
    {
        var file = GetFileByName(audioName);

        if (file != null)
        {
            if (file.Clip != null)
            {
                music.clip = file.Clip[Random.Range(0, file.Clip.Length - 1)];
                music.volume = instance.OverallVolume_Music;
                music.Play();
            }
            else Debug.LogError("This AudioFile does not have any AudioClip, merluzo: " + audioName);
        }
        else Debug.LogError("Trying to play a sound that not exist, merluzo: " + audioName);
    }



    public void PlaySFX(string audioName)
    {
        var file = GetFileByName(audioName);

        if (file != null)
        {
            if (file.Clip != null)
            {
                sfx.clip = file.Clip[Random.Range(0, file.Clip.Length - 1)];
                sfx.volume = instance.OverallVolume_SFX;
                sfx.Play();
            }
            else Debug.LogError("This AudioFile does not have any AudioClip: " + audioName);
        }
        else Debug.LogError("Trying to play a sound that not exist: " + audioName);
    }

    public void PlaySFX(string audioName, AudioSource source)
    {
        var file = GetFileByName(audioName);

        if (file != null)
        {
            if (file.Clip != null)
            {

                source.clip = file.Clip[Random.Range(0, file.Clip.Length - 1)];
                source.volume = file.Volume * instance.OverallVolume_SFX;
                source.Play();
            }
            else Debug.LogError("This AudioFile does not have any AudioClip: " + audioName);
        }
        else Debug.LogError("Trying to play a sound that not exist: " + audioName);
    }


    private AudioFile GetFileByName(string soundName)
    {
        var file = audioFiles.First(x => x.Name == soundName);
        if (file != null)
        {
            return file;
        }
        else Debug.LogError("Trying to play a sound that not exist: " + soundName);
    
        return null;

    }

    public void VolumeDown()
    {
        StartCoroutine(MusicVolumeDown());
    }

    public string WhatAudioMusic()
    {
        return music.clip.name;
    }

    private IEnumerator MusicVolumeDown()
    {
        float volumeSpeed = 0.15f;
        for (float i = music.volume; i >= 0; i -= volumeSpeed * Time.deltaTime)
        {
            music.volume = i;
            yield return null;
        }
        yield return null;
    }
}
