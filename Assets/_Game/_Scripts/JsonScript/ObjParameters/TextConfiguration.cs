using UnityEngine;


public class TextConfiguration : IConfiguration
{
    public JsonUnity Get(GameObject obj, JsonUnity newValue)
    {
        IdNameValue content = new IdNameValue(SwitcherValues.MYTText.ToString());

        content.MYTValue.Add(GetName(obj));
        content.MYTValue.Add(GetDescription(obj));
        content.MYTValue.Add(GetStateDescription(obj));
        newValue.MYTJson.Add(content);
        return newValue;
    }

    private IdNameValue GetName(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("Name");
        // idNameValue.MYTValue.Add(new IdNameValue(obj.localScale.ToString()));
        return idNameValue;
    }

    private IdNameValue GetDescription(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("Description");
        // Debug.LogError(obj.GetComponent<PrefabName>().Prefab + " " + obj.name);
        // idNameValue.MYTValue.Add(new IdNameValue(obj.rotation.ToString()));
        return idNameValue;
    }

    private IdNameValue GetStateDescription(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("StateDescription");
        // idNameValue.MYTValue.Add(new IdNameValue(obj.position.ToString()));
        return idNameValue;
    }

    public void Set(GameObject obj, IdNameValue value)
    {
        throw new System.NotImplementedException();
    }
}