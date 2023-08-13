using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsGame = false;
    public static Action EscapeWasPressed;

    private CoreManager coreManager;
    private string map;

    [SerializeField]
    private GameObject windows;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private TextMeshProUGUI passages;
    [SerializeField]
    private TextMeshProUGUI levelNumber;

    [SerializeField]
    private Transform levelSelection;

    private float timeToYellow;
    private float timeFromYellow;
    private float desiredDuration = 2f;

    private List<List<Light>> lights = new List<List<Light>>();

    private void Start()
    {
        map = PlayerPrefs.HasKey(PlayerData.CurrentLevel) == false
            ? Levels.GetLevel(0)
            : Levels.GetLevel(PlayerPrefs.GetInt(PlayerData.CurrentLevel));

        coreManager = new CoreManager(map);

        for (int i = 0; i < 9; i++)
        {
            lights.Add(new List<Light>());
            for (int j = 0; j < 18; j++)
            {
                Transform pointLight = windows.transform.Find("p " + i + " " + j);
                Light light = pointLight.transform.GetComponent<Light>();
                lights[i].Add(light);
            }
        }
    }

    private void Update()
    {
        if (IsGame)
        {
            timeToYellow = 0f;
            // отрисовка
            Print(coreManager.LogicManager.Map, ref timeFromYellow);

            if (coreManager.LogicManager.Player.IsWin)
            {
                coreManager = EndLevel(coreManager);
                Debug.Log("End Game");
            }
            else
                coreManager = Step(coreManager);
        }
        else
        {
            timeFromYellow = 0f;
            Print(coreManager.LogicManager.Map, ref timeToYellow);
        }
    }

    private void CellProcessing(Cell cell, Light light, float percentageComplete)
    {
        // если это как бы пустая клетка
        if (cell.ToString().CompareTo(".") == 0) 
        {
            light.intensity = 0f;
            return;
        }

        // увеличим интенсивность света у клетки Hole, т.к. blue выглядит тускло
        if (cell.ToString().CompareTo("~") == 0)
        {
            light.intensity = 15f;
        }
        else
        {
            light.intensity = 7f;
        }

        light.color = IsGame 
            ? Color.Lerp(Color.yellow, cell.color, curve.Evaluate(percentageComplete))      // в игровой цвет
            : Color.Lerp(light.color, Color.yellow, curve.Evaluate(percentageComplete));    // в цвет на паузе
    }

    private void Print(Cell[,] map, ref float time)
    {
        time += Time.deltaTime;
        float percentageComplete = time / desiredDuration;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                CellProcessing(map[i, j], lights[i][j], percentageComplete);
            }
        }

        passages.text = $"Проходов: {coreManager.LogicManager.Player.Passages}";
        levelNumber.text = $"Уровень: {Levels.CurrentLevel + 1}";
    }

    public CoreManager Step(CoreManager coreManager)
    {
        if (Input.GetKeyDown(ControlSettings.Left))
            return coreManager.Move(DivMove.left);
        if (Input.GetKeyDown(ControlSettings.Right))
            return coreManager.Move(DivMove.right);
        if (Input.GetKeyDown(ControlSettings.Up))
            return coreManager.Move(DivMove.up);
        if (Input.GetKeyDown(ControlSettings.Down))
            return coreManager.Move(DivMove.down);
        if (Input.GetKeyDown(ControlSettings.Pause))
            EscapeWasPressed?.Invoke();
        if (Input.GetKeyDown(ControlSettings.Reset))
        {
            coreManager.Reset();
            map = Levels.Restart();
            return new CoreManager(map);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs deleted");
        }

        return coreManager;
    }

    public CoreManager EndLevel(CoreManager coreManager)
    {
        coreManager.Reset();
        map = Levels.NextLevel();

        // делаем кнопку кликабельной, если игрок дошёл до определённого уровня
        var button = levelSelection.Find($"{Levels.CurrentLevel + 1}_LvlButton");
        button.GetComponent<Button>().enabled = true;
        button.GetComponent<Image>().color = Color.white;

        return new CoreManager(map);
    }

    private void SetLevel(int levelNumber)
    {
        coreManager.Reset();
        map = Levels.GetLevel(levelNumber);
        coreManager = new CoreManager(map);
    }

    private void OnEnable()
    {
        InitLevelsButtons.LevelSelected += SetLevel;
    }

    private void OnDisable()
    {
        InitLevelsButtons.LevelSelected -= SetLevel;
    }
}