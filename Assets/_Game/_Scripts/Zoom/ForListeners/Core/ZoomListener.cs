using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomListener : MonoBehaviour
{
    private void Start()
    {
        InitializeListener();
    }

    protected virtual void InitializeListener()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(S_ZoomRegister)) as S_ZoomRegister).OnZoom += OnZoom;
    }

    protected virtual void OnZoom(float proportion)
    {

    }
}
