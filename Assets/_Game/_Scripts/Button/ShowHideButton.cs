using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShowHideButton : SubscribeEventCursor
{
    [field: SerializeField,
            Tooltip("Location to shrink to on hiding")]
    private Transform hideLocation;

    [field: SerializeField,
            Tooltip("Duration of hiding animation")]
    private float hideAnimationDuration;

    [field: SerializeField,
            Tooltip("This wondow's CanvasGroup reference"),
            ReadOnly]
    private CanvasGroup playerCanvas;

    [field: SerializeField,
            Tooltip("Position of the player window to return to on Show()")
    ]
    private Vector3 ActivePosition;

    [field: SerializeField
    ]
    private GameObject Slider;

    [SerializeField] private bool isLock = false;
    [SerializeField] private bool isHide = true;

    private EventListener _listenerHideCursor;
    private EventListener _listenerShowCursor;

    private void Start()
    {
        // AddMethodToListener(HideTrigger, ShowTrigger);
        // RegisterEvent();
    }


    [ButtonGroup("VisibilityToggle")]
    public void HideWithChangePosition()
    {
        if (isLock)
            return;

        if (hideLocation != null)
        {
            ChangePosition();

            transform.DOMove(hideLocation.position, hideAnimationDuration);
            transform.DOScale(Vector3.zero, hideAnimationDuration);
        }

        playerCanvas.DOFade(0, hideAnimationDuration);
//        Invoke("Deactivate", hideAnimationDuration);
    }


    private void ChangePosition()
    {
        ActivePosition = transform.localPosition;
    }

    public void Hide()
    {
        if (hideLocation != null)
        {
            transform.DOMove(hideLocation.position, hideAnimationDuration);
            transform.DOScale(Vector3.zero, hideAnimationDuration);
        }

        SimpleHide();
        // playerCanvas.DOFade(0, hideAnimationDuration);
    }

    public void SimpleHide()
    {
        CheckPlayerCanvas();
        playerCanvas.DOFade(0, hideAnimationDuration);
    }

    private void CheckPlayerCanvas()
    {
        if (playerCanvas == null)
        {
            playerCanvas = GetComponent<CanvasGroup>();
        }
    }

    /// <summary>
    /// Show this video window
    /// </summary>
    [ButtonGroup("VisibilityToggle")]
    public void Show()
    {
        if (isLock)
            return;

        transform.position = hideLocation.position;
        SimpleShow();
        transform.DOLocalMove(ActivePosition, hideAnimationDuration);
        transform.DOScale(Vector3.one, hideAnimationDuration);
    }

    public void SimpleShow()
    {
        CheckPlayerCanvas();
        playerCanvas.DOFade(1, hideAnimationDuration);
    }

    public void ChangeLock()
    {
        isLock = !isLock;

        if (isHide)
            HideWithChangePosition();
    }

    void HideTrigger()
    {
        if (!isHide)
            SimpleHide();
    }

    void ShowTrigger()
    {
        if (!isHide)
            SimpleShow();
    }

    public void NormalOpen()
    {
        if (!isHide)
            return;
        
        isHide = false;
        SimpleShow();
    }

    public void NormalClose()
    {
        if (isHide)

            return;

        isHide = true;
        SimpleHide();
    }
}