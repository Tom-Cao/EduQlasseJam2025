using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip ObjectionMaleSFX; // Reference to the male objection sound effect

    public AudioClip ObjectionFemaleSFX; // Reference to the female objection sound effect

    public AudioSource oneShotSource; // Reference to the AudioSource component

    public AudioSource BGMusic; // Reference to the background music AudioSource

    public void Awake()
    {
        if(instance == null)
        {
            instance = this; // Assign the instance if it's null
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        oneShotSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject
        if (oneShotSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject. Please attach an AudioSource component.");
        }
    }

    public void PlaySource(AudioClip clip)
    {
        if (clip != null && oneShotSource != null)
        {
            oneShotSource.PlayOneShot(clip); // Play the sound
        }
        else
        {
            Debug.LogError("Audio clip or AudioSource is null. Cannot play sound.");
        }
    }
}
