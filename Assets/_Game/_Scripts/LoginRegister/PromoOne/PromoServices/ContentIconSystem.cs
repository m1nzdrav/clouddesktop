using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class ContentIconSystem
{
    public Transform containerIcon;

    public void Set(Transform container)
    {
        containerIcon = container;
    }


    public void TrySpawnIcon(Transform currentTransform, CanvasGroup _canvasGroup)
    {
        if (containerIcon != null)
        { 
            
            _canvasGroup.blocksRaycasts = true;
            containerIcon.parent = currentTransform;
            // containerIcon.localPosition =
            //     new Vector3(45f, containerIcon.localPosition.y, containerIcon.localPosition.z);
            containerIcon.gameObject.SetActive(true);
            containerIcon.DOScale(Vector3.one, .5f);
        }
    }

}