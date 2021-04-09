using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private int firstPlayInt;
    public Slider musicSlider;
    private float musicFloat;
    public AudioSource musicAudio;
    [SerializeField] int menuScreen;
    [SerializeField] bool keyDown;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        
        if(firstPlayInt == 0)
        {
            musicFloat = .50f;
            musicSlider.value = musicFloat;
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicSlider.value = musicFloat;
        }
    }

    void Update()
    {
        menuScreen = MenuButton.menuScreen;
        if (menuScreen == 3)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (!keyDown)
                {
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        musicSlider.value -= 0.1f;
                    }
                    else if (Input.GetAxis("Horizontal") > 0)
                    {
                        musicSlider.value += 0.1f;
                    }
                    keyDown = true;
                }
            }
            else
            {
                keyDown = false;
            }
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
    }

    void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        musicAudio.volume = musicSlider.value;
    }
}
