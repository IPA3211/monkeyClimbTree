using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource bgm;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
        }

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
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.PlayOneShot(s.source.clip);
    }
}
