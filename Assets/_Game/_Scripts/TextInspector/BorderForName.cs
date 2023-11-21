using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderForName : MonoBehaviour
{
    [SerializeField] GameObject DummyText_pref;
    [SerializeField] Text controlledText;
    [SerializeField] Re_InputField controlledField;
    [SerializeField] RectTransform PatternObject;
    //[SerializeField] RectTransform ActivateButton_rectTr;

    HorizontalLayoutGroup group;
    RectTransform controlledField_rectTr;
    RectTransform dummy_rectTr;
    RectTransform parentLayout_rectTr;
    LayoutElement element;
    ContentSizeFitter fitter;
    Text dummyText;

    float sizeDelta = 300;
    bool minSize_mod = false;

    void Start()
    {
        parentLayout_rectTr = transform.parent.GetComponent<RectTransform>();
        group = GetComponent<HorizontalLayoutGroup>();
        element = GetComponent<LayoutElement>();
        fitter = GetComponent<ContentSizeFitter>();
        controlledField_rectTr = GetComponent<RectTransform>();
        //текст-дублер, благодаря которому мы всегда знаем "настоящие" размеры текстового окна
        dummyText = Instantiate(DummyText_pref).GetComponent<Text>();
        dummy_rectTr = dummyText.gameObject.GetComponent<RectTransform>();
        GameObject dummiesContainer = GameObject.Find("TextDummiesContainer");
        
        if(dummiesContainer == null) 
        {
            dummiesContainer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            dummiesContainer.name = "TextDummiesContainer";
            dummiesContainer.GetComponent<Renderer>().enabled = false;
        }
        dummyText.transform.parent = dummiesContainer.transform;
        
        //проверяем все настройки сразу
        
        // OnValueChanged();
        // OnSettingsChanged();
    }

    public void OnValueChanged() 
    {
        // dummyText.text = controlledField.text;
        // RebuildLayouts();
        //
        // if(minSize_mod)
        //     StartCoroutine(UpdateRect());
        //
        // CheckWidth();
    }

    public void OnSettingsChanged() 
    {
        dummyText.fontSize = controlledText.fontSize;
        dummyText.fontStyle = controlledText.fontStyle;
        RebuildLayouts();
        CheckWidth();
    }

    public void CheckWidth()
    {
        float prefWidth = PatternObject.rect.width - sizeDelta;

        if (dummy_rectTr.rect.width > prefWidth)
        {
            element.minWidth = prefWidth;
            // fitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
            minSize_mod = true;
        }
        else
        {
            
            element.minWidth = 0;
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            minSize_mod = false;
        }

        
    }

    void RebuildLayouts() 
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(controlledField_rectTr);
        LayoutRebuilder.ForceRebuildLayoutImmediate(dummy_rectTr);
        LayoutRebuilder.ForceRebuildLayoutImmediate(PatternObject);
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayout_rectTr);
    }

    public IEnumerator UpdateRect()
    {
        group.enabled = false;
        yield return new WaitForSeconds(0.1F);
        group.enabled = true;
    }
}
