using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using UnityEngine;
using UnityEngine.Events;

public class AreaForScrollAnim : ScrollAnimation
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform baseRect;

    [SerializeField] private float baseScale = 1f;
    [SerializeField] private Vector3 maxPosition;


    [SerializeField] private UnityEvent actionMax;
    [SerializeField] private UnityEvent actionMin;
    [SerializeField] private UnityEvent eventAfterMaxTime;
    [SerializeField] private UnityEvent eventAfterMinTime;
    private SequenceController _sequenceController;


    public override void ActionMax()
    {
        actionMax.Invoke();
    }

    public override void ActionMin()
    {
        actionMin.Invoke();
    }

    public override void ActionAfterMin()
    {
        eventAfterMinTime?.Invoke();
    }

    public override void ActionAfterMax()
    {
        eventAfterMaxTime?.Invoke();
    }

    public override void ActionScroll(float time)
    {
        if (LockAnimation)
            return;

        _rectTransform.DOLocalMove(-baseRect.localPosition * time, 1f);
        _rectTransform.DOScale(baseScale + time * (2.85f - .5f), 1f);
    }
    
    public void AnimationToMax()
    {
        _rectTransform.DOScale(baseScale + 2.25f, 1f);
        _rectTransform.DOLocalMove(-baseRect.localPosition * 1, 1f);
    }

    public void AnimationToMin()
    {
        _rectTransform.DOScale(baseScale, 1f);
        _rectTransform.DOLocalMove(-baseRect.localPosition * 0, 1f);
    }

    public void AnimationSplitMinMax()
    {
        if (_sequenceController == null) _sequenceController = new SequenceController();

        if (_rectTransform.localScale.x > baseScale)
        {
            _sequenceController._same.Append(_rectTransform.DOScale(baseScale, 1f).OnComplete(() =>
            {
                _rectTransform.DOScale(baseScale + 2.25f, 1f);
                _rectTransform.DOLocalMove(-baseRect.localPosition * 1, 1f);
            }));
            _sequenceController._same.Join(_rectTransform.DOLocalMove(-baseRect.localPosition * 0, 1f));
        }
        else
        {
            _sequenceController._same.Append(_rectTransform.DOScale(baseScale + 2.25f, 1f));
            _sequenceController._same.Join(_rectTransform.DOLocalMove(-baseRect.localPosition * 1, 1f));
        }


        _sequenceController.PlaySequence(null);
    }
}