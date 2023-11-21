using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class LockerButton : MonoBehaviour
{
    [SerializeField] private LocalLocker localLocker;
    [SerializeField] private bool isOpen;
    [SerializeField] private LockerActivator lockerActivator;

    [SerializeField, FoldoutGroup("event")]
    private UnityEvent eventLock;

    [SerializeField, FoldoutGroup("event")]
    private UnityEvent eventUnLock;

    public void OpenCloseLocker()
    {
        if (isOpen)
        {
            eventUnLock.Invoke();
            isOpen = false;
        }
        else
        {
            isOpen = true;
            lockerActivator.Lock();
            eventLock.Invoke();
        }
    }
}