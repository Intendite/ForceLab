using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // If an AudioManager instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }

        // Keep this AudioManager alive when transitioning between scenes
        DontDestroyOnLoad(gameObject);

        // Initialize and set up audio sources for each sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.AudioGroup;
        }
    }

    private void Start()
    {
        // Play the "Theme" sound when the game starts
        Play("Theme");
    }

    public void Play(string name)
    {
        // Find and play the sound by name
        Sound s = FindSoundByName(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    private Sound FindSoundByName(string name)
    {
        // Find a sound in the sounds array by its name
        return System.Array.Find(sounds, sound => sound.name == name);
    }
}
