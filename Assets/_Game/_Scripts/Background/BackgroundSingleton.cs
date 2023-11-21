using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class BackgroundSingleton : MonoBehaviour, ISingleton
{
    [SerializeField] private Canvas _background;
    [SerializeField] private Camera _backCamera;
    [SerializeField] private CanvasGroup cloud;
    [SerializeField] private float fadeDuration = .5f;
    [SerializeField] private List<BackgroundHelper> _backgroundHelpers ;
    

    public string NameComponent
    {
        get => name;
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    private void Start()
    {
        StartCoroutine(EngineBackground());
    }

    public void CheckRegister()
    {
        DeActivateBackCamera();
    }

    [Button]
    public void ActivateBackCamera(Action action)
    {
        cloud.DOFade(0, fadeDuration).OnComplete(action.Invoke);
        _backCamera.gameObject.SetActive(true);
        _backCamera.Render();

        _background.worldCamera = _backCamera;

        Camera.main.enabled = false;
    }

    [Button]
    public void DeActivateBackCamera()
    {
        cloud.DOFade(1, fadeDuration).OnComplete((() =>
        {
            _background.worldCamera = Camera.main;
            _backCamera.gameObject.SetActive(false);
        }));
    }

    private IEnumerator EngineBackground()
    {
        yield return new WaitForSeconds(0.1f);

        _backgroundHelpers.ForEach(x =>
            x.SetRect());
        StartCoroutine(EngineBackground());
    }
}