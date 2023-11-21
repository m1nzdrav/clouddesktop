using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TimelineController : MonoBehaviour
{
    [SerializeField] private List<VideoItem> _timelineItem;

    private void Start()
    {
        var count = _timelineItem.Count;

        for (int i = 0; i < count; i++)
        {
            var item = _timelineItem[i];
            var delay = item.StartSeconds;

            if (item.Animation != null)
            {
                StartCoroutine(StartItem(delay, item.Animation));
                return;
            }

            if (item.Player != null)
            {
                StartCoroutine(StartItem(delay, item.Player));
            }
        }
    }

    IEnumerator StartItem(int delay, VideoPlayer video)
    {
        yield return new WaitForSeconds(delay);

        video.Play();
    }

    IEnumerator StartItem(int delay, MoveCursorToPos mouse)
    {
        yield return new WaitForSeconds(delay);

        mouse.Start();
    }
}

[Serializable]
public class VideoItem
{
    public VideoPlayer Player;
    public MoveCursorToPos Animation;
    public int StartSeconds;
}
