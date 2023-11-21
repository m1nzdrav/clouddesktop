using System;
using _Game._Scripts.Panel;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateModelPrefab : UpdateJson
{
    [FormerlySerializedAs("prefab")] public GameObject Prefab;
    private ICreator creator;
    [SerializeField] private Transform parent;
    [SerializeField] private bool needPreLastSibling;

    public Transform Parent => parent;

    [SerializeField] private String path;

    [SerializeField] private CircleFactory _circleFactory;

    public string Path
    {
        get => path;
        set => path = value;
    }

    private void Awake()
    {
        try
        {
            creator = Prefab.GetComponent<ICreator>();
        }
        catch (Exception e)
        {
            Debug.LogError("Объект не содержит компонент " + typeof(ICreator));
        }
    }
    public void CreateBtn(Prefab prefabName)
    {
        Create(null, prefabName, path);
    }

    public void CreateBtn()
    {
        Create(null, Prefab.GetComponent<PrefabName>().Prefab, path);
    }

    public GameObject Create(string json, Prefab prefab, string pathToJson = null, string gameObjectName = null, bool needCreateUserJson = true)
    {
        if (creator == null)
        {
            creator = Prefab.GetComponent<ICreator>();
        }

        creator.NeedCreateJson = json;
        if (pathToJson != null)
            creator.PathToJson = pathToJson;

        GameObject child = creator.Create(parent, gameObject, prefab, null, gameObjectName, needCreateUserJson);

        if (needPreLastSibling)
        {
            child.transform.SetSiblingIndex(child.transform.GetSiblingIndex() - 1);
        }

        if (_circleFactory != null)
        {
            Debug.Log(child);
            var circleManager = child.GetComponent<CircleSettings>();
            _circleFactory.AddCircle(circleManager);
            circleManager.CircleFactory = _circleFactory;
        }

        return child;
    }

    public GameObject CreateFromPool(string json, Prefab prefab, string pathToJson = null, string gameObjectName = null, bool needCreateUserJson = true)
    {
        if (creator == null)
        {
            Debug.LogError("awake не сработал");
            creator = Prefab.GetComponent<ICreator>();
        }

        creator.NeedCreateJson = json;

        if (pathToJson != null)
            creator.PathToJson = pathToJson;

        GameObject child = creator.CreateFromPool(parent, gameObject, prefab, null, gameObjectName, needCreateUserJson);

        if (needPreLastSibling)
        {
            child.transform.SetSiblingIndex(child.transform.GetSiblingIndex() - 1);
        }

        return child;
    }
}