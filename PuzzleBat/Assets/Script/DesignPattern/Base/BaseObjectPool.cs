using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    protected IObjectPool<GameObject> Pool {
        get
        {
            pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultCapacity, maxPoolSize);
            return pool;
        }
    }

    protected virtual GameObject CreatePooledItem()
    {
        int rand = Random.Range(0, goPref.Count);

        var obj = Instantiate(goPref[rand], goPref[rand].transform.parent);
        obj.SetActive(false);

        return obj;
    }

    protected virtual void OnReturnedToPool(GameObject go)
    {
        go.SetActive(false);
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
