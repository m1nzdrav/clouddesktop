using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToScaleBy : MonoBehaviour
{
    public Action<Vector2> OnNewDimensions;

    private void OnRectTransformDimensionsChange()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        OnNewDimensions?.Invoke(new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y));
    }
}
