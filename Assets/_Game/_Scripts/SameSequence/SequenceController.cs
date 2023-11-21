using System;
using DG.Tweening;
using UnityEngine;

namespace Project._Game._Scripts.SameSequence
{
    public class SequenceController
    {
        public Sequence _same;

        public SequenceController()
        {
            SetSequence();
        }

        public void SetSequence()
        {
            _same = DOTween.Sequence();
            _same.Pause();
        }

        public void PlaySequence(Action doAfterComplete)
        {
            _same.Play().OnComplete(() =>
            {
                SetSequence();
                doAfterComplete();
            });
        }
    }
}