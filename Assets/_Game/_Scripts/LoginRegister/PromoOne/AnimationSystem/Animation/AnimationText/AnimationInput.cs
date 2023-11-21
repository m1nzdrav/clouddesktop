using System;
using System.Collections;
using _Game._Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class AnimationInput : AnimationText
{
    private AnimationTextType _animationTextType = AnimationTextType.Input;

    [SerializeField] private PromoLinks _text;

    public override void StartState()
    {
        StartCoroutine(StartAnimation());
    }

    public override void EndState()
    {
        _parentAnimationStateChanger?.ChangeState();
    }

    public override void FastStartState()
    {
        StartCoroutine(FastStartAnimation());
    }

    public override void FastEndState()
    {
        _parentAnimationStateChanger.FastChangeState();
    }

    public override void ReadyEndState()
    {
        _parentAnimationStateChanger.ChangeState();
    }

    public override void SetSetting(Content content)
    {
        animationTextJson = content.animationTextJson;
        _text = GetComponent<PromoOneStrController>().PromoLinks;
    }

    protected override void DoConsistently(PromoOneStrController promoOneStrController)
    {
        inputAnimation(promoOneStrController, () =>
        {
            promoOneStrController.EndPutText();
            ReadyEndState();
        });
    }

    protected override void DoParallel(PromoOneStrController promoOneStrController)
    {
        inputAnimation(promoOneStrController, promoOneStrController.EndPutText);

        // promoOneStrController.NextActivitySystem.TryNext(true, true);
        ReadyEndState();
    }

    private void inputAnimation(PromoOneStrController promo, Action action = null)
    {
        string currentText = _text.Text.text;
        _text.Text.text = "";
        _text.Text.DOFade(_text.Alpha, 0);

        StartCoroutine(CustomAnimationInput(promo, _text.Text, currentText, animationTextJson.time, action));
    }


    IEnumerator CustomAnimationInput(PromoOneStrController promo, Text textField, string fulText, float timeToChar,
        Action action = null)
    {
        if (promo.CircleSettings.CircleFactory != null)
        {
            promo.CircleSettings.CircleFactory.ShowCircle(promo.CircleSettings);
            yield return new WaitForSeconds(Config.TIME_CIRCLE);
        }
        else
        {
            yield return null;
        }

        GetComponent<CircleAudioAnimation>().StartAnimation();
        WaitForSeconds test = new WaitForSeconds(timeToChar);

        foreach (var oneChar in fulText)
        {
            textField.text += oneChar;
            yield return test;
        }

        action?.Invoke();
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