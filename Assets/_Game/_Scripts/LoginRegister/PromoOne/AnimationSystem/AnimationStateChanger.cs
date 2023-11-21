using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AnimationStateChanger
{
    private AnimationState parentState;
    [SerializeField] private List<AnimationState> _animationStates;

    private int _currentAnimationCount;

    public AnimationStateChanger()
    {
        _animationStates = new List<AnimationState>();
    }

    public AnimationState GetNextState()
    {
        return _currentAnimationCount + 1 >= _animationStates.Count
            ? null
            : _animationStates[_currentAnimationCount + 1];
    }

    public void ChangeState()
    {
        _currentAnimationCount++;

        if (_currentAnimationCount == _animationStates.Count)
            parentState?.EndState();
        else if (_currentAnimationCount < _animationStates.Count)
            _animationStates[_currentAnimationCount].FirstStart(this);
    }

    public void StartState(AnimationState animationState)
    {
        parentState = animationState;
        _currentAnimationCount = 0;
        _animationStates[_currentAnimationCount]?.FirstStart(this);
    }

    public void StartState()
    {
        SequenceController animation = new SequenceController();
        animation._same.Append(_animationStates[0].transform.DOScale(Vector3.one, .8f).OnPlay(() =>
        {
            (RegisterSingleton._instance.GetSingleton(typeof(MusicStock)) as MusicStock)?.PlayMusic();
        }));

        if ((_animationStates[0] as AnimationText) == null)
            animation._same.Append(_animationStates[0].transform.DOPunchScale(Vector3.up * 10, .3f, 1));

        AddAnimation(animation, 1);
        animation.PlaySequence(() => _animationStates[_currentAnimationCount]?.FirstStart(this));
    }

    public void StartChildState(PromoOneStrController parentDot, int childCount)
    {
        SequenceController animation = new SequenceController();
        AddAnimation(animation, 0);
        animation.PlaySequence(() => _animationStates[_currentAnimationCount]?.FirstStart(this));
    }

    private void AnimationSwordAndLine(PromoOneStrController parentDot, int childCount, SequenceController animation)
    {
        animation._same.Append(parentDot.Line.DOFillAmount(1, .4f));
        animation._same.Join(parentDot.WhiteSword.DOFillAmount(1, .4f));

        animation._same.Append(
            parentDot.Line.transform.DOMoveY(_animationStates[_animationStates.Count - 1].transform.position.y,
                .2f * childCount));
        animation._same.Join(
            parentDot.WhiteSword.transform.DOMoveY(_animationStates[_animationStates.Count - 1].transform.position.y,
                .2f * childCount));


        animation._same.Append(parentDot.Line.DOFillAmount(0, .5f));
        animation._same.Join(parentDot.WhiteSword.DOFillAmount(0, .5f));

        animation._same.Append(
            parentDot.Line.transform.DOLocalMoveY(0, .4f));
        animation._same.Join(
            parentDot.WhiteSword.transform.DOLocalMoveY(0,
                .4f));
    }

    private void AddAnimation(SequenceController animation, int startIndex)
    {
        RectTransform rectTransform = _animationStates[0].transform.parent.GetComponent<RectTransform>();
        for (var index = startIndex; index < _animationStates.Count; index++)
        {
            //todo если размер равен 1, то надо сделать анимацию увеличения
            if (_animationStates[index].transform.localScale == Vector3.one)
            {
                if ((_animationStates[index] as AnimationText) != null) continue;

                animation._same.Append(_animationStates[index].transform.DOScale(Vector3.up * 2, .04f)
                );
                animation._same.Append(_animationStates[index].transform.DOScale(Vector3.one, .04f)
                    .OnComplete(() => LayoutRebuilder.MarkLayoutForRebuild(rectTransform)));
            }
            else
                animation._same.Append(_animationStates[index].transform.DOScale(Vector3.one, .11f)
            .OnComplete(() => LayoutRebuilder.MarkLayoutForRebuild(rectTransform)));
        }

        
        // animation._same.OnUpdate(() =>
        //     LayoutRebuilder.MarkLayoutForRebuild(rectTransform
        //     ));
        parentState = null;
        _currentAnimationCount = 0;
    }

    #region FastAnim

    public void FastChangeState()
    {
        _currentAnimationCount++;

        if (_currentAnimationCount == _animationStates.Count)
            parentState?.FastEndState();
        else if (_currentAnimationCount < _animationStates.Count)
            _animationStates[_currentAnimationCount].FirstFastStart(this);
    }

    public void FastStartState(AnimationState animationState)
    {
        parentState = animationState;
        _currentAnimationCount = 0;
        _animationStates[_currentAnimationCount]?.FirstFastStart(this);
    }

    public void FastStartState()
    {
        SequenceController animation = new SequenceController();
        animation._same.Append(_animationStates[0].transform.DOScale(Vector3.one, 0));

        AddFastAnimation(animation, 1);
        animation.PlaySequence(() => _animationStates[_currentAnimationCount]?.FirstFastStart(this));
    }

    private void AddFastAnimation(SequenceController animation, int startIndex)
    {
        for (var index = startIndex; index < _animationStates.Count; index++)
        {
            animation._same.Append(_animationStates[index].transform.DOScale(Vector3.one, 0));
        }

        animation._same.OnUpdate(() =>
            LayoutRebuilder.ForceRebuildLayoutImmediate(_animationStates[0].transform.parent
                .GetComponent<RectTransform>()));

        parentState = null;
        _currentAnimationCount = 0;
    }

    #endregion

    public void AddStateToList(AnimationState animationState)
    {
        _animationStates.Add(animationState);
    }
}