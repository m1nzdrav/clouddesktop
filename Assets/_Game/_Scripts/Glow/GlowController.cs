using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlowController : MonoBehaviour
{
    enum Type
    {
        Button,
        Icon,
        Border,
        Cell
    }

    
    [SerializeField] private GameObject _glow;
    [SerializeField] private Image _glowImage;
    [SerializeField] private RectTransform _glowRect;

    [Header("Settings")]
    [SerializeField] private Transform _obj;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Color _color;
    [SerializeField] private Material _material;
    [SerializeField] private bool _inheritСolor;
    [SerializeField] private bool _activity;
    [SerializeField] private bool _mouse;

    [Header("Type")]
    [SerializeField] private Type _type;

    [Button]
    private void InstantiateGlow()
    {
        _glow = Instantiate(_glowImage, Vector3.zero, Quaternion.identity, _obj).gameObject;

        _glowRect.anchorMin = Vector2.zero;
        _glowRect.anchorMax = Vector2.one;

        _glowImage.sprite = _sprite;
        _glowImage.color = _color;
        _glowImage.material = _material;

        _glowImage.gameObject.SetActive(_activity);

        if (_inheritСolor)
        {
            var childBorder = _glow.AddComponent<ChildBorder>();
        }

        if (_mouse)
        {
            var mouseControl = _obj.gameObject.AddComponent<MouseControlGlow>();
            mouseControl.Glow = _glow;
        }

        switch (_type)
        {
            case Type.Icon:
                var mouseControl = _obj.gameObject.AddComponent<MouseControlGlow>();
                mouseControl.Glow = _glow;
                break;

            case Type.Button:
                var buttonGlowController = _obj.gameObject.AddComponent<ButtonGlowController>();
                buttonGlowController.Glow = _glowImage;
                break;
            case Type.Cell:
                var cellControl = _obj.gameObject.AddComponent<MouseControlGlow>();
                cellControl.Glow = _glow;
                break;
            case Type.Border:
                var borderGlow = _obj.gameObject.AddComponent<Border>();
                borderGlow.Glow = _glow;
                break;
        }
    }


}