using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class OldCreator : MonoBehaviour
{
    [SerializeField] private Parent folderParent;
    [SerializeField] private Parent iconParent;
    [SerializeField] private DataForObject _dataForObject;
    [SerializeField] private IconModel icon;
    [SerializeField] private GameObject folder;
    [SerializeField] private bool needReturnParent = false;
    [SerializeField] private bool needOpen = true;
    [SerializeField] private bool createAfterStart = false;

    private void Start()
    {
        if (createAfterStart) IconCreate();
    }

    [Button]
    public void Create()
    {
        icon.SimpleSet(_dataForObject);
        icon.gameObject.name = _dataForObject.name;
        icon.ParentHierarchy.DataForObject = _dataForObject;
        icon.ParentHierarchy.DataForObject.nameJson = gameObject.name;

        if (needReturnParent)
        {
            iconParent.SpawnChild(icon.gameObject);
        }

        (RegisterSingleton._instance.GetSingleton(typeof(HierarchyGlobal)) as HierarchyGlobal)?.AddChild(icon.ParentHierarchy, new List<int>());
        folder.GetComponent<ISetter>().Set(icon.ParentHierarchy.DataForObject);
        folderParent.SpawnChild(folder);
        icon.SetFolder(folder);

        if (needOpen)
        {
            icon.GetComponent<Button>().onClick.Invoke();
        }
    }

    [Button]
    public void IconCreate()
    {
        icon.SimpleSet(_dataForObject);
        icon.gameObject.name = _dataForObject.name;
        icon.ParentHierarchy.DataForObject = _dataForObject;
        icon.ParentHierarchy.DataForObject.nameJson = icon.gameObject.name;
        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.Test(icon.ParentHierarchy.DataForObject.nameJson, icon.gameObject);
    }
}