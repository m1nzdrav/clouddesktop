using UnityEngine;
using UnityEngine.UI;

public class MytPrefabColorTypes : MonoBehaviour
{
    [SerializeField] private Image _parentBorder;
    [SerializeField] private bool needChangeChild = true;

    [SerializeField] private Color lastSelectedColor;

    public Color LastSelectedColor
    {
        get
        {
            if (_parentBorder != null && lastSelectedColor == _parentBorder.color)
            {
                return _parentBorder.color;
            }

            return lastSelectedColor;
        }
        set => lastSelectedColor = value;
    }

    public Image ParentBorder
    {
        get => _parentBorder;

        set { _parentBorder = value; }
    }

    private void Start()
    {
        SetColor();
    }

    public void SetColor(Image border = null)
    {
        //int childCount;

        if (!needChangeChild)
            return;

        Color color;

        if (border == null)
        {
            color = _parentBorder != null ? _parentBorder.color : Color.black;
        }
        else
        {
            color = border.color;
        }


        foreach (var VARIABLE in GetComponentsInChildren<ChildBorder>())
        {
            VARIABLE.SetColor(color);
        }
    }
}