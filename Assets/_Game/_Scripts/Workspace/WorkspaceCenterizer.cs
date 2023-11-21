using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkspaceCenterizer : MonoBehaviour
{
    [SerializeField] private float targetRectangleAreaDevider = 2;
    [SerializeField] private bool calculateTwice = false;
    [SerializeField] private SimpleZoom simpleZoom;
    [Space]
    /*[SerializeField] private GameObject testPoint;
    [SerializeField] private Transform testParent;
    [SerializeField] private RectTransform canvas;*/
    [SerializeField] private List<WorkspaceEntityMember> members;

    [Button]
    public void CenterizeMembersFromInspector()
    {
        CenterizeByMass(members);
    }

    public void CenterizeByMass(List<WorkspaceEntityMember> workspaceEntityMembers)
    {
        MoveToPoint(FindCenterOfMass(workspaceEntityMembers));
        (Vector2, Vector2) minMax = FindMinMax(workspaceEntityMembers);
        Zoom(minMax.Item1, minMax.Item2);
    }

    public void CenterizeByCenter(List<WorkspaceEntityMember> workspaceEntityMembers)
    {
        //TODO: cant move to center correctly
        (Vector2, Vector2) minMax = FindMinMax(workspaceEntityMembers);
        //Instantiate(testPoint, testParent).GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, FindCenter(workspaceEntityMembers));
        MoveToPoint(FindCenter(RectTransformUtility.WorldToScreenPoint(Camera.main, minMax.Item1), RectTransformUtility.WorldToScreenPoint(Camera.main, minMax.Item2)));
        Zoom(minMax.Item1, minMax.Item2);
    }

    private void Zoom(Vector2 min, Vector2 max)
    {
        /*Instantiate(testPoint, canvas).GetComponent<RectTransform>().position = max;
        Instantiate(testPoint, canvas).GetComponent<RectTransform>().position = min;*/
        //Find zoom
        Debug.Log(min);
        Debug.Log(max);
        float viewportArea = Screen.currentResolution.height * Screen.currentResolution.width;
        Vector2 rectangleSizeDelta = new Vector2(max.x - min.x, max.y - min.y);
        float rectangleArea = rectangleSizeDelta.x * rectangleSizeDelta.y;
        float targetRectangleArea = viewportArea / targetRectangleAreaDevider;
        float differenceMultiplier = targetRectangleArea / rectangleArea;
        if (calculateTwice)
        {
            rectangleArea *= differenceMultiplier;
            differenceMultiplier = targetRectangleArea / rectangleArea;
        }
        float targetZoom = simpleZoom.DefaultZoom * differenceMultiplier;
        //
        float clampledZoom = Mathf.Clamp(targetZoom, simpleZoom.MinMaxZoom.min, simpleZoom.MinMaxZoom.max);
        Debug.Log(targetZoom);
        Debug.Log(clampledZoom);
        simpleZoom.SetZoom(clampledZoom);
    }

    private Vector2 FindCenter(Vector2 min, Vector2 max)
    {
        return new Vector2(max.x - ((max.x - min.x) / 2), max.y - ((max.y - min.y) / 2));
    }

    private Vector2 FindCenter(List<WorkspaceEntityMember> members)
    {
        Vector2 center = Vector2.zero;
        foreach(var member in members)
        {

        }
        return center;
    }

    private Vector2 FindCenterOfMass(List<WorkspaceEntityMember> members)
    {
        Vector3 center = Vector2.zero;
        foreach (var m in members)
        {
            center += m.GetComponent<RectTransform>().position;
        }
        center = center / members.Count();
        Debug.Log(center);
        center = RectTransformUtility.WorldToScreenPoint(Camera.main, center);
        Debug.Log(center);
        return center;
    }

    private (Vector2, Vector2) FindMinMax(List<WorkspaceEntityMember> members)
    {
        //Find min max
        Vector2 min = members[0].transform.position;
        Vector2 max = members[0].transform.position;
        foreach (var m in members)
        {
            var objectToSeeSizes = m.GetComponent<RectTransform>();
            Vector2 sizeDelta = objectToSeeSizes.sizeDelta * objectToSeeSizes.localScale;// * simpleZoom.CurrentZoom / simpleZoom.DefaultZoom;
            Vector2 leftDownPos = new Vector2(objectToSeeSizes.position.x - sizeDelta.x / 2, objectToSeeSizes.position.y - sizeDelta.y / 2);
            Vector2 rightUpPos = new Vector2(objectToSeeSizes.position.x + sizeDelta.x / 2, objectToSeeSizes.position.y + sizeDelta.y / 2);
            if (leftDownPos.x < min.x)
                min.x = leftDownPos.x;
            if (leftDownPos.y < min.y)
                min.y = leftDownPos.y;
            if (rightUpPos.x > max.x)
                max.x = rightUpPos.x;
            if (rightUpPos.y > max.y)
                max.y = rightUpPos.y;
        }
        return (min, max);
    }

    private void MoveToPoint(Vector2 point)
    {
        Vector2 pivot = Vector2.zero;
        Vector2 localPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(simpleZoom.Content, point, Camera.main, out localPosition))
        {
            float x = simpleZoom.Content.pivot.x + (localPosition.x / simpleZoom.Content.rect.width);
            float y = simpleZoom.Content.pivot.y + (localPosition.y / simpleZoom.Content.rect.height);
            pivot = new Vector2(x, y);
        }
        simpleZoom.Content.pivot = pivot;
        Debug.Log(point);
        Debug.Log(pivot);
        simpleZoom.Content.DOLocalMove(Vector2.zero, 1);
    }
}
