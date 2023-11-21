using System;
using DG.Tweening;
using UnityEngine;

namespace Project._Game._Scripts.SameSequence
{
    public class SameSequence : MonoBehaviour, ISingleton
    { 

        public string NameComponent
        {
            get => name;
        }
        public SequenceController sequenceHide;
        public SequenceController sequenceShow;
        public SequenceController sequenceCoin;

        private void Awake()
        {
            if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }

        public void CheckRegister()
        {
            SetSequence();
        }

        private void SetSequence()
        {
            sequenceHide = new SequenceController();
            sequenceShow = new SequenceController();
            sequenceCoin = new SequenceController();
        }
    }
}