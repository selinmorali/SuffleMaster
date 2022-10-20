using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }

    private void Awake()
    {
        Instance = this;
    }
 
    private void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject GetCardFromPool (string tag, Vector3 position, Stack<GameObject> currentHand)
    {
        if(!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + " isimli bir pool bulunmamaktadýr.");
            return null;
        }

        GameObject cardToGet = PoolDictionary[tag].Dequeue();

        currentHand.Push(cardToGet);
        cardToGet.SetActive(true);
        cardToGet.transform.position = position;

        PoolDictionary[tag].Enqueue(cardToGet);

        return cardToGet;
    }
}
