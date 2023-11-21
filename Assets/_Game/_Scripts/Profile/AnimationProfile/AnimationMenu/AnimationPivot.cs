using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


public class AnimationPivot : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 basePivot;
    [SerializeField] private Vector2 newPivot=new Vector2(.5f,.5f);
    [SerializeField] private float animDuration = 1f;
    
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

   
}