using System;
using System.Collections.Generic;
using _Game._Scripts;
using _Game._Scripts.FolderScript.Name;
using _Game._Scripts.Panel;
using Doozy.Engine.Extensions;
using UnityEngine;
using UnityEngine.UI;


public class CalendarFolder : Parent, ISetter, IFolderBone
{
    private bool isSpawned = false;

    public GameObject My
    {
        get => gameObject;
    }

    [SerializeField] private List<GameObject> myImportantAreas;

    public List<GameObject> MyImportantAreas => myImportantAreas;

    [SerializeField] private GameObject _prefab;


    [SerializeField] private String defaultName;
    [SerializeField] private bool isLock;
    [SerializeField] private List<ShowableArea> allAreas;

    public List<ShowableArea> AllAreas => allAreas;


    public string DefaultName
    {
        get => defaultName;
        set
        {
            SetName(value);
            ChangeName(value);
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

    public int Year = 1900;


    #region Delete

    public void Delete()
    {
        TopPanelSetParametrs.DeleteActionWindow();
        Destroy(icon.gameObject);
        Destroy(gameObject);
    }

    #endregion


    #region SetName

    public void SetName(String _text, bool needUpdate = false)
    {
        defaultName = _text;
        text.text = _text;
        try
        {
            _topPanelSetParametrs.SetNameActionHierarhy(_text);
            if (needUpdate)
            {
                // SetJson(gameObject);
            }
        }
        catch (Exception e)
        {
        }
    }

    public void ChangeName(String _text, bool needUpdate = false)
    {
        defaultName = _text;
        try
        {
            icon.SetName(_text);
            if (needUpdate)
            {
                // SetJson(gameObject);
            }
        }
        catch (Exception e)
        {
//            Debug.LogError(" у объекта с именем "+gameObject.name+" "+e);
        }


        try
        {
            _topPanelSetParametrs.SetNameActionHierarhy(_text);
        }
        catch (Exception e)
        {
        }
    }

    #endregion


    public void Set(DataForObject dataForObject)
    {
        gameObject.SetActive(false);

        gameObject.name = dataForObject.nameJson;


        SetColor(dataForObject.color);


        // foreach (var VARIABLE in parentchild.MYTValue)
        // {
        //     RegisterSingleton._instance.ManagerJson.CreateChildJson(VARIABLE.MYTName, this,
        //         Config.PATH_TO_DIRECTORY_JSON);
        // }

        GetComponent<MytPrefabColorTypes>().LastSelectedColor = Color.blue;


        // throw new NotImplementedException();
    }

    public void Spawner()
    {
        if (isSpawned)
            return;

        isSpawned = true;
        for (int i = 0; i < 90; i++)
        {
            // SpawnChild(RegisterSingleton._instance.ObjectPoolManager.IconPool.GetObjectWithQueue());
        }
    }

    private void SetColor(Color jsonData)
    {
        GetComponent<MytPrefabColorTypes>().ParentBorder.color = jsonData;
        GetComponent<MytPrefabColorTypes>().SetColor();
    }


    public void SetFromIcon()
    {
        _topPanelSetParametrs.SetParametrs(gameObject, icon.gameObject);
    }

    public override void SpawnChild(GameObject child)
    {
        child.GetComponent<Parent>().MyParent = this;
        GridCellGetGridController gridCellGetGridController = GetComponent<GridCellGetGridController>();
        gridCellGetGridController?.Inventory.AddIcon(child,
            gridCellGetGridController.gridControllerInFolderDesktop.IsGrid);

        if (child.GetComponent<PrefabName>().Prefab == _Game._Scripts.Panel.Prefab.MYTIcon60_60)
            SetChildIcon(child);
        else
            SetChildFolder(child);
    }

    public void SetChildIcon(GameObject icon)
    {
        icon.SetActive(true);

        ChangeName(icon.GetComponent<EventName>());

        GetComponent<Inventory>().AddIcon(icon, true);
        icon.GetComponent<RectTransform>().FullScreen(true);
    }

    private void ChangeName(EventName icon)
    {
        icon.name_textField.gameObject.SetActive(true);
        // icon.SetName(ReturnCurrentName());
    }

    private string ReturnCurrentName()
    {
        return (Year++).ToString();
    }

    public void SetChildFolder(GameObject folder)
    {
        folder.transform.SetParent(rotatedArea, false);
    }
}