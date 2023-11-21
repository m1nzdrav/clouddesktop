using UnityEngine;
using UnityEngine.EventSystems;

public class NextOpenContents : MonoBehaviour
{
    [SerializeField] private ContentSetting mainContent;
    [SerializeField] private string jsonName;
    [SerializeField] private bool alreadyInst;

    private void Start()
    {
        // gameObject.AddComponent<EventTrigger>().AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);
    }

    public void Set(ContentSetting mainContent, string jsonName)
    {
        this.mainContent = mainContent;
        this.jsonName = jsonName;
    }

    public void InvokeClick()
    {
        if (alreadyInst)
        {
            mainContent.FastInst(false, jsonName+ChangeLanguageLoginScene.currentLanguage);
        }
        else
        {
            mainContent.Inst(false, jsonName+ChangeLanguageLoginScene.currentLanguage);
            alreadyInst = true;
        }
    }
}