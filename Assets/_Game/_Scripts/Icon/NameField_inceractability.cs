﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameField_inceractability : MonoBehaviour
{
    [HideInInspector] public GridControllerInFolderDesktop gridControllerInFolderDesktop;
    [SerializeField] InputField nameField;

    // Start is called before the first frame update
    void Start()
    {
        //nameField = transform.GetChild(0).gameObject.GetComponent<InputField>();
    }

    void OnEnable()
    {
        SwitchIneractability(gridControllerInFolderDesktop.ShowNamespaces);
        gridControllerInFolderDesktop.onSwitchVisibility += SwitchIneractability;
    }

    void OnDisable()
    {
        Debug.Log("disable nameField_int");
        gridControllerInFolderDesktop.onSwitchVisibility -= SwitchIneractability;
    }

    void SwitchIneractability(bool interactable) 
    {
        nameField.interactable = interactable;
    }

    /*На случай если OnEnable/OnDisable нужно будет заменить
    public void InteractabilityEventInitial() 
    {
        SwitchIneractability(gridControllerInFolderDesktop.showNamespaces);
        gridControllerInFolderDesktop.onSwitchVisibility += SwitchIneractability;
    }

    public void InteractabilityDeaktiv() 
    {
        gridControllerInFolderDesktop.onSwitchVisibility -= SwitchIneractability;
    }
    */
}
