using System;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class ChildBorder : MonoBehaviour
{
    private bool isChanged = false;
    public bool NeedChangeAlpha = true;

    private Image image;

    public virtual void SetColor(Color color)
    {
        CheckNull();


        if (!CheckInteractableButton())
        {
            image.color = NeedChangeAlpha ? color : new Color(color.r, color.g, color.b, image.color.a);

            //if (_glow)
            //{
            //    SetGlow();
            //}
        }


        isChanged = true;
    }

    private void CheckNull()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        //if (color == Color.black && _changeMaterialToBlack)
        //{
        //    image.material = _blackMaterial;
        //}
        //else
        //{
        //    image.material = _standartMaterial;
        //}
    }

    private bool CheckInteractableButton()
    {
        return transform.parent.GetComponent<Button>() != null &&
               !transform.parent.GetComponent<Button>().IsInteractable();
    }

    private void SetGlow()
    {
        // image.color = _glowColor;
        // RectTransform transform = image.GetComponent<RectTransform>();
        // Vector2 scale = transform.sizeDelta;
        // Vector2 newScale = new Vector2(scale.x + 10f, scale.y + 10f);
        // transform.sizeDelta = newScale;
    }

    public void AccessColor()
    {
        Debug.LogError("test SetColor + "+ gameObject.name);
        try
        {
            // SetColor(GetComponentInParent<MytPrefabColorTypes>().ParentBorder.color);
        }
        catch (Exception e)
        {
            isChanged = true;
        }
    }

    private void OnEnable()
    {
        if (!isChanged)
        {
            // AccessColor();
        }
    }
}