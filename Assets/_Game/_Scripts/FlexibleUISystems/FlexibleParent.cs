using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleParent : MonoBehaviour
{
    [SerializeField] private float delta = 10;
    [SerializeField] private WorkspaceEntityMember workspaceEntityMember;
    
    public void CheckResize()
    {
        if (workspaceEntityMember == null)
            workspaceEntityMember = GetComponent<WorkspaceEntityMember>();
        Vector2[] childAreaOffsets = workspaceEntityMember.ChildSystem.ChildAreaOffsets;
        childAreaOffsets[0].x -= delta;
        childAreaOffsets[0].y -= delta;
        childAreaOffsets[1].x += delta;
        childAreaOffsets[1].y += delta;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetOffsetMin(childAreaOffsets[0]);
        rectTransform.SetOffsetMax(childAreaOffsets[1]);
    }
}
