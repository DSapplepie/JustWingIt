using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource1;
    public AudioSource musicSource2;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic1;
    public AudioClip background2;
    public AudioClip finishMusic;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Start background music
        if (musicSource1 != null && backgroundMusic1 != null)
        {
            musicSource1.clip = backgroundMusic1;
            musicSource1.loop = true;
            musicSource1.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SwitchMusic(AudioClip newClip)
    {
        if (musicSource1 != null && newClip != null)
        {
            musicSource1.Stop();
            musicSource1.clip = newClip;
            musicSource1.loop = true;
            musicSource1.Play();
        }
    }
}
