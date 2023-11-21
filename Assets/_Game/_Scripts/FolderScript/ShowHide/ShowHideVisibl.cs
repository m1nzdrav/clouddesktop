using System.Collections.Generic;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideVisibl : MonoBehaviour, IShowHide
{
    [SerializeField] private List<ObjectWithPosition> _objectWithPositions = new List<ObjectWithPosition>();

    public List<ObjectWithPosition> ObjectWithPositions
    {
        get => _objectWithPositions;
        set => _objectWithPositions = value;
    }

    [SerializeField] private bool _isShowed = true;
    [SerializeField] private float hideAnimationDuration;
    [SerializeField] private bool needShow;

    public bool IsShowed
    {
        get => _isShowed;
        set => _isShowed = value;
    }

    public bool NeedShow
    {
        get => needShow;
        set => needShow = value;
    }

    public float HideAnimationDuration
    {
        get => hideAnimationDuration;
        set => hideAnimationDuration = value;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowHideObject);
    }

    public void ShowHideObject()
    {
        if (IsShowed)
        {
            HideObject();

            (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceHide.PlaySequence(() =>
            {
                Debug.LogError("hide");
                foreach (var VARIABLE in _objectWithPositions)
                {
                    VARIABLE.obj.transform.transform.localScale = Vector3.zero;

                    LayoutRebuilder.ForceRebuildLayoutImmediate(
                        VARIABLE.obj.transform.parent.GetComponent<RectTransform>());
                }

                // GetComponent<CanvasGroup>().interactable = true;
                // Debug.LogError("interactable true " + gameObject.name);
            });
        }
        else
        {
            ShowObject();

            (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow.PlaySequence(() => { });
        }
    }

    public void Show(bool isButton = false)
    {
//        Debug.LogError("test");
        _isShowed = true;
        foreach (var VARIABLE in _objectWithPositions)
        {
            if (VARIABLE.obj != null)
            {
                VARIABLE.obj.transform.transform.localScale = Vector3.one;

                LayoutRebuilder.ForceRebuildLayoutImmediate(
                    VARIABLE.obj.transform.parent.GetComponent<RectTransform>());

                CanvasGroup canvasGroup = VARIABLE.obj.GetComponent<CanvasGroup>();

//                Debug.LogError("ShowKill");
                (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same
                    .Append(canvasGroup.DOFade(1, hideAnimationDuration));
                // canvasGroup.DOKill();
                // canvasGroup.DOFade(1, hideAnimationDuration * 4);
            }
        }
    }

    public void Hide()
    {
        _isShowed = false;
        foreach (var VARIABLE in _objectWithPositions)
        {
            if (VARIABLE.obj != null)
//                VARIABLE.obj.SetActive(false);
            {
                CanvasGroup canvasGroup = VARIABLE.obj.GetComponent<CanvasGroup>();

//                Debug.LogError("HideKill");
                (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceHide._same
                    .Append(canvasGroup.DOFade(0, hideAnimationDuration)).OnComplete(() => { });


//                 canvasGroup.DOKill();
//                 canvasGroup.DOFade(0, hideAnimationDuration)
//                     .OnComplete(() =>
//                     {
//                         VARIABLE.obj.transform.transform.localScale = Vector3.zero;
//                         LayoutRebuilder.ForceRebuildLayoutImmediate(
//                             VARIABLE.obj.transform.parent.GetComponent<RectTransform>());
// //                        Debug.LogError("HideEnd");
//                     });
            }
        }
    }

    public void HideObject()
    {
        foreach (var VARIABLE in _objectWithPositions)
        {
            if (VARIABLE.obj != null && VARIABLE.obj.GetComponentsInChildren<IShowHide>() != null &&
                VARIABLE.obj.GetComponentsInChildren<IShowHide>().Length != 0)
            {
                HideChildFolder();
                Hide();
                // Invoke("Hide", HideChildFolder());
            }

            else
            {
                Hide();
            }
        }
    }

    public void ShowObject(bool isButton = false)
    {
        foreach (var VARIABLE in _objectWithPositions)
        {
            Show();
//            if (VARIABLE.obj.GetComponent<IShowHide>() != null||VARIABLE.obj.GetComponentsInChildren<IShowHide>()!=null)
//            {
            ShowChildFolder();
            // Invoke("ShowChildFolder", HideAnimationDuration);
//            }
        }
    }

    public float ShowChildFolder()
    {
        foreach (var hidedObjectWithPosition in _objectWithPositions)
        {
            if (hidedObjectWithPosition.obj != null)
            {
                foreach (var _iShowHide in hidedObjectWithPosition.obj.GetComponentsInChildren<IShowHide>())
                {
                    if (_iShowHide.NeedShow)
                    {
                        _iShowHide.ShowObject();
                        _iShowHide.NeedShow = false;
                    }
                }
            }
        }

        return HideAnimationDuration;
    }

    public float HideChildFolder()
    {
        foreach (var hidedObjectWithPosition in _objectWithPositions)
        {
            foreach (var _iShowHide in hidedObjectWithPosition.obj.GetComponentsInChildren<IShowHide>())
            {
                if (_iShowHide.IsShowed)
                {
                    _iShowHide.NeedShow = true;
                    _iShowHide.HideObject();
                }
            }
        }

        return HideAnimationDuration;
    }
}