
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSpriteOnEnter : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Rect rect;



    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("enterr");
        rect =GetComponent<RectTransform>().rect;
        
    }

    public void OnPointerExit(PointerEventData data)
    {Debug.Log("Exit");
        GetComponent<RectTransform>().rect.SetMax(rect.max);
        GetComponent<RectTransform>().rect.SetMin(rect.min);
    }


}
