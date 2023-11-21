using System;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using UnityEngine;


public class ParentRegister : MonoBehaviour, ISingleton
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
    }
    [SerializeField] private Transform standartParent;
    [SerializeField] private bool isShowable;

    public Transform StandartParent
    {
        get => standartParent;
        set => standartParent = value;
    }

//    private CreateObjFromJson _createObjFromJson;
    private ShowableArea _panelNewParentTransform;
    private Transform _oldParent;
    [SerializeField] private List<ShowableArea> showableAreas;

    public List<ShowableArea> ShowableAreas
    {
        get => showableAreas;
        set => showableAreas = value;
    }

    public ShowableArea PanelTransform
    {
        get => _panelNewParentTransform;
        set { _panelNewParentTransform = value; }
    }


    private void Start()
    {
        DisableAllArea();
    }


    public void EnableAllArea(GameObject _parentForComponent)
    {
        try
        {
            foreach (var VARIABLE in _parentForComponent.GetComponentsInChildren<ShowableArea>())
            {
                VARIABLE.NeedShowing = false;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        foreach (var VARIABLE in showableAreas)
        {
            if (VARIABLE.CanShowing(_parentForComponent))
            {
                VARIABLE.enabled = true;
                VARIABLE.ChangeColor();
            }
        }
    }

    public void DisableAllArea()
    {
        showableAreas.RemoveAll(x => x == null);

        foreach (var VARIABLE in showableAreas)
        {
            VARIABLE.ReturnColor();
            VARIABLE.NeedShowing = true;

            VARIABLE.enabled = false;
        }
    }

    /// <summary>
    /// Смена слота у иконки
    /// </summary>
    /// <param name="newParent">новый слот</param>
    /// <param name="oldParent">старый слот</param>
    /// <param name="child">иконка</param>
    public void SetNewParent(Transform newParent, Transform oldParent, GameObject child)
    {
        //set parent for icon
        child.transform.SetParent(newParent);
        child.transform.localPosition = Vector3.zero;

        IconModel childIconModel = child.GetComponent<IconModel>();
        //set parent for folder
        if (childIconModel.Folder != null)
        {
            childIconModel.Folder.transform
                .SetParent(newParent.GetComponent<GridCell>().MyInventory.transform);
        }


        //Debug.LogError("final" + child);


        childIconModel.SetCellNumberToIcon(child);

        ParentChild newParentChild =
            newParent.GetComponent<GridCell>().MyInventory.GetComponent<ParentChild>();
        ParentChild oldParentChild =
            oldParent.GetComponent<GridCell>().MyInventory.GetComponent<ParentChild>();

        UpdateJson(child, newParentChild, oldParentChild);

        // if (newParentChild.gameObject.name != oldParentChild.gameObject.name)
        // {
        //     newParentChild.AddChild(null, child.GetComponent<IconModel>().Folder.gameObject);
        //     oldParentChild.RemoveChild(child.GetComponent<IconModel>().Folder.gameObject, false, true);
        // }

        childIconModel.SetJson();
    }

    private static void UpdateJson(GameObject child, ParentChild newParentChild, ParentChild oldParentChild)
    {
        if (newParentChild.gameObject.name == oldParentChild.gameObject.name)
            return;


        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetChildWithThread(child.name, newParentChild.gameObject.name,
            newParentChild.GetComponent<PrefabName>().Prefab == Prefab.MYTDesktop);

        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.RemoveChildWithThread(child.name, oldParentChild.gameObject.name,
            oldParentChild.GetComponent<PrefabName>().Prefab == Prefab.MYTDesktop);
    }

    public void DraggbleFolder(Transform _transform)
    {
        Transform icon;
        ParentChild folder;
//        Debug.LogError(_transform.name);
        if (_transform.GetComponent<PrefabName>().Prefab != Prefab.MYTFolder)
        {
            folder = _transform.GetComponent<IconModel>().Folder.GetComponent<ParentChild>();
            icon = _transform;
        }
        else
        {
            folder = _transform.GetComponent<ParentChild>();
            icon = _transform.GetComponent<FolderModel>().Icon.transform;
        }
//        try
//        {
//            
//        }
//        catch (Exception e)
//        {
//            
//        }


        if (PanelTransform.CanShowing(folder.gameObject))
        {
            icon.parent.SetParent(PanelTransform.ChildArea, true);
            folder.ChangeParent(PanelTransform);
//            PanelTransform.GetComponent<ParentChild>().AddChild(null,folder.gameObject);
        }


//        //Запись старого родителя
//        _oldParent = transform.parent;
//        Debug.LogError("Начал присваивать родителя " + transform);
//
//
//        //Сохранение нового родителя
//        transform.parent = PanelTransform;
//        transform.SetAsFirstSibling();
//        Debug.LogError("End Drag " + PanelTransform);
//
//        //Сохранение новых параметров в json
    }
}