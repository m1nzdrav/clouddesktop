using System;
using UnityEngine;

public class UserJsonComponent : MonoBehaviour
{

   [SerializeField] private IdNameValue componentJson;
   [SerializeField] private CreateModelPrefab folderCreator;

   public void SetComponentJson(IdNameValue _componentJson)
   {
      componentJson = _componentJson;
   }

   private void OnEnable()
   {
      CreateValueChild();
   }

   public void CreateValueChild()
   {
      CheckNull();
      for (int i = 0; i < componentJson.MYTValue.Count; i++)
      {
         // folderCreator.Create()
      }
   }

   public void CheckNull()
   {
      if (folderCreator==null)
      {
         folderCreator = GetComponent<CreateModelPrefab>();
      }
   }
}
