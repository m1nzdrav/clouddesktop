using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginAndLoadScene : MonoBehaviour
{
    [SerializeField] private InputField _inputField;

    [SerializeField] private Image _glow;
    [SerializeField] private GameObject _buttonLogin;
    [SerializeField] private StartAnimation dropDown, login;
    [SerializeField] private GameObject buttonLockIcon;
    [SerializeField] private RotatePlanes _rotatePlanes;
    private bool _mouseCheck;
    private static bool isLogin = false;

    public static bool IsLogin => isLogin;

    public void CheckAndLogin()
    {
        var text = _inputField.text;


        Debug.Log("Check");

        if (text == "123")
        {
            isLogin = true;
            buttonLockIcon.SetActive(false);
            _rotatePlanes.AnimLockArea();
        }
    }

    public void CheckEnter()
    {
        if (!_mouseCheck) StartCoroutine(ClickImitation());

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckAndLogin();
        }
    }

    IEnumerator ClickImitation()
    {
        _buttonLogin.GetComponent<CircleMenuButtonController>().EnterPointer();
        _glow.GetComponent<GlowEdit>().GlowAOn();

        yield return new WaitForSeconds(0.5f);

        _glow.GetComponent<GlowEdit>().ReturnColorGlow();
        _buttonLogin.GetComponent<CircleMenuButtonController>().ExitPointer();
    }

    public void MouseCheck()
    {
        _mouseCheck = !_mouseCheck;
    }
}