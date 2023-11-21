using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class TooltypeShow : MonoBehaviour
{
    [SerializeField] private Vector3 endValue;
    [SerializeField] private float jumpPower;
    [SerializeField] private int numJumps;
    [SerializeField] private float duration;
    [SerializeField] private bool _isShowed = true;

    public void ShowHideTooltype()
    {
        if (_isShowed)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        _isShowed = true;
//        gameObject.SetActive(true);
        transform.DOLocalJump(endValue, 1, 1, 0.5f);
        StartCoroutine(UpAlpha(true));
       
    }

    public void Hide()
    {
        _isShowed = false;

        transform.DOLocalJump(-endValue, jumpPower, numJumps, duration);
        StartCoroutine(UpAlpha(false));


    }

    IEnumerator UpAlpha(bool isUp)
    {
        float time = duration;

        while (time>0)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            if (isUp)
            {
                canvasGroup.alpha += duration * Time.deltaTime;
            }
            else
            {
                canvasGroup.alpha -= duration * Time.deltaTime;
            }

            time -= duration * Time.deltaTime;
        }
        

        yield return null;
    }
}