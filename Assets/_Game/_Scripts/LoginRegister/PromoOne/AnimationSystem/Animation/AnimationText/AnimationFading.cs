using System;
using System.Collections;
using _Game._Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFading : AnimationText
{
    private AnimationTextType _animationTextType = AnimationTextType.Fading;

    [SerializeField] private PromoLinks _text;

    public override void StartState()
    {
        StartCoroutine(StartAnimation());
    }

    public override void EndState()
    {
        End();
    }

    public override void FastStartState()
    {
        StartCoroutine(FastStartAnimation());
    }

    public override void FastEndState()
    {
        FastEnd();
    }

    public override void ReadyEndState()
    {
        End();
    }

    public override void SetSetting(Content content)
    {
        animationTextJson = content.animationTextJson;
        _text = GetComponent<PromoOneStrController>().PromoLinks;
    }

    protected override void DoConsistently(PromoOneStrController promoOneStrController)
    {
        StartCoroutine(CustomAnimationParallel(promoOneStrController, () =>
        {
            promoOneStrController.EndPutText();
            ReadyEndState();
        }));
    }


    IEnumerator CustomAnimationParallel(PromoOneStrController promoOneStrController, Action action = null)
    {
        if (promoOneStrController.CircleSettings.CircleFactory != null)
        {
            promoOneStrController.CircleSettings.CircleFactory.ShowCircle(promoOneStrController.CircleSettings);
            yield return new WaitForSeconds(Config.TIME_CIRCLE);
        }
        else
        {
            yield return null;
        }


        GetComponent<CircleAudioAnimation>().StartAnimation();
        _text.Text.DOFade(_text.Alpha, animationTextJson.time)
            .OnComplete(() => { action?.Invoke(); });
    }

    protected override void DoParallel(PromoOneStrController promoOneStrController)
    {
        // promoOneStrController.NextActivitySystem.TryNext(true, true);
        StartCoroutine(CustomAnimationParallel(promoOneStrController, promoOneStrController.EndPutText));
        ReadyEndState();
    }

    protected override void DoFastText(PromoOneStrController promoOneStrController)
    {
        if (promoOneStrController.CircleSettings.CircleFactory != null)
        {
            promoOneStrController.CircleSettings.CircleFactory.ShowCircle(promoOneStrController.CircleSettings);
        }

        promoOneStrController.FastEndPutText();
        _text.Text.DOFade(_text.Alpha, 0);
        FastEndState();
    }
}