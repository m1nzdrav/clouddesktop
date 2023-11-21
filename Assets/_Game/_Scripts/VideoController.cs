using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasController))]
public class VideoController : MonoBehaviour
{
    /// <summary>
    /// This controller's VideoPlayer
    /// </summary>
    [field: SerializeField,
            Tooltip("This controller's VideoPlayer"),
            Required]
    public VideoPlayer player;

    public static bool checkPlayngVideo = true;

    /// <summary>
    /// Location to shrink to on hiding
    /// </summary>
    [field: SerializeField,
            Tooltip("Location to shrink to on hiding")]
    private Transform hideLocation;

    /// <summary>
    /// Duration of hiding animation
    /// </summary>
    [field: SerializeField,
            Tooltip("Duration of hiding animation")]
    private float hideAnimationDuration;

    /// <summary>
    /// Position of the player window to return to on Show()
    /// </summary>
    [field: SerializeField,
            Tooltip("Position of the player window to return to on Show()"),
            ReadOnly]
    private Vector3 videoPlayerActivePosition;

    /// <summary>
    /// This wondow's CanvasGroup reference
    /// </summary>
    [field: SerializeField,
            Tooltip("This wondow's CanvasGroup reference"),
            ReadOnly]
    private CanvasGroup playerCanvas;

    /// <summary>
    /// Cover image for thi video
    /// </summary>
    [field: SerializeField,
            Tooltip("Cover image for thi video"),
            Required]
    private Image cover;

    [field: SerializeField,
            Tooltip("Cover image for thi video"),
            Required]
    private Image coverBlack;

    private void Start()
    {
        //Cache needed values
        videoPlayerActivePosition = transform.position;
        playerCanvas = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Play this controller's video
    /// </summary>
    [ButtonGroup("Playback")]
    public void Play()
    {
        if (checkPlayngVideo)
        {
            // Debug.LogError("Play AudioSourse = "+ player.gameObject.GetComponent<AudioSource>().time);
//            player.gameObject.GetComponent<AudioSource>().UnPause();
            player.Play();
        }
    }

    /// <summary>
    /// Pause this controller's video
    /// </summary>
    [ButtonGroup("Playback")]
    public void Pause()
    {
        if (checkPlayngVideo)
        {
//            player.gameObject.GetComponent<AudioSource>().Pause();
            player.Pause();
        }
    }

    /// <summary>
    /// Stop this controller's video
    /// </summary>
    [ButtonGroup("Playback")]
    public void Stop()
    {
        if (checkPlayngVideo)
        {
//            player.gameObject.GetComponent<AudioSource>().Stop();
            player.Stop();
        }
    }

    /// <summary>
    /// Hide this video window
    /// </summary>
    [ButtonGroup("VisibilityToggle")]
    public void Hide()
    {
        player.Stop();
        checkPlayngVideo = false;
        if (hideLocation != null)
        {
            transform.DOMove(hideLocation.position, hideAnimationDuration);
            transform.DOScale(Vector3.zero, hideAnimationDuration);
        }

        playerCanvas.DOFade(0, hideAnimationDuration);
        Invoke("Deactivate", hideAnimationDuration);
    }

    /// <summary>
    /// Show this video window
    /// </summary>
    [ButtonGroup("VisibilityToggle")]
    public void Show()
    {
        checkPlayngVideo = true;
        playerCanvas.DOFade(1, hideAnimationDuration);
        transform.DOMove(videoPlayerActivePosition, hideAnimationDuration);
        transform.DOScale(Vector3.one, hideAnimationDuration);
    }

    /// <summary>
    /// Activates this window's cover
    /// </summary>
    [ButtonGroup("CoverControls")]
    public void ActivateCover()
    {
        Debug.LogError("CoverActiv");
        cover.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activates this window's cover
    /// </summary>
    [ButtonGroup("CoverControls")]
    public void ActivateBlackScreen()
    {
        coverBlack.enabled = true;
    }

    /// <summary>
    /// Deactivates this window's cover
    /// </summary>
    [ButtonGroup("CoverControls")]
    public void DeactivateCover()
    {
        Debug.LogError("Deactivate" + gameObject.name);
        cover.gameObject.SetActive(false);
    }

    [ButtonGroup("Time")]
    public void GetTime()
    {
        Debug.LogError(player.time);
    }
}