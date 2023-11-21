using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(CanvasGroup))]
public class BatchVideoController : MonoBehaviour
{
    [SerializeField]
    private List<VideoPlayer> videoPlayers;

    [SerializeField]
    private float videoDuration;

    private CanvasGroup playerCanvas;

    private void Start()
    {
        //videoPlayers.AddRange(FindObjectOfType<VideoPlayer>());
        playerCanvas = GetComponent<CanvasGroup>();
    }

    public void PlayAll()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            player.Play();
        }
        Hide();
        Invoke("Show", videoDuration);
    }

    public void PauseAll()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            player.Pause();
        }
    }

    public void StopAll()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            player.Stop();
        }
    }

    public void HideAll()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            //player.transform.parent.gameObject.SetActive(false);
            player.transform.parent.GetComponent<VideoController>().Hide();
            FindObjectOfType<SubtitlesController>().Hide();
        }
    }

    public void ShowAll()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            player.transform.parent.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        playerCanvas.alpha = 0f;
        playerCanvas.interactable = false;
        playerCanvas.blocksRaycasts = false;
    }

    public void Show()
    {
        playerCanvas.alpha = 1f;
        playerCanvas.interactable = true;
        playerCanvas.blocksRaycasts = true;
    }
}
