using UnityEngine;

public class LoginInstantiate : MonoBehaviour
{
    [SerializeField] private DropdownCodeCountry _dropdownCCPrefab;
    [SerializeField] private PromoOneStrController _inputCode;
    [SerializeField] private Transform _listParent;

    public DropdownCodeCountry DropdpwnCC;

    private static LoginInstantiate _instantiate;

    public static LoginInstantiate Instantiate
    {
        get
        {
            if (_instantiate == null)
            {
                _instantiate = FindObjectOfType<LoginInstantiate>();
            }

            return _instantiate;
        }
    }

    public PromoOneStrController SetDropdown(int size)
    {
        DropdownCodeCountry.Height = size;
        DropdpwnCC = Instantiate(_dropdownCCPrefab, _listParent);

        var item = DropdpwnCC.GetComponent<PromoOneStrController>();
        item.DropdownCode = DropdpwnCC;

        return item;
    }

    public PromoOneStrController SetInputCode(int size)
    {
        InputText.Height = size;

        var item = Instantiate(_inputCode, _listParent);
        item.InputText = item.GetComponent<InputText>();

        return item;
    }
}
