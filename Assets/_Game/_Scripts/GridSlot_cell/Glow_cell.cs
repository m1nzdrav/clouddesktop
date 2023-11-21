using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Glow_cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject Glow_obg;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Glow_obg.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Glow_obg.SetActive(false);
    }
}
