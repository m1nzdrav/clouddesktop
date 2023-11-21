using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Answer : Question, IAnswer
{
    private SecondChatModel _secondChatModel;
    [SerializeField] private Button button;
    [SerializeField] private List<string> answer = new List<string>(1);

    public void SetAnswer(string answer, SecondChatModel secondChatModel,MainChat mainChat)
    {
        transform.DOScale(Vector3.one, .5f);
        _mainChat = mainChat;
        text.font = _mainChat.Font;
        text.text = answer;
        _secondChatModel = secondChatModel;

        this.answer.Add(answer);
    }

    private void OnPointerClick(BaseEventData eventData)
    {
        SendAnswer();
    }

    public void SendAnswer()
    {
        _secondChatModel.SendAnswer(answer);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}