using UnityEngine;

public class BtnSend : MonoBehaviour
{
    [SerializeField] private KeyCode _keyCode = KeyCode.Return;
    [SerializeField] private bool isSelect;
    [SerializeField] private SecondChatModel secondChatModel;
    [SerializeField] private CanvasGroup canvasGroup;

    public void Select()
    {
        isSelect = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DeSelect()
    {
        isSelect = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode) && isSelect) BtnSendAnswer();
    }

    private void BtnSendAnswer()
    {
        secondChatModel.BtnSendAnswer();
    }
}