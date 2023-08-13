using System;
using TMPro;
using UnityEngine;

public class ColorPalette : MonoBehaviour 
{
    [SerializeField] 
    private TextMeshProUGUI currentPaletteName;

    public enum Palette
    {
        classic,
        holiday,
        communistic
    }

    public static int CurrentPalette;
    public static Color Border;
    public static Color End;
    public static Color Hole;
    public static Color Passage;
    public static Color Player;
    public static Color Portal;

    private void Start()
    {
        CurrentPalette = PlayerPrefs.HasKey(PlayerData.CurrentPalette) == false
            ? (int)Palette.classic
            : PlayerPrefs.GetInt(PlayerData.CurrentPalette);

        currentPaletteName.text = ((Palette)CurrentPalette).ToString();
        SetPalette((Palette)CurrentPalette);
    }

    public static void SetPalette(Palette palette)
    {
        switch(palette)
        {
            case Palette.classic:
                SetColors(Color.yellow, Color.red, Color.blue, Color.green, Color.cyan, Color.magenta);
                CurrentPalette = (int)Palette.classic;
                break;
            case Palette.holiday:
                SetColors(Color.cyan, Color.red, Color.blue, Color.green, Color.yellow, Color.magenta);
                CurrentPalette = (int)Palette.holiday;
                break; 
            case Palette.communistic:
                SetColors(Color.red, Color.cyan, Color.blue, Color.green, Color.yellow, Color.magenta);
                CurrentPalette = (int)Palette.communistic;
                break;
            default:
                throw new ArgumentException("Incorrect palette value");
        }
    }

    private static void SetColors(Color border, Color end, Color hole, Color passage, Color player, Color portal)
    {
        Border = border;
        End = end;
        Hole = hole;
        Passage = passage;
        Player = player; 
        Portal = portal;
    }

    public void NextPalette()
    {
        Debug.Log($"current Palette = {CurrentPalette}");
        CurrentPalette = Math.Abs((CurrentPalette + 1) % 3);
        currentPaletteName.text = ((Palette)CurrentPalette).ToString();
        SetPalette((Palette)CurrentPalette);
    }

    public void PrevPalette()
    {
        Debug.Log($"current Palette = {CurrentPalette}");
        CurrentPalette = Math.Abs((CurrentPalette - 1) % 3);
        currentPaletteName.text = ((Palette)CurrentPalette).ToString();
        SetPalette((Palette)CurrentPalette);
    }
}
