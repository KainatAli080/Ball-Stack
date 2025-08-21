using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// ------------------------------------------------------------
// -----------  What Audio Manager will do?  ------------------
// ------------------------------------------------------------
// 1. Centralizes all audio logic.
// 2. One AudioSource for music, one for SFX (or more if needed).
// 3. Play clips without having to drag references everywhere.
// 4. Handle volume settings, mute, etc.
// ------------------------------------------------------------

[System.Serializable]
public class Sounds
{
    public string name;         // Unique sound name to call the audio
    public AudioClip clip;      // Audio Clip to play
    public float volume = 1f;   // Default volume
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Sounds Library")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private List<Sounds> sounds;   // List to show in inspector 
                                                    // Showing list in inspector because unity doesn't show dictionary in inspector currently
                                                    // Will convert it into a dictionary at runtime for fast lookups in O(1) time
    private Dictionary<string, Sounds> soundsDict;  // For faster lookups than list

    private void Awake()
    {
        // Creating singular instance for managers
        if (AudioInstance != null && AudioInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        AudioInstance = this;
        DontDestroyOnLoad(gameObject);
        // "this" refers to the current script or component instance
        // "gameobject" refers to the actual gameobject the script is attached to

        // Now converting list to dict
        soundsDict = new Dictionary<string, Sounds>();
        foreach (var sound in sounds)
        {
            soundsDict[sound.name] = sound; // creating the dict key as the sound name
        }
    }

    private void Start()
    {
        if(backgroundMusic != null)
            PlayMusic(backgroundMusic); // starting background music as soon as game starts
    }

    public void PlayMusic(AudioClip clip = null)
    {
        if (clip == null)
            musicSource.clip = backgroundMusic;
        else
            musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();
    
    public void PlaySFX(string name)
    {
        if(soundsDict.TryGetValue(name, out Sounds s))
        {
            sfxSource.PlayOneShot(s.clip, s.volume);
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found!");
        }
    }
}

