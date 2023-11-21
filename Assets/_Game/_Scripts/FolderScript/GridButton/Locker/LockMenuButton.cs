using System;
using System.Collections;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LockMenuButton : LockButton
{
    [SerializeField] private Image mainImage, lockerImage, openLockerImage;
    [SerializeField] private Image glow;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float waitDuration = 1;
    [SerializeField] private float animationChange = .5f;
    [SerializeField] private UnityEvent _eventWithOpen;
    [SerializeField] private UnityEvent _eventLocker;
    [SerializeField] private UnityEvent _eventAfterOpen;
    private SequenceController _sequenceController;

    private void Start()
    {
        _sequenceController = new SequenceController();
    }

    public void FastUnLock()
    {
        locked = false;
    }

    [Button]
    public override void TryOffLocker()
    {
        if (!locked) return;

        _eventWithOpen?.Invoke();
        OpenLocker();
    }

    private void OpenLocker()
    {
        _sequenceController._same.Join(glow.DOFade(1, .5f).OnPlay(() =>
        {
            glow.gameObject.SetActive(true);
            audioSource.PlayOneShot(audioClip);
        }));
        _sequenceController._same.Append(glow.DOFade(0, .5f));
        _sequenceController._same.AppendInterval(.5f);

        _sequenceController._same.Append(lockerImage.transform.DORotate(new Vector3(0, 180, 90), waitDuration / 2)
            .OnPlay(() => _eventLocker?.Invoke()));
        _sequenceController._same.Join(openLockerImage.DOFade(1, waitDuration / 2));
        _sequenceController._same.Join(lockerImage.DOFade(0, waitDuration / 2));

        _sequenceController._same.AppendInterval(1f);

        _sequenceController.PlaySequence(() =>
        {
            ChangeSpriteLocker();
            _eventAfterOpen?.Invoke();
        });

        // glow.DOFade(1, .5f).OnComplete(() =>
        // {
        //     glow.DOFade(0, .5f).OnComplete(() =>
        //     {
        //         _eventLocker?.Invoke();
        //         lockerImage.transform.DORotate(new Vector3(0, 180, 90), waitDuration / 2);
        //         openLockerImage.DOFade(1, waitDuration / 2);
        //         lockerImage.DOFade(0, waitDuration / 2).OnComplete(() =>
        //         {
        //             ChangeSpriteLocker();
        //             _eventAfterOpen?.Invoke();
        //         });
        //     });
        // });
    }

    private void ChangeSpriteLocker()
    {
        // openLockerImage.DOFade(1, waitDuration * 2);
        // lockerImage.DOFade(0, waitDuration * 2).OnComplete(() =>
        // {
        locked = false;
        StartCoroutine(WaitChangeLocker());
        // });
    }


    IEnumerator WaitChangeLocker()
    {
        yield return new WaitForSeconds(animationChange);

        // lockerImage.transform.DOLocalRotate(new Vector3(0, 0, 0), waitDuration);
        lockerImage.DOFade(0, waitDuration);
        openLockerImage.DOFade(0, waitDuration)
            .OnComplete((() => mainImage.GetComponent<SpriteAnimation>().ChangeStateDissolve()))
            ;
        ;
        // mainImage.DOFade(1, waitDuration)
        //    ;

        // mainImage.GetComponent<SpriteAnimation>().ChangeStateDissolve();
    }
}