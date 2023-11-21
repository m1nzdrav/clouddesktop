//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using Sirenix.OdinInspector;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class SubRussian : MonoBehaviour
//{
//  //  public LanguageSub[] LanguageSubs = {};
//    
//    private  List<LanguageSub> obj;
//    string path;
//    string jsonString;
//    public GameObject sub;
//
//    // Start is called before the first frame update
//    private void Start()
//    {
//        // ЗАписываем Путь к json файлу
//        path = Application.dataPath + "/ENGLISH.json";
//        // Записываем в строку все содержимое json файла
//        jsonString = File.ReadAllText (path);
//        Debug.LogError(jsonString);
//        
//    }
//    [Button]
//    public void OnCreateJson()
//    {
//        // если строка пустая то мы присваимваем ей значение нашего объекта и записываем в файл эту строку
//        if (jsonString.Length == 0)
//        {
////            for (int i = 0; i < sub.GetComponent<LanguageSubContainer>().LanguageSubs.Length; i++)
//               //            {
//               //                obj[i].Subtitles1 = sub.GetComponent<LanguageSub[]>()[i].Subtitles1;
//               //                obj[i].TimeCode = sub.GetComponent<LanguageSub[]>()[i].TimeCode;
//               //            }
//           // obj = sub.GetComponent<LanguageSubContainer>().LanguageSubs;
//            jsonString = JsonUtility.ToJson(sub.GetComponent<LanguageSubContainer>().LanguageSubs);
//            File.WriteAllText(path, jsonString);
//        }
//        // иначе мы должны получить из файла строку и перезаписать его
//       
//
//    }
//    [Button]
//    public void OnSetJsonParameters()
//    {
//        ListaRecords listaRecords = JsonUtility.FromJson<ListaRecords> (jsonString);
//
//        print(listaRecords);
//
//        foreach (LanguageSub VARIABLE in listaRecords.Records)
//        {
//            Debug.LogError(VARIABLE.Subtitles1);
//        }
//    }
//
//}
//
////[Serializable]
// 
////public class ListaRecords{
////    public List<LanguageSub> Records;
////}