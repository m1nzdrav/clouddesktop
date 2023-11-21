using System;
using _Game._Scripts;
using DG.Tweening;
using Shapes2D;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Question : MonoBehaviour
{
    [SerializeField] protected MainChat _mainChat;
    [SerializeField] private SingleText singleText;
    [SerializeField] private RectTransform rectTransform;

    public RectTransform RectTransform => rectTransform;

    public SingleText SingleText
    {
        get => singleText;
    }

    [SerializeField] protected Text text;
    [SerializeField] private GameObject icon;
    [SerializeField] private Image image;

    public void Set(SingleText singleText, MainChat mainChat, bool isAnswer = false)
    {
        this.singleText = singleText;
        _mainChat = mainChat;


        // if (singleText.question.IsNullOrWhitespace())
        // {
        //     int test = Random.Range(0, singleText.randomQuestion.Length);
        //     text.text =
        //         singleText.randomTest[test].Strings[0];
        //     _mainChat.SpawnRandom(singleText, isAnswer);
        //
        // }
        // else
        // {
        text.font = mainChat.Font;
        text.text = singleText.question;
        // }


        if (!isAnswer) return;

        icon.SetActive(true);
        text.color = Config.ANSWER_COLOR;
        image.DOFade(0, 0f);
    }

    private void OnBecameInvisible()
    {
        Debug.LogError(text.text);
    }

    public void SetRandom(SingleText singleText, MainChat mainChat, int firstRandom, int currentText,
        bool isAnswer = false)
    {
        this.singleText = singleText;
        _mainChat = mainChat;
        text.text = singleText.randomQuestion[firstRandom].Strings[currentText];
        // _mainChat.SpawnSecond(singleText, isAnswer);
    }
}