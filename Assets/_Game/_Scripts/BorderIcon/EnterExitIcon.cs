using System.Collections.Generic;
using UnityEngine;

public class EnterExitIcon : MonoBehaviour
{
    [SerializeField] private IconModel _iconModel;
    [SerializeField] private CircleSettings circleSettings;

    [SerializeField] private GameObject _item;

    [SerializeField] private bool _isCreate;

    private List<GameObject> _circles = new List<GameObject>();
    private int _circleCount;

    private bool _isClick;

    private EventTriggerSystem _eventTrigger;

    private void Awake()
    {
        if (_iconModel != null)
        {
            //var circleManager = _iconModel.GetComponent<CircleSettings>();

            //if (circleManager != null)
            //{
            //    _circles = circleManager.Circles;
            //}
        }

        if (_item == null)
        {
            _item = _iconModel.gameObject;
        }

        //if (_circleManager != null)
        //{
        //    _circles = _circleManager.Circles;
        //}

        //EventTrigger trigger = _item.GetComponent<EventTrigger>();

        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerEnter;
        //entry.callback.AddListener((data) => { GlowOn(); });
        //trigger.triggers.Add(entry);

        //entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerExit;
        //entry.callback.AddListener((data) => { GlowOff(); });
        //trigger.triggers.Add(entry);

        //if (_isCreate) return;

        //entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;
        //entry.callback.AddListener((data) => { GlowClick(); });
        //trigger.triggers.Add(entry);

        _eventTrigger = _item.GetComponent<EventTriggerSystem>();

        if (_eventTrigger != null)
        {
            _eventTrigger.ActionEnter.AddListener(GlowOn);
            _eventTrigger.ActionExit.AddListener(GlowOff);
            _eventTrigger.ActionClick.AddListener(GlowClick);
        }
    }

    public void GlowOn()
    {
        if (_circleCount == 0)
        {
            _circleCount = _circles.Count;
        }

        //_circleManager.ShowCircles();
    }

    public void GlowOff()
    {
        if (_isClick)
        {
            _isClick = false;
            return;
        }

        //_circleManager.HideCircles();
    }

    public void GlowClick()
    {
        _isClick = true;
    }

    private void OnDestroy()
    {
        _eventTrigger?.ActionEnter?.RemoveAllListeners(); //todo не отписывается от ивента - переделать ивенты?
        _eventTrigger?.ActionExit?.RemoveAllListeners();
        _eventTrigger?.ActionClick?.RemoveAllListeners();
    }
}
