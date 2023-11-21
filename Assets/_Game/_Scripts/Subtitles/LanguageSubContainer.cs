using System.Collections;
using System.Collections.Generic;
using OneLine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Game._Scripts
{
    public class LanguageSubContainer : MonoBehaviour
    {
        [field: SerializeField,
                Required]
        private CircleMenuController circleController;

        [field: SerializeField,
                Range(0f, 0.01f)]
        private float acceptableDeviation = 0.005f;

        private float activationPosition;

        [SerializeField] private TextMeshProUGUI subtitlesPanel;

        [SerializeField, OneLine, OneLine.HideLabel,]
        private LanguageSub[] _languageSubs;

        public LanguageSub[] LanguageSubs
        {
            get => _languageSubs;
            set => _languageSubs = value;
        }

        private void FixedUpdate()
        {
            ActivationCheck();
        }

        private void ActivationCheck()
        {
            foreach (var VARIABLE in LanguageSubs)
            {
                if (Mathf.Abs(circleController.RadialSlider.SliderValueRaw - VARIABLE.TimeCode) <= acceptableDeviation)
                {
                    StartCoroutine(ResetActivation(VARIABLE.Subtitles1));
                }
            }
        }

        IEnumerator ResetActivation(string subs)
        {
            yield return new WaitForSeconds(acceptableDeviation * 2);
            subtitlesPanel.text = subs;
        }
    }
}