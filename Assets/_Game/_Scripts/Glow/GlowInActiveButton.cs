using UnityEngine;

public class GlowInActiveButton : MonoBehaviour
{
    [SerializeField] private GameObject activeGlow;

    public void ChangeActive()
    {
        activeGlow.SetActive(!activeGlow.activeSelf);
    }
}