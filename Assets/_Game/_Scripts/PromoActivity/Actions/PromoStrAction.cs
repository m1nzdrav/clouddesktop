using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Game._Scripts.PromoActivity.Actions
{
    public class PromoStrAction : Activity
    {
        [SerializeField] private UnityEvent afterEnd;
        [SerializeField] private PromoOneInstantiate _promoOneInstantiate;
        [SerializeField] private Paragraph paragraphs;
        [SerializeField] private int currentNesting = 0;

        [Button]
        public override void StartActivity()
        {
            // first = true;
            // paragraphs = _promoOneInstantiate.AllItems;
            // throw new System.NotImplementedException();
        }

        public override void EndActivity()
        {
            NextActivity(null);
        }

        public void CheckNesting(PromoOneStrController paragraph)
        {
            // currentNesting++;
            paragraphs.Paragraphs.Remove(paragraph);
            if (paragraphs.Paragraphs.Count == 0)
            {
                EndActivity();
            }
        }
    }
}