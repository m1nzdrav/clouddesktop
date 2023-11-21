using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ChildSystem
{
    public WorkspaceEntityMember my;
    public WorkspaceEntityMember parent;
    public List<WorkspaceEntityMember> child;

    public Vector2[] ChildAreaOffsets
    {
        get
        {
            if (child != null && child.Count > 0)
            {
                //TODO: убрать GetComponent и хранить RectTransform внутри WorkspaceEntityMember
                Vector2[] minMax = { child[0].GetComponent<RectTransform>().GetOffsetMin(), child[0].GetComponent<RectTransform>().GetOffsetMax() };
                for (int i = 1; i < child.Count; i++)
                {
                    minMax = CheckMinMax(minMax, new Vector2[]{child[i].GetComponent<RectTransform>().GetOffsetMin(), child[i].GetComponent<RectTransform>().GetOffsetMax()});
                    minMax = CheckMinMax(minMax, child[i].ChildSystem.ChildAreaOffsets);
                }

                return minMax;
            }

            return null;
        }
    }

    private Vector2[] CheckMinMax(Vector2[] oldMinMax, Vector2[] newMinMax)
    {
        if (newMinMax[0].x < oldMinMax[0].x)
            oldMinMax[0].x = newMinMax[0].x;
        if (newMinMax[0].y < oldMinMax[0].y)
            oldMinMax[0].y = newMinMax[0].y;
        if (newMinMax[1].x > oldMinMax[1].x)
            oldMinMax[1].x = newMinMax[1].x;
        if (newMinMax[1].y > oldMinMax[1].y)
            oldMinMax[1].y = newMinMax[1].y;
        return oldMinMax;
    }
}