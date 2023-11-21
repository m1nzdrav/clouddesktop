using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


public class PivotForScrollAnim : ScrollAnimation
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 basePivot;
    [SerializeField] private Vector2 newPivot = new Vector2(.5f, .5f);
    [SerializeField] private float animDuration = 1f;
    [SerializeField] private Vector2 tempPivot;

    new void Start()
    {
        base.Start();
        tempPivot = newPivot - basePivot;
    }
    
    [Button]
    public void Animate()
    {
        rectTransform.DOPivot(newPivot, animDuration);
    }

    [Button]
    public void Return()
    {
        rectTransform.DOPivot(basePivot, animDuration);
    }


    public override void ActionMin()
    {
        Debug.LogError(gameObject.name + "max");
    }

    public override void ActionMax()
    {
        Debug.LogError(gameObject.name + "min");
    }

    public override void ActionAfterMin()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionAfterMax()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionScroll(float time)
    {
        if (LockAnimation)
            return;
        
        rectTransform.DOPivot(basePivot+tempPivot*time , animDuration);
    }
}