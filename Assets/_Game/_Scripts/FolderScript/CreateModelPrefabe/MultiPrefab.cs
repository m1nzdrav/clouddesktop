using System;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using UnityEngine;

public class MultiPrefab : Parent, ILock, ISetter, IFolderBone
{
    [SerializeField] private IconModel icon;

    public IconModel Icon
    {
        get => icon;
        set => icon = value;
    }

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

    [SerializeField] private List<GameObject> myImportantAreas;
    public List<GameObject> MyImportantAreas => myImportantAreas;

    [SerializeField] private MainStart _mainStart;

    public MainStart MainStart => _mainStart;

    [SerializeField] private MultiPrefabInstruction _multiPrefabInstruction;

    public MultiPrefabInstruction MultiPrefabInstruction => _multiPrefabInstruction;

    #region Lock

    public bool IsLock { get; set; }

    public void Lock()
    {
        throw new NotImplementedException();
    }

    public void Unlock()
    {
        throw new NotImplementedException();
    }

    #endregion

    public void Set(DataForObject dataForObject)
    {
        gameObject.SetActive(false);

        gameObject.name = dataForObject.nameJson;

        InstantiateEvent();

        SetColor(dataForObject.color);

        SetChild(dataForObject.nameChilds);
    }

    public void SetFromIcon()
    {
        _topPanelSetParametrs.SetParametrs(gameObject, icon.gameObject);
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

    public override void SpawnChild(GameObject child)
    {
        child.GetComponent<Parent>().MyParent = this;

        icon?.SetChild(child);

        if (child.GetComponent<PrefabName>().Prefab == _Game._Scripts.Panel.Prefab.MYTIcon60_60)
            SetChildIcon(child);
        else
            SetChildFolder(child);
    }

    public void SetChildIcon(GameObject icon)
    {
        SetChild(icon);
        icon.SetActive(true);
        GetComponent<Inventory>().AddIcon(icon, true);
        if (icon.transform.parent.parent.childCount > 0)
        {
            icon.transform.parent.SetSiblingIndex(1);
        }
    }

    public void SetChildFolder(GameObject folder)
    {
        folder.transform.SetParent(rotatedArea, false);
    }
}