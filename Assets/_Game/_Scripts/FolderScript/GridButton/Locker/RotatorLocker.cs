using System.Collections;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game._Scripts.FolderScript.GridButton.Locker
{
    public class RotatorLocker : LockButton
    {
        [SerializeField] private Image lockerImage, openLockerImage;
        [SerializeField] private Image glow;
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

        [Button]
        public override void TryOffLocker()
        {
            if (!locked) return;
            //
            // if (isRotateLocker)
            //     ChekLocker();
            _eventWithOpen?.Invoke();
            OpenLocker();
        }

        private void OpenLocker()
        {
            _sequenceController._same.Join(glow.DOFade(1, .5f));
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

            // lockerImage.transform.DORotate(new Vector3(0, 180, 90), waitDuration / 2);
            // openLockerImage.DOFade(1, waitDuration / 2);
            // lockerImage.DOFade(0, waitDuration / 2).OnComplete(ChangeSpriteLocker);
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
                // .OnComplete((() => mainImage.GetComponent<SpriteAnimation>().ChangeStateDissolve()))
                ;
            ;
            // mainImage.DOFade(1, waitDuration)
            //    ;

            // mainImage.GetComponent<SpriteAnimation>().ChangeStateDissolve();
        }
    }
}