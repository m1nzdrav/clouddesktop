using System;
using System.Collections;
using System.Collections.Generic;
using OneLine;
using UnityEngine;

public class AreaChildFolder : MonoBehaviour
{

    [SerializeField,OneLineWithHeader] private FrontierArea _frontier;

   

    public Vector3 GetPosition(ObjectWithPosition check)
    {
        switch (check.TransformArea)
        {
            case TransformArea.Bottom:
                check.obj.GetComponent<RectTransform>().pivot=new Vector2(0.5f,1);
                return _frontier.Bottom.position;
            
            case TransformArea.Left:
                check.obj.GetComponent<RectTransform>().pivot=new Vector2(1f,0.5f);
                return _frontier.Left.position;
            
            case TransformArea.Right:
                check.obj.GetComponent<RectTransform>().pivot=new Vector2(0f,0.5f);
                return _frontier.Right.position;
            
            case TransformArea.Top:
                check.obj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                return _frontier.Top.position;
            default: return Vector3.zero;
        }
    }


}

[Serializable]
public class FrontierArea
{
    public Transform Left;
    public Transform Right;
    public Transform Bottom;
    public Transform Top;

}
