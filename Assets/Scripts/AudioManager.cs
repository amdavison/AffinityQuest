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
    public AudioClip mainMenu;
    public AudioClip gamePlay;
    public AudioClip finale;
    public AudioClip transition;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    public AudioClip npc;
    public AudioClip buttonClick;
    public AudioClip portalOpen;
    public AudioClip portalEntry;
    public AudioClip correct;
    public AudioClip incorrect;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        PlayBackground(mainMenu);
    }

    /// <summary>
    /// Updates background music to new AudioClip.
    /// </summary>
    /// <param name="clip">AudioClip clip of new audio</param>
    public void PlayBackground(AudioClip clip)
    {
        musicSource.clip = clip;
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
