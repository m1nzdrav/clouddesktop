using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game._Scripts.FolderScript.Name;
using _Game._Scripts.JsonScript.ParentChild.NumberHierarchy;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using Doozy.Engine.Extensions;
using Shapes2D;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class IconModel : Parent, ISetter
{
    public Prefab MainPrefab => mainPrefab;
    [Header("MainData"), SerializeField] private Prefab mainPrefab;

    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject folder;

    public InputField Text
    {
        get => text;
    }

    [SerializeField] private InputField text;

    [SerializeField] private EventName _eventName;

    public EventName EventName
    {
        get => _eventName;
    }

    string currentName;

    public string CurrentName
    {
        get => currentName;
        set => currentName = value;
    }

    [SerializeField] private GameObject _prefab;

    [SerializeField] private int _gridCellNumber;
    [SerializeField] private GameObject _prefabSlot;
    [SerializeField] private Border border;
    [SerializeField] private ChildBorder _statsChildBorder;
    private bool firstOpen;
    [SerializeField] private bool isOpen = false;
    private string sourcePath = "";

    public string SourcePath
    {
        get => sourcePath;
    }

    [SerializeField] private RawImage iconImage;

    //private Texture iconImage_texture;

    public Texture IconImage_Texture
    {
        get => iconImage.texture;

        set
        {
            if (value == null) return;
            
            iconImage.texture = value;
            iconImage.enabled = true;
            GetComponent<Image>().enabled = false;
            iconImage.color = Color.white;
        }
    }

    // private String needCreateJson;//нигде не использовалось...

    bool showNamespaces = true;

    public bool ShowNamespaces
    {
        get => showNamespaces;
    }


    public int GridCellNumber
    {
        get => _gridCellNumber;
        set => _gridCellNumber = value;
    }

    public GameObject Folder
    {
        get => folder;
        set => folder = value;
    }

    public GameObject Prefab
    {
        get => _prefab;
        set => _prefab = value;
    }

    [SerializeField] IconGlowController iconGlowController;

    public IconGlowController IconGlowController
    {
        get => iconGlowController;
    }

    private String needCreateJson;

    public void SetGridCellNumber(GameObject icon)
    {
        var grid = icon.transform.parent.parent;

        for (int i = 0; i < grid.childCount; i++)
        {
            var cell = grid.GetChild(i);


            if (cell.name == _gridCellNumber.ToString())
            {
                icon.transform.SetParent(cell);
                icon.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    public void SetCellNumberToIcon(GameObject icon)
    {
        var cell = icon.transform.parent;
        int.TryParse(cell.name, out var cellName);
        _gridCellNumber = cellName;
    }

    public void SetIconToCell()
    {
        if (_gridCellNumber == 0)
        {
            return;
        }

        GridControllerInFolderDesktop gridController =
            folder.GetComponent<ParentChild>().Parent.GetComponent<GridControllerInFolderDesktop>();
        List<GameObject> cells = gridController.Cells;
        int cellCount = cells.Count;

        if (_gridCellNumber > cellCount)
        {
            int needCells = _gridCellNumber - cellCount;
            RectTransform gridContainer = gridController.GridContainer;

            for (int i = 0; i < needCells; i++)
            {
                GameObject newCell = Instantiate(_prefabSlot, gridContainer.transform);
                newCell.transform.localScale = transform.parent.localScale;
                newCell.name = (cellCount + i).ToString();
                cells.Add(newCell.gameObject);
            }
        }

        transform.SetParent(cells[_gridCellNumber - 1].transform);
        transform.localPosition = Vector3.zero;
        gridController.ResizeWindow();
    }

    public void SetJson()
    {
        // SetJson(folder.gameObject);

        // var iconFolder = folder.GetComponent<ParentChild>().Parent;
        // iconFolder.GetComponent<GridControllerInFolderDesktop>().OpenFile();
    }


    #region Create

    public GameObject Create(GameObject _icon = null, string gameObjectName = null, bool needCreateUserJson = true)
    {
        return Create(null, _Game._Scripts.Panel.Prefab.MYTFolder, _icon, gameObjectName, needCreateUserJson);
    }

    public GameObject Create(GameObject whoCalls, Prefab prefab, GameObject _icon = null, string gameObjectName = null,
        bool needCreateUserJson = true)
    {
        return Create(FindObjectOfType<MainIconArea>().transform, whoCalls, prefab, _icon, gameObjectName,
            needCreateUserJson);
    }

    public GameObject CreateFromPool(GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        return CreateFromPool(FindObjectOfType<MainIconArea>().transform, whoCalls, prefab, _icon, gameObjectName,
            needCreateUserJson);
    }


    public GameObject Create(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        GameObject icon =
            Instantiate(
                (RegisterSingleton._instance.GetSingleton(typeof(PrefabStock)) as PrefabStock)?.CheckPrefab(_Game._Scripts.Panel.Prefab.MYTIcon60_60));

        GridCellGetGridController gridCellGetGridController = _parent.GetComponent<GridCellGetGridController>();

        var isGrid = false;

        if (gridCellGetGridController != null)
        {
            isGrid = gridCellGetGridController.gridControllerInFolderDesktop.IsGrid;
        }

        gridCellGetGridController?.Inventory.AddIcon(icon, isGrid);


        ShowHideObjectsScaleAndMove iconShowHideObjectsScaleAndMove = icon.GetComponent<ShowHideObjectsScaleAndMove>();
        IconModel iconModel = icon.GetComponent<IconModel>();


        if (prefab == _Game._Scripts.Panel.Prefab.MYTDesktop)
        {
            // iconModel.folder = _needCreateObjDesktop.Create(whoCalls, prefab, icon, gameObjectName, needCreateUserJson)
            //     .GetComponent<FolderModel>();

            // CreateGrid(iconModel.gameObject);
            // iconModel.folder.transform.SetParent(iconModel.transform.parent);
            //
            // iconModel.folder.GetComponent<RectTransform>().FullScreen(true);
            //октрытие рабочего стола, через метод открытия папки
            // iconModel.GetComponent<Button>().onClick.AddListener(((MainArea) iconModel.folder).CloseDesktop);

            // iconModel.GetComponent<GridInIcon>().UpdateGridInChild();
            //iconModel.GetComponent<IconGlowController>().InactiveGlow();

            iconModel.border.gameObject.SetActive(true);

            // Color color = iconModel.folder.GetComponent<MainArea>().RotatedArea.GetComponent<Image>().color;

            iconModel._statsChildBorder.enabled = true;
            // iconModel._statsChildBorder.SetColor(color);
            //
            // iconModel.border.SetColor(color);
            iconModel.GetComponent<WaitPress>().enabled = false;

            iconModel.GetComponent<Shape>().settings.outlineSize = 1;
            iconModel.GetComponent<Shape>().settings.outlineColor = Color.white;
            // iconModel.GetComponent<Image>().color=
        }
        else
        {
//             iconModel.folder =
//                 _needCreateObj.Create(whoCalls, prefab, icon, gameObjectName, needCreateUserJson)
//                     .GetComponent<FolderModel>();
//             iconShowHideObjectsScaleAndMove.IsShowed = false;
//             iconShowHideObjectsScaleAndMove.ObjectWithPositions = new List<ObjectWithPosition>()
//             {
//                 new ObjectWithPosition
//                 {
// //            currentPosition = iconModel.folder.transform.position,
//                     obj = iconModel.folder.gameObject
//                 }
//             };
        }


        ShowableArea iconShowableArea = icon.GetComponent<ShowableArea>();
        ShowableArea folderShowableArea = iconModel.folder.GetComponent<ShowableArea>();

        iconShowableArea.CantDropInMe.Add(folderShowableArea);

        iconShowableArea.ChildArea = folderShowableArea.ChildArea;


        iconModel.SetCellNumberToIcon(icon);

        iconModel.GetComponent<GridControllerInIcon>().UpdateGrid();

        return icon;
    }

    public GameObject CreateFromPool(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null,
        string gameObjectName = null, bool needCreateUserJson = true)
    {
        GameObject icon = (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.IconChildFormComponentWindowPool.GetObject();
        icon.SetActive(true);
        GridCellGetGridController gridCellGetGridController = _parent.GetComponent<GridCellGetGridController>();

        var isGrid = false;

        if (gridCellGetGridController.gridControllerInFolderDesktop != null)
        {
            isGrid = gridCellGetGridController.gridControllerInFolderDesktop.IsGrid;
        }

        gridCellGetGridController.Inventory.AddIcon(icon, isGrid);


        ShowHideObjectsScaleAndMove iconShowHideObjectsScaleAndMove = icon.GetComponent<ShowHideObjectsScaleAndMove>();
        IconModel iconModel = icon.GetComponent<IconModel>();

        // iconModel.folder =
        //     _needCreateObj.CreateFromPool(whoCalls, prefab, icon, gameObjectName).GetComponent<FolderModel>();

        iconShowHideObjectsScaleAndMove.IsShowed = false;
        iconShowHideObjectsScaleAndMove.ObjectWithPositions = new List<ObjectWithPosition>()
        {
            new ObjectWithPosition
            {
//            currentPosition = iconModel.folder.transform.position,
                obj = iconModel.folder.gameObject
            }
        };

        ShowableArea iconShowableArea = icon.GetComponent<ShowableArea>();
        ShowableArea folderShowableArea = iconModel.folder.GetComponent<ShowableArea>();

        iconShowableArea.CantDropInMe.Add(folderShowableArea);

        iconShowableArea.ChildArea = folderShowableArea.ChildArea;

        iconModel.SetCellNumberToIcon(icon);

        return icon;
    }

    private void CreateGrid(GameObject icon)
    {
        GameObject _grid = Instantiate(grid,
            (RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.ParentDesktop.GetComponent<MainDesktop>().GridLayout.transform);


        _grid.transform.SetSiblingIndex(_grid.transform.GetSiblingIndex() - 1);
        icon.GetComponent<IconModel>().Folder.GetComponent<MainArea>().Index = _grid.transform.GetSiblingIndex();
        icon.transform.SetParent(_grid.transform);
        icon.GetComponent<RectTransform>().FullScreen(true);
    }

    #endregion

    #region Delete

    public void Delete()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region SetName

    void OnNameChanged_IconModel(string name)
    {
        currentName = name;
    }

    public void ChangeName(string text)
    {
        // folder.SetName(text, true);
    }

    public void SetName(String _text)
    {
        text.SetTextWithoutNotify(_text);
    }

    public void OnNamespacesVisibilityChange(bool showNamespaces)
    {
        this.showNamespaces = showNamespaces;
    }

    #endregion

    //расчитано на 2 префаба в json
    public void Set(DataForObject dataForObject)
    {
        SimpleSet(dataForObject);
        // GetComponent<Button>().onClick.AddListener(CreateAfterClick);
    }

    public void SimpleSet(DataForObject dataForObject)
    {
        mainPrefab = dataForObject.mainPrefab;
        InstantiateEvent();
        SetColor(dataForObject);


        // SetName(jsonData);
        // CreateChild(jsonData.MYTValue.Find(x => x.MYTName == "MYTParentChild"));

        GetComponent<PrefabName>().SetName();
        IconImage_Texture = dataForObject.texture;
        sourcePath = dataForObject.sourcePath;
    }


    private void SetColor(DataForObject dataForObject)
    {
        Color newColor = dataForObject.color;
        GetComponent<Image>().color = newColor;
        _statsChildBorder?.SetColor(newColor);
        border?.SetColor(newColor);
    }

    private void CreateChild(IdNameValue jsonParentChild)
    {
        List<GameObject> childs = new List<GameObject>();
        foreach (var VARIABLE in jsonParentChild.MYTValue.Find(x => x.MYTName == "MYTChild").MYTValue)
        {
            // childs.Add(RegisterSingleton._instance.ManagerJson.CreateChildJson(VARIABLE.MYTName, this));
        }
    }

    private bool flagOpen = false;

    [Button]
    public void CreateAfterClick()
    {
        if (flagOpen)
            return;

        flagOpen = true;
        switch (mainPrefab)
        {
            case global::_Game._Scripts.Panel.Prefab.MYTDesktop:
            {
                SetMYTDesktop();
                break;
            }
            case global::_Game._Scripts.Panel.Prefab.MYTCalendar:
            {
                // StartCoroutine(RotateSIbtitles(SetMYTCalendar));

                break;
            }
            case global::_Game._Scripts.Panel.Prefab.MYTImage:
            {
                SetMYT_Model(ImageSetup);
                break;
            }
            case global::_Game._Scripts.Panel.Prefab.MYTVideo:
                SetMYT_Model(VideoSetup);
                break;
            default:
            {
                // StartCoroutine(RotateSIbtitles(SetMYTFolder));
                // SetMYT_Model(SetMYTFolder);
                SetMYTFolder();
                break;
            }
        }

        firstOpen = true;
    }

    private async void SetMYT_Model(Action action)
    {
        var query = (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetNormal(gameObject.name, MyParent);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        folder = query.Result;
        folder.SetActive(true);
        action.Invoke();

        ShowHideObjectsScaleAndMove iconShowHideObjectsScaleAndMove =
            GetComponent<ShowHideObjectsScaleAndMove>();

        iconShowHideObjectsScaleAndMove.IsShowed = false;

        iconShowHideObjectsScaleAndMove.ObjectWithPositions = new List<ObjectWithPosition>()
        {
            new ObjectWithPosition
            {
                obj = folder.gameObject,
                // currentPosition = new Vector3(0, 0, 100)
            }
        };
        folder.GetComponent<NumberHierarchy>().ChangeNumber();

        GetComponent<Button>().onClick.AddListener(() => TurnOpen());
        // TurnOpen();
        GetComponent<Button>().onClick.Invoke();
    }

    void VideoSetup()
    {
        folder.GetComponent<VideoModel>().Icon = this;
        folder.GetComponent<VideoModel>().SetFromIcon();
    }

    void ImageSetup()
    {
        folder.GetComponent<ImageModel>().Icon = this;
        folder.GetComponent<ImageModel>().SetFromIcon();
    }

    void FolderSetup()
    {
        folder.GetComponent<FolderModel>().Icon = this;
        folder.GetComponent<FolderModel>().SetFromIcon();
    }

    private async void SetMYTFolder()
    {
        var query = (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetNormal(gameObject.name, MyParent);
        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        folder = query.Result;
        folder.SetActive(true);
        folder.GetComponent<FolderModel>().Icon = this;
        folder.GetComponent<FolderModel>().SetFromIcon();

        //todo убрать 
        folder.transform.localScale *= 3;
        //todo 
        // folder.transform.position =
        //     new Vector3(folder.transform.position.x, folder.transform.position.y, 100);

        ShowHideObjectsScaleAndMove iconShowHideObjectsScaleAndMove =
            GetComponent<ShowHideObjectsScaleAndMove>();

        iconShowHideObjectsScaleAndMove.IsShowed = false;

        iconShowHideObjectsScaleAndMove.ObjectWithPositions = new List<ObjectWithPosition>()
        {
            new ObjectWithPosition
            {
                obj = folder.gameObject,
                // currentPosition = new Vector3(0, 0, 100)
            }
        };
        folder.GetComponent<NumberHierarchy>().ChangeNumber();

        //
        // GetComponent<Button>().onClick.AddListener(TurnOpen);
        // GetComponent<Button>().onClick.Invoke();
    }

    public void SetFolder(GameObject _folder)
    {
        folder = _folder;
        folder.SetActive(true);
        folder.GetComponent<IFolderBone>().Icon = this;
        folder.GetComponent<IFolderBone>().SetFromIcon();

        //todo убрать 
        folder.transform.localScale *= 3;
        //todo 
        // folder.transform.position =
        //     new Vector3(folder.transform.position.x, folder.transform.position.y, 100);

        ShowHideObjectsScaleAndMove iconShowHideObjectsScaleAndMove =
            GetComponent<ShowHideObjectsScaleAndMove>();

        iconShowHideObjectsScaleAndMove.IsShowed = false;

        iconShowHideObjectsScaleAndMove.ObjectWithPositions = new List<ObjectWithPosition>()
        {
            new ObjectWithPosition
            {
                obj = folder.gameObject,
                // currentPosition = new Vector3(0, 0, 100)
            }
        };
        folder.GetComponent<NumberHierarchy>().ChangeNumber();
    }

    private void SetMYTCalendar()
    {
        GetComponent<Button>().onClick.RemoveListener(CreateAfterClick);

        folder = (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.GetNormal(gameObject.name, MyParent);


        folder.SetActive(true);
        folder.GetComponent<IFolderBone>().Icon = this;
        folder.GetComponent<CalendarFolder>().SetFromIcon();

        //todo убрать 
        folder.transform.localScale *= 3;
        //todo 
        // folder.transform.position =
        //     new Vector3(folder.transform.position.x, folder.transform.position.y, 100);

        ShowHideObjectsScaleAndMove iconShowHideObjectsScaleAndMove =
            GetComponent<ShowHideObjectsScaleAndMove>();

        iconShowHideObjectsScaleAndMove.IsShowed = false;

        iconShowHideObjectsScaleAndMove.ObjectWithPositions = new List<ObjectWithPosition>()
        {
            new ObjectWithPosition
            {
                obj = folder.gameObject,
                // currentPosition = new Vector3(0, 0, 100)
            }
        };
        // folder.GetComponent<NumberHierarchy>().ChangeNumber();

        // Action newAction = () =>
        //     StartCoroutine(RotateSIbtitles(GetComponent<Button>().onClick.Invoke, TurnOpen));

        // GetComponent<Button>().onClick.RemoveAllListeners();
        // GetComponent<Button>().interactable = false;
        //
        // Button test = gameObject.AddComponent<Button>();
        // test.onClick.AddListener(newAction.Invoke);
        // GetComponent<Button>()  .onClick.AddListener(newAction.Invoke);
        GetComponent<Button>().onClick.AddListener(TurnOpen);
        GetComponent<Button>().onClick.Invoke();
    }

    private async void SetMYTDesktop()
    {
        GetComponent<Button>().onClick.RemoveListener(CreateAfterClick);
        var query =(RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetMain(gameObject.name, MainDesktop.instance);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }


        folder = (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.lastObject;
        folder.SetActive(true);
        folder.GetComponent<MainArea>().Icon = this;
        folder.transform.SetParent(transform.parent);

        GetComponent<Button>().onClick.AddListener((folder.GetComponent<MainArea>()).CloseDesktop);

        (folder.GetComponent<MainArea>()).CloseDesktop();
    }

    public void TurnOpen()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(HeadSelectedLoginScene)) as HeadSelectedLoginScene)?.CheckSelectedObj(folder.GetComponent<SelectedWindow>());
            (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.SetOpenedPrefab(folder.GetComponent<PrefabName>());
        }
        else
            (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.RemoveOpenedPrefab(folder.GetComponent<PrefabName>());
    }


    public override void SpawnChild(GameObject child)
    {
        // SetChild(child);
        child.GetComponent<Parent>().MyParent = this;
    }
}