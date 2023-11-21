using System;
using System.Collections;
using System.Collections.Generic;
using _Game._Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextInspector_Singleton : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    private bool _shouldDeselect = true;

    public bool ShouldDeselect
    {
        get => _shouldDeselect;
        set => _shouldDeselect = value;
    }

    private bool _doubleText = false;

    public bool DoubleText
    {
        get => _doubleText;
        set => _doubleText = value;
    }

    [SerializeField] private TextInspector _textInspector;

    public TextInspector TextInspector
    {
        get => _textInspector;
    }
    
    public void CheckRegister()
    {
            
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
    }
}

