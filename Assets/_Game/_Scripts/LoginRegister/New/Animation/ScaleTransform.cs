using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ScaleTransform
{
    public LayoutElement LayoutElement;
    public Transform target;
    public Vector3 downScale;
    public Vector3 upScale;
}