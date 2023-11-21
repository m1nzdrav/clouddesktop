using System;
using System.Collections.Generic;
using UnityEngine;

public class RegisterSingleton : MonoBehaviour, ISingleton
{
    public static RegisterSingleton _instance;


    public string NameComponent
    {
        get => name;
    }

    public void CheckRegister()
    {
        if (_instance == null)
        {
            _instance = this;
            stock = new List<ISingleton>();
            DontDestroyOnLoad(this);
        }

        if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // private static Singleton<ISingleton> _register;

    private List<ISingleton> stock;
    public List<ISingleton> Stock => stock;

    //
    // #region SingletonPrivate
    //
    // [SerializeField] private SameSequence sequence;
    // [SerializeField] private InitLoader initLoader;
    // [SerializeField] private ManagerJson managerJson;
    // [SerializeField] private DragManager dragManager;
    // [SerializeField] private ObjectPoolManager objectPoolManager;
    // [SerializeField] private HeadRotate headRotate;
    // [SerializeField] private HeadSelectedLoginScene headSelected;
    // [SerializeField] private CustomThread customThread;
    // [SerializeField] private OpenManager openManager;
    // [SerializeField] private ParentRegister parentRegister;
    // [SerializeField] private PrefabStock prefabStock;
    // [SerializeField] private HierarchyGlobal hierarchyGlobal;
    // [SerializeField] private MusicStock musicStock;
    // [SerializeField] private CoroutineServerLoader coroutineServerLoader;
    // [SerializeField] private TextInspector_Singleton textInspectorSingleton;
    // [SerializeField] private ApplicationManager applicationManager;
    // [SerializeField] private TimerToHideCursor timerToHideCursor;
    // [SerializeField] private Factory factory;
    // [SerializeField] private VolumeController volumeController;
    // [SerializeField] private OpenedStock openedStock;
    // [SerializeField] private AudioEqualizer audioEqualizer;
    // [SerializeField] private CursorMoveChecker _moveCursorChecker;
    // [SerializeField] private TextureLoader textureLoader;
    // [SerializeField] private BackgroundSingleton backgroundSingleton;
    // [SerializeField] private BreadcrumbsOnLoad _breadcrumbsOnLoad;
    // [SerializeField] private ActivityFinder activityFinder;
    // [SerializeField] private CookieController cookieController;
    // [SerializeField] private Connector connector;
    // [SerializeField] private User user;
    // [SerializeField] private RegUser regUser;
    // [SerializeField] private LockerScene lockerScene;
    // [SerializeField] private LanguageBundleSaver languageBundleSaver;
    // [SerializeField] private FullSizeHidingShowing fullSizeHidingShowing;
    //
    // #endregion
    //
    // #region SingletonPublic
    //
    // public TextInspector_Singleton TextInspectorSingleton => textInspectorSingleton;
    //
    // public TextureLoader TextureLoader => textureLoader;
    //
    // public MusicStock MusicStock => musicStock;
    //
    // public SameSequence Sequence => sequence;
    //
    // public ManagerJson ManagerJson => managerJson;
    //
    // public DragManager DragManager => dragManager;
    //
    // public ObjectPoolManager ObjectPoolManager => objectPoolManager;
    //
    // public HeadRotate HeadRotate => headRotate;
    //
    // public HierarchyGlobal HierarchyGlobal => hierarchyGlobal;
    //
    // public HeadSelectedLoginScene HeadSelectedLoginScene => headSelected;
    //
    // public CustomThread CustomThread => customThread;
    //
    // public OpenManager OpenManager => openManager;
    //
    // public ParentRegister ParentRegister => parentRegister;
    //
    // public PrefabStock PrefabStock => prefabStock;
    //
    // public ApplicationManager ApplicationManager => applicationManager;
    //
    // public TimerToHideCursor TimerToHideCursor => timerToHideCursor;
    //
    // public Factory Factory => factory;
    //
    // public VolumeController VolumeController => volumeController;
    //
    // public OpenedStock OpenedStock => openedStock;
    //
    // public CoroutineServerLoader CoroutineServerLoader => coroutineServerLoader;
    //
    // public FullSizeHidingShowing FullSizeHidingShowing => fullSizeHidingShowing;
    //
    // public InitLoader InitLoader => initLoader;
    //
    // public AudioEqualizer AudioEqualizer => audioEqualizer;
    //
    // public CursorMoveChecker MoveCursorChecker => _moveCursorChecker;
    //
    // public BackgroundSingleton BackgroundSingleton => backgroundSingleton;
    //
    // public BreadcrumbsOnLoad BreadcrumbsOnLoad => _breadcrumbsOnLoad;
    //
    // public ActivityFinder ActivityFinder => activityFinder;
    //
    // public CookieController CookieController => cookieController;
    //
    // public Connector Connector => connector;
    //
    // public User User => user;
    // public RegUser RegUser => regUser;
    //
    // public LockerScene LockerScene => lockerScene;
    //
    // public LanguageBundleSaver LanguageBundleSaver => languageBundleSaver;
    //
    // #endregion


    private void Awake()
    {
        CheckRegister();
        // if (_register == null) _register = new Singleton<ISingleton>();

        // Register();
    }

    public bool AddNewSingleton(ISingleton singleton)
    {
        if (stock.Exists(x => x.NameComponent == singleton.NameComponent))
        {
            // Debug.LogError(singleton.NameComponent);
            return false;
        }

        stock.Add(singleton);
        // singleton.CheckRegister();

        return true;
    }

    public ISingleton GetSingleton(Type type)
    {
        // Debug.LogError("try get " + type + "\n " + stock.Find(x => x.GetType() == type)?.GetType());
        return stock.Find(x => x.GetType() == type);
    }

    //
    // private void Register()
    // {
    //     _register.Register(this);
    //     _register.Register(_breadcrumbsOnLoad);
    //     _register.Register(connector);
    //     _register.Register(cookieController);
    //     _register.Register(user);
    //     _register.Register(regUser);
    //     _register.Register(lockerScene);
    //
    //     _register.Register(audioEqualizer);
    //     _register.Register(timerToHideCursor);
    //     _register.Register(openedStock);
    //     _register.Register(hierarchyGlobal);
    //     _register.Register(coroutineServerLoader);
    //     _register.Register(customThread);
    //     _register.Register(prefabStock);
    //     _register.Register(fullSizeHidingShowing);
    //     _register.Register(textInspectorSingleton);
    //     _register.Register(_moveCursorChecker);
    //     _register.Register(textureLoader);
    //     _register.Register(languageBundleSaver);
    //     // _register.Register(_factory);
    //
    //     // _register.Register(openedStock);
    //
    //     _register.Register(initLoader);
    //     _register.Register(parentRegister);
    //
    //     _register.Register(objectPoolManager);
    //
    //     _register.Register(openManager);
    //     _register.Register(sequence);
    //
    //     _register.Register(headRotate);
    //     _register.Register(headSelected);
    //
    //     _register.Register(dragManager);
    //
    //
    //     _register.Register(applicationManager);
    //     _register.Register(activityFinder);
    //     _register.Register(backgroundSingleton);
    //
    //
    //     // managerJson.StartOnLoad();
    // }
}