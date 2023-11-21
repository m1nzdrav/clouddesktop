using System;
using System.Collections;
using _Game._Scripts.Panel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(FlexibleDraggableObject))]
public class WaitPress : SelectedWindow, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler,
    IEndDragHandler
{
    [SerializeField] private Transform localParent;

    public Transform LocalParent
    {
        get { return localParent != null ? localParent : transform; }
        set => localParent = value;
    }

    [SerializeField] private bool isLockDrag;

    public bool IsLockDrag
    {
        get => isLockDrag;
        set => isLockDrag = value;
    }

    [SerializeField] private bool isCanDrag;

    public bool IsCanDrag
    {
        get => isCanDrag;
        set => isCanDrag = value;
    }

    [SerializeField] private float _timeDelay = .5f;
    [SerializeField] private float _delayForTimer;
    private EventTrigger _eventTrigger;
    [SerializeField] private MonoBehaviour _component;
    [SerializeField] private bool needLastSibling = true;
    [SerializeField] private FlexibleDraggableObject dragHelper;


    public float TimeDelay
    {
        get => _timeDelay;
        set => _timeDelay = value;
    }

    public MonoBehaviour Component
    {
        get => _component;
        set => _component = value;
    }

    private void Start()
    {
        dragHelper = GetComponent<FlexibleDraggableObject>();
        //todo uncomment after fix
        // base.selectedObject = dragHelper.Target;
    }

    #region EventTriggerImplementation

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLockDrag)
        {
            return;
        }

        if ( (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).startSelection)
        {
            DeleteTimer();
            (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).NowSelected.Add(this);
        }
        else
        {
            EndSelected();
        }

        EndSelected();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isLockDrag)
        {
            return;
        }

        StartSelection(eventData);
        base.OnPointerDown(eventData);
    }

    private void StartSelection(PointerEventData eventData)
    {
        if (isLockDrag)
        {
            return;
        }
        if ((RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).startSelection ||
            (eventData).button != PointerEventData.InputButton.Left) return;
        InitTimer(eventData);

        StartCoroutine(DelayDrag());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isLockDrag)
        {
            return;
        }
        base.OnPointerUp(eventData);
        // if (checkSelected)
        // {
        //     SendCurentWindow(dragHelper.Target);
        //     checkSelected = false;
        // }
        if (!(RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).startSelection)
        {
            DeleteTimer();
            StopAllCoroutines();
        }

        EndSelected();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLockDrag)
        {
            return;
        }
        base.OnDrag(eventData);

        DeleteTimer();
        StopAllCoroutines();
        if ((RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).startSelection)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).LoadDraggable(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLockDrag)
        {
            return;
        }
        if (!(RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).startSelection)
        {
            StopAllCoroutines();

            //            EndSelected();
        }
        else
        {
            (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).SetLocalParentForAllSelected(this);
        }
    }

    #endregion


    private IEnumerator DelayDrag()
    {
        yield return new WaitForSeconds(_timeDelay);
        StartSelected();
        // SendNullWindow();
    }

    public void StartSelected(bool needMask = true)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).startSelection = true;

        if (_component != null)
        {
            _component.enabled = false;
        }

        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().interactable = false;
        }

        if (dragHelper != null)
        {
            dragHelper.CanDrag = true;
        }

        DragItem dragItem = GetComponent<DragItem>();
        if (dragItem != null)
        {
            dragItem.CanDrag = true;
        }


        (RegisterSingleton._instance.GetSingleton(typeof(ParentRegister)) as ParentRegister)?.EnableAllArea(dragHelper.Target);


        if (needLastSibling && GetComponent<NeedFirstView>() != null)
            foreach (var VARIABLE in GetComponent<NeedFirstView>().ObjectOnFirstView)
            {
                try
                {
                    VARIABLE.SetAsLastSibling();
                }
                catch (Exception e)
                {
                }
            }

        //var mouseCircleManager = CircleSettings.Instance;//todo
        //mouseCircleManager.SpawnCircle(transform, LocalParent);
    }

    public void EndSelected(bool needMask = true)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager).startSelection = false;

        DeleteTimer();
        if (_component != null)
        {
            _component.enabled = true;
        }

        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().interactable = true;
        }

        if (dragHelper != null)
        {
            dragHelper.CanDrag = false;
        }


        //        if (needMask)
        //        {
        //            FindObjectOfType<MaskDragArea>().OffMask();
        //        }

        (RegisterSingleton._instance.GetSingleton(typeof(ParentRegister)) as ParentRegister)?.DisableAllArea();
    }

    #region Timer

    public void InitTimer(PointerEventData data)
    {
        Vector3 newPos =
            new Vector3(data.position.x - data.pressEventCamera.pixelWidth / 2,
                data.position.y - data.pressEventCamera.pixelHeight / 2, 0);
        //        Vector3 newPos = Camera.main.ScreenPointToRay(data.position).GetPoint(100);
        //        Debug.LogError("point pos"+ data.position+"\n"+
        //                       "new pos "+ newPos);

        Color newColor;

        if (dragHelper.Target.GetComponent<PrefabName>().Prefab == Prefab.MYTIcon60_60)
        {
            newColor = dragHelper.Target.GetComponent<Image>().color;
        }
        else
            try
            {
                newColor = dragHelper.Target.GetComponent<MytPrefabColorTypes>()
                    .ParentBorder.color;
            }
            catch (Exception e)
            {
                Debug.LogError("Проблема с цветом");
                newColor = GetComponent<Image>().color;
            }

        (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager)?.InitTimerToDrag(newPos, _timeDelay, newColor);
    }

    private void DeleteTimer()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(DragManager)) as DragManager)?.DeleteTimer();
    }

    #endregion


    //public void OffGlowOnParent()
    //{
    //    var parent = transform.parent;
    //    var glow = parent.GetChild(0);
    //    glow.gameObject.SetActive(false);
    //}
}