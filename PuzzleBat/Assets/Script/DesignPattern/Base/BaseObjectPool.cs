using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BaseObjectPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> goPref;

    private IObjectPool<GameObject> pool;

    public bool collectionChecks = true;
    public int defaultCapacity = 10;
    public int maxPoolSize = 100;

    // getter setter
    public IObjectPool<GameObject> Pool {
        get
        {
            pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultCapacity, maxPoolSize);
            return pool;
        }
    }

    public void Init()
    {

    }

    private GameObject CreatePooledItem()
    {
        int rand = Random.Range(0, goPref.Count);

        var obj = Instantiate(goPref[rand], goPref[rand].transform.parent);
        obj.SetActive(false);

        return obj;
    }

    private void OnReturnedToPool(GameObject go)
    {
        go.SetActive(false);
    }

    private void OnTakeFromPool(GameObject go)
    {
        go.SetActive(true);
    }

    private void OnDestroyPoolObject(GameObject go)
    {
        Destroy(go);
    }


}
