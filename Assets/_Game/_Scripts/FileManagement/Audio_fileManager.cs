using System.Collections;
using System.Collections.Generic;
using System.IO;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using UnityEditor;
using UnityEngine;

public class Audio_fileManager : FileManager
{
    protected override void AddAsIcon(string path = null, byte[] data = null)
    {
        Parent parent = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab[0].GetComponent<Parent>();
        DataForObject dataForObject = new DataForObject();
        dataForObject.AddNewFields(sourcePath: path, name: "image");
        
        Debug.Log("registerSing");
        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetNew(Prefab.MYTAudio, parent, dataForObject);
    }
}