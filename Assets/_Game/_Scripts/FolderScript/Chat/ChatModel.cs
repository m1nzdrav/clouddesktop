using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatModel : Parent, ILock, ISetter, IFolderBone
{
    #region Lock

    [SerializeField] private bool isLock;

    public bool IsLock
    {
        get => isLock;
        set
        {
            isLock = value;
            if (value)
            {
                Lock();
            }
            else
            {
                Unlock();
            }
        }
    }

    public void Lock()
    {
        throw new System.NotImplementedException();
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region ExtendedAtributs

    [SerializeField] private TopPanelSetParametrs _topPanelSetParametrs;

    public TopPanelSetParametrs TopPanelSetParametrs
    {
        get => _topPanelSetParametrs;
        set => _topPanelSetParametrs = value;
    }

    [SerializeField] private Transform rotatedArea;

    public Transform RotatedArea
    {
        get => rotatedArea;
        set => rotatedArea = value;
    }

    [SerializeField] private IconModel icon;

    public IconModel Icon
    {
        get => icon;
        set => icon = value;
    }

    #endregion

    #region ExtendedMetods

    public void Set(DataForObject dataForObject)
    {
        gameObject.SetActive(false);

        gameObject.name = dataForObject.nameJson;

        InstantiateEvent();

        SetColor(dataForObject.color);

        SetChild(dataForObject.nameChilds);
    }

    private void SetColor(Color jsonData)
    {
        GetComponent<MytPrefabColorTypes>().ParentBorder.color = jsonData;
        GetComponent<MytPrefabColorTypes>().SetColor();
    }

    private void SetChild(List<string> nameChilds)
    {
        foreach (var VARIABLE in nameChilds)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetMain(VARIABLE, this);
        }
    }

    public void SetFromIcon()
    {
        _topPanelSetParametrs.SetParametrs(gameObject, icon.gameObject);
    }

    public override void SpawnChild(GameObject child)
    {
        throw new System.NotImplementedException();
    }

    #endregion

 
}