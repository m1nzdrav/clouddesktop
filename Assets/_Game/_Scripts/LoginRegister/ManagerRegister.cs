using TMPro;
using UnityEngine;

public class ManagerRegister : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _canvasLogin;
    [SerializeField] private GameEvent startAllVideos;

    [SerializeField] private bool needCanvasLogin;
    // Start is called before the first frame update

    private void Start()
    {
        MainDesktop.instance.SetParametrs();
        PromoOneSingleton.Instance.SetParametrs();
        if (!needCanvasLogin)
        {
            startAllVideos.Raise();
            _canvasLogin.SetActive(false);
        }
    }

    public void CheckAndLogin()
    {
        if (_inputField.text.Length == _inputField.characterLimit)
        {
            startAllVideos.Raise();
            _canvasLogin.SetActive(false);
        }
    }
}