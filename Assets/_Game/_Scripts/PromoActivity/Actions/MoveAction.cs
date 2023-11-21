using DG.Tweening;
using UnityEngine;

public class MoveAction : Activity
{
    [SerializeField] private Transform purposeTransform;
    [SerializeField] private Transform movingPosition;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private bool isActivity = true;

    public override void StartActivity()
    {
        if (purposeTransform==null || movingPosition==null)
            return;
        
        startPosition = transform.localPosition;
        purposeTransform.DOMove(movingPosition.position, 1f).OnComplete(EndActivity);
    }

    public override void EndActivity()
    {
        if (isActivity) NextActivity(null);
    }

    public void ReturnToStartPosition()
    {
        purposeTransform.DOMove(startPosition, 1);
    }

    public void FastMove()
    {
        startPosition = transform.localPosition;
        purposeTransform.position = movingPosition.position;
    }
}