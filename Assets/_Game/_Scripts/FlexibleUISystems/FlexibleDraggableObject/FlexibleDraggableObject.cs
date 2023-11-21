using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class FlexibleDraggableObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] public RectTransform DragArea;
    public GameObject Target;
    public bool CanDrag = true;
    [SerializeField] private bool notWait;
    private Vector3 velocity = Vector3.zero;
    private CanvasGroup canvasGroup;
    public static bool IsCkick = true;

    private Vector3 _mouseDelta;
    private bool _isDragInProgress = false;

    public UnityEvent OnFlexibleDragEnd;

    private void Start()
    {
        canvasGroup = Target.GetComponent<CanvasGroup>();
        
        if(Target == null)
            Target = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (notWait || CanDrag)
        {
            try
            {
                IsCkick = false;
                Vector2 targetPoint;
                //TODO: получать dragArea из Singletone
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        DragArea,
                        eventData.position,
                        Camera.main,
                        out targetPoint
                    );
                if (!_isDragInProgress)
                {
                    _mouseDelta = Target.transform.localPosition - new Vector3(targetPoint.x, targetPoint.y);
                    _isDragInProgress = true;
                }
                Target.transform.localPosition = _mouseDelta + new Vector3(targetPoint.x, targetPoint.y);

                if (canvasGroup != null)
                {
                    canvasGroup.blocksRaycasts = false;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(gameObject.name);
                Debug.Log(ex.Message);
                Debug.Log(DragArea);
                Debug.Log(Target);
            }

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnFlexibleDragEnd?.Invoke();
        _isDragInProgress = false;
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}