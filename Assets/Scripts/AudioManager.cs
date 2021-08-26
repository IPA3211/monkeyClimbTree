using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource bgm;
    public AudioSource sfx;
    public Slider bgmVol;
    public Slider sfxVol;
    // Start is called before the first frame update0
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            //이거 audiosource 계속 생성하길래 바꿈
            s.source = sfx;
        }
        bgm.volume = SecurityPlayerPrefs.GetFloat("bgmVol", 1);
        sfx.volume = SecurityPlayerPrefs.GetFloat("sfxVol", 1);
        PlayerPrefs.SetInt("Hi there", 1);
        bgmVol.value = bgm.volume;
        sfxVol.value = sfx.volume;
        /* 나중에 배경음악 음소거 버튼 생기면 이케이케 하면됨
        if(BtnType.isSound == true)
        {
            bgm.mute = false;
        }
        else
        {
            bgm.mute = true;
        }
        */    
    }

    // Update is called once per frame
    public void Play(string name)
    {
        //이거 find로 하는거 보다 Enum으로 하는게 나을듯
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.PlayOneShot(s.clip, s.volume * sfxVol.value);
    }

    public void changeBGMVolume(){
        Debug.Log(bgmVol.value);
        bgm.volume = bgmVol.value;
        SecurityPlayerPrefs.SetFloat("bgmVol", bgmVol.value);
    }
    public void changeSFXVolume(){
        SecurityPlayerPrefs.SetFloat("sfxVol", sfxVol.value);
    }
}
