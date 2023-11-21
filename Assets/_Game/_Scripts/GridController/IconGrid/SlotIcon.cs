using System.Collections;
using _Game._Scripts;
using Doozy.Engine.Extensions;
using Shapes2D;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SlotIcon : MonoBehaviour
{
    public float test = 0;

    [SerializeField] private GameObject icon;

    public GameObject Icon => icon;

    public void Compare(GameObject _icon, int iconNumberHierarchy, bool isDesktop)
    {
        if (_icon == null)
        {
            RemoveIcon();
            return;
        }

        if (icon != null && icon.name == _icon.name) return;

        RemoveIcon();


        if (isDesktop)
        {
            SetIcon(_icon, iconNumberHierarchy);
        }
        else
        {
            SetIconAndGraphics(_icon, iconNumberHierarchy);
        }
    }

    private void SetIconAndGraphics(GameObject _icon, int iconNumberHierarchy)
    {
        SetIcon(_icon, iconNumberHierarchy);

        ChangeAlpha();

        // SetView();

        OffName();
        SetShape();
    }

    private void SetIcon(GameObject _icon, int iconNumberHierarchy)
    {
        CreateIconCopy(_icon);

        SetGridInIcon(iconNumberHierarchy);

        SetView();

        icon.GetComponent<IconModel>().Text.transform.localScale *=
            Screen.height / Config.PROP_OF_SELL_SIZE * 1.5f / 384;
    }

    private void CreateIconCopy(GameObject _icon)
    {
        if (icon != null)
        {
            Destroy(icon.gameObject);
        }

        DeleteChild();

        icon = Instantiate(_icon, transform);
    }

    [Button]
    private void DeleteChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    private void ChangeAlpha()
    {
        Color color = icon.GetComponent<Shape>().settings.fillColor;
        icon.GetComponent<Shape>().settings.fillColor = new Color(color.r, color.g, color.b, 0.5f);
    }

    private void SetGridInIcon(int iconNumberHierarchy)
    {
        GridControllerInIcon gridControllerInIcon = icon.GetComponent<GridControllerInIcon>();

        gridControllerInIcon.IconNumberHierarchy = iconNumberHierarchy;
        gridControllerInIcon.ResetListener();
        gridControllerInIcon.SetReference();
        gridControllerInIcon.UpdateGrid();
        icon.gameObject.name = gridControllerInIcon.folderParentChild.name;


        // Debug.LogError(iconNumberHierarchy + icon.gameObject.name+" "+ transform.parent.parent.gameObject.name);
    }

    private void SetView()
    {
        icon.GetComponent<RectTransform>().FullScreen(true);
        icon.GetComponent<Image>().raycastTarget = false;
    }

    private void OffName()
    {
        icon.GetComponent<IconModel>().Text.gameObject.SetActive(false);
    }

    private void SetShape()
    {
        Shape.UserProps props = icon.GetComponent<Shape>().settings;
        props.roundness = 1f;
        props.outlineSize = .3f;
    }

    public void RemoveIcon()
    {
        Destroy(icon);
    }
}