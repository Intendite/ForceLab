using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;             // A user-friendly name for the sound

    public AudioClip clip;          // The audio clip to be played

    [Range(0f, 1f)]
    public float volume;            // The volume of the audio clip

    [Range(0.1f, 3f)]
    public float pitch;             // The pitch of the audio clip

    public bool loop;               // Whether the audio should loop

    public AudioMixerGroup AudioGroup; // The audio mixer group to route the audio to

    [HideInInspector]
    public AudioSource source;      // The AudioSource component associated with this sound
}
