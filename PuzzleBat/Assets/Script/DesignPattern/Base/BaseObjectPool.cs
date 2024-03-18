using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Pool;

public class BaseObjectPool : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> goPref;

    protected IObjectPool<GameObject> pool;

    public bool collectionChecks = true;
    public int defaultCapacity = 10;
    public int maxPoolSize = 100;

    // getter setter
    protected IObjectPool<GameObject> Pool
    {
        get
        {
            pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultCapacity, maxPoolSize);
            return pool;
        }
    }

    protected virtual GameObject CreatePooledItem()
    {
        int rand = Random.Range(0, goPref.Count);

        if(transform.Find(goPref[rand].name).childCount != 0)
        {
            return transform.Find(goPref[rand].name).GetChild(0).gameObject;
        }

        var obj = Instantiate(goPref[rand], transform.GetChild(rand).transform);
        obj.name = goPref[rand].name;
        obj.SetActive(false);

        return obj;
    }

    protected virtual void OnReturnedToPool(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform.Find(go.name).transform);
    }

    protected virtual void OnTakeFromPool(GameObject go)
    {
        go.SetActive(true);
    }

    protected virtual void OnDestroyPoolObject(GameObject go)
    {
        Destroy(go);
    }
}
