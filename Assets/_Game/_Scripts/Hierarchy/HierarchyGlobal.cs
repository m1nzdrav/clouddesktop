using System.Collections.Generic;
using UnityEngine;

public class HierarchyGlobal : MonoBehaviour, ISingleton
{
    [SerializeField] private List<ParentHierarchy> childs;
    

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
    }
    public void AddChild(ParentHierarchy child, List<int> to)
    {
        if (to.Count == 0)
        {
            child.myNumber = childs.Count;
            child.parent = null;
            childs.Add(child);
        }
        else
        {
            int thisNumber = to[0];
            to.RemoveAt(0);
            childs[thisNumber].AddChild(child, to);
        }
    }

    public void RemoveChild(List<int> from)
    {
        if (from.Count == 1)
        {
            childs[from[0]].parent = null;
            childs.RemoveAt(from[0]);
        }
        else
        {
            int thisNumber = from[0];
            from.RemoveAt(0);
            childs[thisNumber].RemoveChild(from);
        }
    }

    public void ChangeChild(List<int> from, List<int> to)
    {
        AddChild(GetChild(from), to);
        RemoveChild(from);
    }

    public ParentHierarchy GetChild(List<int> from)
    {
        if (from.Count == 1)
            return childs[from[0]];

        int thisNumber = from[0];
        from.RemoveAt(0);

        return childs[thisNumber].GetChild(from);
    }

    public void SetParent(ParentHierarchy parentHierarchy)
    {
        parentHierarchy.FolderTransform.SetParent(parentHierarchy.GetActiveParent());
    }

    public void SetChild(ParentHierarchy parentHierarchy)
    {
        parentHierarchy.GetActiveChild().ForEach(x => x.SetParent(parentHierarchy.FolderTransform));
    }
}