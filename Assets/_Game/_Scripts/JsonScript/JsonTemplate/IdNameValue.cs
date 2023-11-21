using System;
using System.Collections.Generic;
using System.Linq;
using _Game._Scripts;
using UnityEngine;

[Serializable]
public class IdNameValue
{
    public string MYTId;
    public string MYTName;
    public string MYTUserName;
    public string MYTHashIpfs;
    public List<IdNameValue> MYTValue = new List<IdNameValue>();
    public bool OpenStatus = false;
    public List<string> MYTAccess = new List<string>() {"01111"};


    public string MYTID
    {
        get => MYTId;
        set
        {
            // Random random = new Random();
            // byte[] bytes = Encoding.UTF8.GetBytes(value + random.Next(0, 100));
            // SHA256 hashstring = SHA256.Create();
            // byte[] hash = hashstring.ComputeHash(bytes);
            // string hashString = string.Empty;
            // foreach (byte x in hash)
            // {
            //     hashString += String.Format("{0:x2}", x);
            // }

            Config.StringBuilder.Length = 0;
            Config.StringBuilder.Append(value);
            Config.StringBuilder.Append(Config.Random.Next(0, 100));
            // string hashString = value + random.Next(0, 100);


            MYTId = Config.StringBuilder.ToString();
        }
    }


    public IdNameValue()
    {
    }

    public IdNameValue(String mytName)
    {
        MYTID = mytName;
        MYTName = mytName;
    }

    public void SetIdNameValue<T>(T component)
    {
        MYTID = component.ToString();
        MYTName = component.GetType().ToString();
    }


    public IdNameValue Convert(String path, char sep)
    {
        String[] sArray = path.Split(sep);


        return Convert(sArray.ToList());
    }

    public IdNameValue Convert(List<String> path)
    {
        IdNameValue converted = new IdNameValue(path[0]);
        path.ToList().RemoveAt(0);
        if (path.Count == 0)
        {
            return converted;
        }

        converted.MYTValue.Add(new IdNameValue().Convert(path));
        return converted;
    }

    public bool Equals(IdNameValue obj)
    {
        if (obj.MYTName != MYTName)
        {
            return false;
        }

        return MYTValue[0].Equals(obj.MYTValue[0]);
    }

    #region CRUD

    public bool AddChapter(String name)
    {
        try
        {
            MYTValue.Add(new IdNameValue(name));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка добавления " + name + "\n" + e);
            return false;
        }
    }

    public bool DeleteChapter(String name)
    {
        try
        {
            MYTValue.Remove(MYTValue.Find(x => x.MYTName == name));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка Удаления " + name + "\n" + e);
            return false;
        }
    }

    public bool UpdateChapter(String name, List<IdNameValue> newValue)
    {
        try
        {
            MYTValue.Find(x => x.MYTName == name).MYTValue = newValue;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка обновления в " + name + "\n" + e);
            return false;
        }
    }

    #endregion

    //    public void SetValue(FieldInfo component)
    //    {
    //        if (component != null)
    //        {
    //            IdNameValue obj = new IdNameValue
    //                {MYTID = component.Name, MYTName = component.Name};
    ////            obj.SetValue(component);
    //            MYTValue.Add(obj);
    //        }
    //    }
    //
    //    public void SetValue(PropertyInfo component)
    //    {
    //        if (component != null)
    //        {
    //            IdNameValue obj = new IdNameValue
    //                {MYTID = component.Name, MYTName = component.Name};
    ////            obj.SetValue(component);
    //            MYTValue.Add(obj);
    //        }
    //    }
}