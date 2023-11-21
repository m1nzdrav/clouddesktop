using System.Collections.Generic;
using _Game._Scripts.Button.Rotate;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using DG.Tweening;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.Events;

namespace _Game._Scripts.Button.RadialMenu
{
    public class RadialMenu : SubscribeEventOpenedStock
    {
        [SerializeField] private UnityEvent _eventOpened;
        [SerializeField] private UnityEvent _eventClose;
        [SerializeField] private CanvasGroup slider;
        [SerializeField] private List<RadialListener> chainListeners;


        private void Start()
        {
            (RegisterSingleton._instance.GetSingleton(typeof(HeadSelectedLoginScene)) as HeadSelectedLoginScene).ButtonVideoRotate = this;
            AddMethodToListener(OpenedTrigger, RemoveTrigger);
            RegisterEvent();
        }

        public void SetSelectedState()
        {
            CheckCurrentPrefab();
            slider.DOFade(1, .5f);
        }

        private void CheckCurrentPrefab()
        {
            chainListeners.ForEach(x => x.SetNewSetting());
        }

        public void RemoveSelectedState()
        {
            // slider.DOFade(0, .5f);
        }

        void RemoveTrigger()
        {
            _eventClose?.Invoke();
            if ((RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab.Count == 0)
            {
                GetComponent<ShowHideButton>().NormalClose();
            }
            else
                (RegisterSingleton._instance.GetSingleton(typeof(HeadRotate)) as HeadRotate)?.ChangePrefabSettingButton();
        }

        void OpenedTrigger()
        {
            _eventOpened?.Invoke();
            GetComponent<ShowHideButton>().NormalOpen();
            (RegisterSingleton._instance.GetSingleton(typeof(HeadRotate)) as HeadRotate)?.ChangePrefabSettingButton();
        }
    }
}