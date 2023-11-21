using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DesktopController : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private Color backgroundColor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        backgroundColor = color;
    }

    void Start()
    {
        //Caching internal references
        image = GetComponent<Image>();

        image.color = backgroundColor;
    }
}