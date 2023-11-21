using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class TextAnimation : MonoBehaviour
{
    [SerializeField] private Text mainText;
    [SerializeField] private string fullText;

    [Header("InputAnimation")] [SerializeField]
    private float timeToChar;

    [Header("FadeAnimation"), SerializeField]
    private float timeToFade;

    public void StartFadeAnimation()
    {
        mainText.DOFade(1, timeToFade);
    }

    public void StartInputAnimation()
    {
        StartCoroutine(CustomAnimationInput(mainText));
    }

    IEnumerator CustomAnimationInput(Text textField)
    {
        WaitForSeconds test = new WaitForSeconds(timeToChar);

        foreach (var oneChar in fullText)
        {
            textField.text += oneChar;
            yield return test;
        }
    }
}