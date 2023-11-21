using System.Collections;
using _Game._Scripts;
using _Game._Scripts.Events.StartEnd;
using UnityEngine;


public class TimerToHideCursor : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
        First();
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        else
        {
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
    }
    private Coroutine co_HideCursor;

    private FirstSecondEvent _firstSecondEvent;

    private SubscribeFirstSecond _subscribeFirstSecond;

    public FirstSecondEvent FirstSecondEvent => _firstSecondEvent;

    public SubscribeFirstSecond SubscribeFirstSecond => _subscribeFirstSecond;


    private void First()
    {
        _firstSecondEvent = gameObject.AddComponent<FirstSecondEvent>();
        _firstSecondEvent.InstantiateEvent();
        _subscribeFirstSecond = gameObject.AddComponent<SubscribeFirstSecond>();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") == 0 && (Input.GetAxis("Mouse Y") == 0))
        {
            if (co_HideCursor == null)
            {
                co_HideCursor = StartCoroutine(HideCursor());
            }
        }
        else
        {
            StopHide();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StopHide();
        }
    }

    private void StopHide()
    {
        if (co_HideCursor != null)
        {
            StopCoroutine(co_HideCursor);
            co_HideCursor = null;
            _firstSecondEvent.FirstEvent.Raise();
            Cursor.visible = true;
        }
    }

    private IEnumerator HideCursor()
    {
        yield return new WaitForSeconds(Config.DELAY_FOR_HIDE_CURSOR);
        _firstSecondEvent.SecondEvent.Raise();
        Cursor.visible = false;
    }

    public void Hide()
    {
        Debug.LogError("try");
        Cursor.visible = false;
    }
}