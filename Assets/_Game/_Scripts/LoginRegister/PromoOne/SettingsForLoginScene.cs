using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsForLoginScene : ParagraphController, IPointerExitHandler, IPointerEnterHandler,
    IPointerClickHandler
{
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

    public Animation OpenLineSixPrefab;
    public Animation OpenLineSixChildSubPrefab;


    private Animation _openLine;

    private static bool _openLockAnimation;

    public bool OpenLock;


    private DropdownCodeCountry _dropdownCodeCountry;

    public void InstantiateSettingsForLoginPage(PromoOneStrController sub, bool isArabic,
        VerticalLayoutGroup verticalLayoutGroup, PromoOneInstantiate promoOneInstantiate)
    {
        _verticalLayoutGroup = verticalLayoutGroup;

        _openLine = Instantiate(OpenLineSixPrefab, sub.transform);

        _openLine.transform.SetAsFirstSibling();

        if (sub.Line.gameObject.activeSelf)
        {
            // sub.Line = _openLine;
        }
    }

    public void ShowLine()
    {
        _openLine.Play("GlowLineOpen");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var sub = GetComponent<PromoOneStrController>();

        if (sub.Active && !OpenLock && sub.Line.gameObject.activeSelf)
        {
            _openLine.Play("GlowLineOpen");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var sub = GetComponent<PromoOneStrController>();

        if (sub.Active && !OpenLock && sub.Line.gameObject.activeSelf)
        {
            _openLine.Play("GlowLineClose");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PromoOneStrController item = GetComponent<PromoOneStrController>();

        if (!item.Active) return;

        CircleSettings circleSettings = item.CircleSettings;
        var circleCount = circleSettings.CirclesSettings.Count;

        if (OpenLock)
        {
            circleSettings.CircleFactory.HideCircle(circleSettings);

            OpenCloseParagraph(item.Nesting, false, true, _verticalLayoutGroup);
        }
        else
        {
            if (item.Line.transform.localScale.x == 0f)
            {
                ShowLine();
            }

            OpenCloseParagraph(item.Nesting, true, true, _verticalLayoutGroup);

            circleSettings.CircleFactory.ShowCircle(circleSettings);
        }

        OpenLock = !OpenLock;
    }
}