using UnityEngine;

public class SetSounds : MonoBehaviour
{
    [SerializeField] 
    private AudioSource border;
    [SerializeField]
    private AudioSource portal;
    [SerializeField]
    private AudioSource hole;
    [SerializeField]
    private AudioSource passage;
    [SerializeField]
    private AudioSource end;


    void Start()
    {
        Border.Sound = border;
        Portal.Sound = portal;
        Hole.Sound = hole;
        Passage.Sound = passage;
        End.Sound = end;
    }
}
