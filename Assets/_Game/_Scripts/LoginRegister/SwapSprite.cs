using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwapSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite _newSprite;

    private Sprite _oldSprite;

    void SwapMouseOn()
    {
        _oldSprite = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = _newSprite;
    }

    void SwapMouseOut()
    {
        GetComponent<Image>().sprite = _oldSprite;
    }
}
