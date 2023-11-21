using System;
using System.Collections;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using UnityEngine;

public class GridPrefab : MonoBehaviour, ICreator
{
    #region DefineArea

    [SerializeField] private ManagerJson _managerJson;
    [SerializeField] private GameObject _prefab;

    public GameObject Prefab
    {
        get => _prefab;
        set => _prefab = value;
    }

    [SerializeField] private String needCreateJson;

    public String NeedCreateJson
    {
        get => needCreateJson;
        set => needCreateJson = value;
    }

    [SerializeField] private String pathToJson;

    public string PathToJson
    {
        get => pathToJson;
        set => pathToJson = value;
    }

    [SerializeField] private TopPanelSetParametrs _topPanelSetParametrs;

    public TopPanelSetParametrs TopPanelSetParametrs => _topPanelSetParametrs;

    #endregion

    #region Create

    public GameObject CreateFromPool(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson = true)
    {
        _managerJson = FindObjectOfType<ManagerJson>();
        GameObject desktop = Instantiate(Prefab, _parent);
        if (NeedCreateJson != null)
        {
            desktop.name = NeedCreateJson;
        }



        desktop.GetComponent<ParentChild>().Parent = whoCalls;

        desktop.GetComponent<MainArea>().TopPanelSetParametrs.SetParametrs(desktop, desktop);

   


        return desktop;
    }

    public GameObject Create(GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson = true)
    {
        return Create(null, global::_Game._Scripts.Panel.Prefab.MYTFolder, _icon);
    }

    public GameObject Create(GameObject whoCalls, Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson = true)
    {
        return Create(FindObjectOfType<MainDesktop>().transform, whoCalls, prefab, _icon);
    }

    public GameObject CreateFromPool(GameObject whoCalls, Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson = true)
    {
        return CreateFromPool(FindObjectOfType<MainDesktop>().transform, whoCalls, prefab, _icon);
    }

    public GameObject Create(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,string gameObjectName = null,bool needCreateUserJson = true)
    {
        _managerJson = FindObjectOfType<ManagerJson>();
        GameObject desktop = Instantiate(Prefab, _parent);
        if (NeedCreateJson != null)
        {
            desktop.name = NeedCreateJson;
        }



        desktop.GetComponent<ParentChild>().Parent = whoCalls;

        desktop.GetComponent<MainArea>().TopPanelSetParametrs.SetParametrs(desktop, desktop);

     


        return desktop;
    }

    #endregion

    #region Delete

    public void Delete()
    {
        throw new NotImplementedException();
    }

    #endregion
}