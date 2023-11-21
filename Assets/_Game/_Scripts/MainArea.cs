using _Game._Scripts;
using _Game._Scripts.Panel;
using Project;
using Shapes2D;
using UnityEngine;
using UnityEngine.Serialization;

public class MainArea : Parent, ISetter, IFolderBone
{
    [SerializeField] private SimpleZoom simpleZoom;
    [SerializeField] private GameObject grid;

    public GameObject My
    {
        get => gameObject;
    }

    [SerializeField] private TopPanelSetParametrs topPanelSetParametrs;
    [SerializeField] private IconModel icon;
    [SerializeField] private Transform rotatedArea;

    public Transform RotatedArea
    {
        get => rotatedArea;
        set => rotatedArea = value;
    }

    public IconModel Icon
    {
        get => icon;
        set => icon = value;
    }

    public TopPanelSetParametrs TopPanelSetParametrs
    {
        get => topPanelSetParametrs;
        set => topPanelSetParametrs = value;
    }

    [SerializeField] private bool isOpen;
    [SerializeField] private int index;

    [FormerlySerializedAs("_glowDesktop")] [SerializeField]
    private GetCircleDesktopFromIcon getCircleDesktopFromIcon;

    private bool _firstOpen = true;

    public int Index
    {
        get => index;
        set => index = value;
    }

    public bool IsOpen
    {
        get => isOpen;
        set => isOpen = value;
    }

    [SerializeField] private GameObject buttonOpen;

    public GameObject ButtonOpen => buttonOpen;

    #region Create

    public GameObject Create(GameObject _icon = null, string gameObjectName = null, bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Create(GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        return Create(FindObjectOfType<MainDesktop>().transform, whoCalls, prefab, _icon, gameObjectName,
            needCreateUserJson);
    }

    public GameObject CreateFromPool(GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null,
        bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    public void Delete()
    {
        throw new System.NotImplementedException();
    }

    public GameObject Prefab { get; set; }
    public string NeedCreateJson { get; set; }
    public string PathToJson { get; set; }

    public GameObject Create(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        GameObject desktop = Instantiate(FindObjectOfType<PrefabStock>().CheckPrefab(prefab), _parent);
        desktop.SetActive(false);

        // if (NeedCreateJson != null)
        // {
        //     desktop.name = NeedCreateJson;
        // }

        // ModifyFromToJson modifyFromToJson = desktop.GetComponent<ModifyFromToJson>();
        // MainArea mainArea = desktop.GetComponent<MainArea>();


        desktop.GetComponent<ParentChild>().Parent = whoCalls;

        // mainArea.TopPanelSetParametrs
        //     .SetParametrs(desktop, desktop.GetComponent<MainArea>().ButtonOpen);

        // mainArea.Icon = _icon.GetComponent<IconModel>();
        // mainArea.Icon.GetComponent<GridInIcon>().SetReference();


        // desktop.GetComponent<RectTransform>().FullScreen(true);
        return desktop;
    }

    public GameObject CreateFromPool(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Metods

    public void CloseDesktop()
    {
        Debug.LogError("firstOpen " + _firstOpen);

        if (_firstOpen)
        {
            _firstOpen = false;
            //getCircleDesktopFromIcon.SetGlowParentForOpen();
            (RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.OpenDesktop(gameObject);
            return;
        }

        Debug.LogError("alredyOpen " + isOpen);

        if (isOpen)
        {
            //getCircleDesktopFromIcon.SetGlowParentForClose();
        }
        else
        {
            //getCircleDesktopFromIcon.SetGlowParentForOpen();
        }

        (RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.OpenDesktop(gameObject);
    }

    #endregion

    public void Set(DataForObject dataForObject)
    {
        gameObject.name = dataForObject.nameJson;


        topPanelSetParametrs
            .SetParametrs(gameObject, GetComponent<MainArea>().ButtonOpen);


        foreach (var VARIABLE in dataForObject.nameChilds)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.CreateChild(VARIABLE, this);
        }


        gameObject.SetActive(false);
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
        GetComponent<Inventory>().AddIcon(icon, true);
    }

    public void SetChildFolder(GameObject folder)
    {
        folder.transform.SetParent(rotatedArea, false);
    }

    public void SetFromIcon()
    {
        TopPanelSetParametrs.SetParametrs(gameObject, icon.gameObject);
    }

    public void OnSelect()
    {
        simpleZoom.enabled = true;
    }

    public void OnDeselect()
    {
        simpleZoom.enabled = false;
    }
}