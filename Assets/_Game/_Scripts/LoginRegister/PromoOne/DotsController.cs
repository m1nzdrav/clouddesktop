using System.Collections;
using System.Linq;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using UnityEngine;
using UnityEngine.Events;


public class DotsController : MonoBehaviour
{
    private UnityAction _afterLoad;

    private static DotsController _instance;

    [SerializeField] private AnimationStateChanger animationStateChanger;

    public static DotsController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DotsController>();
            }

            return _instance;
        }
    }

    [SerializeField] private AnimationState currentState;

    public void FastStartInitAnimation(Paragraphs paragraphs, int[] pagesToOpen)
    {
        animationStateChanger = new AnimationStateChanger();
        int ignoreState = AddFirstState(paragraphs, pagesToOpen);

        for (int i = 0; i < ignoreState; i++)
        {
            paragraphs.Pages[pagesToOpen[0] - 1].Paragraphs[i].gameObject.SetActive(true);
        }

        ignoreState++; // т. к перечисление мы долшжны начать игнорируя элемент который ввели
        CheckAllSubs(paragraphs, pagesToOpen, ignoreState); // добавление остальных субтитров в animationstate
        animationStateChanger.FastStartState();
    }

    public void StartInitAnimation(Paragraphs paragraphs, int[] pagesToOpen)
    {
        animationStateChanger = new AnimationStateChanger();
        int ignoreState = AddFirstState(paragraphs, pagesToOpen);

        for (int i = 0; i < ignoreState; i++)
            paragraphs.Pages[pagesToOpen[0] - 1].Paragraphs[i].gameObject.SetActive(true);

        ignoreState++; // т. к перечисление мы долшжны начать игнорируя элемент который ввели
        CheckAllSubs(paragraphs, pagesToOpen, ignoreState); // добавление остальных субтитров в animationstate
        animationStateChanger.StartState();
    }

    private int AddFirstState(Paragraphs paragraphs, int[] pagesToOpen)
    {
        PromoOneStrController firstSub = paragraphs.Pages[pagesToOpen[0] - 1].Paragraphs
            .Find(x => x.Dot.GetComponent<AnimationOpen>() != null);
        currentState = firstSub.Dot.GetComponent<AnimationOpen>();
        firstSub.gameObject.SetActive(true);
        animationStateChanger.AddStateToList(currentState);
        
        return paragraphs.Pages[pagesToOpen[0] - 1].Paragraphs.FindIndex(x => x == firstSub);
    }

    private void CheckAllSubs(Paragraphs paragraphs, int[] pagesToOpen, int startParagraphs)
    {
        for (int i = 0; i < paragraphs.Pages.Count; i++)
        {
            if (pagesToOpen.Contains(i + 1))
            {
                AddSuitableSubs(paragraphs, startParagraphs, i); // добавление подходящих субтитров
                startParagraphs = 0;
            }
            else
                DeactivateSubs(paragraphs, i); // Отключение неподходящих
        }
    }

    private void AddSuitableSubs(Paragraphs paragraphs, int startParagraphs, int i)
    {
        for (var index = startParagraphs; index < paragraphs.Pages[i].Paragraphs.Count; index++)
        {
            PromoOneStrController sub = paragraphs.Pages[i].Paragraphs[index];
            sub.gameObject.SetActive(true);
            AddChildState(sub);
        }
    }

    private void AddChildState(PromoOneStrController sub)
    {
        if (sub.Dot.TryGetComponent(out AnimationOpen animationOpen))
        {
            currentState = animationOpen;
            animationStateChanger.AddStateToList(currentState);
        }
        else if (sub.TryGetComponent(out AnimationText animationText))
        {
            currentState = animationText;
            animationStateChanger.AddStateToList(currentState);
        }
    }

    private static void DeactivateSubs(Paragraphs paragraphs, int i)
    {
        foreach (PromoOneStrController subs in paragraphs.Pages[i].Paragraphs) subs.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _afterLoad?.Invoke();
        _afterLoad = null;
    }

 
}