
using UnityEngine;
using UnityEngine.UI;

public class ButtonVideoRotate : MonoBehaviour, IWaitStateForButton
{
    [SerializeField]
    private bool onPressed = true;
    [SerializeField] private GameObject _gameObject;
    private CircleMenuButtonController _circleMenuButtonController;
    private RotateButton _rotateButtonCircleIndicator;

    [SerializeField] private TopPanelSetParametrs _topPanel;

    private void Start()
    {
        _circleMenuButtonController = GetComponent<CircleMenuButtonController>();
        _rotateButtonCircleIndicator = GetComponentInChildren<RotateButton>();
    }

    public void ClickButtonMetod()
    {
        if (onPressed)
        {
            Debug.LogError("show");
            Show();
        }
        else
        {
            
            HideWithVhangePosition();
        }
    }

    public void HideWithVhangePosition()
    {

   
        _gameObject.GetComponent<ShowHideButton>().HideWithChangePosition();
        onPressed = true;
    }

    public void Hide()
    {
        _gameObject.GetComponent<ShowHideButton>().Hide();
        onPressed = true;
    }
    public void Show()
    {
        if (!onPressed)
        {
            return;
        }
        _gameObject.GetComponent<ShowHideButton>().Show();
        onPressed = false;
    }

    public void ChangeWaitState()
    {
        if (_rotateButtonCircleIndicator.gameObject.GetComponentInChildren<Slider>().value == 0)
        {
            _circleMenuButtonController.WaitState = false;
        }
        else
        {
            _circleMenuButtonController.WaitState = true;
        }
    }
}