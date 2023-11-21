using System.Collections.Generic;
using _Game._Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class Translator : MonoBehaviour
{
    [SerializeField] private List<TranslateBlock> NeedTranslate;

    [SerializeField] private AllText _allText;
    
[Button]
    public void UpdateLanguage()
    {
        UpdateLanguage(ChangeLanguageLoginScene.currentLanguage);
    }


    private void UpdateLanguage(string newLanguage)
    {
        LoadLanguage(newLanguage);

        foreach (IdNameValue VARIABLE in _allText.Key)
        {
            NeedTranslate.FindAll(x => x != null && x.Key == VARIABLE.MYTName)
                .ForEach(x => x.ChangeText(VARIABLE.MYTValue[0].MYTName, VARIABLE.MYTValue[1].MYTName));
        }
    }


    private void LoadLanguage(string newLanguage)
    {
        _allText = Config.LoadJson<AllText>(@"AllText_" + newLanguage);
    }

    [Button]
    private void CreateJson()
    {
        _allText = new AllText();
        foreach (TranslateBlock VARIABLE in NeedTranslate)
        {
            IdNameValue tempKeyValue = new IdNameValue(VARIABLE.Key);
            tempKeyValue.MYTValue.Add(new IdNameValue(VARIABLE.GetText()));
            _allText.Key.Add(tempKeyValue);
        }

        Debug.LogError(JsonUtility.ToJson(_allText));
    }
}