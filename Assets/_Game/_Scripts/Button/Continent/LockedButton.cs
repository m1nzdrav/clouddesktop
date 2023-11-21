using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LockedButton : MonoBehaviour
{
    private bool isUnlocked = false;
    [SerializeField] private Image lockImage, unLockImage;
    [SerializeField] private CanvasGroup canvasText;
    [SerializeField] private float waitDuration = .3f;
    [SerializeField] private float fadeDuration = .5f;


    public void Unlock()
    {
        if (isUnlocked)
            return;

        isUnlocked = true;

        StartCoroutine(WaitUnlock());
    }

    IEnumerator WaitUnlock()
    {
        // unLockImage.gameObject.SetActive(true);
        lockImage.transform.DORotate(new Vector3(0, 180, 90), waitDuration / 2);
        unLockImage.DOFade(1, waitDuration/2);
        lockImage.DOFade(0, waitDuration/2);


        yield return new WaitForSeconds(waitDuration);

        unLockImage.DOFade(0, fadeDuration);
        canvasText?.DOFade(1, fadeDuration);
    }
}