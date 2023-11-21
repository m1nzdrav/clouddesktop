using System;
using System.Collections.Generic;
using _Game._Scripts;
using _Game._Scripts.Panel;
using UnityEngine;

[Serializable]
public class JsonUnity : JsonClass
{
    public JsonUnity(bool needStruct)
    {
        MYTJson = new List<IdNameValue>();
        if (needStruct)
        {
            AddChapter(StructJson.Main.ToString());
            AddChapter(StructJson.Who.ToString());
            AddChapter(StructJson.What.ToString());
            AddChapter(StructJson.Where.ToString());
            AddChapter(StructJson.When.ToString());
            AddChapter(StructJson.Events.ToString());
            AddChapter(StructJson.Data.ToString());
            AddChapter(StructJson.Actions.ToString());
        }
        else
        {
            AddChapter(Config.DEFAULT_USER_JSON_MYT_NAME);
            AddChapter(Config.DEFAULT_USER_JSON_MYT_ID);
            AddChapter(Config.DEFAULT_USER_JSON_GRID_CONTROLLER);
            AddChapter(Config.DEFAULT_USER_JSON_PREFABE_NAME);
            AddChapter(Config.DEFAULT_USER_JSON_MYT_PREFABE_COLOR_TYPES);
            AddChapter(Config.DEFAULT_USER_JSON_BACKGROUND);
            AddChapter(Config.DEFAULT_USER_JSON_COVERS);
        }
    }

    public JsonUnity(Prefab prefabType, Parent parent)
    {
        MYTJson = JsonUtility.FromJson<JsonUnity>((RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.TemplateJsonUnity)
            .MYTJson;
        SetPrefabType(prefabType);
        SetPrefabType(Prefab.MYTIcon60_60);

        SetColor(parent);
    }
}