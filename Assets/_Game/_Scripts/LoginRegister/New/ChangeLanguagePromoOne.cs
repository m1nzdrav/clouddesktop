using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChangeLanguagePromoOne : MonoBehaviour
{

    private bool _isTest;

    // [Button]
    // public void Russian()
    // {
    //     // _isTest = true;
    //     ClearPrevAndSetNew("Russian");
    // }
    //
    // private void ClearPrevAndSetNew(string language)
    // {
    //     _instantiate.StopAndClearAll();
    //
    //     if (_isTest)
    //     {
    //         _isTest = false;
    //         var path = Application.dataPath;
    //         _instantiate.JsonString = File.ReadAllText(path + @"\Page" + language + ".json");
    //     }
    //     else
    //     {
    //         _instantiate.JsonString = Resources.Load<TextAsset>("Page" + language).text;
    //     }
    //
    //     _instantiate.Inst(false);
    // }
}
