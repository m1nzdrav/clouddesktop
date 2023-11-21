using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


    public class ShowTopPanel : ShowOn
    {
        [SerializeField] private GameObject AreaToShow;

        [SerializeField] private float animationDuration = .2f;


        public override void Enter(EventTrigger who)
        {
            if (!IsBlocked)
            {
                Show(who);
            }
        }

        public override void Exit(EventTrigger who)
        {
            if (!IsBlocked)
            {
                Hide(who);
            }
        }

        private void Show(EventTrigger who)
        {
            if (AreaToShow == null) return;
            AreaToShow.transform.localScale = Vector3.one;
            LayoutRebuilder.ForceRebuildLayoutImmediate(
                AreaToShow.transform.parent.GetComponent<RectTransform>());

            CanvasGroup canvasGroup =
                AreaToShow.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
            canvasGroup.DOFade(1, animationDuration * 4).OnComplete(() => { }
            );
        }

        private void Hide(EventTrigger who)
        {
            if (AreaToShow == null) return;
            CanvasGroup canvasGroup =
                AreaToShow.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
            canvasGroup.DOFade(0, animationDuration).OnComplete(() =>
                {
                    AreaToShow.transform.localScale = Vector3.zero;

                    LayoutRebuilder.ForceRebuildLayoutImmediate(
                        AreaToShow.transform.parent.GetComponent<RectTransform>());
                    // AreaToShow.SetActive(false);
                }
            );
        }
    }
