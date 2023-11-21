using _Game._Scripts;
using _Game._Scripts.FolderScript.ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    [Header("Prefab for pool")] [SerializeField]
    private GameObject
        // prefabAction,
        // prefabForm,
        // prefabHierarchy,
        // prefabChildForm,
        prefabIconChildComponentForm,
        prefabGridCell,
        prefabIcon,
        // prefabFolder,
        // prefabDesktop,
        // calendarPrefab,
        // twoVideosPrefab,
        // imagePrefab,
        // videoPrefab,
        // audioPrefab,
        promoOneStrController
        // circle
        ;
    
    // [Header("PoolingObject")] [SerializeField]
    // private PoolingObject actionWindowPool;
    //
    // public PoolingObject ActionWindowPool => actionWindowPool;

    // [SerializeField] private PoolingObject formWindowPool;
    // public PoolingObject FormWindowPool => formWindowPool;

    //
    // [SerializeField] private PoolingObject hierarchyWindowPool;
    // public PoolingObject HierarchyWindowPool => hierarchyWindowPool;

    // [SerializeField] private PoolingObject calendarPool;
    // public PoolingObject CalendarPool => calendarPool;


    // [SerializeField] private PoolingObject childFormWindowPool;
    // public PoolingObject ChildFormWindowPool => childFormWindowPool;

    //
    [SerializeField] private PoolingObject iconChildFormComponentWindowPool;
    public PoolingObject IconChildFormComponentWindowPool => iconChildFormComponentWindowPool;
    //
    [SerializeField] private PoolingObject gridCellPool;
    public PoolingObject GridCellPool => gridCellPool;
    //
    [SerializeField] private PoolingObject iconPool;
    public PoolingObject IconPool => iconPool;
    //
    // [SerializeField] private PoolingObject folderPool;
    // public PoolingObject FolderPool => folderPool;

    // [SerializeField] private PoolingObject desktopPool;
    // public PoolingObject DesktopPool => desktopPool;
    //
    // [SerializeField] private PoolingObject twoVideosPool;
    // public PoolingObject TwoVideosPool => twoVideosPool;
    // [SerializeField] private PoolingObject imagePool;
    // public PoolingObject ImagePool => imagePool;
    //
    // [SerializeField] private PoolingObject videoPool;
    // public PoolingObject VideoPool => videoPool;
    //
    // [SerializeField] PoolingObject audioPool;
    // public PoolingObject AudioPool => audioPool;
    //
    [SerializeField] private PoolingObject promoStr;
    public PoolingObject PromoStr => promoStr;
    //
    // private PoolingObject circlePool;
    //
    // public PoolingObject CirclePool => circlePool;

    private bool isSpawned = false;

    public void CheckRegister()
    {
        if (!isSpawned) SetPool();
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        else
        {
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
    }
    [Button]
    public void SetPool()
    {
        // isSpawned = true;
        // actionWindowPool = new PoolingObject(prefabAction);
        // // formWindowPool = new PoolingObject(prefabForm);
        // hierarchyWindowPool = new PoolingObject(prefabHierarchy);
        // childFormWindowPool = new PoolingObject(prefabChildForm);
        //
        // folderPool = new PoolingObject(prefabFolder);
        //
        // // twoVideosPool = new PoolingObject(twoVideosPrefab);
        //
        iconPool = new PoolingObject(prefabIcon, Config.POOL_FORM_COMPONENT_SIZE_);
        //
        // // desktopPool = new PoolingObject(prefabDesktop);
        //
        gridCellPool = new PoolingObject(prefabGridCell, Config.POOL_GRID_CELL_SIZE);
        //
        // // calendarPool = new PoolingObject(calendarPrefab);
        //
        promoStr = new PoolingObject(promoOneStrController, 100);

        // circlePool = new PoolingObject(circle, 400);
        // imagePool = new PoolingObject(imagePrefab);
        // videoPool = new PoolingObject(videoPrefab);

        // audioPool = new PoolingObject(audioPrefab);
    }
}