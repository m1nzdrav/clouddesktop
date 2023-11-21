using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class AnimationLightEarth : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject light;
    [SerializeField] private Transform parent;

    public void OnPointerDown(PointerEventData eventData)
    {
        // GameObject _light = Instantiate(light, parent);
        // _light.transform.position =eventData.pointerPressRaycast.screenPosition;
        // _light.transform.DOMoveZ(-70, 1).OnComplete(() => _light.transform.DOMoveZ(0, 1));
    }
}