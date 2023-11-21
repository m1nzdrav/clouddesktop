using UnityEngine;

public class CreateActionHierarchy : MonoBehaviour
{

    [SerializeField] private GameObject _prefabChildForm;

    public GameObject CreateAction()
    {
        // return RegisterSingleton._instance. ObjectPoolManager.ActionWindowPool.GetObject();
        // return Instantiate(_prefabAction, folder.transform);
        return null;
    }

    public GameObject CreateHierarchy()
    {
        // return RegisterSingleton._instance. ObjectPoolManager.HierarchyWindowPool.GetObject();
        // return Instantiate(_prefabHierarchy,folder);
        return null;
    }

    public GameObject CreateForm()
    {
        // return Instantiate(_prefabForm, folder);
        // return RegisterSingleton._instance. ObjectPoolManager.FormWindowPool.GetObject();
        Debug.LogError("need prefab");
        return null;
    }

    public GameObject CreateChildForm()
    {
        // return RegisterSingleton._instance. ObjectPoolManager.ChildFormWindowPool.GetObject();
        Debug.LogError("need prefab");
        return null;
    }
}