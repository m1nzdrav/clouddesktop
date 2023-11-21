using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(CanvasGroup))]
public class ButtonController : MonoBehaviour
{
    /// <summary>
    /// If the button should be hidden on Start
    /// </summary>
    [field: SerializeField,
            Tooltip("If the button should be hidden on Start")]
    private bool hideOnStart = false;
    
    /// <summary>
    /// Reference to the Button component on this GameObject
    /// </summary>
    [SerializeField]
    private Button button;

    /// <summary>
    /// Reference to the CanvasGroup component on this GameObject
    /// </summary>
    [SerializeField]
    private CanvasGroup canvasGroup;
    
    // Start is called before the first frame update
    void Awake()
    {
        //Cache internal references
        button = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();

        //Settings checks
        if (hideOnStart)
            HideButton();
    }

    /// <summary>
    /// Hides connected button without diactivating it or the gameobject
    /// using CanvasGroup
    /// </summary>
    [ButtonGroup("ShowHide")]
    public void HideButton()
    {
//        Debug.LogError("ЗАкрыл кнопку"+ this.gameObject.name);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Shows connected button using CanvasGroup
    /// </summary>
    [ButtonGroup("ShowHide")]
    public void ShowButton()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
//        StartCoroutine(AutoHide());
    }

    /// <summary>
    /// If desable event for pointer enter
    /// We need to use corotine for hide button automatic
    /// </summary>
    ///
    IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(2f);
        HideButton();
    }
}
