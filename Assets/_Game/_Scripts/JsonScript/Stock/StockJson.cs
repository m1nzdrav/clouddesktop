using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.JsonScript.Stock
{
    [Serializable]
    public class StockJson
    {
        private static Dictionary<string, List<GameObject>> stockJsonObject;


        public StockJson()
        {
            stockJsonObject = new Dictionary<string, List<GameObject>>();
        }

        public List<GameObject> GetObjects(string json)
        {
            return stockJsonObject[json];
        }

        public void Set(string jsonName, GameObject jsonObject)
        {
            if (stockJsonObject.ContainsKey(jsonName))
            {
                if (!stockJsonObject[jsonName].Contains(jsonObject))
                {
                    stockJsonObject[jsonName].Add(jsonObject);
                }
            }
            else
            {
                stockJsonObject.Add(jsonName, new List<GameObject>());
                stockJsonObject[jsonName].Add(jsonObject);
            }
        }

        public void Remove(string jsonName, GameObject jsonObject)
        {
            if (!stockJsonObject[jsonName].Contains(jsonObject)) return;

            stockJsonObject[jsonName].Remove(jsonObject);
        }

        public void Check()
        {
            foreach (var VARIABLE in stockJsonObject)
            {
                Debug.LogError(VARIABLE.Key + " "+ VARIABLE.Value.Count);
            }
        }
    }
}