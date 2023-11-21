using System.Collections.Generic;
using _Game._Scripts;
using _Game._Scripts.Panel;
using Doozy.Engine.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class MainDesktop : Parent, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    // [SerializeField] private FamilyTree _familyTree;
    public static MainDesktop instance;

    public GameObject My
    {
        get => gameObject;
    }

    [SerializeField] private GameObject grid;

    public void CheckRegister()
    {
        if (instance == null)
        {
            Debug.LogError("Не было MainDesktop");
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(true);
        }
        else
            Destroy(gameObject);
    }


    [SerializeField] private GridLayoutGroup _gridLayout;
    [SerializeField] private List<GameObject> needCloseShow;
    [SerializeField] private Canvas mainCanvas;

    public GridLayoutGroup GridLayout => _gridLayout;

    public void CreateMain()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetNew(Prefab.MYTDesktop,
            this); //todo убрал отсюда путь до локального json (Сейчас такого пути нигде не существует :( )
        // GetComponent<CreateModelPrefab>().Create(null,Prefab.MYTDesktop,Config.PATH_TO_MAIN_JSON);
    }

    [Button]
    public void SetParametrs()
    {

        if (GetComponent<CreateModelPrefab>() != null)
        {
            _gridLayout = GetComponent<CreateModelPrefab>().Parent.GetComponent<GridLayoutGroup>();
        }

        _gridLayout.cellSize =
            new Vector2(Screen.height / Config.PROP_OF_SELL_SIZE / .7f, Screen.height / Config.PROP_OF_SELL_SIZE);

        mainCanvas.worldCamera = Camera.main;

        //mainCanvas.enabled = true;
    }

    public void CloseChild()
    {
        foreach (GameObject child in needCloseShow)
        {
            child.SetActive(false);
        }
    }

    public void ShowChild()
    {
        foreach (GameObject child in needCloseShow)
        {
            child.SetActive(true);
        }
    }


    public override void SpawnChild(GameObject child)
    {
        child.GetComponent<Parent>().MyParent = this;
        child.SetActive(true);
        CreateGrid(child);
        child.GetComponent<RectTransform>().FullScreen(true);
    }

    private void CreateGrid(GameObject child)
    {
        switch (child.GetComponent<PrefabName>().Prefab)
        {
            case Prefab.MYTIcon60_60:
            {
                GameObject _grid = Instantiate(grid,
                    (RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.ParentDesktop.GetComponent<MainDesktop>().GridLayout
                        .transform);

                _grid.transform.SetSiblingIndex(_grid.transform.GetSiblingIndex() - 1);
                // child.GetComponent<IconModel>().Folder.GetComponent<MainArea>().Index = _grid.transform.GetSiblingIndex();
                child.transform.SetParent(_grid.transform);
                break;
            }
            case Prefab.MYTDesktop:
            {
                break;
            }
        }
    }
}