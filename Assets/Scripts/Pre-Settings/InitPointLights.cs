using System.Collections.Generic;
using UnityEngine;

public class InitPointLights : MonoBehaviour
{
    [SerializeField]
    private Transform windows;
    [SerializeField]
    private Transform decorWindows1;
    [SerializeField] 
    private Transform decorWindows2;

    private Transform p_0_0;
    private Transform p_0_3;
    private Transform p_0_9;
    private Transform p_0_15;

    private Transform decor1_p_0_15;

    private Transform decor2_p_0_0;
    private Transform decor2_p_0_3;
    private Transform decor2_p_0_9;
    private Transform decor2_p_0_15;

    private float dx = 3.15f;
    private float dy = 2.75f;

    [SerializeField]
    private Transform decorHouse1;

    [SerializeField]
    private Transform decorHouse2;
    
    private System.Random random = new System.Random();
    private List<int> intensities = new List<int>() { 0, 0, 10 };

    private void Awake()
    {
        BoardHouse();
        DecorHouse1();
        DecorHouse2();
    }

    private void Start()
    {
            
    }

    private void DecorHouse1()
    {
        decor1_p_0_15 = decorWindows1.Find("decor1 p 0 15");

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                if (i == 0 && (j == 0 || j == 3 || j == 9 || j == 15))
                    continue;

                if (j >= 15)
                    SetPointLightParams(decorWindows1, decor1_p_0_15, i, j, 15, intensities[random.Next(0, intensities.Count)]);
            }
        }

        decor1_p_0_15.GetComponent<Light>().intensity = intensities[random.Next(0, intensities.Count)];

        decorHouse1.localEulerAngles = new Vector3(-90, 90, 0);
        decorHouse1.localEulerAngles = new Vector3(-90, 0, 90);
    }

    private void DecorHouse2()
    {
        decor2_p_0_0 = decorWindows2.Find("decor2 p 0 0");
        decor2_p_0_3 = decorWindows2.Find("decor2 p 0 3");
        decor2_p_0_9 = decorWindows2.Find("decor2 p 0 9");
        decor2_p_0_15 = decorWindows2.Find("decor2 p 0 15");

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                if (i == 0 && (j == 0 || j == 3 || j == 9 || j == 15))
                    continue;

                if (j >= 0 && j < 3)
                    SetPointLightParams(decorWindows2, decor2_p_0_0, i, j, 0, intensities[random.Next(0, intensities.Count)]);
                if (j >= 3 && j < 9)
                    SetPointLightParams(decorWindows2, decor2_p_0_3, i, j, 3, intensities[random.Next(0, intensities.Count)]);
                if (j >= 9 && j < 15)
                    SetPointLightParams(decorWindows2, decor2_p_0_9, i, j, 9, intensities[random.Next(0, intensities.Count)]);
                if (j >= 15)
                    SetPointLightParams(decorWindows2, decor2_p_0_15, i, j, 15, intensities[random.Next(0, intensities.Count)]);
            }
        }

        decor2_p_0_0.GetComponent<Light>().intensity = intensities[random.Next(0, intensities.Count)];
        decor2_p_0_3.GetComponent<Light>().intensity = intensities[random.Next(0, intensities.Count)];
        decor2_p_0_9.GetComponent<Light>().intensity = intensities[random.Next(0, intensities.Count)];
        decor2_p_0_15.GetComponent<Light>().intensity = intensities[random.Next(0, intensities.Count)];

        decorHouse2.localEulerAngles = new Vector3(-90, -45, 0);
        decorHouse2.localEulerAngles = new Vector3(-90, 0, -45);
    }

    private void BoardHouse()
    {
        p_0_0 = windows.Find("p 0 0");
        p_0_3 = windows.Find("p 0 3");
        p_0_9 = windows.Find("p 0 9");
        p_0_15 = windows.Find("p 0 15");

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                if (i == 0 && (j == 0 || j == 3 || j == 9 || j == 15))
                    continue;

                if (j >= 0 && j < 3)
                    SetPointLightParams(windows, p_0_0, i, j, 0, 0);
                if (j >= 3 && j < 9)
                    SetPointLightParams(windows, p_0_3, i, j, 3, 0);
                if (j >= 9 && j < 15)
                    SetPointLightParams(windows, p_0_9, i, j, 9, 0);
                if (j >= 15)
                    SetPointLightParams(windows, p_0_15, i, j, 15, 0);
            }
        }

        p_0_0.GetComponent<Light>().intensity = 0;
        p_0_3.GetComponent<Light>().intensity = 0;
        p_0_9.GetComponent<Light>().intensity = 0;
        p_0_15.GetComponent<Light>().intensity = 0;
    }

    private void SetPointLightParams(Transform parent, Transform _window, int i, int j, int dj, int intensity)
    {
        var newWindow = Instantiate(_window);
        
        newWindow.transform.position = new Vector3(
            _window.position.x + dx * (j - dj),
            _window.position.y - dy * i, 
            _window.position.z
            );

        newWindow.name = "p " + i + " " + j;
        newWindow.GetComponent<Light>().intensity = intensity;
        newWindow.parent = parent.transform;
    }
}
