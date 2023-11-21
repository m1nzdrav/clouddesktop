using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ZoomRegister : MonoBehaviour, ISingleton
{
    public Action<float> OnZoom;

    public string NameComponent { get => name; }

    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);

        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    public void CheckRegister()
    {
        
    }
}
