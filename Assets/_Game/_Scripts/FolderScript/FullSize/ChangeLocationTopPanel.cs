using Doozy.Engine.Extensions;
using UnityEngine;

namespace _Game._Scripts.FolderScript.FullSize
{
    public class ChangeLocationTopPanel : MonoBehaviour
    {
        [SerializeField] private Transform locationToMove;
        [SerializeField] private Transform oldLocation;

        public void ChangeLocationIn()
        {
            Transform obj = GetComponent<FolderModel>().TopPanelSetParametrs.transform;
            obj.SetParent(locationToMove, false);
            obj.GetComponent<RectTransform>().FullScreen(true);
            obj.SetSiblingIndex(0);
        }

        public void ChangeLocationOut()
        {
            Transform obj = GetComponent<FolderModel>().TopPanelSetParametrs.transform;
            obj.SetParent(oldLocation);
            obj.GetComponent<RectTransform>().FullScreen(true);
            obj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 60);
        }
    }
}