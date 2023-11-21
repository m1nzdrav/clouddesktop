using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FullSizeHidingShowing : MonoBehaviour, ISingleton
{ 

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
        CheckRegister();
    }

    [SerializeField] private List<CanvasGroup> fadingObj;
    [SerializeField] private float animationDuration;

    public void Hide()
    {
        fadingObj.ForEach(x => x.DOFade(0, animationDuration).OnComplete(() => x.gameObject.SetActive(false)));
    }

    public void Show()
    {
        fadingObj.ForEach(x =>
        {
            x.gameObject.SetActive(true);
            x.DOFade(1, animationDuration);
        });
    }
}