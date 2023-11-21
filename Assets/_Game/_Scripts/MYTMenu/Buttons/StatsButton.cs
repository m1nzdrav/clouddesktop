using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatsButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup myCanvas;
    [SerializeField] private GameObject glowStats;
    [SerializeField] private Text statsText;

    [SerializeField] private int maxCount;
    [SerializeField] private int currentCount = 0;
    [SerializeField] private EnterChoseShower EnterChoseShower;
    [SerializeField] private LockButton lockButton;
    [SerializeField] private float _timeToActivate = 3f;

    [SerializeField, Header("actionsActivate")]
    private List<UnityEvent> events;

    [SerializeField, Header("Balance")] private Transform coin;
    [SerializeField] private Transform positionBalance;
    [SerializeField] private float waitCoin = .5f;
    [SerializeField] private Balance _balance;
    [SerializeField] private bool needBlockRaycast = false;
    [SerializeField] private bool needBlockInteractable = false;

    private int CurrentCount
    {
        get => currentCount;
        set
        {
            currentCount = value;

            statsText.text = currentCount + "/" + maxCount;
        }
    }

    private void Start()
    {
        CurrentCount = currentCount;
    }

    public void FastActivate()
    {
        FastActivateCanvas();

        glowStats.SetActive(true);
        EnterChoseShower.Block = true;

        SetNextCurrent();
    }

    public void Activate()
    {
        StartCoroutine(IEActivateCanvas());

        if (lockButton != null)
        {
            lockButton.TryOffLocker();
        }

        glowStats.SetActive(true);
        EnterChoseShower.Block = true;
        StartAction();
        SetNextCurrent();
        // StartCoroutine(WaitCoin());
    }

    IEnumerator IEActivateCanvas()
    {
        yield return new WaitForSeconds(_timeToActivate);
        ActivateCanvas();
    }

    private void ActivateCanvas()
    {
        myCanvas.DOFade(1, 1f);

        if (!needBlockRaycast) myCanvas.blocksRaycasts = true;

        if (!needBlockInteractable) myCanvas.interactable = true;
    }

    private void FastActivateCanvas()
    {
        myCanvas.alpha = 1;
        myCanvas.interactable = true;
        myCanvas.blocksRaycasts = true;
    }

    public void DeActivate()
    {
        EnterChoseShower.Block = false;
        // glowStats.SetActive(false);
    }

    private void SetNextCurrent()
    {
        if (currentCount >= maxCount)
            return;

        CurrentCount++;
    }

    [Button]
    public void StartCoin()
    {
        // StartCoinAnimation();
        StartCoroutine(WaitCoin());
    }

    IEnumerator WaitCoin()
    {
        yield return new WaitForSeconds(waitCoin);
        StartCoinAnimation();
    }

    private void StartCoinAnimation()
    {
        coin.position = statsText.transform.position;
        // coin.localScale = Vector3.one;
        coin.gameObject.SetActive(true);
        (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same.Append(
            glowStats.transform.DOScaleY(3f, .2f));
        // RegisterSingleton._instance.Sequence.sequenceShow._same.Append();
        (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same.Append(
                coin.DOMove(positionBalance.position,
                    1))
            .Join(glowStats.transform.DOScaleY(1f, .2f))
            .Join(coin.DOScale(Vector3.one, .2f)); // движение к балансу
        (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same.Append(
            coin.DOScale(new Vector3(1.5f, 1.5f),
                .2f)); // увеличение до замедленной точки
        (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same.Append(
            coin.DOScale(new Vector3(1.9f, 1.9f),
                .4f)); // медленное увеличение до верхней точки
        (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same.Append(
            coin.DOScale(new Vector3(1.5f, 1.5f),
                .4f)); // медленное падение до замедленной точки
        (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow._same.Append(
            coin.DOScale(Vector3.zero,
                .2f)); // полное падение
        (RegisterSingleton._instance.GetSingleton(typeof(SameSequence)) as SameSequence)?.sequenceShow.PlaySequence(
            _balance.AddBalance);
    }

    private void StartAction()
    {
        if (currentCount >= events.Count)
            return;

        events[currentCount]?.Invoke();
    }

    public void EnableText()
    {
        statsText.gameObject.SetActive(true);
    }

    public void DisableText()
    {
        statsText.gameObject.SetActive(false);
    }
}