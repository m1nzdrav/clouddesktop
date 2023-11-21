using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class VectorObjectScale : MonoBehaviour
{
    [SerializeField] private Vector3 baseScale;
    [SerializeField] private Vector3 upScale;
    [SerializeField] private float timeAnimation;

    [SerializeField, FoldoutGroup("Rebuild")]
    private bool needRebuild;

    [SerializeField, FoldoutGroup("Rebuild")]
    private RectTransform transformToRebuild;

    [Button]
    public void UpScale()
    {
        if (needRebuild)
            transform.DOScale(upScale, timeAnimation).OnUpdate((() =>
                LayoutRebuilder.MarkLayoutForRebuild(transformToRebuild)));
        else

            transform.DOScale(upScale, timeAnimation);
    }

    [Button]
    public void DownScale()
    {
        if (needRebuild)
            transform.DOScale(baseScale, timeAnimation).OnUpdate((() =>
                LayoutRebuilder.MarkLayoutForRebuild(transformToRebuild)));
        else
            transform.DOScale(baseScale, timeAnimation);
    }
}