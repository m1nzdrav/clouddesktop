using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DbUser
{
    public int Id;
    public string Username;
    public string Password;
    public string Number;
    public string Referer;
    public string SelfReferer;
    public string Code;
    public DateTime Date;
    public string Language;
    public string Token;
    public string Bread;
    public string Ip;
    public string Country;
    public string Iso;
    public string Town;
    public string Lat;
    public string Lon;
    public string Activity;
    public string Link;
    public string Email;
    public string CountryCode;
    public string Name;
    public string Surname;
    public string Patronymic;


    public DbUser()
    {

    }

    public DbUser(string name, string password)
    {
        Id = -5;
        Username = name;
        Password = password;
        Number = "";
        Referer = "";
        SelfReferer = "";
        Code = "";
        Date = DateTime.Now;
        Language = "Russian";
        Token = "";
        Bread = "";
        Ip = "";
        Country = "";
        Iso = "";
        Town = "";
        Lat = "";
        Lon = "";
        Activity = "";
        Link = "";
        Email = "";
        CountryCode = "";
    }
}