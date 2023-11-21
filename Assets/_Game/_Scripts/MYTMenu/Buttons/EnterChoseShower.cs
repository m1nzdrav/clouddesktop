using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterChoseShower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private List<CanvasGroup> stockShower;
    [SerializeField] private float animationDuration;
    [SerializeField] private bool block;
    [SerializeField] private LockButton lockButton;

    public bool Block
    {
        get => block;
        set
        {
            block = value;
            if (block)
            {
                Show();
            }
            else
            {
                OnPointerExit(null);
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (lockButton!=null &&!lockButton.Locked)
        {
            Show();
        }
    }

    private void Show()
    {
        stockShower.ForEach(x => x.DOFade(1, animationDuration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!block)
        {
            stockShower.ForEach(x => x.DOFade(0, animationDuration));
        }
    }
}