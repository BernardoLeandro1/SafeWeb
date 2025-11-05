using UnityEngine;
using UnityEngine.UI;

public class MusicOptions : MonoBehaviour
{
    public Scrollbar musicScrollbar;

    void Start()
    {
        // Set scrollbar to saved volume
        if (musicScrollbar != null)
            musicScrollbar.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public void OnMusicScrollbarChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(musicScrollbar.value);
    }
}
