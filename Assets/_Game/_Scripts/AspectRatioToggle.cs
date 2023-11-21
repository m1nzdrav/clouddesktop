using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.AspectRatioFitter;

[RequireComponent(typeof(AspectRatioFitter))]
public class AspectRatioToggle : MonoBehaviour
{
    /// <summary>
    /// Cached reference to the AspectRatioFitter component
    /// </summary>
    private AspectRatioFitter fitter;

    /// <summary>
    /// Original value of the fitter control mode
    /// </summary>
    private AspectMode defaultControlMode;

    // Start is called before the first frame update
    void Start()
    {
        //Cache reference
        fitter = GetComponent<AspectRatioFitter>();
        defaultControlMode = fitter.aspectMode;
    }

    /// <summary>
    /// Changes AspectRatioFitter Aspect mode to WidthControlsHeight
    /// </summary>
    public void SetWidthControlMode()
    {
        fitter.aspectMode = AspectMode.WidthControlsHeight;
    }

    /// <summary>
    /// Changes AspectRatioFitter Aspect mode to HeightControlsWidth
    /// </summary>
    public void SetHeightControlMode()
    {
        fitter.aspectMode = AspectMode.HeightControlsWidth;
    }

    /// <summary>
    /// Sets fitter control mode to its original value
    /// </summary>
    public void SetDefaultControlMode()
    {
        fitter.aspectMode = defaultControlMode;
    }
}
