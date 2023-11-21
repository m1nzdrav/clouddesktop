using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonRotateGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image glow;
    [SerializeField] private bool block;
    [SerializeField] private HighlightsButtonRotate _highlightsButtonRotate;
    [SerializeField] private float fadeDuration = .5f;

    public bool Block
    {
        get => block;
        set
        {
            block = value;
            if (value == false && _highlightsButtonRotate != null)
            {
                _highlightsButtonRotate.OffPressed();
            }

            glow?.gameObject.SetActive(block);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (glow != null && !glow.gameObject.activeSelf)
        {
            glow.gameObject.SetActive(true);
        }

        glow?.DOFade(1, fadeDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // if (!block)
        // {
        glow?.DOFade(0f, fadeDuration);
        // }
    }
}