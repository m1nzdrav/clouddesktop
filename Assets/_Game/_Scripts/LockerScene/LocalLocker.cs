 using System;
using System.Collections;
using _Game._Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class LocalLocker : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent eventLock;

    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent eventUnLock;

    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent eventSpawn;

    [SerializeField] private ContentSceneLocker currentLocker;

    public ContentSceneLocker CurrentLocker => currentLocker;

    [SerializeField] private MainChat _mainChat;

    [SerializeField] private bool spawnAfterClose = true;
    [SerializeField] private float waitLockerSpawn = 5f;

    public void OnEnable()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.SetLocalLocker(this);
        (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.SubscribeEvent(EventLocker);
    }

    // private void Start()
    // {
    //     (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.SetLocalLocker(this);
    //     (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.SubscribeEvent(EventLocker);
    // }

    private void EventLocker(ContentSceneLocker nameLocker)
    {
        eventLock.Invoke();
        currentLocker = nameLocker;

        if (spawnAfterClose)
            StartCoroutine(AwaitForDialog(nameLocker.locker));
    }

    public void AfterLockBtn()
    {
        eventSpawn.Invoke();
        StartCoroutine(AwaitForDialog(currentLocker.locker));
    }

    public void EventUnLocker()
    {
        eventUnLock.Invoke();
    }

    IEnumerator AwaitForDialog(string nameLocker)
    {
        yield return new WaitForSeconds(waitLockerSpawn);
        _mainChat.StartDialog(nameLocker);
    }
}