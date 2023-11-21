using System.Collections;
using _Game._Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GlowAction : Activity
{
    [SerializeField] private Button triggerObject;

    [SerializeField, FoldoutGroup("Event start end")]
    private UnityEvent startEvent, afterEnd;

    [SerializeField, FoldoutGroup("Glow")] private Image Glow;
    [SerializeField, FoldoutGroup("Glow")] private float endFadeGlow = .5f;
    [SerializeField, FoldoutGroup("Glow")] private float animationBreath = .7f;
    [SerializeField, FoldoutGroup("Glow")] private float breathIntensivity = .3f;
    [SerializeField, FoldoutGroup("Glow")] private float tempAlpha;
    private Tweener animation;
    [SerializeField] private bool isEnd = false;
    [SerializeField] private bool blockNextActivity = false;
    [SerializeField, FoldoutGroup("Audio")] private AudioSource audioSource;
    [SerializeField, FoldoutGroup("Audio")] private bool audioBeforeAnim = true;
    [SerializeField, FoldoutGroup("Audio")] private bool audioAfterAnim = false;
    private int countSoundPlay = 0;

    public override void StartActivity()
    {
        if (startEvent != null) startEvent?.Invoke();

        triggerObject.onClick.AddListener(EndActivity);

        if (!Glow.gameObject.activeSelf) Glow.gameObject.SetActive(true);

        tempAlpha = Glow.color.a;
        animation = Glow.DOFade(1, animationBreath).OnComplete(() => StartCoroutine(GlowBreath()));
    }

    [Button]
    public override void EndActivity()
    {
        triggerObject.onClick.RemoveListener(EndActivity);
        
        if (!blockNextActivity) NextActivity(Ending);
        
    }


    private void Ending()
    {
        EndBreath();
        afterEnd?.Invoke();
    }

    IEnumerator GlowBreath()
    {
        animation = Glow.DOFade(endFadeGlow, animationBreath);

        if (audioSource != null && countSoundPlay <= Config.MAX_COUNT_SOUND_GLOW && audioBeforeAnim)
        {
            audioSource?.Play();
            countSoundPlay++;
        }

        yield return new WaitForSeconds(animationBreath + breathIntensivity);
        
        if (audioSource != null && countSoundPlay <= Config.MAX_COUNT_SOUND_GLOW && audioAfterAnim)
        {
            audioSource?.Play();
            countSoundPlay++;
        }

        animation = Glow.DOFade(1, animationBreath).OnComplete(() => StartCoroutine(GlowBreath()));
    }

    public void EndBreath()
    {
        animation.Kill();
        StopAllCoroutines();
        Glow.DOFade(tempAlpha, animationBreath);
    }
}