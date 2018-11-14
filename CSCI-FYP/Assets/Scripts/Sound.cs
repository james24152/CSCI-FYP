using UnityEngine.Audio;
using UnityEngine;
using Assets.MultiAudioListener;

[System.Serializable]
public class Sound{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    //public MultiAudioSource source;
    public AudioSource source;
}
