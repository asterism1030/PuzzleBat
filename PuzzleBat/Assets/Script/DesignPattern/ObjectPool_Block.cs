using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool_Block : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> blockPref;

    private IObjectPool<Block> pool;

    // getter setter
    public IObjectPool<Block> Pool {
        get
        {
            // TODO : pool ����
            return pool;
        }
    }

    public void Init()
    {

    }

    public Block CreateBlock()
    {
        // TODO : ����

        return null;
    }
}
