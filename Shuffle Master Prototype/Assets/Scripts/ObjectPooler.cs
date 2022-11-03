using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public GameObject Prefab;
        public int Size;
    }

    Queue<GameObject> objectQueue;

    public Pool objectPool;
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
        CreateObjectPool();
    }

    //Object pool olusturma islemi
    void CreateObjectPool()
    {
        objectQueue = new Queue<GameObject>();

        for (int i = 0; i < objectPool.Size; i++)
        {
            GameObject obj = Instantiate(objectPool.Prefab);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }


    //Pooldan kullanilacak karti cekme islemi
    public GameObject GetCard ()
    {
        GameObject cardToGet = objectQueue.Dequeue();

        objectQueue.Enqueue(cardToGet);

        return cardToGet;
    }
}
