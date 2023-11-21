using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.FolderScript.ObjectPool
{
    [Serializable]
    public class PoolingObject
    {
        private GameObject prefabe;
        private int poolSize;
        private int queue = 0;

        [SerializeField] private List<GameObject> pool;

        public PoolingObject(GameObject prefabe, int countPool = 0)
        {
            pool = new List<GameObject>();
            this.prefabe = prefabe;

            poolSize = countPool;


            if (countPool == 0)
                poolSize = Config.POOL_OBJECT_SIZE;

            SetPoolingObject(poolSize);
        }


        public GameObject GetObject()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i] != null && !pool[i].activeSelf)
                {
                    if (i == pool.Count - 3)
                    {
                        SetPoolingObject(pool.Count + poolSize / 2);
                        // StartCoroutine(UpdatePoolSize());
                    }

                    return pool[i];
                }
            }

            return null;
        }

        public GameObject GetObjectWithQueue()
        {
            if (queue == pool.Count - 3)
            {
                SetPoolingObject(pool.Count + poolSize / 2);
            }

            return pool[queue++];
        }

        private void SetPoolingObject(int count)
        {
            for (int i = pool.Count; i < count; i++)
            {
                pool.Add(
                    Factory.InstantiateGameObject(prefabe, (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.transform));
                pool[i].SetActive(false);
            }
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}