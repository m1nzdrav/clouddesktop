using System;
using System.Collections.Generic;
using _Game._Scripts;
using Sirenix.Utilities;
using UnityEngine;

public class LockerScene : MonoBehaviour, ISingleton
{
    public string NameComponent
    {
        get => name;
    }

    public void CheckRegister()
    {
        lockers = new List<ContentSceneLocker>();
    }

    private Action<ContentSceneLocker> eventLock;

    [SerializeField] private List<ContentSceneLocker> lockers;

    [SerializeField] private LocalLocker _localLocker;

    public LocalLocker LocalLocker => _localLocker;

    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    public void SubscribeEvent(Action<ContentSceneLocker> newEventLock)
    {
        eventLock = newEventLock;
    }

    public void ChangeLocker(LocalLocker localLocker)
    {
        if (LocalLocker!=null) 
        {
            LocalLocker.gameObject.SetActive(false);
        }
        
        localLocker.gameObject.SetActive(true);
        localLocker.OnEnable();
    }

    public void TryUnlock(ContentSceneLocker _locker)
    {
        if (!lockers.Contains(_locker))
        {
            eventLock.Invoke(_locker);
            lockers.Add(_locker);
        }
        else
        {
            if (CheckLocker(_locker.locker)) _locker.Activate();
            else
                eventLock.Invoke(_locker);
        }
    }


    private bool ChangeStateLocker(ContentSceneLocker contentSceneLocker)
    {
        if (lockers.Contains(contentSceneLocker))
        {
            lockers.Find(x => x == contentSceneLocker).key = true;
            return true;
        }

        Debug.LogError("Нет нужного локера");
        return false;
    }

    public void Unlock()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        CookieController.OnSetCookieClick(_localLocker.CurrentLocker.locker, Config.KEY_TRUE);
#endif
        ChangeStateLocker(_localLocker.CurrentLocker);
        SimpleUnlock();
    }

    public void SimpleUnlock()
    {
        _localLocker.EventUnLocker();
    }

    public void SetLocalLocker(LocalLocker locker)
    {
        _localLocker = locker;
    }

    private bool CheckLocker(string lockerName)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return !CookieController.OnGetCookieClick(lockerName).IsNullOrWhitespace();
#endif
        return lockers.Find(x => x.locker == lockerName).key;
        // return _localLocker.CurrentLocker.key;
    }
}