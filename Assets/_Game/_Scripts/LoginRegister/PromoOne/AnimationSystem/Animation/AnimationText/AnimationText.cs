using System.Collections;
using _Game._Scripts;
using DG.Tweening;
using UnityEngine;

public abstract class AnimationText : AnimationState
{
    private AnimationTextType _animationTextType;
    [SerializeField] protected AnimationTextJson animationTextJson;
    public AnimationTextType AnimationTextType => _animationTextType;

    protected void FastEnd()
    {
        _parentAnimationStateChanger?.FastChangeState();
        if (TryGetComponent(out ChildContainer result))
        {
            result.TryOpenOnClick();
        }
    }

    protected void End()
    {
        _parentAnimationStateChanger?.ChangeState();
        if (TryGetComponent(out ChildContainer result))
        {
            result.TryOpenOnClick();
        }
    }

    public override IEnumerator StartAnimation()
    {
        PromoOneStrController promoOneStrController = GetComponent<PromoOneStrController>();
        promoOneStrController.TriangleImage.SetActive(false);
        promoOneStrController.Lamp.transform.DOScale(Config.VECTOR_ANIMATION_LAMP_ENTER, .3f).OnComplete(
            () =>
            {
                promoOneStrController.Lamp.transform.DOScale(Vector3.zero, .3f);
                promoOneStrController.TriangleImage.SetActive(true);
            });

        yield return new WaitForSeconds(animationTextJson.delay);

        AnimationState nextState;

        if (GetComponent<AnimationOnSubs>().AnimationStateOpen != null)
        {
            nextState = GetComponent<AnimationOnSubs>().AnimationStateOpen.GetComponent<AnimationOpen>()
                ._parentAnimationStateChanger.GetNextState();
        }
        else
        {
            nextState = GetComponent<AnimationOnSubs>().AnimationStateText.GetComponent<AnimationText>()
                ._parentAnimationStateChanger.GetNextState();
        }

        promoOneStrController.NextActivitySystem.TryNext(true, true);

        if (nextState == null || nextState.GetComponent<AnimationParallel>() != null)
        {
            DoParallel(promoOneStrController);
            yield break;
        }

        DoConsistently(promoOneStrController);
    }

    public override IEnumerator FastStartAnimation()
    {
        PromoOneStrController promoOneStrController = GetComponent<PromoOneStrController>();
        promoOneStrController.TriangleImage.SetActive(false);
        promoOneStrController.Lamp.transform.DOScale(Config.VECTOR_ANIMATION_LAMP_ENTER, 0).OnComplete(
            () =>
            {
                promoOneStrController.Lamp.transform.DOScale(Vector3.zero, 0);
                promoOneStrController.TriangleImage.SetActive(true);
            });


        yield return null;

        if (TryGetComponent(out AnimationOnSubs animationOnSubs) &&
            animationOnSubs.AnimationStateOpen!=null && animationOnSubs.AnimationStateOpen.TryGetComponent(out AnimationOpen animationOpen))
            animationOpen._parentAnimationStateChanger.GetNextState();

        promoOneStrController.NextActivitySystem.TryNext(true, true);
        DoFastText(promoOneStrController);
    }

    protected abstract void DoConsistently(PromoOneStrController promoOneStrController);
    protected abstract void DoParallel(PromoOneStrController promoOneStrController);
    protected abstract void DoFastText(PromoOneStrController promoOneStrController);
}