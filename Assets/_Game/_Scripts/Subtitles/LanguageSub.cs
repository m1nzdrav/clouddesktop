using System;
using UnityEngine;

namespace _Game._Scripts
{
    [Serializable]

    public class LanguageSub
    {
        [SerializeField] private float timeCode;
        [SerializeField] public string Subtitles;

        public LanguageSub(float timeCode)
        {
            this.timeCode = timeCode;
        }

        public float TimeCode
        {
            get => timeCode;
            set => timeCode = value;
        }

        public string Subtitles1
        {
            get => Subtitles;
            set => Subtitles = value;
        }
    }
}