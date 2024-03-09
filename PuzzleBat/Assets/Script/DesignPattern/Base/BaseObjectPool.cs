using System.Collections.Generic;
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

    /*
     * TODO) 아이템 종류 만큼의 빈 오브젝트 생성 해야함 (리턴 시 해당 오브젝트 하위 자식으로)
     */
    protected virtual GameObject CreatePooledItem()
    {
        int rand = Random.Range(0, goPref.Count);

        var obj = Instantiate(goPref[rand], transform);
        obj.SetActive(false);

        return obj;
    }

    protected virtual void OnReturnedToPool(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
    }

    protected virtual void OnTakeFromPool(GameObject go)
    {
        go.SetActive(true);
    }

    protected virtual void OnDestroyPoolObject(GameObject go)
    {
        Destroy(go);
    }

    /*
     * 
     * TODO) 리스트의 GO 를 각각 N 개 만큼 생성 후
     * take 시 랜덤하게 줌
     * 
     * 
     */
}
