using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class FirstLaunch : MonoBehaviour
{
    private bool isFirstStart = true;

    [SerializeField]
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioSource onOffSound;
    [SerializeField]
    private Material blackPanel;

    private float desiredDuration = 1f;
    private void StartGame()
    {
        if (isFirstStart)
        {
            onOffSound.Play();
            StartCoroutine(PanelLerp());
            backgroundMusic.Play();
        }

        isFirstStart = false;
    }

    private IEnumerator PanelLerp()
    {
        float time = 0f, percentageComplete = 0f;

        while (time < desiredDuration)
        {
            time += Time.deltaTime;
            percentageComplete = time / desiredDuration;
            blackPanel.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), percentageComplete);
            yield return null;
        }
    }

    private void EndGame()
    {
        blackPanel.color = new Color(0, 0, 0, 1);
        onOffSound.Play();
    }

    private void OnEnable()
    {
        GameMenu.PlayPressed += StartGame;
        GameMenu.ExitPressed += EndGame;
    }

    private void OnDisable()
    {
        GameMenu.PlayPressed -= StartGame;
        GameMenu.ExitPressed -= EndGame;
    }
}