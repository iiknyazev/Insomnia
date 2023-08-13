using UnityEngine;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour
{
    [SerializeField] 
    private Toggle inversionToggle;

    public enum inversion
    {
        no = 1,
        yes = -1        
    }

    public static inversion mouseInversion { get; private set; } = inversion.no;

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private float sensivity;

    private float xRot;
    private float yRot;
    

    private void Start()
    {
        mouseInversion = PlayerPrefs.HasKey(PlayerData.MouseInversion) == false
            ? inversion.no
            : (inversion)PlayerPrefs.GetInt(PlayerData.MouseInversion);

        inversionToggle.isOn = mouseInversion == inversion.yes ? true : false;

        inversionToggle.onValueChanged.AddListener(delegate 
        { 
            SetInversion(inversionToggle); 
        });
    }

    private void Update()
    {
        if (GameManager.IsGame)
        {
            MouseMove();
        }
    }

    private void MouseMove()
    {
        xRot += Input.GetAxis("Mouse X") * sensivity;
        yRot += Input.GetAxis("Mouse Y") * sensivity;

        if (xRot >= 55f)
            xRot = 55f;
        if (xRot <= -30f)
            xRot = -30f;

        if (yRot >= 15f)
            yRot = 15f;
        if (yRot <= -10f)
            yRot = -10f;

        camera.transform.localRotation = Quaternion.Euler(-yRot, xRot * (int)mouseInversion, 0f);
    }

    public void SetInversion(Toggle toggle)
    {
        Debug.Log(toggle.isOn);
        mouseInversion = toggle.isOn ? inversion.yes : inversion.no;
    }
}
