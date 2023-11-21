#if !UNITY_EDITOR && UNITY_WEBGL
    using System.Runtime.InteropServices;
#endif
using UnityEngine;
using UnityEngine.EventSystems;


public class UrlOpen : MonoBehaviour, IPointerClickHandler
{
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void OpenNewTab(string url);
#endif


    [SerializeField] private string url;
    [SerializeField] private bool notClickable;

    public void SetClickable(bool trigger)
    {
        notClickable = trigger;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!notClickable)
            OpenUrl();
    }

    private void OpenUrl()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            OpenNewTab(url);
#else
        Application.OpenURL(url);
#endif
    }

   
}