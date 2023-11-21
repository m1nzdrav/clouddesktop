using UnityEngine;


public class RotateVertical : MonoBehaviour
{
    [SerializeField] private GameObject glow;

    public void OnGlow()
    {
        glow.SetActive(true);
    }

    public void OffGlow()
    {
        glow.SetActive(false);
    }
}