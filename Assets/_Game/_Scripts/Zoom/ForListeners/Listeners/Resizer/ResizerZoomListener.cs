using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizerZoomListener : ZoomListener
{
    [SerializeField] private ResizerType resizerType;
    [SerializeField] private float proportionMultiplier = 1;
    [SerializeField] private float startMultiplier = 5;
    [SerializeField] private float proportionBorder = 0.4f;
    //
    private RectTransform _rectTransform;
    private Vector2 _defaultSize;

    protected override void InitializeListener()
    {
        base.InitializeListener();
        InitializeResizer();
    }

    private void InitializeResizer()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultSize = _rectTransform.sizeDelta;
        _rectTransform.sizeDelta = _defaultSize;
        _rectTransform.sizeDelta = _defaultSize * startMultiplier;
    }

    protected override void OnZoom(float proportion)
    {
        base.OnZoom(proportion);
        if(proportion < proportionBorder)
        {
            proportion = (float)Math.Pow(proportion, -1) * proportionMultiplier;
            Vector2 newSize = _rectTransform.sizeDelta;
            switch (resizerType)
            {
                case ResizerType.Width:
                    newSize = new Vector2(_defaultSize.x * proportion, _defaultSize.y);
                    break;
                case ResizerType.Height:
                    newSize = new Vector2(_defaultSize.x, _defaultSize.y * proportion);
                    break;
                case ResizerType.Both:
                    newSize = new Vector2(_defaultSize.x * proportion, _defaultSize.y * proportion);
                    break;
            }
            if (newSize.x < _defaultSize.x || newSize.y < _defaultSize.y)
            {
                return;
            }
            _rectTransform.sizeDelta = newSize;
        }
        else
        {
            _rectTransform.sizeDelta = _defaultSize;
        }
    }
}
