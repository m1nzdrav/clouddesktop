using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconFullController : MonoBehaviour
{
    [SerializeField] private GameObject[] showableGameObjects;

    [SerializeField] private GameObject noAccessMessage;
    /// <summary>
    /// if pointer enter
    /// </summary>
    [SerializeField] private Sprite pointerEnterButtonImage;

    /// <summary>
    /// If pointer exit
    /// </summary>
    [SerializeField] private Sprite outButtonImage;

    /// <summary>
    /// If this icon is accessible
    /// </summary>
    [field: SerializeField,
            Tooltip("If this icon is accessible")]
    private bool accessible = false;

    /// <summary>
    /// 
    /// </summary>
    [field: SerializeField,
            Tooltip(""),
            ReadOnly]
    private bool clickable = true;

    [SerializeField] private bool isAcces=true;

    public bool IsAcces
    {
        get => isAcces;
        set => isAcces = value;
    }

    /// <summary>
    /// 
    /// </summary>
    [field: SerializeField,
            ReadOnly,
            Tooltip("")]
    private bool rotated = false;

    /// <summary>
    /// Cached reference to the Button component
    /// </summary>
    [field: SerializeField,
            Tooltip("Cached reference to the Button component"),
            Required]
    private Button button;

    private CanvasController canvasController;

    /// <summary>
    /// 
    /// </summary>
    [field: SerializeField,
            Tooltip("")]
    private Ease noAccessAnimationEase = Ease.OutElastic;

    [field: SerializeField,
            Tooltip("")]
    private Vector3 noAccessRotation = new Vector3(0f, 90f, 0f);

    private Vector3 defaltRotation = Vector3.zero;

    [field: SerializeField,
            Tooltip("")]
    private float noAccessAnimationDuration = 0.5f;

    private bool pointerEnter;

    private float noAccessAnimationDurationHalf
    {
        get { return noAccessAnimationDuration / 2; }
    }
    

    void Start()
    {

//        Debug.LogError("Name: "+ name+ " panelRegister: "+_panelRegister.name);
        //Caching internal references
        canvasController = GetComponent<CanvasController>();

        //Check if button is accessible
        if (!accessible)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(ShowNoAccessMessage);

        }
    }

    // private void Update()
    // {
    //     if (rotated && clickable && Input.GetMouseButtonDown(0))
    //     {
    //         HideNoAccessMessage();
    //     }
    // }

    [Button]
    public void ShowNoAccessMessage()
    {
        if ((accessible && !rotated && !clickable)||isAcces){return;}
        noAccessMessage.SetActive(true);
        rotated = true;
        SetUnClickable();
        transform.DORotate(noAccessRotation, noAccessAnimationDuration).SetEase(noAccessAnimationEase)
            .OnComplete(SetClickable);

        Debug.Log("noAccessMessage" + noAccessMessage);
    }

    public void HideNoAccessMessage()
    {
        if (accessible && rotated && !clickable)
            return;
        rotated = false;
        SetUnClickable();
        transform.DORotate(defaltRotation, noAccessAnimationDuration).SetEase(noAccessAnimationEase)
            .OnComplete(SetClickable);
        noAccessMessage.SetActive(false);
    }

    
    private void SetClickable()
    {
        clickable = true;
        canvasController.SetClickable();

        // Проверка : остался ли курсор на иконке
        if (pointerEnter)
            ExitPointer();
    }

    private void SetUnClickable()
    {
        clickable = false;
        canvasController.SetUnClickable();
    }

    public void EnterPointer()
    {
        if (GetComponent<Button>().IsInteractable())
        {
            pointerEnter = true;

            if (pointerEnterButtonImage != null)
                GetComponent<Image>().sprite = pointerEnterButtonImage;

            //foreach (var VARIABLE in showableGameObjects)
            //{
            //    VARIABLE.SetActive(true);
            //}
        }
    }
    
    public void ExitPointer()
    {
        if (GetComponent<Button>().IsInteractable())
        {
            pointerEnter = false;

            if (outButtonImage != null)
                GetComponent<Image>().sprite = outButtonImage;

            //foreach (var VARIABLE in showableGameObjects)
            //{
            //    VARIABLE.SetActive(false);
            //}
        }
    }

   
}