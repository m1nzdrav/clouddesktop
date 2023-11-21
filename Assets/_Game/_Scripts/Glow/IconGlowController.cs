using UnityEngine;
using UnityEngine.EventSystems;

public class IconGlowController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Glow;
    bool block;

    public bool Block
    {
        set => block = value;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        block = !block;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!block && Glow != null)
            Glow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!block && Glow != null) Glow.SetActive(false);
    }
}