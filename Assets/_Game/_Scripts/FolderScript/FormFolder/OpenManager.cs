using System.Collections.Generic;
using _Game._Scripts.Panel;
using DG.Tweening;
using Doozy.Engine.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class OpenManager : MonoBehaviour, ISingleton
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
    [SerializeField] private RotatePlanes sibtitles;

    public RotatePlanes Sibtitles => sibtitles;

    [SerializeField] private Transform parentDesktop;

    public Transform ParentDesktop => parentDesktop;

    [SerializeField] private float _animDuration = .5f;

    [SerializeField] private float currentFade = .085f;


    [SerializeField] private ManagerJson _managerJson;

    [SerializeField] private MainArea currentArea;

    [SerializeField] private RectTransform _rectTransformCurrentArea;

    public RectTransform RectTransformCurrentArea
    {
        get => _rectTransformCurrentArea;
        set => _rectTransformCurrentArea = value;
    }

    [SerializeField] private BottomPanelSetParent _bottomPanel;


    public MainArea CurrentArea => currentArea;

    public void OpenDesktop(GameObject desktop)
    {
        bool isOpen = desktop.GetComponent<MainArea>().IsOpen;

        if (isOpen && currentArea != null)
        {
            _bottomPanel.SetPrew();

            currentArea = null;
            _rectTransformCurrentArea = null;
            SmallSize(desktop);
        }
        else
        {
            currentArea = desktop.GetComponent<MainArea>();
            _rectTransformCurrentArea = desktop.GetComponent<RectTransform>();

            desktop.SetActive(true);

            currentArea.IsOpen = true;
            currentArea.Index = desktop.transform.parent.GetSiblingIndex();

            SetFade(1, desktop);


            desktop.transform.SetParent(parentDesktop, false);

            _rectTransformCurrentArea.Center(true);
            // _rectTransformCurrentArea.localScale = new Vector3(.5f,.5f);
            desktop.SetActive(true);
            // desktop.transform.parent = parentDesktop;

            // StartAnimationOpen(desktop);
            StartAnimationOpen(desktop);
        }
    }

    [Button]
    private void StartAnimationOpen(GameObject desktop)
    {
        desktop.GetComponent<FadeArea>().OpenArea(null);
        _rectTransformCurrentArea.DOLocalMove(Vector3.zero, _animDuration);
        _rectTransformCurrentArea
            .DOSizeDelta(new Vector2(Screen.width, Screen.height), _animDuration)
            .OnComplete(() => { CompleteTween(desktop); });
    }

    private void SetFade(float fade, GameObject desktop)
    {
        Image image = desktop.GetComponent<IFolderBone>().RotatedArea.GetComponent<Image>();
        currentFade = image.color.a;
        image.DOFade(fade, _animDuration);
    }

    private void CompleteTween(GameObject desktop)
    {
        parentDesktop.GetComponent<MainDesktop>().CloseChild();


        _rectTransformCurrentArea.Center(true);
        _rectTransformCurrentArea.FullScreen(false);

        desktop.transform.localPosition = Vector3.zero;

        // foreach (var VARIABLE in desktop.GetComponent<ParentChild>().Child)
        // {
        //     var icon = VARIABLE.GetComponent<FolderModel>().Icon;
        //     IShowHide showHide = icon.GetComponent<IShowHide>();
        //     if (showHide.IsShowed)
        //     {
        //         //                icon.NeedShow = true;
        //         showHide.ShowHideObject	();
        //         icon.GetComponent<IconGlowController>().TurnOff();
        //     }
        // //     IShowHide icon = VARIABLE.GetComponent<FolderModel>().Icon.GetComponent<IShowHide>();
        // //
        // //     if (icon.NeedShow)
        // //     {
        // //         icon.NeedShow = false;
        // //         icon.ShowObject();
        // //     }
        // }
        // desktop.GetComponent<CanvasGroup>().blocksRaycasts = true;
        desktop.GetComponent<MainArea>().ButtonOpen.SetActive(false);

        // ManagerJson.instance.UpdateJson(desktop);

        _bottomPanel.SetParent();

        currentArea.GetComponent<GridControllerInFolderDesktop>().OpenFile();
    }

    public void SmallSize(GameObject desktop)
    {
        RectTransform desktopRectTransform = desktop.GetComponent<RectTransform>();
        MainArea desktopMainArea = desktop.GetComponent<MainArea>();
        desktopMainArea.TopPanelSetParametrs.BtnRotate.GetComponent<ButtonVideoRotate>().HideWithVhangePosition();
        parentDesktop.GetComponent<MainDesktop>().ShowChild();

        SetFade(0, desktop);

        desktopMainArea.IsOpen = false;

        GameObject grid = parentDesktop.GetComponent<MainDesktop>().GridLayout.transform
            .GetChild(desktopMainArea.Index).gameObject;

        desktop.GetComponent<FadeArea>().CloseArea(null);
        SetupAnim(desktop, desktopRectTransform, grid, desktopMainArea);
    }

    private void SetupAnim(GameObject desktop, RectTransform desktopRectTransform, GameObject grid,
        MainArea desktopMainArea)
    {
        desktop.transform.SetParent(grid.transform, true);
        // desktop.transform.parent =grid.transform ;
        desktopRectTransform.Center(true);
        desktopRectTransform.sizeDelta =
            new Vector2(Screen.width * 2, Screen.height * 2); //размер сетки в 2 раза меньше


        // foreach (var VARIABLE in desktop.GetComponent<ParentChild>().Child)
        // {
        //     var icon = VARIABLE.GetComponent<FolderModel>().Icon;
        //     IShowHide showHide = icon.GetComponent<IShowHide>();
        //     if (showHide.IsShowed)
        //     {
        //         showHide.NeedShow = true;
        //         showHide.ShowHideObject	();
        //         icon.GetComponent<IconGlowController>().TurnOff();
        //     }
        //     //     IShowHide icon = VARIABLE.GetComponent<FolderModel>().Icon.GetComponent<IShowHide>();
        //     //
        //     //     if (icon.NeedShow)
        //     //     {
        //     //         icon.NeedShow = false;
        //     //         icon.ShowObject();
        //     //     }
        // }
        desktopRectTransform.DOMove(grid.transform.position, _animDuration);
        desktopRectTransform
            .DOSizeDelta(grid.GetComponent<RectTransform>().sizeDelta, _animDuration)
            .OnComplete(() =>
            {
                desktopRectTransform.FullScreen(true);


                // desktop.GetComponent<CanvasGroup>().blocksRaycasts = false;

                desktopMainArea.ButtonOpen.SetActive(true);

                (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.UpdateJson(desktop);

                desktop.SetActive(false);

                if (currentArea == null) return;

                currentArea.GetComponent<GridControllerInFolderDesktop>().IsAutosize = false;
            });
    }
}