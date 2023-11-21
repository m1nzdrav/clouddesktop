using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    [SerializeField] private ChangeLanguageLoginScene _changeLanguageLoginScene;
    [SerializeField] private List<CanvasGroup> needClose;
    [SerializeField] private float animationDuration;
    [SerializeField] private CanvasGroup SubtitleText;

    public void StartPlatform()
    {
        SubtitleText.DOFade(1, animationDuration);

        GetComponent<SelectedWindow>().SendCurrentWindow(gameObject);
        FadeAll(0);
        _changeLanguageLoginScene.PromoLanguage();
    }

    private void FadeAll(float fade)
    {
        Debug.LogError("fade");
        foreach (var VARIABLE in needClose)
        {
            VARIABLE.DOFade(fade, animationDuration);
        }
    }

    public void Return()
    {
        StartCoroutine(WaitFade());
    }

    IEnumerator WaitFade()
    {
        SubtitleText.DOFade(0, animationDuration);
        yield return new WaitForSeconds(animationDuration);
        FadeAll(1);
    }
}