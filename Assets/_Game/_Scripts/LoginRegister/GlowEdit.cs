using UnityEngine;
using UnityEngine.UI;

public class GlowEdit : MonoBehaviour
{
    [SerializeField] 
    private Image _glow;

    private Color _saveColor;

    public void GlowAOn()
    {
        _saveColor = _glow.color;
        _glow.color = new Color(_saveColor.r, _saveColor.g, _saveColor.b, 1f);
    }

    public void ChangeColor()
    {
        _glow.GetComponent<Animation>().Play("OpenMoreLamp");
    }

    public void ReturnColorGlow()
    {
        _glow.GetComponent<Animation>().Play("CloseMoreLamp");
    }
}
