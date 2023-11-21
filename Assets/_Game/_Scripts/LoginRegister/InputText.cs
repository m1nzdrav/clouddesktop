using UnityEngine;

public class InputText : MonoBehaviour
{
    [SerializeField] private RectTransform _textCodeRect;

    public static int Height;

    private bool _open;

    public void ShowHideInput()
    {
        if (_open)
        {
            _textCodeRect.localScale = Vector3.zero;

            var size = _textCodeRect.sizeDelta;
            size.y = 0;
            _textCodeRect.sizeDelta = size;
        }
        else
        {
            _textCodeRect.localScale = Vector3.one;

            var size = _textCodeRect.sizeDelta;
            size.y = Height;
            _textCodeRect.sizeDelta = size;
        }

        _open = !_open;
    }
}
