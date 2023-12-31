using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds, loopSfxSounds;
    public AudioSource musicSource, sfxSource, loopSfxSource;

    [SerializeField] private string _testNameMusic;
    [SerializeField] private string _testNameSFX;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    public void TestSound(bool isMusic)
    {
        if (isMusic)
        {
            PlayMusic(_testNameMusic);
        }
        else
        {
            PlaySFX(_testNameSFX);
        }
    }

    public void ResetAudioSources()
    {
        musicSource.clip = null;
        sfxSource.clip = null;
        loopSfxSource.clip = null;
    }

    public void VolumeMusic(float volume)
    {
        musicSource.volume = volume;
    }

    public void VolumeSFX(float volume)
    {
        sfxSource.volume = volume;
        loopSfxSource.volume = volume;
    }

    public void MuteMusic(bool mute)
    {
        musicSource.mute = mute;
    }

    public void MuteSFX(bool mute)
    {
        sfxSource.mute = mute;
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Music Sound Not Found");
            return;
        }

        Debug.Log("Play Music");
        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX Sound Not Found");
            return;
        }

        Debug.Log("Play SFX");
        sfxSource.PlayOneShot(s.clip);
        
    }

    public void PlayLoopSFX(string name)
    {
        Sound s = Array.Find(loopSfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX Loop Sound Not Found");
            return;
        }

        Debug.Log("Play SFX Loop");
        loopSfxSource.clip = s.clip;
        loopSfxSource.Play();

    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void StopSFXLoop()
    {
        loopSfxSource.Stop();
    }


}
