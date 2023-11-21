using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextPageSubs : MonoBehaviour
{
    [SerializeField] private bool inInspector = false;
    [SerializeField] private ContentSetting contentSetting;
    [SerializeField] private int[] pages;
    [SerializeField] private NextPageSystem nextPageSystem;
    [SerializeField] private bool isPlayed = false;
    [SerializeField] private bool linkedPage;

    public void SetPage(ContentSetting contentSetting, int[] pages, bool linked = false)
    {
        this.pages = pages;
        this.contentSetting = contentSetting;
        linkedPage = linked;
    }

    public void StartSubscribe()
    {
        gameObject.GetComponent<EventTrigger>()?.AddEventTrigger(OnPointerClick, EventTriggerType.PointerClick);
    }

    public void InvokeClick()
    {
        if (inInspector)
            contentSetting.CurrentPage(pages);
        else
            contentSetting.CurrentPage(pages);

        isPlayed = true;
    }

    public void InvokeFastClick()
    {
        if (linkedPage)
            return;

        if (inInspector)
            contentSetting.FastCurrentPage(pages);
        else
            nextPageSystem.FastCurrentPage(pages);

        isPlayed = true;
    }

    private void OnPointerClick(BaseEventData eventData)
    {
        InvokeClick();
    }
}