using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

/// <summary>
/// Script help load json file when Dropdown changed
/// </summary>
public class LoadPanelScript : MonoBehaviour
{
    private DataPanelScript DPS => GetComponentInParent<DataPanelScript>();

    /// <summary>
    /// TMP_Dropdown
    /// </summary>
    public TMP_Dropdown DD;

    /// <summary>
    /// Saved json files in directory "ObjectsData"
    /// </summary>
    public string[] GetFilenames()
    {
        if (!Directory.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + DataFolder))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + DataFolder);
        }

        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + DataFolder,
            "*." + DPS.ExtensionJsonFile);
        if (files.Length == 0)
        {
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + DataFolder +
                                           Path.DirectorySeparatorChar + "DefaultClass." + DPS.ExtensionJsonFile,
                FileMode.Create);

            fs.Dispose();

            files = Directory.GetFiles(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + DataFolder,
                "*." + DPS.ExtensionJsonFile);
        }

        return files;
    }

    /// <summary>
    /// Current Path to data folder
    /// </summary>
    public string PathToDataFolder => Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + DataFolder;

    public string SelectedFile
    {
        get { return GetFilenames()[DD.value]; }
    }

    public string SelectedFileName => new FileInfo(GetFilenames()[DD.value]).Name;

    /// <summary>
    /// Save folder for classes
    /// </summary>
    private readonly string DataFolder = "ObjectsData";

    private void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(SelectedFile), DPS.TC);
        LoadOptions();
    }

    /// <summary>
    /// Load files from data directory to Dropdown
    /// </summary>
    public void LoadOptions()
    {
        DD.options.Clear();
        List<TMP_Dropdown.OptionData> OptionsList = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < GetFilenames().Length; i++)
        {
            FileInfo fi = new FileInfo(GetFilenames()[i]);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(fi.Name);
            OptionsList.Add(option);
        }

        DD.AddOptions(OptionsList);
    }

    /// <summary>
    /// On Dropdown change event
    /// </summary>
    public void On_DDChange()
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(SelectedFile), DPS.TC);
        LoadOptions();
        DPS.CreateSavePanel();
        DPS.LoadClassToTable();
    }
}