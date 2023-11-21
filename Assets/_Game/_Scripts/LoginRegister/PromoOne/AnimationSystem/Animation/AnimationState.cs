using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class AnimationState : MonoBehaviour
{
    public AnimationStateChanger thisAnimationStateChanger;
    public AnimationStateChanger _parentAnimationStateChanger;
    public float _durationScale = .5f;
    [SerializeField] private bool isStarted = false;

    public void FirstStart(AnimationStateChanger animationOnSubs)
    {
        _parentAnimationStateChanger = animationOnSubs;
        if (isStarted)
        {
            
            EndState();
        }
        else
        {
            isStarted = true;
            // _parentAnimationStateChanger = animationOnSubs;

            gameObject.SetActive(true);
            transform.parent.gameObject.SetActive(true);
            // transform.DOScale(Vector3.one, _durationScale);

            StartState();
        }
    }
    public void FirstFastStart(AnimationStateChanger animationOnSubs)
    {
        _parentAnimationStateChanger = animationOnSubs;
        if (isStarted)
        {
            
            FastEndState();
        }
        else
        {
            isStarted = true;
            // _parentAnimationStateChanger = animationOnSubs;

            gameObject.SetActive(true);
            transform.parent.gameObject.SetActive(true);
            // transform.DOScale(Vector3.one, _durationScale);

            FastStartState();
        }
    }
    public abstract void StartState();
    
    
    public abstract void EndState();

    public abstract void FastStartState();
    
    
    public abstract void FastEndState();

    public abstract void ReadyEndState();

    public abstract void SetSetting(Content content);

    public abstract IEnumerator StartAnimation();
    public abstract IEnumerator FastStartAnimation();

    public void AddNewChildStates(AnimationState state)
    {
        if (thisAnimationStateChanger == null)
        {
            thisAnimationStateChanger = new AnimationStateChanger();
        }

        thisAnimationStateChanger.AddStateToList(state);
    }
}