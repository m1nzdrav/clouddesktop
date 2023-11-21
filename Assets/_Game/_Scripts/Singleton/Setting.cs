using System.Collections.Generic;
using UnityEngine;



public abstract class Setting : ScriptableObject
{

    public abstract List<ISingleton> GetRegister();
  

}