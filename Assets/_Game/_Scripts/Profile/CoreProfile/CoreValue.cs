using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CoreValue : MonoBehaviour, IUpdaterValue
{
    [SerializeField] private WorkspaceEntityMember block;
    [SerializeField] private WorkspaceEntityMember whatEntity;
    [SerializeField] private Transform parentEntity;
    [SerializeField] private DbUser dbUser;
    [SerializeField] private List<WorkspaceEntityMember> userLineInfos;

    private void Start()
    {
        userLineInfos = new List<WorkspaceEntityMember>(22);
    }

    [Button]
    public void Spawn()
    {
        CreateUserInfo("Username", dbUser.Username);
        CreateUserInfo("Number", dbUser.Number);
        CreateUserInfo("Referer", dbUser.Referer);
        CreateUserInfo("SelfReferer", dbUser.SelfReferer);
        CreateUserInfo("Code", dbUser.Code);
        CreateUserInfo("Date", dbUser.Date.ToString());
        CreateUserInfo("Language", dbUser.Language);
        CreateUserInfo("Token", dbUser.Token);
        CreateUserInfo("Bread", dbUser.Bread);
        CreateUserInfo("Ip", dbUser.Ip);
        CreateUserInfo("Country", dbUser.Country);
        CreateUserInfo("Iso", dbUser.Iso);
        CreateUserInfo("Town", dbUser.Town);
        CreateUserInfo("Lat", dbUser.Lat);
        CreateUserInfo("Lon", dbUser.Lon);
        CreateUserInfo("Activity", dbUser.Activity);
        CreateUserInfo("Link", dbUser.Link);
        CreateUserInfo("Email", dbUser.Email);
        CreateUserInfo("CountryCode", dbUser.CountryCode);
        CreateUserInfo("Name", dbUser.Name);
        CreateUserInfo("Surname", dbUser.Surname);
        CreateUserInfo("Patronymic", dbUser.Patronymic);
    }

    private void CreateUserInfo(string key, string value)
    {
        WorkspaceEntityMember temp = Instantiate(block, parentEntity);
        userLineInfos.Add(temp);
        temp.Set(key, value, whatEntity, this);
    }
}