using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowZoomListener : ZoomListener
{
    protected override void InitializeListener()
    {
        base.InitializeListener();
    }

    protected override void OnZoom(float proportion)
    {
        base.OnZoom(proportion);
        
    }
}
