using System;
using System.Collections.Generic;
using _Game._Scripts.LoginRegister.New.Animation;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using UnityEngine;
using UnityEngine.Events;

public class CanvasLoader : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> _canvasGroup;
    public CanvasGroup FirstCanvas => _canvasGroup[0];
    [SerializeField] private RenderFade _renderFade;
    private SequenceController _sequenceController;
    [SerializeField] private float animationDisappear = 1f;
    [SerializeField] private float animationAppear = 2f;
    [SerializeField] private float waitLoadTime = 3;

    [SerializeField] private UnityEvent actionStartFade;
    [SerializeField] private UnityEvent actionAfterLoad;


    private void Start()
    {
        _sequenceController = new SequenceController();

        if (RegisterSingleton._instance != null)
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader).CanvasLoader = this;

        Invoke(nameof(AfterLoad), waitLoadTime);
    }

    public void LoadNewScene(Action afterLoad = null)
    {
        foreach (CanvasGroup VARIABLE in _canvasGroup)
        {
            _sequenceController._same.Join(VARIABLE.DOFade(0, animationDisappear));
        }

        if (_renderFade != null)
        {
            _sequenceController._same.Join(_renderFade.Test(0));
        }

        _sequenceController.PlaySequence(() => { afterLoad?.Invoke(); });
    }

    private void AfterLoad()
    {
        actionStartFade.Invoke();
        foreach (var VARIABLE in _canvasGroup)
        {
            _sequenceController._same.Join(VARIABLE.DOFade(1, animationAppear));
        }


        _sequenceController.PlaySequence(actionAfterLoad.Invoke);
    }
}