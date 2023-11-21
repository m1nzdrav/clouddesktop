using _Game._Scripts.Panel;
using UnityEngine;

public class TransformConfiguration : IConfiguration
{
    #region Get

    public JsonUnity Get(GameObject obj, JsonUnity newValue)
    {
        IdNameValue content = new IdNameValue(SwitcherValues.MYTTransform.ToString());
    
        content.MYTValue.Add(GetScale(obj.transform));
        content.MYTValue.Add(GetRotation(obj.transform));
        content.MYTValue.Add(GetPosition(obj.transform));
        newValue.MYTJson.Add(content);
        
        return newValue;
    }

    private IdNameValue GetScale(Transform obj)
    {
        IdNameValue idNameValue = new IdNameValue("Scale");
        idNameValue.MYTValue.Add(new IdNameValue(obj.localScale.ToString()));
        return idNameValue;
    }

    private IdNameValue GetRotation(Transform obj)
    {
        IdNameValue idNameValue = new IdNameValue("Rotation");
        idNameValue.MYTValue.Add(new IdNameValue(obj.rotation.ToString()));
        return idNameValue;
    }

    private IdNameValue GetPosition(Transform obj)
    {
        IdNameValue idNameValue = new IdNameValue("Position");
        idNameValue.MYTValue.Add(new IdNameValue(obj.position.ToString()));
        return idNameValue;
    }

    #endregion

    #region Set

    public void Set(GameObject obj, IdNameValue idNameValue)
    {
        Debug.LogError("Set obj from Json");
    }

    #endregion
}