using UnityEngine;

namespace _Game._Scripts.Events.StartEnd
{
    public class FirstSecondEvent : MonoBehaviour
    {
        private GameEvent _firstEvent, _secondEvent;
        public GameEvent FirstEvent => _firstEvent;

        public GameEvent SecondEvent => _secondEvent;


        public void InstantiateEvent()
        {
            if (_firstEvent != null) return;

            _firstEvent = new GameEvent();
            _secondEvent = new GameEvent();
        }

        public void RegisterForAllEvent(EventListener listener)
        {
            RegisterFirst(listener);
            RegisterSecond(listener);
        }

        public void DeRegisterForAllEvent(EventListener listener)
        {
            DeRegisterFirst(listener);
            DeRegisterSecond(listener);
        }

        public void RegisterFirst(EventListener listener)
        {
            InstantiateEvent();
            _firstEvent.Register(listener);
        }

        public void RegisterSecond(EventListener listener)
        {
            InstantiateEvent();
            _secondEvent.Register(listener);
        }

        public void DeRegisterFirst(EventListener listener)
        {
            InstantiateEvent();
            _firstEvent.DeRegister(listener);
        }

        public void DeRegisterSecond(EventListener listener)
        {
            InstantiateEvent();
            _secondEvent.DeRegister(listener);
        }
    }
}