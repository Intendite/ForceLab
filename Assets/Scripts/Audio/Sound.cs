using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range (.1f, 3f)]
    public float pitch;

    public bool loop;
    public AudioMixerGroup AudioGroup;

    [HideInInspector]
    public AudioSource source;
}
