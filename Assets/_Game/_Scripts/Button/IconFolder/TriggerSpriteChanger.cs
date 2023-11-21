using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TriggerSpriteChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite exitSprite, enterSprite;
    [SerializeField] private bool isLock;

    public bool IsLock
    {
        get => isLock;
        set
        {
            isLock = value;
            if (isLock)
            {
                ChangeToEnter();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeToEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeToExit();
    }

    private void ChangeToExit()
    {
        if (!isLock)
        {
            _image.sprite = exitSprite;
        }
    }

    private void ChangeToEnter()
    {
        _image.sprite = enterSprite;
    }
}