using System;
using System.Security.Cryptography;
using System.Text;
using _Game._Scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


public class ContainerHash : MonoBehaviour
{
    [SerializeField] private Text hashText;

    public void CopyHash()
    {
        GUIUtility.systemCopyBuffer = hashText.text;
    }

    public void GenerateHash()
    {
        Random random = new Random();
        byte[] bytes = Encoding.UTF8.GetBytes("salt " + random.Next(0, 100));
        SHA256 hashstring = SHA256.Create();
        byte[] hash = hashstring.ComputeHash(bytes);
        string hashStringg = string.Empty;
        foreach (byte x in hash)
        {
            hashStringg += String.Format("{0:x2}", x);
        }

        hashText.text = Config.DOMAIN + hashStringg;
        CopyHash();
    }
}