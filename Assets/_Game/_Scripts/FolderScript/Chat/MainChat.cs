using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game._Scripts;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainChat : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Base link")]
    private SecondChatModel _secondChatModel;

    [SerializeField, FoldoutGroup("Base link")]
    private ChatConnector _chatConnector;

    [SerializeField, FoldoutGroup("Base link")]
    private Transform parentQuestion;

    [SerializeField, FoldoutGroup("Base link")]
    private RectTransform parentLayout;

    [SerializeField, FoldoutGroup("Base link")]
    private GameObject _question;

    [SerializeField, FoldoutGroup("Base link")]
    private VideoLeapSink videoLeapSink;

    [SerializeField, FoldoutGroup("Audio")]
    private AudioSource _audioSource;

    [SerializeField, FoldoutGroup("Audio")]
    private AudioClip bot;

    [SerializeField, FoldoutGroup("Audio")]
    private AudioClip user;

    [SerializeField] private DialogModel myDialog;
    [SerializeField] private SingleText currentTexts;
    [SerializeField] private Font font;


    public Font Font => font;

    private Dictionary<string, string> saveAnswer;
    private SequenceController _sequenceController;
    public SingleText CurrentTexts => currentTexts;

    private void Start()
    {
        saveAnswer = new Dictionary<string, string>();
        _sequenceController = new SequenceController();
        _sequenceController.SetSequence();
    }


    [Button]
    public void StartDialog(string resourcesDialog)
    {
        myDialog = DialogModel.Init(resourcesDialog);
        font =
            (RegisterSingleton._instance.GetSingleton(typeof(FontSingleton)) as FontSingleton)?.GetFont(myDialog.font);
        SpawnDialog(myDialog.singleTexts);
    }

    public void ClearDialog()
    {
        foreach (var VARIABLE in parentQuestion.GetComponentsInChildren<Question>())
        {
            Destroy(VARIABLE.gameObject);
        }
    }

    private void SpawnDialog(List<SingleText> dialog)
    {
        foreach (SingleText variable in dialog)
        {
            if (variable.needSkip) continue;

            currentTexts = variable;

            if (variable.question != null && !variable.question.IsNullOrWhitespace())
            {
                Question question = SpawnOne(variable);

                _sequenceController._same
                    .Append(question.transform.DOScaleY(1, .8f)
                        // .SetDelay(2f)
                        .OnPlay(() =>
                        {
                            _audioSource.clip = bot;
                            _audioSource.Play();

                            if (variable.timingVideo != null && variable.timingVideo.end != 0)
                                videoLeapSink.StartFromTiming(variable.timingVideo);
                        })
                        .SetDelay(variable.delay)
                        .OnStart(() =>
                        {
                            question.gameObject.SetActive(true);
                            // LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform);
                        })
                        .OnUpdate(() => { LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform); })
                        .OnComplete(() =>
                        {
                            // LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform);
                            if (variable.refAfter) SwitchRef(variable, new List<string> { variable.question });
                        }));

                // _sequenceController._same
                //     .OnUpdate(() => { LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform);});
            }
            else
            {
                int test = Random.Range(0, variable.randomQuestion.Length);

                for (var index = 0; index < variable.randomQuestion[test].Strings.Length; index++)
                {
                    Question question = SpawnRandom(variable, test, index);
                    _sequenceController._same
                        .Append(question.transform.DOScaleY(1, .8f)
                            .OnPlay(() =>
                            {
                                _audioSource.clip = bot;
                                _audioSource.Play();
                            })
                            .SetDelay(variable.delay)
                            .OnStart(() =>
                            {
                                question.gameObject.SetActive(true);
                                LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform);
                            })
                            .OnUpdate(() => { LayoutRebuilder.MarkLayoutForRebuild(parentLayout); }));
                    // _sequenceController._same
                    //     .OnUpdate(() => { LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform);});
                }
            }
        }


        if (dialog.Count == 1 && dialog[0].needSkip)
        {
            AfterSpawn(dialog);
        }
        else
        {
            // _sequenceController._same.AppendInterval(Config.DELAY_FOR_SPAWN_ANSWER);
            _sequenceController.PlaySequence(() => { AfterSpawn(dialog); });
        }
    }

    private void AfterSpawn(List<SingleText> dialog)
    {
        foreach (SingleText VARIABLE in dialog)
        {
            if (VARIABLE.nextAction)
                (RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)
                    ?.CurrentPromoActivity.ChangeActivity();

            if (VARIABLE.needUnlock)
                Unlock(VARIABLE.unlockWithoutSave);

            if (VARIABLE.needRestart)
            {
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)
                    ?.ReloadScene(1f);
                break;
            }

            if (VARIABLE.onlyAnswer) continue;

            if (!VARIABLE.linkToLabel.IsNullOrWhitespace())
            {
                FindLink(myDialog.singleTexts, VARIABLE.linkToLabel);
                break;
            }

            SpawnAnswer(VARIABLE);
        }
    }

    private void Unlock(bool unlockWithoutSave)
    {
        StartCoroutine(UnlockCoroutine(unlockWithoutSave));
    }

    IEnumerator UnlockCoroutine(bool unlockWithoutSave)
    {
        yield return new WaitForSeconds(Config.DELAY_FOR_UNLOCK_CHAT);

        if (unlockWithoutSave)
            (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.SimpleUnlock();
        else
            (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.Unlock();
    }

    //todo починить переход по ссылке(сейчас не работает если только не в начале ссылка)
    private int FindLink(List<SingleText> singleTexts, string link)
    {
        if (singleTexts != null && singleTexts.Count > 0)
        {
            foreach (SingleText singleText in singleTexts)
            {
                if (singleText.label != null &&
                    !singleText.label.IsNullOrWhitespace() &&
                    singleText.label == link)
                {
                    SpawnDialog(singleTexts.FindAll(text => text.label == link));
                    return 0;
                }
            }

            if (singleTexts.Any(singleText => FindLink(singleText.childDialogModels, link) == 0))
            {
                return 0;
            }
        }


        return 1;
    }

    private Question SpawnOne(SingleText singleText, bool isAnswer = false)
    {
        Question question = Instantiate(_question, parentQuestion).GetComponent<Question>();
        question.Set(singleText, this, isAnswer);
        return question;
    }

    private Question SpawnRandom(SingleText singleText, int firstRandom, int currentText, bool isAnswer = false)
    {
        Question question = Instantiate(_question, parentQuestion).GetComponent<Question>();
        question.SetRandom(singleText, this, firstRandom, currentText, isAnswer);
        return question;
    }

    private void SpawnAnswer(SingleText singleText)
    {
        if (singleText.variantAnswer != null && singleText.variantAnswer.Count > 0)
        {
            _secondChatModel.SpawnAnswer(singleText.variantAnswer, singleText.allVariant, singleText.errorText);
        }
        else
        {
            if (singleText.dropdownDialog != null && !singleText.dropdownDialog.nameDropdown.IsNullOrWhitespace())
            {
                _secondChatModel.SpawnAnswer(singleText.dropdownDialog, singleText.dropdownDialog.withNumber,
                    singleText.errorText);
                return;
            }

            if (singleText.numberInput == 0)
                _secondChatModel.SpawnAnswer(singleText.errorText);
            else
                _secondChatModel.SpawnAnswer(singleText.numberInput, singleText.errorText);
        }
    }

    public void WriteAnswer(List<string> answer)
    {
        currentTexts.answer = answer[0];


        // if (currentTexts.saveAnswer!=null && !currentTexts.saveAnswer.IsNullOrWhitespace())
        //     SaveAnswer(currentTexts.saveAnswer, answer[0]);

        foreach (var VARIABLE in answer)
        {
            if (VARIABLE.IsNullOrWhitespace()) continue;

            Question question = SpawnOne(new SingleText() { question = VARIABLE, onlyAnswer = true }, true);
            _sequenceController._same.Append(
                question
                    .transform.DOScaleY(1, .5f)
                    .OnPlay(() =>
                    {
                        _audioSource.clip = user;
                        _audioSource.Play();
                    })
                    .OnStart(() => { question.gameObject.SetActive(true); })
                    .OnUpdate(() => { LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform); }));
        }

        CheckKeyRef(answer);
    }

    private void CheckKeyRef(List<string> answer)
    {
        //проверка ключа, и получение списка ответов
        if (currentTexts.keyRef.IsNullOrWhitespace())
        {
            SimpleWriteAnswer(answer[0]);
        }
        else
        {
            SwitchRef(currentTexts, answer);
        }
    }

    private void SwitchRef(SingleText singleText, List<string> answer)
    {
        switch (singleText.keyRef)
        {
            case "CheckNumber":
            {
                _chatConnector.SendNumber(answer[0], SimpleWriteAnswer);
                // RegisterSingleton._instance.RegUser.SendNumber(answer);
                break;
            }
            case "CheckCode":
            {
                _chatConnector.SendCode(answer[0], SimpleWriteAnswer);
                break;
            }
            case "SendRef":
            {
                // _chatConnector.SendRef(answer[0], SimpleWriteAnswer);
                SimpleWriteAnswer(answer[0] == Config.PASSWORD ? "true" : "false");

                break;
            }
            case "SendDirection":
            {
                _chatConnector.SendDestination(new ContainerString() { Strings = answer.ToArray() }, SimpleWriteAnswer);
                break;
            }
            case "SendCountry":
            {
                _chatConnector.SendCountry(answer[0], SimpleWriteAnswer);
                break;
            }
            case "CheckLocker":
            {
                _chatConnector.CheckLocker(answer[0], SimpleWriteAnswer);
                break;
            }
            case "SendLink":
            {
                _chatConnector.SendLink(answer[0], SimpleWriteAnswer);
                break;
            }
            case "SetEmail":
            {
                _chatConnector.SetEmail(answer[0], SimpleWriteAnswer);
                break;
            }
            case "SendEmail":
            {
                _chatConnector.SendEmail(null);
                break;
            }
            case "GetPassword":
            {
                _chatConnector.GetLocker(WriteRef);
                // StartCoroutine(WaitAnswer(()=>
                // {
                //     _chatConnector.GetLocker(WriteRef);
                // }));

                break;
            }
            case "GoRabbit":
            {
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)?.GoLink(
                    Config.URL_RABBIT);
                break;
            }
            case "GoTelegram":
            {
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)?.GoLink(
                    Config.URL_TELEGRAM);
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)
                    ?.ReloadScene(1);
                break;
            }
            case "GoTelegramMain":
            {
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)?.GoLink(
                    Config.URL_TELEGRAM_MAIN);
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)
                    ?.ReloadScene(1);
                break;
            }
            case "GoMail":
            {
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)?.GoLink(
                    Config.LINK_MAILTO);
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)
                    ?.ReloadScene(1);
                break;
            }
            case "SendCountryCode":
            {
                _chatConnector.SendCountryCode(answer[0].Split(' ').Last(), SimpleWriteAnswer);
                break;
            }
        }
    }

    private void SimpleWriteAnswer(string answer)
    {
        List<SingleText> singleTexts = FindAnswer(answer);
        SpawnDialog(singleTexts.Count > 0 ? singleTexts : myDialog.singleTexts);
    }

    private void WriteRef(string answer)
    {
        Question question = SpawnOne(new SingleText() { question = answer, onlyAnswer = true });

        _sequenceController._same.Append(
            question
                .transform.DOScaleY(1, .5f)
                .OnPlay(() =>
                {
                    _audioSource.clip = user;
                    _audioSource.Play();
                })
                .OnStart(() => { question.gameObject.SetActive(true); })).OnUpdate(() =>
        {
            LayoutRebuilder.MarkLayoutForRebuild(question.RectTransform);
        });

        _sequenceController.PlaySequence(() =>
        {
            // StartCoroutine(WaitAnswer(() =>
            // {
            List<SingleText> singleTexts = FindAnswer(answer);
            SpawnDialog(singleTexts.Count > 0 ? singleTexts : myDialog.singleTexts);
            // }));
        });
    }


    private List<SingleText> FindAnswer(string answer)
    {
        List<SingleText> nextAnswer = currentTexts.childDialogModels.FindAll(x => x.key == answer);
        return nextAnswer.Count == 0 ? currentTexts.childDialogModels.FindAll(x => x.key == "default") : nextAnswer;
    }

    IEnumerator WaitAnswer(Action afterWait)
    {
        yield return new WaitForSeconds(2f);
        afterWait.Invoke();
    }

    private void SaveAnswer(string key, string value)
    {
        if (saveAnswer.ContainsKey(key))
            saveAnswer[key] = value;
        else
            saveAnswer.Add(key, value);
    }

    public void ErrorText()
    {
        Question question = SpawnOne(new SingleText() { question = currentTexts.errorText.textError });
        _sequenceController._same.Append(
            question
                .transform.DOScaleY(1, .5f)
                .OnPlay(() =>
                {
                    _audioSource.clip = user;
                    _audioSource.Play();
                })
                .OnStart(() => { question.gameObject.SetActive(true); }).OnUpdate(() =>
                {
                    LayoutRebuilder.MarkLayoutForRebuild(parentLayout);
                }));
        _sequenceController.PlaySequence(null);
    }

    public string GetSaveAnswer(string key)
    {
        return saveAnswer[key];
    }
}