using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteHighlighted : MonoBehaviour
{
    private Color currentColor;
    private Color highlightedColor;
    private void Awake()
    {
        currentColor = GetComponent<Image>().color;
        highlightedColor = GetComponent<Image>().color;
        highlightedColor.a+=0.2f;

    }

    // Start is called before the first frame update
    public void ChangeSpritePointerEnter()
    {

        
        GetComponent<Image>().color=highlightedColor;
    }

    public void ChangeSpritePointerExit()
    {
        GetComponent<Image>().color=currentColor;
    }
}
