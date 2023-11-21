using System;
using UnityEngine;

[Serializable]
public class LightLampSystem
{
    private bool needLightLamp = false;
    
    [SerializeField]
    private GameObject Lamp;

    public bool NeedLightLamp
    {
        get => needLightLamp;
        set => needLightLamp = value;
    }

    public void ActivateLamp()
    {
        if (needLightLamp) Lamp.SetActive(true);
    }
}
