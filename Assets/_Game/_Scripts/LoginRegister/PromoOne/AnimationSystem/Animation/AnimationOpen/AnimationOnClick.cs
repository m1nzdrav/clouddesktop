using System;
using System.Collections;
using _Game._Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationOnClick : AnimationOpen, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    private AnimationOpenType _animationOpenType = AnimationOpenType.OnClick;


    [SerializeField] private AnimationPointJson animationPointJson;
    [SerializeField] private Transform _dotTransform;
    [SerializeField] private Transform _lampTransform;
    [SerializeField] private GameObject _triangleObj;
    [SerializeField] private bool isPlayed;
    private bool _readyLamp = false;
    private AudioSource _audioSource;
    private float countGlow = 0;
    private void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();
        // gameObject.GetComponent<EventTrigger>().AddEventTrigger(OnPointerClick, EventTriggerType.PointerClick);
    }

    public override void StartState()
    {
        StartCoroutine(StartAnimation());
        ActivateCanvas();
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

    private void ActivateCanvas()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        CanvasGroup parent = transform.parent.GetComponent<CanvasGroup>();
        parent.interactable = true;
        parent.blocksRaycasts = true;
    }

    public override void EndState()
    {
        _parentAnimationStateChanger?.ChangeState();
    }

    public override void SetSetting(Content content)
    {
        animationPointJson = content.animationPointJson;
        _dotTransform = transform;
        PromoOneStrController parent = transform.parent.GetComponent<PromoOneStrController>();

        _lampTransform = parent.Lamp.transform;
        _triangleObj = parent.TriangleImage;
    }

    public override IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(animationPointJson.delay);
        _readyLamp = true;

        StartCoroutine(AnimationBreath());
    }

    private IEnumerator AnimationBreath()
    {
        _dotTransform.DOScale(Vector3.one * 2.5f, animationPointJson.time);
        
        if (countGlow<=Config.MAX_COUNT_SOUND_GLOW)
        {
            _audioSource.Play();
            countGlow++;
        }
        
        yield return new WaitForSeconds(animationPointJson.time);

        _dotTransform.DOScale(Vector3.one, animationPointJson.time)
            .OnComplete((() =>
            {
                if (!isPlayed && gameObject.activeInHierarchy)
                {
                    StartCoroutine(AnimationBreath());
                }
            }));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ReadyEndState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_readyLamp)
        {
            _lampTransform.transform.DOScale(Config.VECTOR_ANIMATION_LAMP_ENTER, .3f)
                // .OnComplete((() => Lamp.transform.DOScale(Vector3.zero, .3f)))
                ;
            _triangleObj.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_readyLamp) return;

        _lampTransform.transform.DOKill();
        _lampTransform.transform.DOScale(Vector3.zero, .3f);
        _triangleObj.SetActive(true);
    }


    public override void ReadyEndState()
    {
        isPlayed = true;
        _readyLamp = false;
        AnimationFade();
    }

    public void AnimationFade()
    {
        CheckConsistently();
    }

    private void CheckConsistently()
    {
        DoConsistently();
    }

    private void DoParallel()
    {
        _lampTransform.DOPunchScale(Vector3.one, animationPointJson.time, animationPointJson.countShake)
            .OnComplete((() => _lampTransform.localScale = Vector3.zero));
        TryEnd();
    }

    private void DoConsistently()
    {
        TryEnd();
        _lampTransform.localScale = Vector3.zero;
    }

    private void TryEnd()
    {
        if (thisAnimationStateChanger != null)
            thisAnimationStateChanger.StartState(this);
        else
            EndState();
    }
}