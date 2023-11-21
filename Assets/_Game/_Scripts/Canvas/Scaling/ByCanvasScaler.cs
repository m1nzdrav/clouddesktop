using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByCanvasScaler : MonoBehaviour
{
    [SerializeField] private CanvasToScaleBy canvasToScaleBy;

    void Start()
    {
        InitilalizeCanvas();
    }

    private void InitilalizeCanvas()
    {
        canvasToScaleBy.OnNewDimensions += ResizeCanvas;
        ResizeCanvas(canvasToScaleBy.GetComponent<RectTransform>().sizeDelta);
    }

    public void ResizeCanvas(Vector2 newSizeDelta)
    {
        GetComponent<RectTransform>().sizeDelta = newSizeDelta;
    }
}
