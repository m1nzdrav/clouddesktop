using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class DragManager : MonoBehaviour, ISingleton
{
    #region Singleton
    

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
    }

    #endregion
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        else
        {
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
    }

    //    [SerializeField] private DragGroup dragGroup;

    [Header("Press Key")] [SerializeField] private KeyCode _keyCode;


    [SerializeField] private List<WaitPress> nowSelected;

    [SerializeField] private List<WaitPress> selecteds;

    [SerializeField] private Image _circlePrefab;

    [SerializeField] private Image _circle;

    public List<WaitPress> NowSelected
    {
        get => nowSelected;
        set => nowSelected = value;
    }

    public void AddSelected(WaitPress selected)
    {
//        dragGroup.AddChild(selected);
//        dragGroup.transform.SetAsLastSibling();
        NowSelected.Add(selected);
    }


    public List<WaitPress> Selecteds
    {
        get => selecteds;
        set => selecteds = value;
    }

    [SerializeField] public static bool StartSelection = false;

    public bool startSelection
    {
        get => StartSelection;
        set
        {
            StartSelection = value;

            if (value)
            {
                HoverEnter();
            }
            else
            {
                HoverExit();
            }
        }
    }

//     void Update()
//     {
//         if (Input.GetKeyDown(_keyCode) && !startSelection)
//         {
//             startSelection = true;
//             foreach (var selected in Selecteds)
//             {
//                 selected.IsCanDrag = true;
//                 try
//                 {
//                     if (selected != null)
//                     {
//                         selected.StartSelected(false);
//                     }
//                 }
//                 catch (Exception e)
//                 {
//                     Debug.LogError(e);
//                 }
//             }
//
// //            _openManager.CurrentArea.MaskDragArea.OnMask();
//         }
//
//         if (Input.GetKeyUp(_keyCode) && startSelection)
//         {
//             startSelection = false;
//             foreach (var selected in Selecteds)
//             {
//                 selected.IsCanDrag = false;
//                 try
//                 {
//                     if (selected != null)
//                     {
//                         selected.EndSelected(false);
//                     }
//                 }
//                 catch (Exception e)
//                 {
//                 }
//             }
//
//             try
//             {
// //                _openManager.CurrentArea.MaskDragArea.OffMask();
//             }
//             catch (Exception e)
//             {
//                 Debug.LogError(e);
//             }
//         }
//     }

    public void LoadDraggable(WaitPress pressedObj)
    {
        foreach (var VARIABLE in NowSelected)
        {
            if (VARIABLE != pressedObj)
                VARIABLE.transform.SetParent(pressedObj.transform, true);
        }
    }

    public void SetLocalParentForAllSelected(WaitPress transform)
    {
        foreach (var VARIABLE in NowSelected)
        {
            if (VARIABLE != transform)
                VARIABLE.transform.SetParent(transform.transform.parent, true);
        }

        Debug.LogError("Clear");
        NowSelected.Clear();
    }

    #region Timer

    [Header("Timer")] [SerializeField] private float delayTimerInit = .03f;

    [SerializeField] private GameObject prefabeTimer;

    [SerializeField] private Texture2D hoverCursor, hoverCursorWeb;


    public void InitTimerToDrag(Vector3 position, float timerAction, Color newColor)
    {
        StartCoroutine(InitAwait(position, timerAction, newColor));
    }

    IEnumerator InitAwait(Vector3 position, float timerAction, Color newColor)
    {
//        Debug.LogError("начал создавать ");
        yield return new WaitForSeconds(delayTimerInit);
        GameObject timer = Instantiate(prefabeTimer, (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.CanvasLoader.FirstCanvas.GetComponent<RectTransform>());

        timer.transform.localPosition = position;
        timer.transform.SetAsLastSibling();

        timer.GetComponent<TimerToDrag>().SetValue(timerAction - delayTimerInit, newColor);

        Image circle = Instantiate(_circlePrefab, (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.CanvasLoader.FirstCanvas.GetComponent<RectTransform>());

        circle.color = newColor;
        Transform circleTransform = circle.transform;
        circleTransform.localPosition = position;
        _circle = circle;

        StartCoroutine(CircleAnim(circle, timerAction));
    }

    IEnumerator CircleAnim(Image circle, float delay)
    {
        //yield return new WaitForSeconds(delay);

        circle.gameObject.SetActive(true);


        var time = 0f;
        var period = 0.7f;
        var circleScale = circle.transform.localScale;
        var circleColor = circle.color;
        var scaleTo = new Vector3(6, 6, 6);

        while (time < period)
        {
            time += Time.deltaTime;
            var lTime = time / period;

            var lVector = Vector3.Lerp(circleScale, scaleTo, lTime);
            circle.transform.localScale = lVector;

            if (period - time < 0.3f)
            {
                var lColor = Color.Lerp(circleColor, new Color(circleColor.r, circleColor.g, circleColor.b, 0f), lTime);
                circle.color = lColor;
            }

            yield return null;
        }
    }

    public void HoverEnter()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // Cursor.SetCursor(hoverCursorWeb, new Vector2(16, 16), CursorMode.ForceSoftware);
#else
        // Cursor.SetCursor(hoverCursor, new Vector2(16, 16), CursorMode.Auto);
#endif
    }

    public void HoverExit()
    {
        // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void DeleteTimer()
    {
//        Debug.LogError("Удаляю ");

        StopAllCoroutines();
        if (TimerToDrag.instance != null)
        {
//            Debug.LogError("Уже создан ");
            TimerToDrag.instance.DeleteTimer();
        }

        if (_circle != null) Destroy(_circle.gameObject);
    }

    #endregion

    #region TestTimer

    [Header("TestTimer")] [SerializeField] private Vector2 position;

    [SerializeField] private float distanceRay = 1000;

    [SerializeField] private float timer;

    [SerializeField] private Color _color;

    [Button]
    public void TestInitTimer()
    {
        Vector3 newPos = Camera.main.ScreenPointToRay(position).GetPoint(distanceRay);
        Vector3 newTestPosition = new Vector3(position.x - Camera.main.pixelWidth / 2,
            position.y - Camera.main.pixelHeight / 2, 0);
        Debug.LogError(newTestPosition);
        InitTimerToDrag(newTestPosition, timer, _color);
    }

    #endregion
}