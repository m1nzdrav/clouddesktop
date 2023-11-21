using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public static class RectTransformExtension
{
    public static Vector2 GetOffsetMin(this RectTransform rectTransform)
    {
        return rectTransform.anchoredPosition - Vector2.Scale(Vector2.Scale(rectTransform.sizeDelta, rectTransform.pivot), rectTransform.localScale);
    }

    //TODO: setOffsets
    public static void SetOffsetMin(this RectTransform rectTransform, Vector2 newOffsetMin)
    {
        Vector2 scaledSizesDelta = Vector2.Scale(Vector2.Scale(rectTransform.sizeDelta, rectTransform.pivot), rectTransform.localScale);
        Vector2 a = Vector2.Scale(newOffsetMin, scaledSizesDelta) - (rectTransform.anchoredPosition - scaledSizesDelta);
        rectTransform.sizeDelta -= a;
        rectTransform.anchoredPosition += Vector2.Scale(a, Vector2.one - rectTransform.pivot);
    }

    public static Vector2 GetOffsetMax(this RectTransform rectTransform)
    {
        return rectTransform.anchoredPosition + Vector2.Scale(Vector2.Scale(rectTransform.sizeDelta, Vector2.one - rectTransform.pivot), rectTransform.localScale);
    }

    public static void SetOffsetMax(this RectTransform rectTransform, Vector2 newOffsetMax)
    {
        
    }

    public static Vector2 GetRotatedOffsetMin(this RectTransform rectTransform)
    {
        return rectTransform.GetRotatedOffsetMin();
        /*Vector2 anchoredPosRotated = MathfExtension.GetRotatedVector(rectTransform.anchoredPosition, rectTransform.rotation);
        Vector2 sizeDeltaRotated = MathfExtension.GetRotatedVector(rectTransform.sizeDelta, rectTransform.rotation);
        Vector2 pivotRotated = MathfExtension.GetRotatedVector(rectTransform.pivot, rectTransform.rotation);
        Vector2 localScaleRotated = MathfExtension.GetRotatedVector(rectTransform.localScale, rectTransform.rotation);*/
        //return MathfExtension.GetRotatedVector(rectTransform.GetOffsetMin(), rectTransform.eulerAngles, rectTransform.anchoredPosition);
        /*return anchoredPosRotated -
               Vector2.Scale(Vector2.Scale(sizeDeltaRotated, rectTransform.pivot), localScaleRotated);*/
    }

    public static Vector2 GetRotatedOffsetMax(this RectTransform rectTransform)
    {
        return rectTransform.GetRotatedOffsetMax();
    }
}
