using System;
using System.Collections.Generic;
using System.Linq;
using _Game._Scripts;
using _Game._Scripts.Panel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class GridControllerInIcon : EventChild
{
    const int countGridForChild = 4;
    [SerializeField] private GridLayoutGroup gridInIcon;

    public GridLayoutGroup GridInIcon1 => gridInIcon;

    [SerializeField] private List<SlotIcon> slotIcons;
    [SerializeField] private IconModel _iconModel;
   
    [SerializeField] private List<IconModel> childIcon;
    [SerializeField] private EventListener _listenerAddChild;
    private EventListener _listenerRemoveChild;
    [SerializeField] private int iconNumberHierarchy;

    public int IconNumberHierarchy
    {
        get => iconNumberHierarchy;
        set => iconNumberHierarchy = value;
    }

  
    [Button]
    public void UpdateGrid()
    {
        CheckParentChild();

        SetSize();

        if (!CheckHierarchy())
        {
            return;
        }

        SetParameters();

        SetChildIcon();

        UpdateChild();
    }

    private void CheckParentChild()
    {
        if (folderParentChild == null)
        {
            SetReference();
        }
    }

    public void SetSize()
    {
        gridInIcon.cellSize =
            transform.parent.parent.GetComponent<GridLayoutGroup>().cellSize / 4; //Получаем размер сетки

        if (folderParentChild.GetComponent<PrefabName>().Prefab == Prefab.MYTDesktop)
        {
            SetForDesktop();
            return;
        }


        if (gridInIcon.cellSize.x < 15)
        {
            SetForChild();
        }
    }

    [Button]
    public void SetReference()
    {
        folderParentChild = _iconModel;
        if (iconNumberHierarchy >= Config.LAST_NUMBER_HIERARCHY)
        {
            return;
        }

        AddMethodToListener(UpdateChild);
        RegisterEvent();
    }

    private void SetForDesktop()
    {
        float propHeight = Screen.height / Config.PROP_OF_SELL_SIZE;
        float propWidth = propHeight * .7f;

        //размер плашки статистики = 30% от размера ячейки
        //иконки должны поместить 3 штуки в длину 
        gridInIcon.cellSize = new Vector2(propHeight * .6f / 3f, propHeight * .6f / 3f);
        gridInIcon.padding = new RectOffset(0, (int) (propHeight / 0.7 * 0.3), 0, 28);
        //0.75 - 1/4 от обычного отступа -  (propWidth - propHeight)*.7f/3f*.75f

        gridInIcon.spacing = new Vector2(propHeight * (1 - .6f) * .25f, propHeight * (1 - .6f) * .25f);
    }

    private void SetForChild()
    {
        gridInIcon.cellSize = new Vector2(5, 5);
        gridInIcon.padding = new RectOffset(2, 2, 2, 2);
        gridInIcon.spacing = new Vector2(0.5f, 0.5f);

        for (int i = countGridForChild; i < slotIcons.Count; i++)
        {
            Destroy(slotIcons[i].gameObject);
            slotIcons.RemoveAt(countGridForChild);
        }
    }

    private void SetParameters()
    {
        if (childIcon == null)
        {
            folderParentChild = _iconModel;
            childIcon = new List<IconModel>();
        }
    }

    private void SetChildIcon()
    {
        if (iconNumberHierarchy == Config.LAST_NUMBER_HIERARCHY)
        {
            return;
        }

        childIcon.Clear();
        DestroyChildIcon();


        if (folderParentChild == null)
        {
            SetReference();
        }

        if (folderParentChild.Childs.Count == 0)
            return;

        foreach (GameObject VARIABLE in folderParentChild.Childs)
        {
            childIcon.Add(VARIABLE.GetComponent<IconModel>());
        }

        SortChildIcon();
        // childIcon = sortedList;
    }

    private void DestroyChildIcon()
    {
        slotIcons.RemoveAll(x => x == null);
        foreach (var VARIABLE in slotIcons)
        {
            VARIABLE.RemoveIcon();
        }
    }

    [Button]
    private void SortChildIcon()
    {
        childIcon =
            childIcon.OrderBy(hierarchy => { return int.Parse(hierarchy.transform.parent.name); })
                .ToList(); //TODO так нельзя, но GridCellNumber не работает
    }

    // private void AddChild()
    // {
    //     SetParameters();
    //     SetChildIcon();
    //
    //     for (int i = 0; i < slotIcons.Count; i++)
    //     {
    //         if (slotIcons[i].Icon == null)
    //         {
    //             ChangeIcon(i);
    //         }
    //     }
    // }

    private bool CheckHierarchy()
    {
        if (iconNumberHierarchy >= Config.LAST_NUMBER_HIERARCHY)
        {
            DeleteGrid();
            return false;
        }

        return true;
    }

    private void DeleteGrid()
    {
        for (int i = 0; i < slotIcons.Count; i++)
        {
            Destroy(slotIcons[i].gameObject);
        }
    }

    private void UpdateChild()
    {
        if (iconNumberHierarchy >= Config.LAST_NUMBER_HIERARCHY)
        {
            return;
        }

        for (int i = 0; i < slotIcons.Count; i++)
        {
            ChangeIcon(i);
        }
    }

    private void ChangeIcon(int i)
    {
        if (i >= childIcon.Count)
        {
            slotIcons[i].Compare(null, 0, false);
        }
        else
        {
            if (GetComponent<IconModel>().Folder.GetComponent<PrefabName>().Prefab == Prefab.MYTDesktop)
            {
                slotIcons[i].Compare(childIcon[i].gameObject, iconNumberHierarchy + 1, true);
            }
            else
            {
                slotIcons[i].Compare(childIcon[i].gameObject, iconNumberHierarchy + 1, false);
            }
        }
    }

    // [Button]
    // public void UpdateGridInChild()
    // {
    //     if (iconNumberHierarchy >= Config.LAST_NUMBER_HIERARCHY-1)
    //     {
    //         return;
    //     }
    //     
    //     foreach (var VARIABLE in slotIcons)
    //     {
    //         if (VARIABLE.Icon == null)
    //         {
    //             continue;
    //         }
    //
    //         VARIABLE.Icon.GetComponent<GridInIcon>().UpdateGrid();
    //     }
    // }
}