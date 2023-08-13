using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitLevelsButtons : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject lvlButton;

    private int numberOfLevelsInRow = 8;

    public static Action<int> LevelSelected;
    
    void Start()
    {
        lvlButton.GetComponent<RectTransform>().localPosition = new Vector3(
                lvlButton.GetComponent<RectTransform>().localPosition.x 
                    - lvlButton.GetComponent<RectTransform>().rect.width * Levels.GetLevelsCount() / 4 
                    + lvlButton.GetComponent<RectTransform>().rect.width / 2,
                lvlButton.GetComponent<RectTransform>().localPosition.y,
                lvlButton.GetComponent<RectTransform>().localPosition.z
                );

        var _button = lvlButton.GetComponent<Button>();
        _button.onClick.AddListener(() => SetLevel(int.Parse(lvlButton.name.ToString().Split('_')[0])));

        for (int i = 0; i < numberOfLevelsInRow; i++)
        {
            for (int j = 0; j < numberOfLevelsInRow; j++)
            {
                if (i == 0 && j == 0 || 8 * i + j >= Levels.GetLevelsCount())
                { 
                    continue;
                }
                var newLvlButton = Instantiate(lvlButton.transform);
                newLvlButton.name = $"{numberOfLevelsInRow * i + j + 1}_LvlButton";
                newLvlButton.parent = canvas.transform;

                var rect = newLvlButton.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.localPosition = new Vector3(
                    lvlButton.GetComponent<RectTransform>().localPosition.x
                        + lvlButton.GetComponent<RectTransform>().rect.width * j,
                    lvlButton.GetComponent<RectTransform>().localPosition.y 
                        - lvlButton.GetComponent<RectTransform>().rect.height * i,
                    lvlButton.GetComponent<RectTransform>().localPosition.z
                    );

                var lvlNumber = newLvlButton.GetChild(0).GetComponent<TMP_Text>();
                lvlNumber.text = (numberOfLevelsInRow * i + j + 1).ToString();

                var button = newLvlButton.GetComponent<Button>();
                button.onClick.AddListener(() => SetLevel(int.Parse(newLvlButton.name.ToString().Split('_')[0])));

                if (i >= Levels.OpenLevels)
                {
                    button.enabled = false;
                    newLvlButton.GetComponent<Image>().color = Color.gray;
                }
            }                
        }
    }

    public int SetLevel(int levelNumber)
    {
        LevelSelected?.Invoke(levelNumber - 1); // -1 т.к. имя кнопок начинается с единицы
        return levelNumber;
    }
}
