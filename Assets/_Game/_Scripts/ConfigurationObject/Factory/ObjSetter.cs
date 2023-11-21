using UnityEngine;

public class ObjSetter
{
    public static void SetObj(DataForObject dataForObject, Parent parent, GameObject unSettableObject)
    {
        unSettableObject.GetComponent<ISetter>().Set(dataForObject);
        parent?.SpawnChild(unSettableObject);
    }
}