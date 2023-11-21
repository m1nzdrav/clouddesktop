using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Border : MonoBehaviour
{
    [SerializeField] private GameObject mainFolder;

    [SerializeField] private List<Color> accessColor;

    public GameObject Glow;

    [SerializeField] private Text numberHierarchyText;

    private EventTriggerSystem _eventTrigger;

    public Text NumberHierarchyText
    {
        get => numberHierarchyText;
        set => numberHierarchyText = value;
    }

    private bool _isDrag;

    public List<Color> AccessColor
    {
        get => accessColor;
        set => accessColor = value;
    }

    public GameObject MainFolder => mainFolder;

    private void Start()
    {
        if (MainFolder == null)
        {
            mainFolder = transform.parent.gameObject;
        }

        _eventTrigger = mainFolder.GetComponent<EventTriggerSystem>();

        if (_eventTrigger != null)
        {
            _eventTrigger.ActionEnter.AddListener(GlowOn);
            _eventTrigger.ActionExit.AddListener(GlowOff);
        }
        else
        {
            EventTrigger trigger = mainFolder.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { GlowOn(); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => { GlowOff(); });
            trigger.triggers.Add(entry);
        }

        //if (_animGlow == null) return;

        //_animGlow.SetActive(false);
    }

    public void SetColor(Color color)
    {
        GetComponent<Image>().color = color;

        var childBorder = Glow.GetComponent<ChildBorder>();

        if (childBorder is null) return;

        Glow.GetComponent<ChildBorder>().SetColor(color);
    }

    public void GlowOn()
    {
        Glow.SetActive(true);
    }

    public void GlowOff()
    {
        Glow.SetActive(false);
    }

    private void OnDestroy()
    {
        _eventTrigger?.ActionEnter?.RemoveAllListeners(); //todo не отписывается от ивента - переделать ивенты!
        _eventTrigger?.ActionExit?.RemoveAllListeners();
    }
}