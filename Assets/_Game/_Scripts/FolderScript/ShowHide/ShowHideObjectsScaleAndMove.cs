using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game._Scripts;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class ShowHideObjectsScaleAndMove : UpdateJson, IShowHide
{
    [SerializeField, FoldoutGroup("Show Hide event")]
    private UnityEvent _show, _hide;

    [SerializeField] private IconGlowController _iconGlowController;

    [SerializeField] private bool needBtnSubscribe = true;
    [SerializeField] private bool _isShowed = true;
    [SerializeField] private bool needShow;
    [SerializeField] private bool isAccess = true;

    //todo сделать листом и в интерфейсе тоже

    [SerializeField] private Vector3 currentPositionForJson;

    [SerializeField] private List<ObjectWithPosition> _objectWithPositions = new List<ObjectWithPosition>();

    [SerializeField] private float hideAnimationDuration = 0.5f;

    private WaitForSeconds timeDelay = new WaitForSeconds(Config.DELAY_FOR_SHOWING);

    [SerializeField] private float _delayTime = 0.7f;

    public bool IsAccess
    {
        get => isAccess;
        set => isAccess = value;
    }

    public bool NeedShow
    {
        get => needShow;
        set => needShow = value;
    }

    public Vector3 CurrentPositionForJson
    {
        get => currentPositionForJson;
        set => currentPositionForJson = value;
    }

    public List<ObjectWithPosition> ObjectWithPositions
    {
        get => _objectWithPositions;
        set => _objectWithPositions = value;
    }

    public float HideAnimationDuration
    {
        get => hideAnimationDuration;
        set => hideAnimationDuration = value;
    }

    public float DelayTime
    {
        get => _delayTime;
        set => _delayTime = value;
    }

    public bool IsShowed
    {
        get => _isShowed;
        set => _isShowed = value;
    }

    private void Awake()
    {
        foreach (var VARIABLE in _objectWithPositions) VARIABLE.currentPosition = new Vector3(0 + 200, 0, 0);

        if (needBtnSubscribe)
            GetComponent<Button>().onClick.AddListener(ShowHideObject);
    }

    public void ShowHideObject()
    {
        if (isAccess && _objectWithPositions.Count > 0)
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.interactable = false;
            }

            if (IsShowed && _objectWithPositions[0].obj.gameObject.activeSelf)
            {
                //GetComponent<IconModel>()?.Folder.GetComponent<CircleSettings>().HideCircles();
                GetComponent<CircleSettings>()?.CircleFactory
                    ?.HideCircle(
                        GetComponent<CircleSettings>()); //todo высчитывать количетсво настроек и задавать вместо "2"

                _iconGlowController?.Glow.SetActive(false);

                HideObject();

                (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceHide
                    .PlaySequence(() => { GetComponent<CanvasGroup>().interactable = true; });
            }
            else
            {
                ShowObject(true);

                GetComponent<CircleSettings>()?.CircleFactory
                    ?.ShowCircle(
                        GetComponent<CircleSettings>()); //todo высчитывать количетсво настроек и задавать вместо "2"
                //GetComponent<IconModel>()?.Folder.GetComponent<CircleSettings>().ShowCircles();

                _iconGlowController?.Glow.SetActive(true);

                (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow
                    .PlaySequence(() =>
                    {
                        // SetJson(_objectWithPositions[0].obj);
                        GetComponent<CanvasGroup>().interactable = true;
                    });
            }


            StartCoroutine(DelayPress());
        }
    }

    public void ShowObject(bool isButton = false)
    {
        //        Debug.LogError("OpenChild");
        Show(isButton);

        // Invoke("ShowChildFolder", HideAnimationDuration);

        ShowChildFolder();

        foreach (var objectWithPosition in _objectWithPositions)
        {
            var gridController = objectWithPosition.obj.GetComponent<GridControllerInFolderDesktop>();
            if (gridController != null)
            {
                gridController.Start();
            }
        }
    }

    public void HideObject()
    {
        if (_objectWithPositions[0].obj.GetComponentsInChildren<IShowHide>().Length != 0)
        {
            HideChildFolder();
            Hide();
        }

        else
        {
            Hide();
        }
    }

    public void Show(bool isButton = false)
    {
        _isShowed = true;
        StopAllCoroutines();
        _show?.Invoke();
        foreach (var VARIABLE in _objectWithPositions)
        {
            VARIABLE.obj.transform.localScale = Vector3.zero;
            VARIABLE.obj.SetActive(true);
            VARIABLE.obj.transform.SetAsLastSibling();

            VARIABLE.obj.transform.position = transform.position;

            if (!CheckEqualsVector3(VARIABLE.obj.transform.position, transform.position))
            {
                if (VARIABLE.obj.transform.localPosition != Vector3.zero)
                    VARIABLE.currentPosition = VARIABLE.obj.transform.localPosition;
            }
            else if (VARIABLE.currentPosition == Vector3.zero)
            {
                // VARIABLE.currentPosition = new Vector3(transform.position.x + 80, transform.position.y - 80, 0);
            }
            else
            {
                //            Debug.LogError("Оставил старое значение");
            }

            if (currentPositionForJson != Vector3.zero)
            {
                // Debug.LogError("Присвоил позицию из json");
                VARIABLE.currentPosition = CurrentPositionForJson;
            }

// Debug.LogError(RegisterSingleton._instance.Sequence.sequenceShow._same);
// Debug.LogError(VARIABLE.obj.gameObject);
            (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same
                .Append(VARIABLE.obj.transform.DOLocalMove(VARIABLE.currentPosition,
                    hideAnimationDuration))
                .Join(VARIABLE.obj.transform.DOScale(Vector3.one, HideAnimationDuration)
                    .OnComplete(() =>
                    {
                        if (isButton)
                            return;
                        SetJson(VARIABLE.obj);
                    })).Join(VARIABLE.obj.GetComponent<CanvasGroup>().DOFade(1, hideAnimationDuration));


            // VARIABLE.obj.transform.DOLocalMove(VARIABLE.currentPosition,
            //     hideAnimationDuration);
            // VARIABLE.obj.transform.DOScale(Vector3.one, HideAnimationDuration)
            //     .OnComplete(() => SetJson(VARIABLE.obj))
            //     ;
        }
    }

    private static bool CheckEqualsVector3(Vector3 first, Vector3 second)
    {
        return first.x.ToString("f").Equals(second.x.ToString("f")) && first.y.ToString("f")
                                                                        .Equals(second.y.ToString("f"))
                                                                    && first.z.ToString("f")
                                                                        .Equals(second.z.ToString("f"));
    }

    [Button]
    public void Hide()
    {
        if (!gameObject.activeInHierarchy) return;
        _hide?.Invoke();

        foreach (var VARIABLE in _objectWithPositions)
        {
            VARIABLE.currentPosition = VARIABLE.obj.transform.localPosition;
            CurrentPositionForJson = VARIABLE.currentPosition;
            _isShowed = false;
            (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceHide._same
                .Append(VARIABLE.obj.transform.DOMove(transform.position, hideAnimationDuration))
                .Join(VARIABLE.obj.transform.DOScale(Vector3.zero, hideAnimationDuration)
                    .OnComplete(() => DeactivateObj(VARIABLE)))
                .Join(VARIABLE.obj.GetComponent<CanvasGroup>().DOFade(0, hideAnimationDuration));
        }

        // SameSequence.Instance.sequenceHide._same.PrependInterval(hideAnimationDuration);
        // DeactivateObj();
        // StartCoroutine(HideAfterTime());
    }

    public float HideChildFolder()
    {
        //        foreach (var VARIABLE in _objectWithPositions.obj.GetComponentsInChildren<IShowHide>())
        foreach (IShowHide child in _objectWithPositions.Select(objects => objects.obj.GetComponent<ParentChild>())
                     .Where(parentChild => parentChild != null).SelectMany(parentChild =>
                         from VARIABLE in parentChild.Child
                         where VARIABLE != null
                         select VARIABLE.GetComponent<FolderModel>().Icon.GetComponent<IShowHide>()
                         into child
                         where child.IsShowed
                         select child))
        {
            child.NeedShow = true;
            child.HideObject();
        }


        return HideAnimationDuration;
    }

    IEnumerator test()
    {
        yield return HideChildFolder();
        Hide();
    }

    public float ShowChildFolder()
    {
        //        Debug.LogError("try ShowChild");
        //        foreach (var VARIABLE in _objectWithPositions.obj.GetComponentsInChildren<IShowHide>())
        //            Debug.LogError(_objectWithPositions.obj.GetComponent<ParentChild>().Child.Count);

        ParentChild parenChild = _objectWithPositions[0].obj.GetComponent<ParentChild>();

        if (parenChild == null || parenChild.Child.Count == 0) return HideAnimationDuration;

        _objectWithPositions[0].obj.GetComponent<ParentChild>().Child.RemoveAll(x => x == null);

        foreach (var child in _objectWithPositions[0].obj.GetComponent<ParentChild>().Child
                     .Select(VARIABLE => VARIABLE.GetComponent<FolderModel>().Icon.GetComponent<IShowHide>())
                     .Where(child => child.NeedShow || child.IsShowed))
        {
            child.ShowObject();
            child.NeedShow = false;
        }

        // StartCoroutine(ShowObjectCorotine());
        return HideAnimationDuration;
    }

    IEnumerator ShowObjectCorotine()
    {
        yield return null;

        if (_objectWithPositions[0].obj.GetComponent<ParentChild>() != null)
        {
            foreach (var VARIABLE in _objectWithPositions[0].obj.GetComponent<ParentChild>().Child)
            {
                IShowHide child = VARIABLE.GetComponent<FolderModel>().Icon.GetComponent<IShowHide>();
                if (child.NeedShow || child.IsShowed)
                {
                    yield return timeDelay;

                    child.ShowObject();
                    child.NeedShow = false;
                }
            }
        }
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(HideAnimationDuration);

        Debug.LogError(_objectWithPositions[0].obj.transform.position + " " +
                       _objectWithPositions[0].obj.activeInHierarchy + " " + transform.position + " " +
                       gameObject.activeSelf + " " + gameObject.activeInHierarchy);

        DeactivateAllObj();
    }

    private void DeactivateAllObj()
    {
        foreach (var VARIABLE in _objectWithPositions)
        {
            VARIABLE.obj.transform.position = transform.position;
            SetJson(VARIABLE.obj);
            VARIABLE.obj.SetActive(false);
        }
    }

    private void DeactivateObj(ObjectWithPosition obj)
    {
        obj.obj.transform.position = transform.position;
        SetJson(obj.obj);
        obj.obj.SetActive(false);
    }

    private IEnumerator DelayPress()
    {
        GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(DelayTime);
        GetComponent<Button>().interactable = true;
        try
        {
            //            FindObjectOfType<ManagerJson>().UpdateJson(_objectWithPositions.obj);
        }
        catch (Exception e)
        {
            Debug.LogError("Нельзя обновить Json при закрытии");
        }
    }
}