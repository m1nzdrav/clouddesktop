using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasController : MonoBehaviour
{
    /// <summary>
    /// If the button should be hidden on Start
    /// </summary>
    [field: SerializeField,
            Tooltip("If the button should be hidden on Start")]
    private bool hideOnStart = false;

    /// <summary>
    /// If the button should be active on Start
    /// </summary>
    [field: SerializeField,
            Tooltip("If the button should be active on Start")]
    private bool activateOnStart = true;

    /// <summary>
    /// Transparency of the attached canvas when it is active
    /// </summary>
    [field: SerializeField,
            Tooltip("Transparency of the attached canvas when it is active"),
            Range(0f, 1f)]
    private float activeCanvasAlpha = 1f;

    /// <summary>
    /// Transparency of the attached canvas when it is inactive
    /// </summary>
    [field: SerializeField,
            Tooltip("Transparency of the attached canvas when it is inactive"),
            Range(0f, 1f)]
    private float inactiveCanvasAlpha = 0.8f;

    /// <summary>
    /// If the attached canvas is active
    /// </summary>
    [field: SerializeField,
            ReadOnly,
            Tooltip("If the attached canvas is active")]
    private bool isActive;

    /// <summary>
    /// If the attached canvas is shown
    /// </summary>
    [field: SerializeField,
            ReadOnly,
            Tooltip("If the attached canvas is shown")]
    private bool isShown = true;

    /// <summary>
    /// Duration of fade out animation
    /// </summary>
    [field: SerializeField,
            Tooltip("Duration of fade out animation")]
    private float fadeDuration = 0.5f;

    /// <summary>
    /// Reference to the CanvasGroup component on this GameObject
    /// </summary>
    [SerializeField]
    private CanvasGroup canvasGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cache internal references
        canvasGroup = GetComponent<CanvasGroup>();

        //Settings checks
        if (hideOnStart)
            HideCanvas();

        if (activateOnStart)
            ActivateCanvas();
    }

    /// <summary>
    /// Shows connected button using CanvasGroup
    /// </summary>
    [ButtonGroup("Showing")]
    public void ShowCanvas()
    {
        isShown = true;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Hides connected button without diactivating it or the gameobject
    /// using CanvasGroup
    /// </summary>
    [ButtonGroup("Showing")]
    public void HideCanvas()
    {
        isShown = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
       
    /// <summary>
    /// Activates attached canvas
    /// </summary>
    [ButtonGroup("Activation")]
    public void ActivateCanvas()
    {
        isActive = true;
        canvasGroup.alpha = activeCanvasAlpha;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Deactivate attached canvas
    /// </summary>
    [ButtonGroup("Activation")]
    public void DeactivateCanvas()
    {
        isActive = false;
        canvasGroup.alpha = inactiveCanvasAlpha;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    [ButtonGroup("Fade")]
    public void FadeOut()
    {
        canvasGroup.DOFade(0, fadeDuration).OnComplete(HideCanvas);
    }

    [ButtonGroup("Fade")]
    public void FadeIn()
    {
        canvasGroup.DOFade(1, fadeDuration).OnComplete(ShowCanvas);
    }

    public void SetClickable()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void SetUnClickable()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
