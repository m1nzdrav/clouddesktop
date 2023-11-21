using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowButtonPanel : ShowOn
{
  

    [SerializeField] private GameObject AreaToShow;

    [SerializeField] private float animationDuration = .2f;

    public override void Enter(EventTrigger who)
    {
        if (!IsBlocked)
        {
            Show();
        }
    }

    public override void Exit(EventTrigger who)
    {
        if (!IsBlocked)
        {
            Hide();
        }
    }

    private void Show()
    {
        AreaToShow.transform.localScale = Vector3.one;

        CanvasGroup canvasGroup =
            AreaToShow.GetComponent<CanvasGroup>();

        AreaToShow.SetActive(true);

        canvasGroup.DOKill();
        canvasGroup.DOFade(1, animationDuration * 4).OnComplete(() => { }
        );
    }

    private void Hide()
    {
        CanvasGroup canvasGroup =
            AreaToShow.GetComponent<CanvasGroup>();

        canvasGroup.DOKill();
        canvasGroup.DOFade(0, animationDuration).OnComplete(() =>
            {
                AreaToShow.transform.localScale = Vector3.zero;
                AreaToShow.SetActive(false);
            }
        );
    }
}