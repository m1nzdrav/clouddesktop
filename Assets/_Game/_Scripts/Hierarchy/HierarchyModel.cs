using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HierarchyModel : EventChild
{
    [SerializeField] private GameObject prefabeComponentView;
    [SerializeField] private Transform parentComponentView;
    [SerializeField] private FolderModel folderParentChild;
    [SerializeField] private List<ComponentHierarchy> childs;


    public List<ComponentHierarchy> Childs
    {
        get => childs;
        set => childs = value;
    }

    public FolderModel Folder
    {
        get => folderParentChild;
        set => folderParentChild = value;
    }

    public GameObject SpawnChild()
    {
        return Instantiate(prefabeComponentView, parentComponentView);
    }

    public void ReloadChild()
    {
        DeleteChild();
        UpdateChild();
    }

    private void UpdateChild()
    {
        CreateAndSetChild();

        var sortedList =
            childs.OrderByDescending(hierarchy => hierarchy.Icon.transform.parent.GetSiblingIndex()).ToList();

        childs = sortedList;

        SortChild();
    }

    private void DeleteChild()
    {
        foreach (ComponentHierarchy VARIABLE in childs)
        {
            Destroy(VARIABLE.gameObject);
        }

        childs.Clear();
    }

    private void OnEnable()
    {
        childs = childs ?? new List<ComponentHierarchy>();

        if (folderParentChild == null) return;

        AddMethodToListener(ReloadChild);
        RegisterEvent();
        ReloadChild();
    }

    private void CreateAndSetChild()
    {
        foreach (GameObject icon in folderParentChild.Childs)
        {
            ComponentHierarchy child = Instantiate(prefabeComponentView, parentComponentView)
                .GetComponent<ComponentHierarchy>();

            child.SetParametrs(icon, 0, this);
            child.gameObject.SetActive(true);

            childs.Add(child);
        }
    }

    private void OnDisable()
    {
        DeleteChild();
    }

    private void SortChild()
    {
        for (int i = 0; i < childs.Count; i++)
        {
            childs[childs.Count - 1 - i].transform.SetSiblingIndex(i);
        }
    }
}