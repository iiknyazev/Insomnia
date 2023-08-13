using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FixedPressedButton : MonoBehaviour
{
    public void FixedMethod()
    {
        var button = GetComponent<Button>();
        button.interactable = false;
        button.interactable = true;
    }
}
