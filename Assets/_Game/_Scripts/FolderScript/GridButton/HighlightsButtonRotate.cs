using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HighlightsButtonRotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private MYTMenu _mytMenu;
    [SerializeField] private LockButton _lockButton;
    [SerializeField] private CanvasGroup myCanvas, belt;

    [SerializeField] private Image mainImage, beltImage, lockerImage, glow;

    [SerializeField, ReadOnly] private bool isPressed;

    [Header("White and Colored sprite")] [SerializeField]
    private WhiteAndColored buttonSprite;

    [SerializeField] private WhiteAndColored beltSprite;

    [Header("Animation Timing")] [SerializeField]
    private float animationBelt = .5f;

    private Tweener animatedFade;
    [SerializeField] private float waitDuration = .5f;
    [SerializeField] private SelectedWindow selectedWindow;

    private void Start()
    {
        if (selectedWindow != null) selectedWindow.Subscribe(ChangeActivate);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_lockButton.Locked)
        {
            return;
        }

        animatedFade = belt.DOFade(1, animationBelt).OnComplete((() =>
        {
            mainImage.sprite = buttonSprite.colored;
            beltImage.sprite = beltSprite.colored;
        }));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_lockButton.Locked)
        {
            return;
        }

        animatedFade.Kill();

        if (!isPressed)
        {
            belt.alpha = 0;
        }

        mainImage.sprite = buttonSprite.white;
        beltImage.sprite = beltSprite.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!myCanvas.interactable) return;
        isPressed = !isPressed;
        _mytMenu?.ChangeHighlightButton(this);
    }

    public void OffPressed()
    {
        isPressed = false;
        belt.alpha = 0;
    }


    public void ActivateBelt()
    {
        animatedFade = belt.DOFade(1, animationBelt);
        _mytMenu?.ChangeHighlightButton(this);
        glow?.DOFade(1, .2f).OnComplete(() => glow.DOFade(0, .2f));
        isPressed = true;
    }

    public void DeactivateBelt()
    {
        animatedFade.Kill();
        isPressed = false;
        belt.alpha = 0;
    }

    private void ChangeActivate(bool test)
    {
        if (test)
        {
            ActivateBelt();
        }
        else
        {
            DeactivateBelt();
        }
    }
}