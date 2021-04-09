using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string MusicPref = "MusicPref";
    private float musicFloat;
    public AudioSource musicAudio;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        musicAudio.volume = musicFloat;
    }
}
