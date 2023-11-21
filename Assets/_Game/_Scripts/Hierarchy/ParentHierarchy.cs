using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParentHierarchy
{

    public DataForObject DataForObject;
    public Transform FolderTransform;
    public int myNumber;
    public List<ParentHierarchy> childs;
    public ParentHierarchy parent;

    public ParentHierarchy()
    {
        childs = new List<ParentHierarchy>();
    }

    public void AddChild(ParentHierarchy child, List<int> to)
    {
        if (to.Count == 0)
        {
            child.myNumber = childs.Count;
            child.parent = this;
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
    }

    public List<int> GetListHierarchy()
    {
        List<int> tempList = parent != null ? parent.GetListHierarchy() : new List<int>();
        tempList.Add(myNumber);

        return tempList;
    }

    public ParentHierarchy GetChild(List<int> from)
    {
        if (from.Count == 1)
            return childs[from[0]];

        int thisNumber = from[0];
        from.RemoveAt(0);

        return childs[thisNumber].GetChild(from);
    }

    public Transform GetActiveParent()
    {
        return parent.FolderTransform.gameObject.activeSelf && parent.FolderTransform != null ? parent.FolderTransform : parent.GetActiveParent();
    }

    public List<Transform> GetActiveChild()
    {
        List<Transform> tempList = new List<Transform>();
        if (FolderTransform.gameObject.activeSelf)
        {
            tempList.Add(FolderTransform);
            return tempList;
        }

        childs.ForEach(x => tempList.AddRange(x.GetActiveChild()));
        return tempList;
    }
}