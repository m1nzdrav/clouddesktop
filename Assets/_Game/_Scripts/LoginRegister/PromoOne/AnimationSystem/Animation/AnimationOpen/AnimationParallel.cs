using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AnimationParallel : AnimationOpen
{
    private AnimationOpenType _animationOpenType = AnimationOpenType.Parallel;


    [SerializeField] private AnimationPointJson animationPointJson;
    [SerializeField] private Transform _dotTransform;
    private AudioSource _audioSource;
    
    public override void StartState()
    {
        
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartAnimation());
        ActivateCanvas();
    }

    public override void EndState()
    {
        _parentAnimationStateChanger?.ChangeState();
    }

    public override void FastStartState()
    {
        StartCoroutine(FastStartAnimation());
        ActivateCanvas();
    }

    public override IEnumerator FastStartAnimation()
    {
        yield return null;

        FastCheckConsistently();
    }

    private void FastCheckConsistently()
    {
        
     
        _dotTransform.DOScale(Vector3.one, 0)
            .OnComplete(FastTryEnd);
    }
    private void FastTryEnd()
    {
        if (thisAnimationStateChanger != null)
            thisAnimationStateChanger.FastStartState(this);
        else
            FastEndState();
    }

    public override void FastEndState()
    {
        _parentAnimationStateChanger?.FastChangeState();
    }

    public override void ReadyEndState()
    {
        StopAllCoroutines();
        // AnimationFade();
    }
    
    public override void SetSetting(Content content)
    {
        animationPointJson = content.animationPointJson;
        _dotTransform = transform;
    }

    public override IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(animationPointJson.delay);

        CheckConsistently();
    }

    private void CheckConsistently()
    {

        _dotTransform.DOScale(Vector3.one, animationPointJson.time)
            .OnComplete(()=>
            {
                _audioSource.Play();
;                TryEnd();
            });
        
        // RegisterSingleton._instance.MusicStock.PlayMusic();
    }

    private void TryEnd()
    {
        if (thisAnimationStateChanger != null)
            thisAnimationStateChanger.StartState(this);
        else
            EndState();
    }

    private void ActivateCanvas()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}