using System;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using Shapes2D;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShowableArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] private string nameArea;

    public string NameArea => nameArea;

    [SerializeField] private bool needFirstView = true;

    [SerializeField] private Transform areaToShow;

    public Transform AreaToShow
    {
        get => areaToShow;
        set => areaToShow = value;
    }

    [SerializeField] private Transform childArea;


    [SerializeField] private bool needShowing = true;
    [SerializeField] private List<Prefab> canDropInMe;
    [SerializeField] private List<ShowableArea> cantDropInMe = new List<ShowableArea>();
    [SerializeField] private ParentRegister parentRegister;

    public int SizeChild = 30;
    //    public Transform ChildArea => childArea;

    public Transform ChildArea
    {
        get => childArea;
        set => childArea = value;
    }

    public List<ShowableArea> CantDropInMe
    {
        get => cantDropInMe;
        set => cantDropInMe = value;
    }

    public List<Prefab> CanDropInMe
    {
        get => canDropInMe;
        set => canDropInMe = value;
    }

    [SerializeField] private Transform myParent;
    [SerializeField] private int siblingIndex;

    public bool NeedShowing
    {
        get => needShowing;
        set => needShowing = value;
    }

    [SerializeField] private Color currentColor, currentColorImage;

    private void Start()
    {
        if (parentRegister == null)
        {
            parentRegister =  (RegisterSingleton._instance.GetSingleton(typeof(ParentRegister)) as ParentRegister);
        }

        parentRegister.ShowableAreas.Add(this);
        try
        {
            currentColorImage = areaToShow.GetComponent<Image>().color;
            currentColor = areaToShow.GetComponent<Shape>().settings.fillColor;
        }
        catch (Exception e)
        {
        }

        GetComponentInParent<ShowableArea>().cantDropInMe.Add(this);
        myParent = transform.parent;
    }

    public bool CanShowing(GameObject _target)
    {
        if (canDropInMe.FindAll(x => x == _target.GetComponent<PrefabName>().Prefab).Count == 0 ||
            cantDropInMe.Find(x => x.gameObject.name == _target.name) != null)
        {
            needShowing = false;
            return needShowing;
        }

        needShowing = true;
        return needShowing;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
//        Debug.LogError("enter");
        parentRegister.PanelTransform = this;
//         if (needFirstView)
//         {
//             siblingIndex = transform.GetSiblingIndex();
// //            transform.parent = parentRegister.StandartParent;
//             transform.SetSiblingIndex(transform.parent.childCount - 2);
//         }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
//         Debug.LogError("exit");
//         if (needFirstView)
//         {
//             transform.SetParent(myParent, false);
// //            transform.parent = myParent;
//             transform.SetSiblingIndex(siblingIndex);
//         }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
//        Debug.LogError("up");
        // if (needFirstView)
        // {
        //     transform.parent = myParent;
        //     transform.SetSiblingIndex(siblingIndex);
        // }
    }

    public void ChangeColor()
    {
        try
        {
            // currentColor = areaToShow.GetComponent<Shape>().settings.fillColor;
            currentColorImage = areaToShow.GetComponent<Image>().color;
            if (GetComponent<PrefabName>().Prefab == Prefab.MYTIcon60_60)
            {
                // areaToShow.GetComponent<Image>().color = Color.red;
            }
            else
            {
                currentColor = areaToShow.GetComponent<Image>().color;
                if (GetComponent<PrefabName>().Prefab == Prefab.MYTIcon60_60)
                {
                    // areaToShow.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    Image image = GetComponent<MytPrefabColorTypes>().ParentBorder;
                    Color newColor = new Color(image.color.r, image.color.g, image.color.b, 0.06f);

                    areaToShow.GetComponentsInChildren<GridCell>()
                        .ForEach(x => x.ChangeColorZeroChild(newColor));
                }
            }


//            areaToShow.GetComponent<Image>().color = Color.red;
//            areaToShow.GetComponent<Shape>().settings.fillColor = Color.red;
        }
        catch (Exception e)
        {
        //     Debug.LogError("ошибка при смене цвета выделения");

//            areaToShow.GetComponent<Image>().color = Color.red;
        }
    }

    public void ReturnColor()
    {
        if (areaToShow == null) return;

//        try
//        {
        if (GetComponent<PrefabName>() != null && GetComponent<PrefabName>().Prefab == Prefab.MYTIcon60_60)
        {
            // areaToShow.GetComponent<Image>().color = currentColorImage;
        }
        else
            areaToShow.GetComponentsInChildren<GridCell>().ForEach(x => x.ReturnColor());

//            areaToShow.GetComponent<Shape>().settings.fillColor = currentColor;
//            areaToShow.GetComponent<Image>().color = currentColorImage;
//        }
//        catch (Exception e)
//        {
//            areaToShow.GetComponent<Image>().color = currentColorImage;
//        }
    }
}