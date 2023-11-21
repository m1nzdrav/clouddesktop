using System;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class NextPageSystem
{
    private DotsController dotsController;
    public Paragraphs paragraphs;
    private RectTransform rectTransform;

    public NextPageSystem(RectTransform rectTransform,DotsController dotsController,Paragraphs paragraphs)
    {
        this.rectTransform = rectTransform;
        this.dotsController = dotsController;
        this.paragraphs = paragraphs;
    }
    public NextPageSystem(RectTransform rectTransform,DotsController dotsController)
    {
        this.rectTransform = rectTransform;
        this.dotsController = dotsController;
    }

    public void SetParagraphs(Paragraphs paragraphs)
    {
        this.paragraphs = paragraphs;
    }

    public void CurrentPage(int[] pagesToSpawn)
    {
        StartSpawnAnimation(pagesToSpawn);
    }

    public void StartSpawnAnimation(int[] pagesToSpawn)
    {
        ScaleText(pagesToSpawn);
        dotsController.StartInitAnimation(paragraphs, pagesToSpawn);
    }

    public void FastCurrentPage(int[] pagesToSpawn)
    {
        StartFastSpawnAnimation(pagesToSpawn);
    }

    public void StartFastSpawnAnimation(int[] pagesToSpawn)
    {
        ScaleText(pagesToSpawn);
        dotsController.FastStartInitAnimation(paragraphs, pagesToSpawn);
    }

    private void ScaleText(int[] pagesToSpawn) 
    {
        SequenceController sequenceController = new SequenceController();
        sequenceController.SetSequence();

        foreach (var VARIABLE in pagesToSpawn)
            paragraphs.Pages[VARIABLE - 1].Paragraphs
                .ForEach(x =>
                {
                    sequenceController._same.Join(x.transform.DOScale(1, .8f))
                        // .OnStart(() =>
                        // LayoutRebuilder.MarkLayoutForRebuild(rectTransform));;
                    .OnUpdate(() =>
                        LayoutRebuilder.MarkLayoutForRebuild(rectTransform));
                })
                ;
        // sequenceController._same.OnComplete(() =>
        //     LayoutRebuilder.MarkLayoutForRebuild(rectTransform));
        sequenceController.PlaySequence(null);
    }
}