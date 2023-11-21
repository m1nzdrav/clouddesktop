using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestJsonSerializ : MonoBehaviour
{
    [ContextMenu("Test")]
    void Start ()
    {
        

    }
[Button]
    void testScript()
    {
        List<IdNameValue> list = new List<IdNameValue>();
        list.Add(new IdNameValue() { objectId= "a", objectName = ""});
        list.Add(new IdNameValue() { objectId= "b", objectName = ""});


        SerializedList serList = new SerializedList()
        {
            types = list.Select(obj => obj.GetType().FullName).ToArray(),
            objects = list.Select(obj => JsonUtility.ToJson(obj, false)).ToArray()
        };

        string jsonStr = JsonUtility.ToJson(serList);
        Debug.Log(jsonStr);

        SerializedList deserList = JsonUtility.FromJson<SerializedList>(jsonStr);

        for (int i = 0; i < deserList.types.Length; i++)
        {
            System.Type dataType = System.Type.GetType(deserList.types[i]);
            IdNameValue item = (IdNameValue)JsonUtility.FromJson(deserList.objects[i], dataType);
            Debug.Log(item.GetType());
        }
    }


    public class IdNameValue
    {
        public string objectId;
        public string objectName;
        public string objectHash;
        public List<IdNameValue> objectValue;

      
    }

    [System.Serializable]
    public class SerializedList
    {
        public string[] types;
        public string[] objects;
    }
}
