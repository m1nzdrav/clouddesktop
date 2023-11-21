using System;
using _Game._Scripts.Events.StartEnd;
using _Game._Scripts.Panel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class SelectedWindow : FirstSecondEvent, IDragHandler
{
    [SerializeField] private bool isMainSelected = false;

    public delegate void EventScroll(bool test);

    private Action<bool> eventScroll;

    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;

    public bool IsMainSelected
    {
        get => isMainSelected;
        set
        {
            isMainSelected = value;
            eventScroll?.Invoke(value);
            if (value == true)
            {
                OnSelected?.Invoke();    
            }
            else
            {
                OnDeselected?.Invoke();
            }
        }
    }

    [SerializeField] private protected GameObject selectedObject;
    [SerializeField] private protected bool checkSelected = false;

    [SerializeField] private EventTrigger _eventTrigger;

    private void Start()
    {
        InstantiateEvent();
        if (_eventTrigger != null)
        {
            _eventTrigger.AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);
        }
    }

    private void OnPointerUp(BaseEventData arg0)
    {
        if (!checkSelected) return;

        SendThisWindow();
        checkSelected = false;
    }

    public void SendThisWindow()
    {
        SendCurrentWindow(selectedObject);
    }

    public void SendCurrentWindow(GameObject obj)
    {
        if (obj != null && obj.GetComponent<PrefabName>()?.Prefab != Prefab.MYTIcon60_60)
        {
            SendCurrentWindowAllPrefab(obj);
        }
        else
            SendNullWindow();
    }

    public void SendNullWindow()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(HeadSelectedLoginScene)) as HeadSelectedLoginScene)?.CheckSelectedObj(null);
        (RegisterSingleton._instance.GetSingleton(typeof(HeadSelectedLoginScene)) as HeadSelectedLoginScene)?.RemoveSelectedWindow(this);
    }

    private void SendCurrentWindowAllPrefab(GameObject obj)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(HeadSelectedLoginScene)) as HeadSelectedLoginScene)?.CheckSelectedObj(obj.GetComponent<SelectedWindow>());
    }

    public void SetMainSelected(bool selected)
    {
        IsMainSelected = selected;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        checkSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!checkSelected) return;
        SendThisWindow();
        checkSelected = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (checkSelected)
        {
            checkSelected = false;
        }
    }

    public void Subscribe(Action<bool> test)
    {
        eventScroll += test;
    }
}