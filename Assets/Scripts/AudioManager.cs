using UnityEngine;

/// <summary>
/// Manages playing of game audio.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip buttonClick;
    public AudioClip portalOpen;
    public AudioClip dialog;

    public static AudioManager instance;

    private void Start()
    {
        instance = this;
        PlayBackground(background);
    }

    /// <summary>
    /// Updates background music to new AudioClip.
    /// </summary>
    /// <param name="clip">AudioClip clip of new audio</param>
    public void PlayBackground(AudioClip clip)
    {
        background = musicSource.clip = clip;
        musicSource.Play();
    }

    /// <summary>
    /// Plays one shot of sound effects.
    /// </summary>
    /// <param name="clip">AudioClip clip of new audio</param>
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
