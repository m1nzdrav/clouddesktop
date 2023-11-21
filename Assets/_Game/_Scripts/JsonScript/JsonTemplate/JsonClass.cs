using System;
using System.Collections.Generic;
using _Game._Scripts;
using _Game._Scripts.Panel;
using UnityEngine;

[Serializable]
public abstract class JsonClass
{
    public List<IdNameValue> MYTJson;

    protected void SetName()
    {
        MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == "MYTDefaultName").MYTValue.Add(new IdNameValue("NewName"));
    }

    protected void SetPrefabType(Prefab prefabType)
    {
        MYTJson
            .Find(x => x.MYTName == "MYTPrefab").MYTValue.Add(new IdNameValue(prefabType.ToString()));
    }

    protected void SetParent(Parent parent)
    {
        if (parent != null)
        {
            MYTJson
                    .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
                    .Find(x => x.MYTName == "MYTParentChild").MYTValue.Find(x => x.MYTName == "MYTParent").MYTValue =
                new List<IdNameValue>() { new IdNameValue(parent.My.name) };
        }
    }

    protected void SetColor(Parent parent)
    {
        Debug.LogError("create color " + MYTJson[0].MYTName);
        if (parent != null)
        {
            MYTJson.Find(x => x.MYTName == SwitcherValues.MYTContent.ToString()).MYTValue
                .Find(x => x.MYTName == "Color").MYTValue = Config.JsonColor(parent).MYTValue;
        }
    }

    public void SetId(string value)
    {
        SetId("IdJson", value);
        SetId("JsonUnity", value);
        SetId("JsonParentChild", value);
    }

    protected void SetId(string chapter, string value)
    {
        MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == chapter).MYTValue.Add(new IdNameValue(value));
    }

    #region CRUD

    public bool AddChapter(String name)
    {
        try
        {
            MYTJson.Add(new IdNameValue(name));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка добавления " + name + "\n" + e);
            return false;
        }
    }

    public bool DeleteChapter(String name)
    {
        try
        {
            MYTJson.Remove(MYTJson.Find(x => x.MYTName == name));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка Удаления " + name + "\n" + e);
            return false;
        }
    }

    public bool UpdateChapter(String name, List<IdNameValue> newValue)
    {
        try
        {
            MYTJson.Find(x => x.MYTName == name).MYTValue = newValue;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка обновления в " + name + "\n" + e);
            return false;
        }
    }

    #endregion
}