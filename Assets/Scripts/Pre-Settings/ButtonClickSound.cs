using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource sound;

    public void PlaySound()
    {
        sound.Play();
    }
}
