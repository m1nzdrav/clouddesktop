using System;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using UnityEngine;
using UnityEngine.UI;

public class FolderModel : Parent, ILock, ISetter, IFolderBone
{
    public GameObject My
    {
        get => gameObject;
    }

    [SerializeField] private List<GameObject> myImportantAreas;

    public List<GameObject> MyImportantAreas => myImportantAreas;

    [SerializeField] private GameObject _prefab;

    [SerializeField] TextToName textToName;
    [SerializeField] GridControllerInFolderDesktop gridControllerInFolderDesktop;

    [SerializeField] private bool isLock;
    [SerializeField] private List<ShowableArea> allAreas;

    public List<ShowableArea> AllAreas => allAreas;

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

    [SerializeField] private String needCreateJson;
    [SerializeField] private InputField text;
    [SerializeField] private IconModel icon;

    public IconModel Icon
    {
        get => icon;
        set => icon = value;
    }

    public String NeedCreateJson
    {
        get => needCreateJson;
        set => needCreateJson = value;
    }

    public GameObject Prefab
    {
        get => _prefab;
        set => _prefab = value;
    }

    [SerializeField] private String pathToJson;

    public string PathToJson
    {
        get => pathToJson;
        set => pathToJson = value;
    }


    #region Delete

    public void Delete()
    {
        TopPanelSetParametrs.DeleteActionWindow();
        Destroy(icon.gameObject);
        Destroy(gameObject);
    }

    #endregion

    #region SetName

//     public void SetName(String _text, bool needUpdate = false)
//     {
//         defaultName = _text;
//         text.text = _text;
//         try
//         {
//             _topPanelSetParametrs.SetNameActionHierarhy(_text);
//             if (needUpdate)
//             {
//                 // SetJson(gameObject);
//             }
//         }
//         catch (Exception e)
//         {
//         }
//     }
//
//     public void ChangeName(String _text, bool needUpdate = false)
//     {
//         defaultName = _text;
//         try
//         {
//             icon.SetName(_text);
//             if (needUpdate)
//             {
//                 // SetJson(gameObject);
//             }
//         }
//         catch (Exception e)
//         {
// //            Debug.LogError(" у объекта с именем "+gameObject.name+" "+e);
//         }
//
//
//         try
//         {
//             _topPanelSetParametrs.SetNameActionHierarhy(_text);
//         }
//         catch (Exception e)
//         {
//         }
//     }

    #endregion

    #region Lock

    public void Unlock()
    {
        _topPanelSetParametrs.Unlock();
    }

    public void Lock()
    {
        _topPanelSetParametrs.Lock();
    }

    #endregion

    public void Set(DataForObject dataForObject)
    {
        ParentHierarchy.DataForObject = dataForObject;

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

        foreach (var nameChild in nameChilds)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.CreateChild(nameChild, this);
        }
    }

    public void SetFromIcon()
    {
        _topPanelSetParametrs.SetParametrs(gameObject, icon.gameObject);

        if (gridControllerInFolderDesktop != null)
        {
            gridControllerInFolderDesktop.ShowNamespaces = Icon.ShowNamespaces;
            gridControllerInFolderDesktop.onSwitchVisibility +=
                Icon.OnNamespacesVisibilityChange; //TODO не забыть сделать отвязку от события при закрытии
        }

        _topPanelSetParametrs.SetParametrs(gameObject, icon.gameObject);
    }

    public override void SpawnChild(GameObject child)
    {
        child.GetComponent<Parent>().MyParent = this;


        // GridCellGetGridController gridCellGetGridController = GetComponent<GridCellGetGridController>();
        // gridCellGetGridController?.Inventory.AddIcon(child,
        //     gridCellGetGridController.gridControllerInFolderDesktop.IsGrid);

        icon?.SetChild(child);


        if (child.GetComponent<PrefabName>().Prefab == _Game._Scripts.Panel.Prefab.MYTIcon60_60)
        {
            SetChildIcon(child);
            if (gridControllerInFolderDesktop != null)
            {
                gridControllerInFolderDesktop.AddIcon(child,
                    gridControllerInFolderDesktop.IsGrid);
            }
        }
        else
            SetChildFolder(child);
    }

    public void SetChildIcon(GameObject icon)
    {
        SetChild(icon);
        icon.SetActive(true);
    }

    public void SetChildFolder(GameObject folder)
    {
        folder.transform.SetParent(rotatedArea, false);
    }

    public void OnFolderClose()
    {
        // Icon.EventName.onNameChanged -= textToName.OnNameChanged_TextToName;
        // Icon.IconGlowController.Block = false;
    }
}