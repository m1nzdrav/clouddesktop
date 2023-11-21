using System;
using UnityEngine;

namespace _Game._Scripts.Panel
{
    public class PrefabName : MonoBehaviour
    {
        [SerializeField] private Prefab prefab;


        public Prefab Prefab
        {
            get => prefab;
            set => prefab = value;
        }

        public void SetName()
        {
            if (Prefab == Prefab.MYTIcon60_60)
            {
                gameObject.name = (Prefab.ToString() + DateTime.UtcNow +
                                   Factory.GetCountCreation())
                    .Replace(" ", "")
                    .Replace(".", "")
                    .Replace(":", "");
            }
        }
    }
}