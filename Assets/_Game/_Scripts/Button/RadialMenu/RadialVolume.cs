using System;
using _Game._Scripts.Events.StartEnd;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using _Game._Scripts.Volume;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RadialVolume : RadialListener
{
    [SerializeField] private FirstSecondEvent _firstSecondEvent;
    [SerializeField] private Image currentImage;
    [SerializeField] private Sprite pauseImage, playImage;
    [SerializeField] private bool isPlay;
    [SerializeField] private UnityEvent action;

    [SerializeField, Header("For Scale")] private Vector3 defaultScale;
    [SerializeField] private Vector3 playScale;


    private void Start()
    {
        AddMethodToListener(ChangePlay,ChangePause);
    }

    public void ChangeVolume()
    {
        ActionVolume();

        PrefabName currentWindow =
            (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab.Find(x =>
                x.GetComponent<PrefabName>().Prefab == Prefab.TwoVideos1);
        
        if (currentWindow == null)
        {
            currentWindow = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.GetLastPrefab().GetComponent<PrefabName>();
        }

        switch (currentWindow.Prefab)
        {
            case Prefab.TwoVideos1:
            {
                currentWindow.GetComponent<MultiPrefabElement>().MultiPrefab.MainStart.StartButtons[0].ClickButton();
                break;
            }
            default:
            {
                (RegisterSingleton._instance.GetSingleton(typeof(VolumeController)) as VolumeController)?.ChangeStateMusic();
                break;
            }
        }
    }

    private void ActionVolume()
    {
        isPlay = !isPlay;
        ChangeScale();
        ChangeSprite();
        action?.Invoke();
    }

    private void ChangeSprite()
    {
        if (isPlay)
            ChangePause();
        else
            ChangePlay();
    }

    private void ChangePlay()
    {
        currentImage.sprite = playImage;
    }

    void ChangePause()
    {
        currentImage.sprite = pauseImage;
    }

    private void ChangeScale()
    {
        transform.localScale = isPlay ? playScale : defaultScale;
    }

    public override void SetNewSetting()
    {
        PrefabName prefabName =
            (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab.Find(x =>
                x.GetComponent<PrefabName>().Prefab == Prefab.TwoVideos1);
        
        if (prefabName == null)
        {
            prefabName = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.GetLastPrefab().GetComponent<PrefabName>();
        }
        
        switch (prefabName.Prefab)
        {
            case Prefab.TwoVideos1:
            {
                RegisterEvent(_firstSecondEvent);
                StateButton stateButton = prefabName.GetComponent<MultiPrefabElement>().MultiPrefab.MainStart
                    .StartButtons[0].StateButton;

                if (stateButton == StateButton.Stop &&
                    isPlay || stateButton == StateButton.Start && !isPlay)
                {
                    isPlay = !isPlay;
                    ChangeSprite();
                }

                break;
            }
            default:
            {
                DeRegisterEvent(_firstSecondEvent);
                if ((RegisterSingleton._instance.GetSingleton(typeof(VolumeController)) as VolumeController)?.IsPlayed != isPlay)
                {
                    isPlay = !isPlay;
                    ChangeSprite();
                }
            }
                break;
        }
    }
}