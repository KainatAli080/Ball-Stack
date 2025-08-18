using UnityEngine;

// ------------------------------------------------------------
// -----------  What Audio Manager will do?  ------------------
// ------------------------------------------------------------
// 1. Centralizes all audio logic.
// 2. One AudioSource for music, one for SFX (or more if needed).
// 3. Play clips without having to drag references everywhere.
// 4. Handle volume settings, mute, etc.
// ------------------------------------------------------------

public class AudioManager : MonoBehaviour
{
    // Creating singleton
    public static AudioManager AudioInstance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("SFX Clips (One Time Clips)")]
    public AudioClip CoinsCollectedAudioClip;
    public AudioClip GameOverAudioClip;

    private void Awake()
    {
        if (AudioInstance != null && AudioInstance != this)
        {
            // Destrying when an instance already exists and it's not the current one
            Destroy(gameObject);
            return;
        }

        AudioInstance = this;
        DontDestroyOnLoad(gameObject); // to persist across scene reloads
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);        
    }
}
