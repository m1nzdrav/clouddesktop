using UnityEngine;
using UnityEngine.UI;

public class VolRestart : MonoBehaviour
{
    [SerializeField]
    private AudioSource _soundSmall;

    [SerializeField]
    private AudioSource _soundLarge;

    [SerializeField]
    private Sprite _falseSpriteSmall;

    [SerializeField]
    private Sprite _falseSpriteLarge;

    [SerializeField]
    private GameObject _buttonUpSmall;

    [SerializeField]
    private GameObject _buttonUpLarge;

    public void VolStandart()
    {
        _soundSmall.volume = 1;
        _soundLarge.volume = 1;

        _buttonUpSmall.GetComponent<Image>().sprite = _falseSpriteSmall;
        _buttonUpLarge.GetComponent<Image>().sprite = _falseSpriteLarge;
    }
}
