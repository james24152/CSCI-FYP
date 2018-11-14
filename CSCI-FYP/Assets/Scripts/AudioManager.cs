using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;
using Assets.MultiAudioListener;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    // Use this for initialization
    private void Awake()
    {
        foreach (Sound s in sounds) {
            /*s.source = gameObject.AddComponent<MultiAudioSource>();
            s.source.AudioClip = s.clip;
            s.source.Volume = s.volume;
            s.source.Pitch = s.pitch;
            s.source.PlayOnAwake = false;*/
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) {
            Debug.Log("playing" + s.name);
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(Fade(s));
    }

    IEnumerator Fade(Sound s) {
        float originVolume = s.volume;
        float volume = s.source.volume;
        while (s.source.volume >= 0.01f)
        {
            volume -= 0.01f;
            s.source.volume = volume;
            yield return null;
        }
        s.source.Stop();
        s.source.volume = originVolume;
    }
}
