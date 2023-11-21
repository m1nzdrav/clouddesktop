using System;
using System.Collections.Generic;
using _Game._Scripts;
using _Game._Scripts.Panel;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class DataForObject
{
    //BaseConfiguration
    public string nameJson;
    public Prefab mainPrefab;
    public List<string> nameChilds;
    public int iconNumberPosition;
    public bool isFolderOpen;

    //ContentConfiguration
    public Color color;
    public Texture texture;
    public string sourcePath;

    //TextConfiguration
    public string name;
    public string description;
    public bool isOpenDescription;

    //TransformConfiguration
    public Vector3 scaleIcon;
    public Vector3 scaleFolder;
    public Vector3 positionFolder;
    public Vector3 positionIcon;
    public Vector4 rotationIcon;
    public Vector4 rotationFolder;


    public DataForObject()
    {
        nameChilds = new List<string>();
    }

    public DataForObject(JsonUnity jsonUnity, JsonParentChild jsonParentChild, JsonData jsonData)
    {
        IdNameValue dataContent = jsonUnity
            .MYTJson.Find(x => x.MYTName == SwitcherValues.MYTContent.ToString());

        color = GetColor(dataContent.MYTValue.Find(x => x.MYTName == "Color"));
        mainPrefab = Config.GetFormatJson(jsonUnity);
        // nameJson = jsonUnity.MYTJson.Find(x => x.MYTName == SwitcherValues.MYTPrefab.ToString())
        //     .MYTValue[0] //MYTFolderNewDesign
        //     .MYTValue[0] //Base
        //     .MYTValue[0] //NameJson
        //     .MYTValue[0].MYTName;
        nameChilds = new List<string>();
        texture = GetTexture(dataContent.MYTValue
            .Find(x => x.MYTName == "Texture"));

        if (jsonData != null)
        {
            IdNameValue dataData = jsonData.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString());
            name = GetName(dataData);
            
        }
        if (jsonParentChild == null)
            return;

        IdNameValue dataParentChild = jsonParentChild.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString());
        SaveChild(dataParentChild);
    }

    //todo получать еще и текстуру из json

    public void AddNewFields(string nameJson = null, Color color = default, string name = null,
        string description = null,
        string sourcePath = null, Prefab mainPrefab = default, List<string> nameChilds = null, Texture texture = null)
    {
        this.nameJson = Config.Set(this.nameJson, nameJson); //CheckNull(this.nameJson, nameJson) as string;
        this.color = Config.CheckColor(this.color, color);
        // this.name = Config.Set(this.name, name);
        this.description = Config.Set(this.description, description);
        this.sourcePath = Config.Set(this.sourcePath, sourcePath);
        this.mainPrefab = mainPrefab;
        this.nameChilds = Config.Set(this.nameChilds, nameChilds);
        this.texture = Config.Set(this.texture, texture);
    }

    private Color GetColor(IdNameValue data)
    {
        return data.MYTValue.Count == 0 ? Color.blue : Config.ColorFromJson(data);
    }

    /// <summary>
    /// find name in resource from path Data/MYTDefaultName[0]
    /// </summary>
    /// <param name="jsonData">path to Data</param>
    /// <returns></returns>
    private string GetName(IdNameValue jsonData)
    {
        return jsonData.MYTValue.Find(x => x.MYTName == SwitcherValues.MYTDefaultName.ToString()).MYTValue[0].MYTName;
    }

    /// <summary>
    /// find texture in resource from path MYTPrefab/MYTFolderNewDesign/Content/Texture
    /// </summary>
    /// <param name="jsonUnity">path to What</param>
    /// <returns></returns>
    private Texture GetTexture(IdNameValue jsonContent)
    {
        if (jsonContent.MYTValue.Count == 0)
            return null;

        return Resources.Load<Texture>(jsonContent.MYTValue
            .Find(x => x.MYTName == SwitcherValues.MYTImage.ToString()).MYTValue[0].MYTName);
    }

    private void SaveChild(IdNameValue json)
    {
        IdNameValue parentchild = json.MYTValue
            .Find(x => x.MYTName == "MYTParentChild").MYTValue.Find(x => x.MYTName == "MYTChild");
        foreach (var VARIABLE in parentchild.MYTValue)
        {
            nameChilds.Add(VARIABLE.MYTName);
        }
    }
}