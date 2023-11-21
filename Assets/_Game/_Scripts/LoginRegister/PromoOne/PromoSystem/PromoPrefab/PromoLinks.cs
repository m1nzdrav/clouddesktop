using UnityEngine;
using UnityEngine.UI;

public class PromoLinks : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float _alpha;

    public Text Text
    {
        get => _text;
        set => _text = value;
    }

    public float Alpha
    {
        get => _alpha;
        set => _alpha = value;
    }
}