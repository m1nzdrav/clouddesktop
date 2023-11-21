using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace _Game._Scripts.FolderScript.Name
{
    public class EventName : MonoBehaviour
    {
        public InputField name_textField;

        public delegate void OnNameChanged(string newName);

        public event OnNameChanged onNameChanged;

        //private GameEvent eventChangeName;
        //private EventListener _listener;

        //public GameEvent EventChangeName => eventChangeName;

        //public EventListener Listener => _listener;

        private void Start()
        {
            onNameChanged += ChangeNameEvent;
        }

        /// <summary>
        /// Функция для OnEndEdit в InputField имени иконки
        /// </summary>
        public void ChangeName() 
        {
            SetName(name_textField.text);
        }

        /// <summary>
        /// локальный ивент для имени в иконке
        /// </summary>
        /// <param name="Name"></param>
        void ChangeNameEvent(string Name)
        {
            name_textField.text = Name;
        }

        /// <summary>
        /// функция, вызывающая событие изменения имени
        /// </summary>
        /// <param name="newName">новое имя</param>
        public void SetName(string newName)
        {
            onNameChanged.Invoke(newName);
        }


        /*
         * private void InstantiateEvent()
        {
            if (eventChangeName != null) return;

            eventChangeName = new GameEvent();
        }
         * 
         * public void RegisterEvent(EventListener listener)
        {
            InstantiateEvent();
            eventChangeName.Register(listener);
        }

        /*public void ResetListener()
        {
            if (_listener == null)
            {
                _listener = gameObject.AddComponent<EventListener>();
            }
        }*/

        /*private void AddMethodToListener()
        {
            if (_listener == null)
            {
                ResetListener();
            }

            //_listener.Response.AddListener(() => { SetName(currentName); });
        }*/


    }
}