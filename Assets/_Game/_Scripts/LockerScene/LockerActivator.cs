using Sirenix.OdinInspector;
using UnityEngine;

public class LockerActivator : MonoBehaviour
{
    [SerializeField] private LocalLocker mainLocker;

    [SerializeField] private ContentSceneLocker contentSceneLocker;

    [Button]
    public void Lock()
    {
        if (mainLocker != null)
            (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.ChangeLocker(mainLocker);

        (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.TryUnlock(contentSceneLocker);
    }
    [Button]
    public void UnLock()
    {
        if (mainLocker != null)
            (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.ChangeLocker(mainLocker);

        (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.SimpleUnlock();
    }
}