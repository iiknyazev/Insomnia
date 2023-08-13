using UnityEngine;

public class SetSkyboxStartSettings : MonoBehaviour
{
    private void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 167);
    }
}
