using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.Panel;
using UnityEngine;

namespace _Game._Scripts.PrefabeScript
{
    public class OpenedStock : MonoBehaviour, ISingleton
    { 

        public string NameComponent
        {
            get => name;
        }
        private GameEvent openedTrigger, removeTrigger;

        public GameEvent OpenedTriggerEvent => openedTrigger;

        public GameEvent RemoveTriggerEvent => removeTrigger;

        public void CheckRegister()
        {
            // stockOpenedPrefab = FindObjectsOfType<PrefabName>().ToList();
            InstantiateEvent();
        }
     
        private void Awake()
        {
            if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
        private void InstantiateEvent()
        {
            if (openedTrigger == null)
            {
                openedTrigger = new GameEvent();
                removeTrigger = new GameEvent();
            }

            stockOpenedPrefab.RemoveAll(x => x == null);
        }

        public void RegisterForAllEvent(EventListener listener)
        {
            RegisterRemoveTrigger(listener);
            RegisterOpenedTrigger(listener);
        }

        public void RegisterRemoveTrigger(EventListener listener)
        {
            InstantiateEvent();
            removeTrigger.Register(listener);
        }

        public void RegisterOpenedTrigger(EventListener listener)
        {
            InstantiateEvent();
            openedTrigger.Register(listener);
        }

        [Header("Stock Opened Prefab")] [SerializeField]
        private List<PrefabName> stockOpenedPrefab;

        public List<PrefabName> StockOpenedPrefab => stockOpenedPrefab;

        public void SetOpenedPrefab(PrefabName prefabName)
        {
            if (stockOpenedPrefab.Contains(prefabName))
                return;

            stockOpenedPrefab.Add(prefabName);
            OpenedTriggerEvent.Raise();
        }

        public void RemoveOpenedPrefab(PrefabName prefabName)
        {
            if (!stockOpenedPrefab.Contains(prefabName))
                return;


            stockOpenedPrefab.Remove(prefabName);
            stockOpenedPrefab.RemoveAll(x => x == null);
            RemoveTriggerEvent.Raise();
        }

        public GameObject GetLastPrefab()
        {
            if (stockOpenedPrefab.Count == 0) return null;
            
            if ( stockOpenedPrefab.Last() != null)
                return stockOpenedPrefab.Last().gameObject;
                
            stockOpenedPrefab.Remove(stockOpenedPrefab.Last());
            return null;
        }
    }
}