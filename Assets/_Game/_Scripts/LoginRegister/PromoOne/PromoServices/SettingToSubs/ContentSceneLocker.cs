using System;

[Serializable]
public class ContentSceneLocker
{
    public string locker;
    public bool key = false;

    public bool unlock;
    
    private Action action;

    public void SetLockerSetting(PromoOneStrController item, Content content)
    {
        if (content.contentSceneLocker == null) return;

        item.ContentSceneLocker = content.contentSceneLocker;
    }

    public void CheckLocker(Action callback)
    {
        if (unlock)
            callback.Invoke();
        else
        {
            action = callback;
            (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.TryUnlock(this);
        }
    }

    public void Activate()
    {
        unlock = true;
        action?.Invoke();
    }
}