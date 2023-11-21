using System;
using System.Collections;
using _Game._Scripts.Button.Rotate;
using _Game._Scripts.FolderScript.Change_Sprite_Border;
using _Game._Scripts.PrefabeScript;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    [SerializeField] private float durationRotate;
    [SerializeField] private Slider _sliderRotate;

    [Header("ChildBorder")] [SerializeField]
    private ChildBorder centerChildBorder;

    [SerializeField] private ChildBorder borderChildBorder;

    [Header("Horror")] [SerializeField] private ColorSync mySync;

    [SerializeField] private float _timeDelay;
    [SerializeField] private Color myColor;
    [SerializeField, ReadOnly] private float prevValue;
    [SerializeField] private bool canSound = true;
    [SerializeField] private float waitForSound = .5f;
    [SerializeField] private CanvasGroup canvasGroup;
    private SequenceController sequenceController;
    [SerializeField] private float durationChangeListener = .2f;

    [SerializeField] private float durationChangeListenerAppear = 1f;

    public Transform VideoToRotate => (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?
        .StockOpenedPrefab[(RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock).StockOpenedPrefab.Count - 1].transform;

    [SerializeField] private UnityEvent actionBasePosition;
    [SerializeField] private UnityEvent actionAfterChangeListener;

    private void Start()
    {
        SetCurrentRotate();
    }

    public void VideoRotateBasePosition()
    {
        // VideoToRotate.DORotate(Vector3.zero, durationRotate);
        actionBasePosition?.Invoke();
        _sliderRotate.SetValueWithoutNotify(0);
        ChangeYRotate(0);
    }

    public void VideoRotate()
    {
        ChangeYRotate(_sliderRotate.value);
    }

    private bool CheckSound()
    {
        if (prevValue < 0 && _sliderRotate.value > 0 || prevValue > 0 && _sliderRotate.value < 0)
            return true;

        return canSound;
    }

    IEnumerator SoundTimer()
    {
        canSound = false;
        yield return new WaitForSeconds(waitForSound);
        canSound = true;
    }

    private void Sound()
    {
    }

    private void ChangeYRotate(float value)
    {
        prevValue = value;
        VideoToRotate?.DOLocalRotate(new Vector3(0, value, 0), durationRotate);
    }

    public void SetVideoPositionLeft()
    {
        _sliderRotate.value = -25;
    }

    public void SetVideoPosition(float value)
    {
        if (value > 180)
        {
            value -= 360;
        }

        _sliderRotate.value = value;
    }

    public void SetWithoutNotifyVideoPosition(float value)
    {
        if (value > 180)
        {
            value -= 360;
        }

        _sliderRotate.SetValueWithoutNotify(value);
    }

    private void SetCurrentRotate()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(HeadRotate)) as HeadRotate)?.SettingAfterStart(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canSound = true;
        SetCurrentRotate();
        StartCoroutine(StartTimer(eventData));
    }

    public void ChangeColor(Color color)
    {
        myColor = color;
        mySync.SetColor(color);
    }

    public void AfterChangeListener()
    {
        AnimationChangeListener();
        actionAfterChangeListener?.Invoke();
    }

    private void AnimationChangeListener()
    {
        canvasGroup.DOFade(0, durationChangeListener)
            .OnComplete(() => canvasGroup.DOFade(1, durationChangeListenerAppear));
    }

    public void RotateAll()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(HeadRotate)) as HeadRotate)?.RotateAll();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canSound)
        {
            StartCoroutine(SoundTimer());
        }

        DeleteTimer();
        StopAllCoroutines();
    }

    private IEnumerator StartTimer(PointerEventData eventData)
    {
        InitTimer(eventData);
        yield return new WaitForSeconds(_timeDelay);
        RotateAll();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canSound = true;
        DeleteTimer();
        StopAllCoroutines();
    }

    #region Timer

    public void InitTimer(PointerEventData data)
    {
        Vector3 newPos =
            new Vector3(data.position.x - data.pressEventCamera.pixelWidth / 2,
                data.position.y - data.pressEventCamera.pixelHeight / 2, 0);
        //        Vector3 newPos = Camera.main.ScreenPointToRay(data.position).GetPoint(100);
        //        Debug.LogError("point pos"+ data.position+"\n"+
        //                       "new pos "+ newPos);


        (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager)?.InitTimerToDrag(newPos, _timeDelay, myColor);
    }

    private void DeleteTimer()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager)?.DeleteTimer();
    }

    #endregion
}