using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Color currentColor;
    [SerializeField]
    private GridControllerInFolderDesktop gridControllerInFolderDesktop;
    RectMask2D rectMask2D;


    public GridControllerInFolderDesktop MyInventory
    {
        get
        {
            if (gridControllerInFolderDesktop == null)
            {
                OnEnable();
            }

            return gridControllerInFolderDesktop;
        } 
            
        set => gridControllerInFolderDesktop = value;
    }

    private void OnEnable()
    {
        rectMask2D = GetComponent<RectMask2D>();
        currentColor = GetComponent<Image>().color;

        var GetGrid = transform.parent?.GetComponent<GridCellGetGridController>();
        if (GetGrid != null)
        {
            gridControllerInFolderDesktop = transform.parent.GetComponent<GridCellGetGridController>().gridControllerInFolderDesktop;
        }
    }

    

    

    public void ChangeColorZeroChild(Color newColor)
    {
        //      currentColor = GetComponent<Image>().color;
        if (transform.childCount == 1)
        {
            GetComponent<Image>().color = newColor;
        }
    }

    public void ReturnColor()
    {
        //      if (transform.childCount==0)
        //      {
        GetComponent<Image>().color = currentColor;
        //      }
    }

    //private void OnTransformChildrenChanged()
    //{
    //    var childCount = transform.childCount;
    //    var cellSize = GetComponent<RectTransform>().sizeDelta;
    //    var isGrid = _gridController.IsGrid;

    //    for (int i = 0; i < childCount; i++)
    //    {
    //        var child = transform.GetChild(i);
    //        child.GetComponent<RectTransform>().sizeDelta = cellSize;

    //        if (child.childCount == 1) continue;

    //        var childText = child.GetChild(0);

    //        if (!isGrid)
    //        {

    //            childText.localPosition = new Vector3(0f, -40f, 0f);
    //        }
    //        else
    //        {
    //            childText.localPosition = new Vector3(65f, 7f, 0f);
    //        }
    //    }
    //}
}
