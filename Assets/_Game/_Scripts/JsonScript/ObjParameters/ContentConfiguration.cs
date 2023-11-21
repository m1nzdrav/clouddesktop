using _Game._Scripts;
using UnityEngine;

public class ContentConfiguration : IConfiguration
{
    public JsonUnity Get(GameObject obj, JsonUnity newValue)
    {
        IdNameValue content = new IdNameValue(SwitcherValues.MYTContent.ToString());

        content.MYTValue.Add(GetColor(obj));
        content.MYTValue.Add(GetTexture(obj));
        content.MYTValue.Add(GetSourcePath(obj));
        
        newValue.MYTJson.Add(content);
        return newValue;
    }

    private IdNameValue GetColor(GameObject obj)
    {
        Debug.LogError("CreateColor in ContentConfig");
        IdNameValue idNameValue = new IdNameValue("Color");
        idNameValue.MYTValue.Add(Config.JsonColor(obj.GetComponent<Parent>()));
        return idNameValue;
    }

    private IdNameValue GetTexture(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("Texture");
        return idNameValue;
    }

    private IdNameValue GetSourcePath(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("SourcePath");
        return idNameValue;
    }

    public void Set(GameObject obj, IdNameValue value)
    {
        throw new System.NotImplementedException();
    }
}