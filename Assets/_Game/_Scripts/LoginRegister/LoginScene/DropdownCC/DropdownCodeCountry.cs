using Michsky.UI.ModernUIPack;
using UnityEngine;

public class DropdownCodeCountry : MonoBehaviour
{
    [SerializeField] private RectTransform _dropdownRect;
        
    private bool _open;

    public static int Height;

    public void SetWidth()      
    {

        if (_open)
        {
            _dropdownRect.localScale = Vector3.zero;

            var size = _dropdownRect.sizeDelta;
            size.y = 0;
            _dropdownRect.sizeDelta = size;
        }
        else
        {
            _dropdownRect.localScale = Vector3.one;


            var size = _dropdownRect.sizeDelta;
            size.y = Height;
            _dropdownRect.sizeDelta = size;
        }

        _open = !_open;
    }
}
   
