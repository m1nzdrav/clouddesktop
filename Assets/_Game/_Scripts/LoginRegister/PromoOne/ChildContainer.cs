using System.Collections;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChildContainer : MonoBehaviour
{
    [SerializeField] private bool isOpen = false;


    [SerializeField] private bool tempIsOpen = false;
    [SerializeField] private Paragraph childs;

    [SerializeField] private Paragraph comments;
    [SerializeField] private float animationFading = .5f;
    [SerializeField] private AnimationStateChanger animationStateChanger;
    private AnimationState _currentState;
    private SequenceController _sameSequence;
    [SerializeField] private bool isPlayed;
    [SerializeField] private bool needGong;
    [SerializeField] private PromoOneStrController strController;
    public Paragraph Childs => childs;
    public Paragraph Comments => comments;

    public void InitChild(int count)
    {
        childs = new Paragraph(count);
    }

    public void InitComment(int count)
    {
        comments = new Paragraph(count);
    }

    public void SubscribeEvent(Transform _dotTransform, bool needGong)
    {
        if (childs.Paragraphs.Count <= 0) return;

        this.needGong = needGong;
    }

    public void TryOpenOnClick()
    {
        if (childs.Paragraphs.Count > 0 && childs.Paragraphs[0].Dot.GetComponent<AnimationOnClick>() != null)
            InvokeClick();
    }

    [Button]
    public void OpenChildWithAnimation()
    {
        _sameSequence = new SequenceController();
        animationStateChanger = new AnimationStateChanger();
        PromoOneStrController firstSub = childs.Paragraphs
            .Find(x => x.Dot.GetComponent<AnimationOpen>() != null);
        _currentState = firstSub.Dot.GetComponent<AnimationOpen>();
        firstSub.gameObject.SetActive(true);
        animationStateChanger.AddStateToList(_currentState);
        int startParagraphs =
            childs.Paragraphs.FindIndex(x =>
                x == firstSub);

        for (int i = 0; i < startParagraphs; i++)
        {
            childs.Paragraphs[i].gameObject.SetActive(true);
        }

        foreach (var VARIABLE in childs.Paragraphs)
        {
            VARIABLE.transform.localScale = Vector3.one;
        }

        foreach (var VARIABLE in childs.Paragraphs)
        {
            VARIABLE.transform.localScale = Vector3.one;
        }

        startParagraphs++; // т. к перечисление мы долшжны начать игнорируя элемент который ввели

        AddSuitableSub(startParagraphs);

        // OpenChild();

        animationStateChanger.StartChildState(GetComponent<PromoOneStrController>(), childs.Paragraphs.Count);
    }

    private void AddSuitableSub(int startParagraphs)
    {
        for (var index = startParagraphs; index < childs.Paragraphs.Count; index++)
        {
            PromoOneStrController VARIABLE = childs.Paragraphs[index];
            AddChildState(VARIABLE);
        }

        for (var index = 0; index < comments.Paragraphs.Count; index++)
        {
            PromoOneStrController VARIABLE = childs.Paragraphs[index];
            AddChildState(VARIABLE);
        }
    }

    private void AddChildState(PromoOneStrController sub)
    {
        if (sub.Dot.TryGetComponent(out AnimationOpen animationOpen))
        {
            _currentState = animationOpen;
            animationStateChanger.AddStateToList(_currentState);
        }
        else if (sub.TryGetComponent(out AnimationText animationText))
        {
            _currentState = animationText;
            animationStateChanger.AddStateToList(_currentState);
        }
        // if (sub.Dot.GetComponent<AnimationOpen>() == null) return;
        //
        // _currentState = sub.Dot.GetComponent<AnimationOpen>();
        // animationStateChanger.AddStateToList(_currentState);
    }

    [Button]
    private void OpenChild()
    {
        _sameSequence = _sameSequence ?? new SequenceController();

        foreach (var VARIABLE in childs.Paragraphs)
        {
            VARIABLE.transform.localScale = Vector3.zero;
            VARIABLE.gameObject.SetActive(true);
            _sameSequence._same.Join(VARIABLE.transform.DOScale(Vector3.one, animationFading)
                // .OnComplete(OpenChildWithAnimation)
            );
        }

        // _sameSequence._same.OnComplete(OpenChildWithAnimation);
        _sameSequence._same.OnUpdate(() => RebuildContent(transform.parent.GetComponent<RectTransform>()));
        _sameSequence.PlaySequence(OpenChildWithAnimation);
    }

    [Button]
    private void CloseChild()
    {
        foreach (var VARIABLE in childs.Paragraphs)
        {
            _sameSequence._same.Join(VARIABLE.transform.DOScale(Vector3.zero, animationFading)
                .OnComplete(OffChild));
        }

        _sameSequence._same.OnUpdate(() => RebuildContent(transform.parent.GetComponent<RectTransform>()));
        _sameSequence.PlaySequence(null);
    }

    private void RebuildContent(RectTransform layoutForRebuild)
    {
        LayoutRebuilder.MarkLayoutForRebuild(layoutForRebuild);
    }

    private void OffChild()
    {
        foreach (var VARIABLE in childs.Paragraphs)
        {
            VARIABLE.gameObject.SetActive(false);
            VARIABLE.ChildContainer.ChangeVariantIsOpen();
        }
    }

    private void OnChild()
    {
        foreach (var VARIABLE in childs.Paragraphs) VARIABLE.gameObject.SetActive(true);
        // VARIABLE.ChildContainer.isOpen = true;
    }

    private void ChangeVariantIsOpen()
    {
        if (isOpen) tempIsOpen = true;

        isOpen = false;
    }

    public void InvokeClick()
    {
        if (needGong && !isPlayed && childs.Paragraphs.Count > 0)
            (RegisterSingleton._instance.GetSingleton(typeof(TimelinePlayer)) as TimelinePlayer)?.Play("NewGongWithoutPause");

        isPlayed = true;

        if (isOpen)
            Close();
        else
            Open();
    }

    private void Open()
    {
        isOpen = true;
        OpenChild();
        ShowCircle();
    }

    private void Close()
    {
        isOpen = false;
        CloseChild();
        HideCircle();
    }

    private void HideCircle()
    {
        GetComponent<CircleSettings>()?.CircleFactory
            ?.HideCircle(GetComponent<CircleSettings>());
    }

    private void ShowCircle()
    {
        GetComponent<CircleSettings>()?.CircleFactory
            ?.ShowCircle(GetComponent<CircleSettings>());
    }

    public void Destroy()
    {
        HideCircle();
        foreach (var VARIABLE in childs.Paragraphs)
        {
            VARIABLE.Destroy();
        }
    }

    private void OnDisable()
    {
        if (isOpen)
        {
            tempIsOpen = true;
        }

        OffChild();
    }

    private void OnEnable()
    {
        if (!tempIsOpen) return;

        OnChild();
        isOpen = true;
        tempIsOpen = false;

        // OffChild();
    }
}