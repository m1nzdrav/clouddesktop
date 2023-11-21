using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGlowController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image Glow;

    private const float START_ALPHA_F = 0.5f;
    private const float ENTER_ALPHA_F = 1f;
    private const float CLICK_ALPHA_F = 0.75f;


    private void Start()
    {
        SetGlowAlpha(START_ALPHA_F);
    }

    public void SetGlowAlpha(float alpha)
    {
        var color = Glow.color;
        color.a = alpha;

        Glow.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetGlowAlpha(ENTER_ALPHA_F);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetGlowAlpha(START_ALPHA_F);
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     SetGlowAlpha(CLICK_ALPHA_F);
    // }
    //
    //
    // public void OnPointerUp(PointerEventData eventData)
    // {
    //     SetGlowAlpha(ENTER_ALPHA_F);
    // }
}