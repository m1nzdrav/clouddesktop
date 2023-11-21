using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create SettingJson", fileName = "SettingJson", order = 0)]
public class SettingJson : Setting
{
    public ManagerJson ManagerJson;


    public override List<ISingleton> GetRegister()
    {
        return new List<ISingleton>() {ManagerJson};
    }
}