using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Change AudioSource to AudioMixer
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("music"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20); // Set the appropriate parameter name
       
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music");
        SetMusicVolume();
    }
}
