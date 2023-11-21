using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;

public class ColoredButton : MonoBehaviour
{
    [SerializeField] private SetParametrsAction _setParametrsAction;
    [SerializeField] private Color color;
    [SerializeField] private GameObject imageColor;

    public void CreatedColored()
    {
        _setParametrsAction.CreateColoredFolder(color);
        // ChangeColor();
    }

    public void ChangeColor()
    {
        Debug.LogError("changeColor");
        float rand = Random.Range(.1f, 1.5f);

        color = new Color(color.r * rand, color.g * rand, color.b * rand);
        Debug.LogError("changeColor "+ rand);
        imageColor.GetComponent<Shape>().settings.fillColor = color;
    }
}