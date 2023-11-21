using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AnimationLayoutPadding : MonoBehaviour
{
    [SerializeField] private HorizontalOrVerticalLayoutGroup targetLayoutGroup;
    [SerializeField] private RectTransform toRebuild;
    [SerializeField] private int maxValue = 300;
    [SerializeField] private int minValue;
    [SerializeField] private int step = 1;
    [SerializeField] private float waitTime = .1f;
    [SerializeField] private UnityEvent _eventEnd;
    private WaitForSeconds _waitForSeconds;

    [Button]
    public void AnimateLayout()
    {
        StopAllCoroutines();

        minValue = targetLayoutGroup.padding.left;

        StartCoroutine(LerpSpacingUp(maxValue));
    }

    [Button]
    public void ReverseAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(LerpSpacingDown(minValue));
    }

    IEnumerator LerpSpacingUp(float valueTo)
    {
        if (!(targetLayoutGroup.padding.left < valueTo))
        {
            _eventEnd?.Invoke();
            yield break;
        }

        yield return new WaitForFixedUpdate();

        targetLayoutGroup.padding.left += step;
        LayoutRebuilder.MarkLayoutForRebuild(toRebuild);
        StartCoroutine(LerpSpacingUp(valueTo));
    }

    IEnumerator LerpSpacingDown(float valueTo)
    {
        if (!(targetLayoutGroup.padding.left > valueTo))
        {
            _eventEnd?.Invoke();
            yield break;
        }

        yield return new WaitForFixedUpdate();

        targetLayoutGroup.padding.left -= step;
        LayoutRebuilder.MarkLayoutForRebuild(toRebuild);
        StartCoroutine(LerpSpacingDown(valueTo));
    }
}