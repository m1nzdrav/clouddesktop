using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowMainIcon : MonoBehaviour
{
    [SerializeField] private RectTransform _rectForwardIcon;
    [SerializeField] private RectTransform _rectBackIcon;
    [SerializeField] private CanvasGroup _canvasGroupMain;
    [SerializeField] private Image myImage;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float imageFadeDuration = .5f;
    [SerializeField] private Text soon;
    [SerializeField] private bool needRotate = true;
    [SerializeField] private bool needWait = false;
    [SerializeField] private Sprite coloredSprite, normalSprite;
    private bool _activity;

    [SerializeField] private float _alphaForwardIcon = 0.5f;

    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _anim;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _showAnim;
    [SerializeField] private float timeToWait = .5f;

    [SerializeField] private UnityEvent _eventRotate;

    private void Start()
    {
        gameObject.GetComponent<EventTrigger>().AddEventTrigger(OnPointerEnter, EventTriggerType.PointerEnter);
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        _eventRotate?.Invoke();
        if (_activity)
        {
            ChangeToNormal();

            Hide();
        }
        else
        {
            ChangeToColored();
            if (needWait)
            {
                StartCoroutine(WaitBeforeAction(Show));
            }
            else
            {
                Show();
            }
        }
    }

    IEnumerator WaitBeforeAction(Action action)
    {
        yield return new WaitForSeconds(timeToWait);
        action.Invoke();
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Hide();
    //}

    private void Awake()
    {
        // var color = myImage.color;
        // color.a = _alphaForwardIcon;
        // myImage.color = color;
    }

    public void Show()
    {
        _activity = true;


        if (!needRotate)
            return;

        _anim.Pause();
        _showAnim.Pause();

        _anim = _rectForwardIcon.DORotate(new Vector3(0f, 180f, 0f), animationDuration);
        _showAnim = _rectBackIcon.DORotate(new Vector3(0f, 0f, 0f), animationDuration);
        _canvasGroupMain.DOFade(1, animationDuration);
        myImage.DOFade(0, imageFadeDuration);
    }

    public void Hide()
    {
        _activity = false;
        if (!needRotate)
            return;

        _anim.Pause();
        _showAnim.Pause();

        _anim = _rectForwardIcon.DORotate(new Vector3(0f, 0f, 0f), animationDuration);
        _showAnim = _rectBackIcon.DORotate(new Vector3(0f, 180f, 0f), animationDuration);
        _canvasGroupMain.DOFade(0, animationDuration);
        myImage.DOFade(_alphaForwardIcon, imageFadeDuration);
    }

    [Button]
    private void ChangeToColored()
    {
        if (coloredSprite != null)
        {
            normalSprite = myImage.sprite;
            myImage.sprite = coloredSprite;
        }
    }

    [Button]
    private void ChangeToNormal()
    {
        if (coloredSprite != null)
        {
            myImage.sprite = normalSprite;
        }
    }

    public void SetSoonText(string text)
    {
        soon.text = text;
    }
}