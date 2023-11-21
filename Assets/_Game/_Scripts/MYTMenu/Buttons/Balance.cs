using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    [SerializeField] private InputField balanceText;

    [SerializeField] private GameObject balanceName;

    // [SerializeField] private CanvasGroup balanceCanvas;
    [SerializeField] private int countBalance;
    [SerializeField] private bool firstCoin = true;
    [SerializeField] private Image lockImage;
    [SerializeField] private float waitDuration = 1f;
    [SerializeField] private Image unLockImage;
    [SerializeField] private Image imageBalance;
    [SerializeField] private Image glowBalance;
    [SerializeField] private float glowFadeAnimationTime;

    public void AddBalance()
    {
        if (firstCoin)
        {
            StartCoroutine(FirstAnim());
        }
        else
        {
            IncBalance();
        }
    }

    private void IncBalance()
    {
        countBalance++;
        balanceText.text = countBalance+"/50";
    }

    IEnumerator FirstAnim()
    {
        RotateLocker();
        yield return new WaitForSeconds(waitDuration);
        DisableLocker();
        IncBalance();
        imageBalance.DOFade(1, waitDuration).OnComplete(GlowFadeAnimation);
        balanceName.SetActive(true);
        firstCoin = false;
        // balanceCanvas.DOFade(1, waitDuration);
    }

    private void GlowFadeAnimation()
    {
        glowBalance.DOFade(1, glowFadeAnimationTime).OnComplete(() => glowBalance.DOFade(0, glowFadeAnimationTime));
    }

    private void RotateLocker()
    {
        lockImage.transform.DORotate(new Vector3(0, 180, 90), waitDuration / 2);
        unLockImage.DOFade(1, waitDuration / 2);
        lockImage.DOFade(0, waitDuration / 2);
    }

    private void DisableLocker()
    {
        lockImage.GetComponent<CanvasGroup>().interactable = false;
        lockImage.GetComponent<CanvasGroup>().blocksRaycasts = false;
        unLockImage.DOFade(0, waitDuration);
    }
}