using System;
using System.Collections;
using System.Runtime.InteropServices;
using _Game._Scripts.Events.StartEnd;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class AnimationLayout : MonoBehaviour

{
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private float spacingValue = 300;
    [SerializeField] private float tempCurrentValue;
    [SerializeField] private float step = 1;
    [SerializeField] private float waitTime = .1f;
    [SerializeField] private UnityEvent _eventEnd;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        // step = Screen.width / 1920 * 2;
    }

    [Button]
    public void AnimateLayout()
    {
        StopAllCoroutines();
        
        tempCurrentValue = _layoutGroup.spacing;
       
        StartCoroutine(LerpSpacingUp(spacingValue));
    }

    [Button]
    public void ReverseAnimation()
    {
        StopAllCoroutines();
        // tempCurrentValue = _layoutGroup.spacing;
        StartCoroutine(LerpSpacingDown(tempCurrentValue));
    }

    IEnumerator LerpSpacingUp(float valueTo)
    {
        if (!(_layoutGroup.spacing < valueTo))
        {
            _eventEnd?.Invoke();
            yield break;
        }
        yield return new WaitForFixedUpdate();


        _layoutGroup.spacing +=  step ;
        StartCoroutine(LerpSpacingUp(valueTo));
    }

    IEnumerator LerpSpacingDown(float valueTo)
    {
        if (!(_layoutGroup.spacing > valueTo))
        {
            _eventEnd?.Invoke();
            yield break;
        }

        yield return new WaitForFixedUpdate();

        _layoutGroup.spacing -=  step ;
        StartCoroutine(LerpSpacingDown(valueTo));
    }

    [Button]
    public void Test()
    {
        Start();
    }
}