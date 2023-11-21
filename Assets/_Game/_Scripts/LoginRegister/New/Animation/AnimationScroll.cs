
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScroll : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Graphic testObj;

    [Button]
    public void ScrollToPoint()
    {
        
        Debug.LogError(testObj.canvasRenderer.hasMoved);
        Debug.LogError(testObj.canvasRenderer.hasMoved);
        scrollbar.value -= (1 - scrollbar.value) / 2;
    }

    private void OnBecameInvisible()
    {
        Debug.LogError("invisible");
    }

    private void OnBecameVisible()
    {
        Debug.LogError("visible");
        
    }
}