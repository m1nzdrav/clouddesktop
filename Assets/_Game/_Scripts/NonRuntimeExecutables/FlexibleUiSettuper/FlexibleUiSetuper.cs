using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public class FlexibleUiSetuper : MonoBehaviour
{
    [SerializeField] private List<Transform> parents;

#if UNITY_EDITOR
    

    [Button]
    public void SetupFlexibleUi()
    {
        //Debug.Log(transform.FindAmongChilds<FlexibleUiDragArea>(null).Count);
        foreach (var parent in parents) 
        {
            parent.FindAmongChilds<FlexibleUiDragArea>(dragArea => SetupDragArea(dragArea));
        }
        Debug.Log("flexibleUi setup");
    }

    private void SetupDragArea(FlexibleUiDragArea dragArea)
    {
        dragArea.transform.FindAmongChilds<FlexibleUiTarget>(target => SetupTargets(dragArea, target));
    }

    private void SetupTargets(FlexibleUiDragArea dragArea, FlexibleUiTarget target)
    {
        target.transform.FindAmongChilds<Transform>(t => CheckMissingDraggables(t));
        target.transform.FindAmongChilds<FlexibleDraggableObject>(draggable => SetupDraggable(dragArea, target, draggable));
    }

    private void SetupDraggable(FlexibleUiDragArea dragArea, FlexibleUiTarget target, FlexibleDraggableObject draggable)
    {
        draggable.Target = target.gameObject;
        draggable.DragArea = dragArea.GetComponent<RectTransform>();
    }

    private void CheckMissingDraggables(Transform transform)
    {
        if(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(transform.gameObject) > 0)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(transform.gameObject);
            transform.gameObject.AddComponent<FlexibleDraggableObject>();
        }
    }
#endif
}
