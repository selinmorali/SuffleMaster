using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string PoolName;
        public GameObject Prefab;
        public int Size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    public static ObjectPooler Instance;
    [SerializeField] private GameObject leftQueue;
    [SerializeField] private GameObject rightQueue;


    private void Awake()
    {
        Instance = this;
        CreateObjectPools();
    }

    void CreateObjectPools()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        Queue<GameObject> leftObjectQueue = new Queue<GameObject>();
        Queue<GameObject> rightObjectQueue = new Queue<GameObject>();

        for (int i = 0; i < pools[0].Size; i++)
        {
            GameObject obj = Instantiate(pools[0].Prefab);
            obj.SetActive(false);
            obj.transform.parent = leftQueue.transform;
            leftObjectQueue.Enqueue(obj);
        }
        PoolDictionary.Add(pools[0].PoolName, leftObjectQueue);

        for (int i = 0; i < pools[1].Size; i++)
        {
            GameObject obj = Instantiate(pools[1].Prefab);
            obj.SetActive(false);
            obj.transform.parent = rightQueue.transform;
            rightObjectQueue.Enqueue(obj);
        }
        PoolDictionary.Add(pools[1].PoolName, rightObjectQueue);

    }

    public GameObject GetCardFromPool (string poolName)
    {
        if(!PoolDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning(poolName + " isimli bir pool bulunmamaktadýr.");
            return null;
        }

        GameObject cardToGet = PoolDictionary[poolName].Dequeue();

        PoolDictionary[poolName].Enqueue(cardToGet);

        return cardToGet;
    }
}
