using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPrefabPromo : MonoBehaviour
{
    public GameObject Prefab;
    [SerializeField] private GameObject _mainMenu;

    public void LoadLoginScene()
    {
        var active = Prefab.activeSelf;
        Prefab.SetActive(!active);
        active = _mainMenu.activeSelf;
        _mainMenu.SetActive(!active);
        LoadNewLoginScene();
    }

    public void LoadNewLoginScene()
    {
        SceneManager.LoadScene("LoginScene 1");
        _mainMenu.SetActive(false);
    }
}
