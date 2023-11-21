using UnityEngine;
using UnityEngine.UI;

public class DragGroup : MonoBehaviour
{
    [SerializeField] private GameObject coverSelected;
    
    public void AddChild(WaitPress child)
    {
       GameObject prefab= Instantiate(coverSelected, transform);
       prefab.transform.position = child.transform.position;
       child.LocalParent.SetParent(prefab.transform,true);
       child.GetComponent<Image>().raycastTarget = false;
    }
}