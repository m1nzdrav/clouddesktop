using System;
using System.Collections;
using System.Collections.Generic;
using _Game._Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PromoOneStrController : ParagraphController, IPointerEnterHandler,
    IPointerExitHandler
{
    #region ForAnimation

    [Header("Animation"), BoxGroup] public Transform Dot;
    [BoxGroup] public Image DotImage;
    [BoxGroup] public Image LampImage;

    [BoxGroup] public Animation Lamp;
    [BoxGroup] public Image Line;
    [BoxGroup] public Image WhiteSword;
    [BoxGroup] [SerializeField] private GameObject triangleImage;
    [BoxGroup] public GameObject TriangleImage => triangleImage;
    [BoxGroup] [SerializeField] private CanvasGroup _canvasGroup;
    [BoxGroup] public CanvasGroup CanvasGroup => _canvasGroup;

    #endregion

    #region Activity

    [SerializeField] private NextActivitySystem _nextActivitySystem = new NextActivitySystem();

    public NextActivitySystem NextActivitySystem
    {
        get { return _nextActivitySystem; }
    }

    #endregion

    #region NextPage

    [SerializeField] private ContentSetting _contentSetting;

    public ContentSetting ContentSetting
    {
        get => _contentSetting;
        set => _contentSetting = value;
    }

    [SerializeField] private bool startPage;

    public bool StartPage
    {
        get => startPage;
        set => startPage = value;
    }

    [SerializeField] private bool linkedPage;

    public bool LinkedPage
    {
        get => linkedPage;
        set => linkedPage = value;
    }

    [SerializeField] private int[] pages;

    public int[] Pages
    {
        get => pages;
        set => pages = value;
    }

    [SerializeField] private NextPageSystem nextPageSystem;

    public NextPageSystem NextPageSystem => nextPageSystem;

    public void SetNextPageSystem(RectTransform transformParent, DotsController dotsController, Paragraphs paragraphs)
    {
        nextPageSystem = new NextPageSystem(transformParent, dotsController, paragraphs);
    }

    #endregion

    #region LightLamp

    [SerializeField] private LightLampSystem lightLampSystem = new LightLampSystem();

    public LightLampSystem LightLampSystem
    {
        get { return lightLampSystem; }
    }

    [SerializeField] private GlowInPromo glowInPromo;

    public GlowInPromo GlowInPromo
    {
        get => glowInPromo;
        set => glowInPromo = value;
    }

    #endregion

    #region LockerInteractiv

    [SerializeField] private ContentInteractivity contentInteractivity;

    public ContentInteractivity ContentInteractivity
    {
        get => contentInteractivity;
        set => contentInteractivity = value;
    }

    #endregion

    #region ClickAudio

    [SerializeField] private bool needClickAudio;

    public bool NeedClickAudio
    {
        get => needClickAudio;
        set => needClickAudio = value;
    }

    #endregion

    #region ClickAfterPut

    [SerializeField] private bool needClickAfterTextPrinting;

    public bool NeedClickAfterTextPrinting
    {
        get => needClickAfterTextPrinting;
        set => needClickAfterTextPrinting = value;
    }

    #endregion

    #region LockerScene

    [SerializeField] private ContentSceneLocker contentSceneLocker;

    public ContentSceneLocker ContentSceneLocker
    {
        get => contentSceneLocker;
        set => contentSceneLocker = value;
    }

    private Action eventAfterLock;

    #endregion

    #region ContentIcon

    [SerializeField] private ContentIconSystem contentIconSystem = new ContentIconSystem();

    public ContentIconSystem ContentIconSystem
    {
        get { return contentIconSystem; }
    }

    #endregion

    #region Atributes

    [Header("Main")] public RectTransform Rect;
    [SerializeField] private PromoLinks _promoLinks;

    public PromoLinks PromoLinks => _promoLinks;

    [SerializeField] private AudioSource _audioSource;

    public AudioSource AudioSource
    {
        get => _audioSource;
        set => _audioSource = value;
    }

    public ContentSizeFitter ContentSize;
    public CircleSettings CircleSettings;
    public Paragraph Nesting;


    public float StrHeight { get; set; }
    public string SaveText { get; set; }

    public bool IsButton;
    public bool SpecialSettings;

    public Content currentContent;

    [SerializeField] private PromoOneStrController _firstStr;

    public PromoOneStrController FirstStr
    {
        get => _firstStr;
        set => _firstStr = value;
    }

    [SerializeField] private SettingsForLoginScene _settingsForLoginScene;

    public SettingsForLoginScene SettingsForLoginScene
    {
        get => _settingsForLoginScene;
        set => _settingsForLoginScene = value;
    }

    public bool Active = true;
    public bool ParagraphIsOpen;
    public OpenIconContainer IconContainer;
    public DropdownCodeCountry DropdownCode;
    public InputText InputText;

    private bool _sword = true;

    [SerializeField] private ChildContainer _childContainer;
    [SerializeField] private Vector3 upScale;

    public ChildContainer ChildContainer
    {
        get => _childContainer;
        set => _childContainer = value;
    }

    private List<PromoOneStrController> _childs;

    private AnimationStateChanger animationStateChanger;

    #endregion

    private void Start()
    {
        // gameObject.GetComponent<EventTrigger>().AddEventTrigger(OnPointerClick, EventTriggerType.PointerClick);
        // Dot.GetComponent<EventTrigger>().AddEventTrigger(OnPointerClick, EventTriggerType.PointerClick);
    }

    [Button]
    public void SetLineSettings()
    {
        var lineRect = Line.GetComponent<RectTransform>();

        lineRect.pivot = new Vector2(0.5f, 0.5f);

        var width = Rect.rect.width;
        var lineSize = lineRect.sizeDelta;
        lineSize.x = width + 250;
        lineRect.sizeDelta = lineSize;

        var pos = lineRect.anchoredPosition;
        pos.x = -lineSize.x / 2;
        lineRect.anchoredPosition = pos;

        lineRect.pivot = new Vector2(0f, 0.5f);
    }

    public void BtnClick()
    {
        OnPointerClick(null);
    }

    private void OnPointerClick(BaseEventData eventData)
    {
        if (_canvasGroup != null && _canvasGroup.interactable)
            Dot.transform.DOScale(Vector3.one * 2, .3f)
                .OnComplete((() => Dot.transform.DOScale(Vector3.one, .3f)));

        if (contentSceneLocker == null || contentSceneLocker.locker.IsNullOrWhitespace())
            EventClick();
        else
            contentSceneLocker.CheckLocker(EventClick);
    }

    private void EventClick()
    {
        if (TryGetComponent(out NextPageSubs result)) result.InvokeClick();

        if (TryGetComponent(out NextOpenContents openContents)) openContents.InvokeClick();

        if (needClickAudio) AudioSource.Play();

        _childContainer.InvokeClick();
        glowInPromo?.StopGlow();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_canvasGroup != null && _canvasGroup.interactable)
        {
            triangleImage.SetActive(false);
            Lamp.transform.DOScale(Config.VECTOR_ANIMATION_LAMP_ENTER, .3f)
                // .OnComplete((() => Lamp.transform.DOScale(Vector3.zero, .3f)))
                ;

            if (!WhiteSword.gameObject.activeSelf) return;

            WhiteSword.DOFillAmount(1, 1f);
            Line.DOFillAmount(1, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_canvasGroup == null || !_canvasGroup.interactable) return;

        Lamp.transform.DOKill();

        if (WhiteSword.gameObject.activeSelf)
        {
            WhiteSword.DOFillAmount(0, 1f);
            Line.DOFillAmount(0, 1);
        }

        triangleImage.gameObject.SetActive(true);
        Lamp.transform.DOScale(Vector3.zero, .3f);
    }

    public void OpenCloseButton(bool open)
    {
        var paragraphIsOpenFirstStr = FirstStr.ParagraphIsOpen;

        if (open)
        {
            if (!paragraphIsOpenFirstStr)
            {
                OnPointerClick(null);
            }
        }
        else
        {
            if (paragraphIsOpenFirstStr)
            {
                OnPointerClick(null);
            }
        }
    }

    [SerializeField] private AnimationState currentState;

    [Button]
    public void TestShowChild()
    {
        animationStateChanger = new AnimationStateChanger();

        currentState = _childs[0].GetComponent<AnimationOpen>();
        animationStateChanger.AddStateToList(currentState);

        int startParagraphs = 1; // самый первый мы добавили ранее, т.к она может не быть клик анимацией


        for (var index = startParagraphs; index < _childs.Count; index++)
        {
            PromoOneStrController VARIABLE = _childs[index];
            if (VARIABLE.currentContent.animationPointJson.AnimationOpenType != AnimationOpenType.OnClick)
            {
                currentState.AddNewChildStates(VARIABLE.GetComponent<AnimationOpen>());
            }
            else
            {
                currentState = VARIABLE.GetComponent<AnimationOpen>();
                animationStateChanger.AddStateToList(currentState);
            }
        }


        animationStateChanger.StartState(null);
    }

    public void Destroy()
    {
        _childContainer.Destroy();

        Destroy(gameObject);
    }

    public void EndPutText()
    {
        NextActivitySystem.TryNext(false, false);
        lightLampSystem.ActivateLamp();
        CheckLocker();

        if (!contentInteractivity.key.IsNullOrWhitespace())
            _contentSetting.ActivateInteractiveForKey(contentInteractivity.key);
        
        if (needClickAfterTextPrinting && TryGetComponent(out NextOpenContents openContents)) openContents.InvokeClick();

        if (startPage) _contentSetting.CurrentPage(pages);

        if (TryGetComponent(out NextPageSubs result)) result.StartSubscribe();
    }

    public void FastEndPutText()
    {
        //NextActivitySystem.TryNext(false, false);
        lightLampSystem.ActivateLamp();
        CheckLocker();
        glowInPromo?.FastStartGlow();
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;

        if (startPage ) _contentSetting.FastCurrentPage(pages);

        if (TryGetComponent(out NextPageSubs result)) result.StartSubscribe();
    }

    private void CheckLocker()
    {
        if (!contentInteractivity.locker.IsNullOrWhitespace())
        {
            //todo вщзможно тут присваивать нажатие, возможно передавать в локер
            _childContainer.SubscribeEvent(Dot, currentContent.needGong);
        }
        else
        {
            glowInPromo?.StartGlow();
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }
    }

    public void ActivateTextLocker(string key)
    {
        if (key != contentInteractivity.locker) return;

        _childContainer.SubscribeEvent(Dot, currentContent.needGong);
        glowInPromo?.StartGlow();
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    public void FastActivateTextLocker(string key)
    {
        if (key != contentInteractivity.locker) return;

        _childContainer.SubscribeEvent(Dot, currentContent.needGong);
        // glowInPromo?.StartGlow();
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    private void OnEnable()
    {
        if (contentIconSystem.containerIcon != null)
            contentIconSystem.TrySpawnIcon(transform, _canvasGroup);

        // _canvasGroup.blocksRaycasts = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
    }
}