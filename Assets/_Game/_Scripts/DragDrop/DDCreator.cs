using System.Collections;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using UnityEngine;

public class DDCreator : ICreator
{
    public GameObject Prefab { get; set; }
    public string NeedCreateJson { get; set; }
    public string PathToJson { get; set; }

    public GameObject Create(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    public GameObject CreateFromPool(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Create(GameObject _icon = null, string gameObjectName = null, bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Create(GameObject whoCalls, Prefab prefab, GameObject _icon = null, string gameObjectName = null,
        bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    public GameObject CreateFromPool(GameObject whoCalls, Prefab prefab, GameObject _icon = null, string gameObjectName = null,
        bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    public void Delete()
    {
        throw new System.NotImplementedException();
    }
}
