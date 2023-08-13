using System;
using TMPro;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelSelection;
    [SerializeField]
    private GameObject additionally;
    [SerializeField]
    private GameObject settings;
    [SerializeField]
    private GameObject gameInterface;
    [SerializeField]
    private GameObject controls;
    [SerializeField]
    private GameObject infoText;

    public static Action ExitPressed;
    public static Action PlayPressed;

    public void PlayGame()
    {
        PlayPressed?.Invoke();
        Cursor.visible = false;
        GameManager.IsGame = true;
        gameInterface.SetActive(true);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
        additionally.SetActive(false);
    }

    public void LevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void Controls()
    {
        settings.SetActive(false);
        controls.SetActive(true);
    }

    public void BackToSettings()
    {
        settings.SetActive(true);
        controls.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Cursor.visible = true;
        GameManager.IsGame = false;
        mainMenu.SetActive(true);
        gameInterface.SetActive(false);
        levelSelection.SetActive(false);
        additionally.SetActive(false);
        settings.SetActive(false);
    }

    public void SetLevel(int levelNumber)
    {
        PlayGame();
    }

    public void Additionally()
    {
        mainMenu.SetActive(false);
        additionally.SetActive(true);    
    }

    public void ExitGame()
    {
        Debug.Log("Exit!");
        ExitPressed?.Invoke();
        Application.Quit();
    }

    private void ShowHideInfoText()
    {
        if (Levels.CurrentLevel == 0)
        {
            infoText.GetComponent<TMP_Text>().text = "WASD - управление\nESC - пауза, R - рестарт";
            infoText.SetActive(true);
        }
        else if (Levels.CurrentLevel == Levels.GetLevelsCount() - 1)
        {
            infoText.GetComponent<TMP_Text>().text = "Конец игры...";
            infoText.SetActive(true);
        }
        else
        {
            infoText.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameManager.EscapeWasPressed += BackToMainMenu;
        InitLevelsButtons.LevelSelected += SetLevel;
        Levels.NewLevelHasBeenLoaded += ShowHideInfoText;
    }

    private void OnDisable()
    {
        GameManager.EscapeWasPressed -= BackToMainMenu;
        InitLevelsButtons.LevelSelected -= SetLevel;
        Levels.NewLevelHasBeenLoaded -= ShowHideInfoText;
    }
}
