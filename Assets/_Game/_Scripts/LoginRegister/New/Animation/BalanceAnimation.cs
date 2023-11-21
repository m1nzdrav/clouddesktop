using System;
using System.Collections.Generic;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BalanceAnimation : MonoBehaviour
{
    [SerializeField] private List<ScaleTransform> targetScale;
    [SerializeField] private bool isBlocked;
    [SerializeField] private RectTransform transformParent;
    [SerializeField] private LayoutGroup layoutParent;
    private SequenceController _sequenceController;

    private void Start()
    {
        _sequenceController = new SequenceController();
    }

    public void ScaleUp()
    {
        if (isBlocked)
            return;
        LayoutRebuilder.MarkLayoutForRebuild(transformParent);
        _sequenceController._same.Pause();

        // _layoutElement.DOPreferredSize()
        targetScale.ForEach(x => { _sequenceController._same.Join(x.target.DOScale(x.upScale, 1)); });

        // _sequenceController._same.OnUpdate(() =>
        // {
        //     layoutParent.CalculateLayoutInputVertical();
        //     layoutParent.SetLayoutVertical();
        // });
        // _sequenceController._same.OnUpdate(() => LayoutRebuilder.MarkLayoutForRebuild(transformParent));
        // _sequenceController._same.OnComplete(() =>
        // {
        //     LayoutRebuilder.MarkLayoutForRebuild(transformParent);
        // });
        _sequenceController.PlaySequence(null);
    }

    public void ScaleDown()
    {
        if (isBlocked)
            return;

        LayoutRebuilder.MarkLayoutForRebuild(transformParent);
        _sequenceController._same.Pause();

        targetScale.ForEach(x => { _sequenceController._same.Join(x.target.DOScale(x.downScale, 1)); });
        // _sequenceController._same.OnUpdate(() =>
        // {
        //     layoutParent.CalculateLayoutInputVertical();
        //     layoutParent.SetLayoutVertical();
        // });
        // _sequenceController._same.OnUpdate(() => LayoutRebuilder.MarkLayoutForRebuild(transformParent));
        
        // _sequenceController._same.OnComplete(() =>
        // {
        //     layoutParent.SetLayoutVertical();
        //     layoutParent.CalculateLayoutInputVertical();
        // });
        _sequenceController.PlaySequence(null);
    }

    public void SetBlock()
    {
        isBlocked = !isBlocked;

        if (!isBlocked)
            ScaleDown();
    }

    public void Set()
    {
        
    }
}