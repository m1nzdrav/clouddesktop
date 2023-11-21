using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class MaskDragArea : MonoBehaviour
    {
        private bool enabled=false;
        public void OnMask()
        {
            if (!enabled)
            {
                transform.SetAsLastSibling();
                GetComponent<Image>().DOColor(new Color(0,0.15f,0.5f, 0.5f), 0);
                enabled = true;
            }
            
            
        }

        public void OffMask()
        {
            if (enabled)
            {
                GetComponent<Image>().DOColor(new Color(1,1,1, 0), 0);
                enabled = false;
            }
            
        }
    }
}
