using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] bgms;
    public Sound[] sounds;
    public AudioSource bgm;
    public AudioSource sfx;
    public Slider bgmVol;
    public Slider sfxVol;
    public Toggle bgmMuteToggle;
    public Toggle sfxMuteToggle;

    // Start is called before the first frame update0
    void Awake()
    {
        if(AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }

        foreach(Sound s in sounds)
        {
            //이거 audiosource 계속 생성하길래 바꿈
            s.source = sfx;
        }

        foreach (Sound s in bgms)
        {
            //이거 audiosource 계속 생성하길래 바꿈
            s.source = bgm;
        }

        bgm.volume = SecurityPlayerPrefs.GetFloat("bgmVol", 1);
        sfx.volume = SecurityPlayerPrefs.GetFloat("sfxVol", 1);
        bgmVol.value = bgm.volume;
        sfxVol.value = sfx.volume;

        if (SecurityPlayerPrefs.GetInt("bgmMute", 0) == 1)
        {
            bgm.mute = true;
            bgmMuteToggle.isOn = true;
        }
        else
        {
            bgm.mute = false;
            bgmMuteToggle.isOn = false;
        }

        if (SecurityPlayerPrefs.GetInt("sfxMute", 0) == 1)
        {
            sfx.mute = true;
            sfxMuteToggle.isOn = true;
        }
        else
        {
            sfx.mute = false;
            sfxMuteToggle.isOn = false;
        }

        PlayerPrefs.SetInt("Hi there", 1);
        
    }

    public void ChangeBGM()
    {
        int statgeNum = GameSystem.getStage();
        bgm.Stop();

        switch (statgeNum)
        {
            case 0:
                bgm.clip = bgms[0].clip;
                break;
            case 1:
                bgm.clip = bgms[1].clip;
                break;
            case 2:
                bgm.clip = bgms[2].clip;
                break;
            case 3:
                bgm.clip = bgms[3].clip;
                break;
            case 4:
                bgm.clip = bgms[4].clip;
                break;
            case 5:
                bgm.clip = bgms[4].clip;
                break;
            case 6:
                bgm.clip = bgms[4].clip;
                break;
        }

        bgm.Play();
    }

    // Update is called once per frame
    public void Play(string name)
    {
        //이거 find로 하는거 보다 Enum으로 하는게 나을듯
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.PlayOneShot(s.clip, s.volume * sfxVol.value);
    }

    public void changeBGMVolume(){
        bgm.volume = bgmVol.value;
        SecurityPlayerPrefs.SetFloat("bgmVol", bgmVol.value);
    }
    public void changeSFXVolume()
    {
        sfx.volume = sfxVol.value;
        SecurityPlayerPrefs.SetFloat("sfxVol", sfxVol.value);
    }

    public void toggleBGMMute()
    {
        bgm.mute = bgmMuteToggle.isOn;
        if(bgm.mute == true)
        {
            SecurityPlayerPrefs.SetInt("bgmMute", 1);
        }
        else
        {
            SecurityPlayerPrefs.SetInt("bgmMute", 0);
        }        
    }

    public void toggleSFXMute()
    {
        sfx.mute = sfxMuteToggle.isOn;
        if (sfx.mute == true)
        {
            SecurityPlayerPrefs.SetInt("sfxMute", 1);
        }
        else
        {
            SecurityPlayerPrefs.SetInt("sfxMute", 0);
        }
    }
}
