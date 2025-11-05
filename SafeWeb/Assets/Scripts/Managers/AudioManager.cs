using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource musicSource;

    [Header("Settings")]
    public float fadeDuration = 1f;

    public AudioClip houseMusic;
    public AudioClip parkMusic;
    public AudioClip schoolMusic;
    public AudioClip shoppingMusic;

    private void Awake()
    {
        // Singleton pattern: keep one AudioManager across scenes
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

        // Create music source if none assigned
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

        // Load saved volume setting
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSource.volume = savedVolume;
    }

    void Start()
    {
        PlayMusic(houseMusic, true);
    }

    // 🎵 Play background music (with optional fade)
    public void PlayMusic(AudioClip clip, bool fade = true)
    {
        if (clip == null || musicSource.clip == clip) return;

        if (fade)
            StartCoroutine(FadeMusic(clip));
        else
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlayHouse()
    {
        PlayMusic(houseMusic);
    }

    public void PlayPark()
    {
        PlayMusic(parkMusic);
    }

    public void PlaySchool()
    {
        PlayMusic(schoolMusic);
    }

    public void PlayShopping()
    {
        PlayMusic(shoppingMusic);
    }

    private IEnumerator FadeMusic(AudioClip newClip)
    {
        float startVol = musicSource.volume;

        // Fade out current track
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in new track to saved volume
        float targetVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, targetVol, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVol;
    }

    // Volume control
    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    // Optional fade-out stop
    public void StopMusic(bool fade = true)
    {
        if (fade)
            StartCoroutine(FadeOutMusic());
        else
            musicSource.Stop();
    }

    private IEnumerator FadeOutMusic()
    {
        float startVol = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
}
