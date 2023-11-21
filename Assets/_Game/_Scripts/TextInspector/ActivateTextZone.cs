using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Project._Game._Scripts.SameSequence;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTextZone : MonoBehaviour
{
    [SerializeField] Image ActivateTextButton_icon;
    [SerializeField] Sprite TextActiveState_sprite;
    [SerializeField] Sprite TextDeactiveState_sprite;

    [SerializeField] GameObject TextZoneObj;
    [SerializeField] GameObject Text_obj;

    InputField InpField;

    private SequenceController _sequenceController;

    private float animationDuration = 0.2f;

    void Start()
    {
        // InpField = TextZoneObj.transform.GetChild(0).gameObject.GetComponent<InputField>();
        _sequenceController = new SequenceController();
    }

    private void Update()
    {
        // if (RegisterSingleton._instance.TextInspectorSingleton.DoubleText) return;
        //
        // if (Input.GetKeyDown(KeyCode.Return) && TextInspector.SelectedText?.gameObject == Text_obj) 
        // {
        //     ActivateText();
        // }
    }

    public void ActivateText() 
    {
        if (TextZoneObj.activeInHierarchy == false)
        {
            TextZoneObj.SetActive(true);
            Show();
            ActivateTextButton_icon.sprite = TextActiveState_sprite;
        }
    }

    public void SwitchText()
    {
        if (TextZoneObj.activeInHierarchy == false) 
        {
            TextZoneObj.gameObject.SetActive(true);
            
            Show();
            ActivateTextButton_icon.sprite = TextActiveState_sprite;
        }
        else 
        {
            Hide();
            ActivateTextButton_icon.sprite = TextDeactiveState_sprite;
        }
            
    }

    void RebuildLayouts()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(PatternLayout_GridArea);
        LayoutRebuilder.ForceRebuildLayoutImmediate(TextZoneObj.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            TextZoneObj.transform.parent.GetComponent<RectTransform>());
    }
    
    void Show()
    {
        CanvasGroup canvasGroup = TextZoneObj.GetComponent<CanvasGroup>();

        _sequenceController._same.Join(
            TextZoneObj.transform.DOScale(Vector3.one, animationDuration).OnUpdate(() =>
                {
                    //RebuildLayouts();
                }
            ));
        _sequenceController._same.Join(
            canvasGroup.DOFade(1, animationDuration)
        );
        _sequenceController.PlaySequence(() =>
        {
            InpField.ActivateInputField();
        });
    }
    
    //LayoutRebuilder.ForceRebuildLayoutImmediate(TextZoneObj.transform.parent.GetComponent<RectTransform>()
    //LayoutRebuilder.ForceRebuildLayoutImmediate(TextZoneObj.transform.parent.GetComponent<RectTransform>()
    
    void Hide()
    {
        StopAllCoroutines();
        CanvasGroup canvasGroup = TextZoneObj.GetComponent<CanvasGroup>();
        
        _sequenceController._same.Join(
            TextZoneObj.transform.DOScale(Vector3.zero, animationDuration).OnUpdate(() =>
                {
                    RebuildLayouts();
                }
                ));
        _sequenceController._same.Join(
            canvasGroup.DOFade(0, animationDuration)
        );
            
        _sequenceController.PlaySequence(() =>
        {
            TextZoneObj.SetActive(false);
            InpField.DeactivateInputField();
        });
    }
}
