using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create SettingLocal", fileName = "SettingLocal", order = 0)]
public class SettingLocal : Setting
{
    public Factory Factory;


    public override List<ISingleton> GetRegister()
    {
        return new List<ISingleton>(){Factory};
    }
}