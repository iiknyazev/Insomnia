using UnityEngine;

public class ControlSettings : MonoBehaviour
{
    public static KeyCode Left  { get; private set; } = KeyCode.A;
    public static KeyCode Right { get; private set; } = KeyCode.D;
    public static KeyCode Up    { get; private set; } = KeyCode.W;
    public static KeyCode Down  { get; private set; } = KeyCode.S;
    public static KeyCode Pause { get; private set; } = KeyCode.Escape;
    public static KeyCode Reset { get; private set; } = KeyCode.R;
}
