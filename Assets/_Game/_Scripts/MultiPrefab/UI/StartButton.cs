using System;
using _Game._Scripts.Events.StartEnd;
using DG.Tweening;
using Shapes2D;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public enum StateButton
{
    Start,
    Stop,
    Reverse
}

public class StartButton : MonoBehaviour
{
    [Header("Component")] [SerializeField] private CanvasGroup textContinue;
    [SerializeField] private Image _selfBg, _selfImage;
    [SerializeField] private Button _button;
    [SerializeField] private Shape bgShape;

    [Header("Setting Color")] [SerializeField]
    private Color playColor;

    [SerializeField] private Color generalColor;

    [Header("Sprite")] [SerializeField] private Sprite _start;
    [SerializeField] private Sprite _stop;
    [SerializeField] private Sprite _revers;

    [Header("General Setting")] [SerializeField, ReadOnly, FoldoutGroup("Flags")]
    private bool isStart = false;

    [SerializeField, FoldoutGroup("Flags")]
    private bool canStart = true;

    [SerializeField, FoldoutGroup("Flags")]
    private bool isLock = false;

    [SerializeField, FoldoutGroup("Flags")]
    private StateButton _stateButton = StateButton.Stop;

    public bool CanStart
    {
        get => canStart;
        set => canStart = value;
    }

    public bool IsStart => isStart;

    public StateButton StateButton => _stateButton;

    public bool IsLock => isLock;


    public void Activate()
    {
        _selfBg.gameObject.SetActive(true);
    }

    public void DeActivate()
    {
        if (_stateButton == StateButton.Start || isLock)
        {
            _selfBg.gameObject.SetActive(false);
        }
    }

    public void ChangeStart()
    {
        _stateButton = StateButton.Start;
        isStart = true;
        textContinue.DOFade(0, 1.5f);

        bgShape.settings.fillColor = generalColor;
        _selfImage.sprite = _start;
    }

    public void ChangeStop()
    {
        _stateButton = StateButton.Stop;
        isStart = false;

        textContinue.DOFade(0, 1.5f);
        _selfImage.sprite = _stop;

        bgShape.settings.fillColor = playColor;
        Activate();
    }

    public void ChangeToReverse()
    {
        if (_revers == null) return;

        _stateButton = StateButton.Reverse;

        textContinue.DOFade(1, 1.5f);

        bgShape.settings.fillColor = generalColor;
        Activate();
        _selfImage.sprite = _revers;
    }

    public void ClickButton()
    {
        if (canStart)
            _button.onClick.Invoke();
    }
}