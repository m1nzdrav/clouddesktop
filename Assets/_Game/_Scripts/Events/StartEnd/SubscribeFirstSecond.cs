using UnityEngine;
using UnityEngine.Events;

namespace _Game._Scripts.Events.StartEnd
{
    public  class SubscribeFirstSecond : MonoBehaviour
    {
        private EventListener _listenerFirst;
        private EventListener _listenerSecond;


        public void ResetListener()
        {
            if (_listenerSecond == null)
            {
                _listenerSecond = gameObject.AddComponent<EventListener>();
            }

            if (_listenerFirst == null)
            {
                _listenerFirst = gameObject.AddComponent<EventListener>();
            }
        }

        protected void AddMethodToListener( UnityAction firstTrigger,UnityAction secondTrigger)
        {
            if (_listenerSecond == null || _listenerFirst == null)
            {
                ResetListener();
            }

            _listenerSecond.Response.AddListener(secondTrigger);
            _listenerFirst.Response.AddListener(firstTrigger);
        }

        protected void RegisterEvent(FirstSecondEvent _event)
        {
            _event.RegisterSecond(_listenerSecond);
            _event.RegisterFirst(_listenerFirst);
        }

        protected void DeRegisterEvent(FirstSecondEvent _event)
        {
            _event?.DeRegisterSecond(_listenerSecond);
            _event?.DeRegisterFirst(_listenerFirst);
        }
    }
}