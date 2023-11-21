using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowingOnEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject AreaToShow;
    [SerializeField] private RectTransform ParentArea;
    [SerializeField] private bool needShowing;
    [SerializeField] private float animationDuration = .5f;


    private void Awake()
    {
        if (needShowing)
        {
            AreaToShow.GetComponent<CanvasGroup>().DOFade(0, animationDuration).OnComplete(
                () => { AreaToShow.transform.localScale = Vector3.zero; }
            );
            ParentArea = AreaToShow.transform.parent.GetComponent<RectTransform>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (needShowing)
        {

            AreaToShow.transform.localScale = Vector3.one;
            LayoutRebuilder.ForceRebuildLayoutImmediate(ParentArea);

            CanvasGroup canvasGroup =
                AreaToShow.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
            canvasGroup.DOFade(1, animationDuration * 4).OnComplete(() => { }
            );


//            AreaToShow.transform.localScale = Vector3.one;
//            LayoutRebuilder.ForceRebuildLayoutImmediate(ParentArea);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {


        if (needShowing)
        {
            CanvasGroup canvasGroup =
                AreaToShow.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
            canvasGroup.DOFade(0, animationDuration).OnComplete(() =>
                {
                    AreaToShow.transform.localScale = Vector3.zero;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(ParentArea);
                }
            );

//            AreaToShow.transform.localScale = Vector3.zero;
//
//            LayoutRebuilder.ForceRebuildLayoutImmediate(ParentArea);
        }
    }
}