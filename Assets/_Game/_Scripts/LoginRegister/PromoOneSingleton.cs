
using UnityEngine;

public class PromoOneSingleton : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public static PromoOneSingleton Instance;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _rectTransform;

    public RectTransform RectTransform => _rectTransform;

    public Canvas Canvas => _canvas;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void CheckRegister()
    {
        // if (Instance == null)
        // {
        //     Instance = this;
        // }
        // else Destroy(gameObject);
        //
        // DontDestroyOnLoad(gameObject);
    }

    public void SetParametrs()
    {
        _canvas.worldCamera = Camera.main;
    }
}
