using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inputfield script
/// </summary>
public class InpFieldScript : MonoBehaviour
{
    public Image img;

    /// <summary>
    /// Change color
    /// </summary>
    public void SetNewColor()
    {
        img = GetComponent<Image>();
        img.color = Color.cyan;
    }
}