using _Game._Scripts.Panel;
using UnityEngine;


public class Factory : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }
    private static float countCreation = 0;

    public static float GetCountCreation()
    {
        return countCreation++;
    }

    public static GameObject CreateObj(Prefab prefabType)
    {
        return CheckPrefabType(prefabType);
    }

    public static GameObject InstantiateGameObject(GameObject _prefabe, Transform _transform)
    {
        return Instantiate(_prefabe, _transform);
    }

    private static GameObject CheckPrefabType(Prefab prefab)
    {
        switch (prefab)
        {
            case Prefab.MYTIcon60_60:
                return (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.IconPool.GetObjectWithQueue();
            // case Prefab.MYTDesktop:
            //     return RegisterSingleton._instance.ObjectPoolManager.DesktopPool.GetObjectWithQueue();
            // case Prefab.MYTCalendar:
            //     return RegisterSingleton._instance.ObjectPoolManager.CalendarPool.GetObjectWithQueue();
            // case Prefab.TwoVideos1:
            //     return RegisterSingleton._instance.ObjectPoolManager.TwoVideosPool.GetObjectWithQueue();
            // case Prefab.MYTImage:
            //     return RegisterSingleton._instance.ObjectPoolManager.ImagePool.GetObjectWithQueue();
            // case Prefab.MYTVideo:
            //     return RegisterSingleton._instance.ObjectPoolManager.VideoPool.GetObjectWithQueue();
            default:
                // return RegisterSingleton._instance.ObjectPoolManager.FolderPool.GetObjectWithQueue();
                return null;
        }
    }
}