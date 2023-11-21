using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game._Scripts.FolderScript.ShowHide.Folder.Stats
{
    public class ShowStats : ShowOn
    {
        [SerializeField] private bool isEnter;
        [SerializeField] private GameObject AreaToShow;

        [SerializeField] private float animationDuration = .2f;

        private EventListener _listenerHideCursor;
        private EventListener _listenerShowCursor;

        private void Start()
        {
            AddMethodToListener();
            RegisterEvent();
        }

        public void ResetListener()
        {
            if (_listenerHideCursor == null)
            {
                _listenerHideCursor = gameObject.AddComponent<EventListener>();
            }

            if (_listenerShowCursor == null)
            {
                _listenerShowCursor = gameObject.AddComponent<EventListener>();
            }
        }

        private void AddMethodToListener()
        {
            if (_listenerHideCursor == null || _listenerShowCursor == null)
            {
                ResetListener();
            }

            _listenerHideCursor.Response.AddListener(HideTrigger);
            _listenerShowCursor.Response.AddListener(ShowTrigger);
        }

        private void RegisterEvent()
        {
            (RegisterSingleton._instance.GetSingleton(typeof(TimerToHideCursor)) as TimerToHideCursor)?.FirstSecondEvent.RegisterSecond(_listenerHideCursor);
            // RegisterSingleton._instance?.TimerToHideCursor.RegisterHideCursor(_listenerHideCursor);
            (RegisterSingleton._instance.GetSingleton(typeof(TimerToHideCursor)) as TimerToHideCursor)?.FirstSecondEvent.RegisterFirst(_listenerShowCursor);
            // RegisterSingleton._instance?.TimerToHideCursor.RegisterShowCursor(_listenerShowCursor);
        }

        public override void Enter(EventTrigger who)
        {
            if (!IsBlocked)
            {
                Show();
                isEnter = true;
            }
        }

        public override void Exit(EventTrigger who)
        {
            if (!IsBlocked)
            {
                StartCoroutine(HideAfterTime());
                isEnter = false;
            }

            // Show();
        }

        private void ShowTrigger()
        {
            if (!isEnter) return;

            Show();
        }

        private void HideTrigger()
        {
            if (!isEnter) return;

            Hide();
        }

        private void Show()
        {
            StopCoroutine(HideAfterTime());

            AreaToShow.transform.localScale = Vector3.one;

            CanvasGroup canvasGroup =
                AreaToShow.GetComponent<CanvasGroup>();

            AreaToShow.SetActive(true);

            canvasGroup.DOKill();
            canvasGroup.DOFade(1, animationDuration * 4).OnComplete(() => { }
            );
        }

        private void Hide()
        {
            CanvasGroup canvasGroup =
                AreaToShow.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
            canvasGroup.DOFade(0, animationDuration).OnComplete(() =>
                {
                    AreaToShow.transform.localScale = Vector3.zero;
                    AreaToShow.SetActive(false);
                }
            );
        }

        IEnumerator HideAfterTime()
        {
            Debug.LogError("StartCorotine");
            yield return new WaitForSeconds(Config.DELAY_FOR_HIDE_CURSOR);
            Hide();
        }
    }
}