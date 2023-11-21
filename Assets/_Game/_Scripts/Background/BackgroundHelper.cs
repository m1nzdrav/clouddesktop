using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class BackgroundHelper
{
    public float speed;
    public Vector2 pos;
    public RawImage image;
    public Rect _rect;
    public bool rotateAround = false;

    public BackgroundHelper()
    {
        speed = 0;
        pos = new Vector2(1, 0);
        image = null;
        _rect = default;
    }

    public void SetRect()
    {
        if (rotateAround)
            image.transform.RotateAround(image.transform.position, image.transform.forward, speed);
        else
            image.uvRect = new Rect(image.uvRect.x + _rect.x, image.uvRect.y + _rect.y, 1, 1);
    }
}