using UnityEngine;
using UnityEngine.UI;

public class CircleDemoButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _activeSprite;

    [SerializeField]
    private GameObject _demoButton;

    public void DemoActive()
    {
        //SpriteState ss = new SpriteState();
        //ss.highlightedSprite = _inactiveSprite;

        _demoButton.GetComponent<Image>().sprite = _activeSprite;
    }
}
