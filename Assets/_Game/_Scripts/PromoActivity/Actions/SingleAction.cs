using UnityEngine;
using UnityEngine.Events;

namespace _Game._Scripts.PromoActivity.Actions
{
    public class SingleAction : Activity
    {
        [SerializeField] private UnityEvent _startEvent;

        public override void StartActivity()
        {
            _startEvent.Invoke();
        }

        public override void EndActivity()
        {
        }
    }
}