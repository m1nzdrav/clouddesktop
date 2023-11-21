using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.Extensions;
using DynamicPanels;
using Michsky.UI.ModernUIPack;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class StartAnimation : MonoBehaviour
{
    private Vector3 _endPos;
    private float _startY = 18f;
    [SerializeField] private bool needFirstStart = true;

    [SerializeField] private List<CanvasGroup> needClose;
    [SerializeField] private RectTransform target;
    [SerializeField] private bool _isLogin;

    [SerializeField] private GameObject _mainImage;
    [SerializeField] private GameObject _mainText;
    [SerializeField] private GameObject _mainExpand;

    [SerializeField] private RectTransform loginTransform;
    [SerializeField] private RectTransform breadTransform;
    private Vector3 _oldTransformLogin;

    [SerializeField] private float startPos;

    [SerializeField] private GameObject lockArea;
    [SerializeField] private CustomDropdown _customDropdown;
    [SerializeField] private List<CanvasGroup> circles;
    [SerializeField] private Vector2 maxOpen, minOpen;
    [SerializeField] private float animationTime = 1.5f;
    [SerializeField] private float positionClosed;

    [SerializeField] private Vector3 localPosition;
    [SerializeField] private float waitAnimOpen = 2f;


    // [SerializeField] private object oldTransformLogin;
    public List<CanvasGroup> Circles => circles;

    private void Start()
    {
        _endPos = transform.localPosition;
    }

    public void FirstStart()
    {
        if (!needFirstStart) return;

        _endPos = transform.localPosition;

        if (_isLogin)
        {
            _startY *= -1f;
        }

        NewAnim();
        // StartCoroutine(AnimLogin());
    }

    public void NewAnim()
    {
        target.Center(true);
        target.DOAnchorMin(new Vector2(0, .5f), animationTime);
        target.DOAnchorMax(new Vector2(1, .5f), animationTime).OnComplete(() =>
        {
            localPosition = transform.localPosition;
            transform.DOLocalMove(new Vector3(0, positionClosed, 0), 0.1f);
            EnableImage();

            if (!_isLogin)
            {
                SetLoginCenter();
            }
        });
    }

    public void ReturnToOpen()
    {
        ReturnToOpen(null);
    }

    public void ReturnToOpen(UnityAction afterAnim = null)
    {
        StartCoroutine(AnimLogin(afterAnim));
    }

    public void ReturnToClose(float position)
    {
        ReturnToClose(position, null);
    }

    [Button]
    public void ReturnToClose(float position, UnityAction afterAnim)
    {
        Circles.ForEach(x => x.DOFade(1, 1f));
        target.DOAnchorMin(new Vector2(0, .5f), animationTime);
        target.DOAnchorMax(new Vector2(1, .5f), animationTime)
            .OnComplete(() =>
            {
                transform.DOLocalMove(new Vector3(0, position, 0), 1f).OnComplete(() => { afterAnim?.Invoke(); });
            });

        if (_customDropdown != null)
        {
            needClose.ForEach(x => { x.DOFade(0, .5f); });
            _customDropdown.animationType = CustomDropdown.AnimationType.STYLISH_600;
        }
    }

    private string test = "test";


    public void SetLoginCenter()
    {
        _oldTransformLogin = loginTransform.localPosition;


        loginTransform.ChangePivotWithoutAffectingPosition(new Vector2(.5f, .5f));
        loginTransform.DOLocalMove(Vector3.zero, 1f);
        breadTransform?.ChangePivotWithoutAffectingPosition(new Vector2(1f, .5f));
        breadTransform?.DOLocalMove(new Vector2(Screen.width / 2f - 25f, .5f), 1f);
    }

    IEnumerator AnimLogin(UnityAction afterAnim = null)
    {
        DeactivateCircles();
        target.DOAnchorMin(minOpen, animationTime);
        target.DOAnchorMax(maxOpen, animationTime).OnComplete(() =>
        {
            transform.DOLocalMove(new Vector3(0, _endPos.y, 0), 0.1f);
            afterAnim?.Invoke();
        });

        yield return new WaitForSeconds(animationTime + waitAnimOpen);

        if (_customDropdown != null)
        {
            needClose.ForEach(x =>
            {
                //Оnключаем в rotatePlanes
                x.interactable = true;
                x.DOFade(1, 1f);
            });
            _customDropdown.animationType = CustomDropdown.AnimationType.SLIDING;
        }
    }

    public void DeactivateCircles()
    {
        Circles.ForEach(x => x.DOFade(0, 1));
    }

    private void EnableImage()
    {
        if (_isLogin)
        {
            // transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        }
        else
        {
            _mainImage.SetActive(true);
            _mainText.SetActive(true);
            _mainExpand.SetActive(true);
        }


        if (_customDropdown != null)
        {
            _customDropdown.animationType = CustomDropdown.AnimationType.STYLISH_600;
        }
    }


    public void AnimReturn(float endPosY, UnityAction afterAnim = null)
    {
        _endPos.y = transform.localPosition.y;
        Circles.ForEach(x => x.DOFade(1, 1f));
        transform.DOLocalMove(new Vector3(0, endPosY, 0), 1f).OnComplete(() => { afterAnim?.Invoke(); });

        if (_customDropdown != null)
        {
            _customDropdown.animationType = CustomDropdown.AnimationType.STYLISH_600;
        }
    }

    public void AnimReturn(UnityAction afterAnim = null)
    {
        Circles.ForEach(x => x.DOFade(0, 1));

        transform.DOLocalMove(new Vector3(0, _endPos.y, 0), 1f).OnComplete(() => { afterAnim?.Invoke(); });
        if (_customDropdown != null)
        {
            _customDropdown.animationType = CustomDropdown.AnimationType.SLIDING;
        }
    }

    public void ReturnLoginLeft()
    {
        loginTransform.ChangePivotWithoutAffectingPosition(new Vector2(0, .5f));
        loginTransform.DOLocalMove(_oldTransformLogin, 1f);
        breadTransform?.ChangePivotWithoutAffectingPosition(new Vector2(.5f, .5f));
        breadTransform?.DOLocalMove(Vector3.zero, 1f);
    }

    private float Easing(float x)
    {
        return x * x;
    }
}