using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class ActionForVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] private UnityEvent endEvent;
    [SerializeField] private bool needDeSubscribe = false;

    private void Start()
    {
        if (needDeSubscribe)
            endEvent.AddListener(DeSubscribe);

        SetActionLoop();
    }

    private void SetActionLoop()
    {
        player.loopPointReached += test();
    }

    private VideoPlayer.EventHandler test()
    {
        return source => endEvent?.Invoke();
    }

    private void DeSubscribe()
    {
        player.loopPointReached -= test();
    }
}