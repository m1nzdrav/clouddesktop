using UnityEngine;
using UnityEngine.UI;

public class HideMe : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private bool needHideMe = true;

    private GameObject _glowIcon;

    public Button Button
    {
        set => _button = value;
    }

    private void Start()
    {
        if (_button != null)
        {
            IconGlowController iconGlowController = _button.GetComponent<IconGlowController>();
            if (iconGlowController != null)
            {
                _glowIcon = iconGlowController.Glow;
            }
        }


        GetComponent<Button>().onClick.AddListener(ClickOnButtonHidingMe);
    }

    public void ClickOnButtonHidingMe()
    {
        if (needHideMe)
        {
            if (_glowIcon != null)
            {
                _glowIcon.SetActive(false);
            }

            _button.onClick.Invoke();
        }
        else if (_glowIcon != null)
            _glowIcon.SetActive(true);

        _button.GetComponent<IconModel>()?.Folder.GetComponent<SelectedWindow>().SendNullWindow();
        
        _button.GetComponent<IShowHide>()?.HideChildFolder();
    }
}