using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

/// <summary>
/// Class help save class to json files
/// </summary>
public class SavePanelScript : MonoBehaviour
{
    private DataPanelScript DPS => GetComponentInParent<DataPanelScript>();
    public TMP_InputField IF_namefile;
    public Button SaveButton;

    private void OnEnable()
    {
        IF_namefile.text = DPS.LPS.SelectedFileName;
    }

    /// <summary>
    /// Event on click "Save" button
    /// </summary>
    public void OnButtonSave()
    {
        DPS.RewriteClass();
        File.WriteAllText(DPS.LPS.PathToDataFolder + Path.DirectorySeparatorChar + IF_namefile.text,
            JsonUtility.ToJson(DPS.TC, true));
        DPS.LPS.LoadOptions();
        TMP_InputField[] inpFs = DPS.GetComponentsInChildren<TMP_InputField>();
        foreach (var item in inpFs)
        {
            item.GetComponent<Image>().color = Color.white;
        }

        DPS.LPS.On_DDChange();
    }
}