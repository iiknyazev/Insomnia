using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider soundsSlider;
    [SerializeField]
    private AudioMixerGroup mixer;

    public static float MusicVolume {  get; private set; }
    public static float SoundsVolume { get; private set; }

    private void Start()
    {
        MusicVolume = PlayerPrefs.HasKey(PlayerData.MusicVolume) == false
            ? 1f
            : PlayerPrefs.GetFloat(PlayerData.MusicVolume);

        SoundsVolume = PlayerPrefs.HasKey(PlayerData.SoundsVolume) == false
            ? 1f
            : PlayerPrefs.GetFloat(PlayerData.SoundsVolume);

        musicSlider.onValueChanged.AddListener((x) => SetMusicVolume(x));
        soundsSlider.onValueChanged.AddListener((x) => SetSoundsVolume(x));

        musicSlider.value = MusicVolume;
        soundsSlider.value = SoundsVolume;

        mixer.audioMixer.SetFloat("MusicLayer", MusicVolume == 0 ? -80 : Mathf.Lerp(-12, 3, MusicVolume));
        mixer.audioMixer.SetFloat("MusicLayer", SoundsVolume == 0 ? -80 : Mathf.Lerp(-12, 3, SoundsVolume));
    }

    private void SetMusicVolume(float value)
    {
        MusicVolume = value;
        mixer.audioMixer.SetFloat("MusicLayer", value == 0 ? -80 : Mathf.Lerp(-12, 3, value));
    }

    private void SetSoundsVolume(float value)
    {
        SoundsVolume = value;
        mixer.audioMixer.SetFloat("SoundsLayer", value == 0 ? -80 : Mathf.Lerp(-12, 0, value));
    }
}
