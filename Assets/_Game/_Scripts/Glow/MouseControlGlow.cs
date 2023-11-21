using UnityEngine;
using UnityEngine.EventSystems;

public class MouseControlGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Glow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Glow != null)
        {
            Glow.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Glow != null)
        {
            Glow.gameObject.SetActive(false);
        }
    }
}