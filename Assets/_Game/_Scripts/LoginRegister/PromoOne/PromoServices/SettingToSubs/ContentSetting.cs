using System.Collections.Generic;
using _Game._Scripts;
// using Jint.Runtime.Debugger;
using Sirenix.OdinInspector;
using UnityEngine;


public class ContentSetting : MonoBehaviour
{
    [Header("Flag and prefab")] [SerializeField]
    private PromoOneStrController _itemText;

    private BookSystem _testNextMoreSystem;
    [SerializeField] private Transform _parentList;
    [SerializeField] private Transform _platformContainer;
    [SerializeField] private Transform _socialContainer;
    [SerializeField] private bool _isArabic;
    [Header("System")] private Documents _subOnLoad;
    [SerializeField] private DotsController _dotAnimation;
    [SerializeField] private CircleFactory _circleFactory;

    private List<PromoOneStrController> lockedStr;

    public List<PromoOneStrController> LockedStr
    {
        get => lockedStr;
    }

    [SerializeField] private ContentSetting linkedContent;
    public ContentSetting LinkedContent => linkedContent;

    [SerializeField] private NextPageSystem nextPageSystem;
    public NextPageSystem NextPageSystem => nextPageSystem;

    private ContentButton _contentButton;
    private ContentText _contentText;
    private ContentAnimation _contentAnimation;
    private ContentAudio _contentAudio;
    private ContentActivity _contentActivity;
    private ContentLightLamp _contentLightLamp;
    private ContentIcon _contentIcon;
    private ContentGlow _contentGlow;
    private ContentInteractivity _contentInteractivity;
    private ContentSceneLocker _contentSceneLocker;

    [SerializeField] private int current = 0;
    [SerializeField] private Documents _documents;
    [SerializeField] private SpawnedPages spawnedPage;

    private void Awake()
    {
        _testNextMoreSystem = new BookSystem();
        _testNextMoreSystem.Book.Add(new Paragraphs());
        lockedStr = new List<PromoOneStrController>();
        _contentButton = new ContentButton();
        _contentText = new ContentText();
        _contentAnimation = new ContentAnimation();
        _contentAudio = new ContentAudio();
        _contentActivity = new ContentActivity();
        _contentLightLamp = new ContentLightLamp();
        _contentIcon = new ContentIcon(_socialContainer, _platformContainer);
        _contentGlow = new ContentGlow();
        _contentInteractivity = new ContentInteractivity();
        _contentSceneLocker = new ContentSceneLocker();
    }

    private Paragraph GetParagraphs(Page page)
    {
        Paragraph paragraph = new Paragraph(page.Contents.Count);
        foreach (Content content in page.Contents) GetParagraph(content, paragraph);

        return paragraph;
    }

    private PromoOneStrController GetParagraph(Content content, Paragraph paragraph)
    {
        current++;
        PromoOneStrController poolStrController =
            (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.PromoStr.GetObjectWithQueue()
                .GetComponent<PromoOneStrController>();
        poolStrController.transform.SetParent(_parentList, false);
        // PromoOneStrController strController = Instantiate(_itemText, _parentList); // создание и вставка в конец
        poolStrController.transform.SetAsLastSibling();
        poolStrController.currentContent = content;
        paragraph.Paragraphs.Add(poolStrController);
        Setting(content, poolStrController);
        AddCircles(poolStrController, content);
        AddComment(poolStrController, content, paragraph);
        AddChild(content, poolStrController);

        // if (content.childContents.Count > 0)
        //     foreach (Content VARIABLE in content.childContents) // добавление детей
        //         GetParagraph(VARIABLE, strController.ChildContainer.Childs);

        //todo когда вернем документы - расскоментить
        // if (content.documents.StartPages!=null)
        // {
        //     AddPages(content, strController);
        // }

        return poolStrController;
    }

    // private void AddPages(Content content, PromoOneStrController strController)
    // {
    //     Paragraphs temp = new Paragraphs();
    //     temp.Pages.Add(new Paragraph());
    //     strController.SetNextPageSystem(_parentList.GetComponent<RectTransform>(), _dotAnimation, temp);
    //
    //     foreach (var VARIABLE in content.documents.Pages[0].Contents)
    //     {
    //         GetParagraph(VARIABLE, temp.Pages[0]);
    //     }
    //
    //     strController.gameObject.AddComponent<NextPageSubs>()
    //         .SetPage(strController.NextPageSystem, content.documents.StartPages);
    //
    // }

    private void AddComment(PromoOneStrController strController, Content content, Paragraph paragraph)
    {
        if (content.commentContents == null || content.commentContents.Count <= 0) return;

        strController.ChildContainer.InitComment(content.commentContents.Count);

        foreach (Content VARIABLE in content.commentContents)
        {
            // добавление комментов
            paragraph.Paragraphs.Add(GetParagraph(VARIABLE, paragraph));
            strController.ChildContainer.Comments.Paragraphs.Add(paragraph.Paragraphs[paragraph.Paragraphs.Count - 1]);
        }
    }


    private void AddChild(Content content, PromoOneStrController strController)
    {
        if (content.childContents == null || content.childContents.Count <= 0) return;

        strController.ChildContainer.InitChild(content.childContents.Count);

        foreach (Content VARIABLE in content.childContents) // добавление детей
            GetParagraph(VARIABLE, strController.ChildContainer.Childs);
    }

    private void Setting(Content content, PromoOneStrController strController)
    {
        strController.ContentSetting = this;
        _contentText.SetTextSettings(strController, content, _isArabic); // вставка параметров текста
        _contentButton.GetSettingGetCurrentString(content, strController,
            this); // проверка на кнопкуи выставление параметров
        _contentAnimation.SetAnimationSetting(strController, content);
        _contentAudio.SetMusicSetting(strController, content);
        _contentActivity.SetActivitySetting(strController, content);
        _contentLightLamp.SetLightLampSetting(strController, content);
        _contentIcon.SetContentIconSetting(strController, content);
        _contentGlow.SetGlowSetting(strController, content);
        _contentInteractivity.SetInteractivitySetting(strController, content, this);
        _contentSceneLocker.SetLockerSetting(strController, content);
    }

    private void AddCircles(PromoOneStrController strController, Content content)
    {
        if (content.Circles.Circle == null)
            return;

        strController.CircleSettings.CirclesSettings = content.Circles.Circle;
        _circleFactory.AddCircle(strController.CircleSettings);
        strController.CircleSettings.CircleFactory = _circleFactory;
    }

    public void ClearSubs()
    {
        if (_testNextMoreSystem.Book.Count <= 0) return;
        foreach (Paragraph paragraph in _testNextMoreSystem.Book[0].Pages)
        {
            paragraph.Paragraphs.ForEach(x => x?.Destroy());
        }

        _testNextMoreSystem.Book[0].Pages.Clear();
    }

    public void FastInst(bool isArabic, string JsonString)
    {
        
        spawnedPage = new SpawnedPages();
        _isArabic = isArabic;
        _subOnLoad = Config.LoadJson<Documents>(JsonString);
        ClearSubs();

        foreach (Page page in _subOnLoad.Pages)
            _testNextMoreSystem.Book[0].Pages.Add(GetParagraphs(page));

        nextPageSystem = new NextPageSystem(_parentList.GetComponent<RectTransform>(), _dotAnimation,
            _testNextMoreSystem.Book[0]);
        nextPageSystem.StartFastSpawnAnimation(_subOnLoad.StartPages);
    }

    public void Inst(bool isArabic, string JsonString)
    {
        _isArabic = isArabic;
        spawnedPage = new SpawnedPages();
        
// #if !UNITY_WEBGL
//          if (_documents == null)
// #endif
        _subOnLoad = Config.LoadJson<Documents>(JsonString);

// #if !UNITY_WEBGL
//         else
//         _subOnLoad = _documents;
// #endif
        ClearSubs();

        foreach (Page page in _subOnLoad.Pages)
            _testNextMoreSystem.Book[0].Pages.Add(GetParagraphs(page));

        nextPageSystem = new NextPageSystem(_parentList.GetComponent<RectTransform>(), _dotAnimation,
            _testNextMoreSystem.Book[0]);
        nextPageSystem.StartSpawnAnimation(_subOnLoad.StartPages);
    }

    [Button]
    public void CurrentPage(int[] pagesToSpawn)
    {
        spawnedPage.AddPages(pagesToSpawn);
        
        nextPageSystem.StartSpawnAnimation(pagesToSpawn);
    }

    public void FastCurrentPage(int[] pagesToSpawn)
    {
        if (!spawnedPage.AddPages(pagesToSpawn))
            return;
        
        nextPageSystem.StartFastSpawnAnimation(pagesToSpawn);
    }

    public void ActivateInteractiveForKey(string key)
    {
        foreach (PromoOneStrController VARIABLE in lockedStr)
        {
            VARIABLE.ActivateTextLocker(key);
        }
    }

    public void FastActivateInteractiveForKey(string key)
    {
        foreach (PromoOneStrController VARIABLE in lockedStr)
        {
            VARIABLE.FastActivateTextLocker(key);
        }
    }
    
    
}