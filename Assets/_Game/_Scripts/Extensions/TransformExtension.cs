using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static List<T> FindAmongChilds<T>(this Transform transform, Action<T> onFind)
    {
        List<T> result = new List<T>();
        if(transform.TryGetComponent(out T component))
        {
            result.Add(component);
            onFind?.Invoke(component);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            result.AddRange(transform.GetChild(i).FindAmongChilds<T>(onFind));
        }
        return result;
    }

    public static bool TryFindAmongParents<T>(this Transform transform, out T result)
    {
        Transform transformToCheck = transform;
        result = default;
        while(transformToCheck.parent != null)
        {
            transformToCheck = transformToCheck.parent;
            if(transformToCheck.gameObject.TryGetComponent(out result))
            {
                return true;
            }
        }
        return false;
    }
}
