using Shapes2D;
using UnityEngine;

public class ChildBorderShapes : ChildBorder
{
    [SerializeField] private Shape _shape;

    [SerializeField] private bool needChangeOutline = false;

    public override void SetColor(Color color)
    {
        _shape.settings.fillColor =
            NeedChangeAlpha ? color : new Color(color.r, color.g, color.b, _shape.settings.fillColor.a);

        if (needChangeOutline)
        {
            _shape.settings.outlineColor =
                NeedChangeAlpha ? color : new Color(color.r, color.g, color.b, _shape.settings.outlineColor.a);
        }
    }
}