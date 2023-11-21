using System;
using System.Collections.Generic;
using _Game._Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ScrollChecker : MonoBehaviour
{
    [SerializeField] private bool canScroll;

    [SerializeField] private bool globalLock = true;
    [SerializeField] private Vector2 mouseScroll;
    [SerializeField, Range(0, 1)] private float time;

    [SerializeField] private UnityEvent eventMinTime;
    [SerializeField] private UnityEvent eventMaxTime;
    [SerializeField] private List<SelectedWindow> SelectedWindow;
    [SerializeField] private ScrollAnimation baseScrollAnimation;
    [SerializeField, ReadOnly] private ScrollAnimation current;

    private Action<float> eventTime;

    public float Time
    {
        get => time;
        set
        {
            if ((value > .99f || value < .1f) && time != value)
            {
                time = value;
                EventStarter();
                return;
            }

            if (time > .9f && value < 1)
            {
                time = value;
                EventAfterMax();
                return;
            }

            if (time < .1f && value > 0)
            {
                time = value;
                EventAfterMin();
                return;
            }

            time = value;
        }
    }

    private void Start()
    {
        SelectedWindow.ForEach(x => x.Subscribe(ChangeScroll));
    }

    public void SubscribeToTime(Action<float> sub)
    {
        eventTime += sub;
    }

    public void AddAreas(ScrollAnimation scrollAnimation)
    {
        scrollAnimation.EventTrigger.triggers.Add(Config.GetEntry(() => { SelectArea(scrollAnimation); },
            EventTriggerType.PointerEnter));
        scrollAnimation.EventTrigger.triggers.Add(Config.GetEntry(() => { DeSelectArea(scrollAnimation); },
            EventTriggerType.PointerExit));
    }

    [Button]
    public void ChangeTime(float time)
    {
        // mouseScroll = time;
        current = baseScrollAnimation;
        Time = time;
        baseScrollAnimation.ActionScroll(time);
        eventTime.Invoke(time);
        
    }

    private void SelectArea(ScrollAnimation areaForScrollAnim)
    {
        current = areaForScrollAnim;
        current.LockAnimation = false;
        canScroll = true;
    }

    private void DeSelectArea(ScrollAnimation areaForScrollAnim)
    {
        canScroll = false;
        current.LockAnimation = true;
        current = null;
    }

    private void Update()
    {
        if (!globalLock || !canScroll || Input.mouseScrollDelta.y == 0) return;

        mouseScroll = Input.mouseScrollDelta;
        Time = Mathf.Lerp(0, 1, (time + mouseScroll.y / 20));
        eventTime.Invoke(time);
    }

    private void EventStarter()
    {
        if (time >= .99f)
        {
            eventMaxTime?.Invoke();
            current?.ActionMax();
        }
        else
        {
            eventMinTime?.Invoke();
            current?.ActionMin();
        }
    }

    private void EventAfterMax()
    {
        current?.ActionAfterMax();
    }

    private void EventAfterMin()
    {
        current?.ActionAfterMin();
    }

    private void ChangeScroll(bool test)
    {
        globalLock = !test;
    }
}