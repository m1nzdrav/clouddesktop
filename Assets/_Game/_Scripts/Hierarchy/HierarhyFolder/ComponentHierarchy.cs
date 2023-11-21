using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentHierarchy : MonoBehaviour
{
    [SerializeField] private Parent currentFolder;


    [SerializeField] private GameObject rowsPrefabe;
    [SerializeField] private Transform parentRows;

    [SerializeField] private GameObject prefabeHierarchy;

    [SerializeField] private HierarchyModel _hierarchyModel;

    [SerializeField] private int numRows;
    [SerializeField] private Button btn;

    [SerializeField] private Text value;

    [SerializeField] private List<ComponentHierarchy> childs;
    [SerializeField] private GameObject newIcon;

    [SerializeField] private GameObject icon;

    public GameObject Icon => icon;

    public Button Btn
    {
        get => btn;
        set => btn = value;
    }

    public int NumRows
    {
        get => numRows;
        set => numRows = value;
    }


    public void SetParametrs(GameObject _currentIcon, int _numRows, HierarchyModel hierarchyModel)
    {
        SetLinks(_currentIcon, _numRows, hierarchyModel);

        for (int i = 0; i < numRows; i++)
        {
            Instantiate(rowsPrefabe, parentRows).transform.SetAsFirstSibling();
        }

        btn.onClick.AddListener(SpawnChild);

        CreateNewIcon();

        if (currentFolder.Childs.Count == 0)
            Btn.interactable = false;
        else
            btn.GetComponent<CircleMenuButtonController>().BorderEnabled();
    }

    private void SetLinks(GameObject _currentIcon, int _numRows, HierarchyModel hierarchyModel)
    {
        childs = new List<ComponentHierarchy>();
        _hierarchyModel = hierarchyModel;
        currentFolder = _currentIcon.GetComponent<Parent>();
        numRows = _numRows;
        value.text = _currentIcon.GetComponent<IconModel>().name;
        icon = _currentIcon;
        Btn.GetComponent<Image>()
            .color = _currentIcon.GetComponent<Image>().color;
    }

    private void CreateNewIcon()
    {
        newIcon = Instantiate(icon, btn.transform.parent);
        // newIcon.SetActive(true);
        newIcon.transform.SetSiblingIndex(btn.transform.GetSiblingIndex() + 1);

        SetLayoutElement();
        SetSize();
        SetContentSizeFitter();
        DisableText();
    }

    private void SetLayoutElement()
    {
        LayoutElement layoutElement = newIcon.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 60f;
        layoutElement.preferredWidth = 60f;
    }

    private void SetSize()
    {
        newIcon.transform.localScale *= 0.5f;
    }

    private void SetContentSizeFitter()
    {
        ContentSizeFitter contentSizeFitter = newIcon.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    private void DisableText()
    {
        newIcon.GetComponent<IconModel>().Text?.gameObject.SetActive(false);
    }

    private void SpawnChild()
    {
        if (currentFolder.Childs.Count <= 0 || currentFolder.Childs.Count == childs.Count) return;

        foreach (GameObject child in currentFolder.Childs)
        {
            GameObject obj = _hierarchyModel.SpawnChild();

            GetComponent<LineInList>().ListToChildLine(obj);

            ComponentHierarchy objComponentHierarchy = obj.GetComponent<ComponentHierarchy>();

            objComponentHierarchy.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);

            _hierarchyModel.Childs.Add(objComponentHierarchy);
            childs.Add(objComponentHierarchy);

            objComponentHierarchy.SetParametrs(child, 1 + numRows, _hierarchyModel);
            objComponentHierarchy.gameObject.SetActive(true);
        }
    }


    private void OnDestroy()
    {
        childs.Clear();
    }
}