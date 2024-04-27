using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip cardFlipSound;
    public AudioClip levelCompleteSound;

    private AudioSource musicSource;
    private AudioSource soundEffectSource;

    private void Awake()
    {
        // Ensure only one instance of SoundManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Create audio sources for music and sound effects
            musicSource = gameObject.AddComponent<AudioSource>();
            soundEffectSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            // Destroy duplicate instances of SoundManager
            Destroy(gameObject);
        }
    }

    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (gameplayMusic != null)
        {
            musicSource.clip = gameplayMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayCardFlipSound()
    {
        if (cardFlipSound != null)
        {
            soundEffectSource.PlayOneShot(cardFlipSound);
        }
    }

    public void PlayLevelCompleteSound()
    {
        if (levelCompleteSound != null)
        {
            soundEffectSource.PlayOneShot(levelCompleteSound);
        }
    }

    // Stop playing music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Pause music
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    // Resume music
    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
}
