using System;
using _Game._Scripts.Panel;
using UnityEngine;


public interface ICreator
{
    GameObject Prefab { get; set; }
    String NeedCreateJson { get; set; }
    String PathToJson { get; set; }
    GameObject Create(Transform _parent, GameObject whoCalls,Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson=true);
    GameObject CreateFromPool(Transform _parent, GameObject whoCalls,Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson=true);
    GameObject Create(GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson=true);
    GameObject Create(GameObject whoCalls,Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson=true);
    GameObject CreateFromPool(GameObject whoCalls,Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson=true);

    void Delete();
}