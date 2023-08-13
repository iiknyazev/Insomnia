using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public readonly static string CurrentLevel = "currentLevel";
    public readonly static string OpenLevels = "openLevels";
    public readonly static string MusicVolume = "musicVolume";
    public readonly static string SoundsVolume = "soundsVolume";
    public readonly static string MouseInversion = "mouseInversion";
    public readonly static string CurrentPalette = "currentPalette";

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt(CurrentLevel, Levels.CurrentLevel);
        PlayerPrefs.SetInt(OpenLevels, Levels.OpenLevels);
        PlayerPrefs.SetFloat(MusicVolume, VolumeSettings.MusicVolume);
        PlayerPrefs.SetFloat(SoundsVolume, VolumeSettings.SoundsVolume);
        PlayerPrefs.SetInt(MouseInversion, (int)CameraControls.mouseInversion);
        PlayerPrefs.SetInt(CurrentPalette, ColorPalette.CurrentPalette);
    }

    void OnApplicationQuit()
    {
        SetPlayerPrefs();
    }

    private void OnEnable()
    {
        GameMenu.ExitPressed += SetPlayerPrefs;
    }

    private void OnDisable()
    {
        GameMenu.ExitPressed -= SetPlayerPrefs;
    }
}
