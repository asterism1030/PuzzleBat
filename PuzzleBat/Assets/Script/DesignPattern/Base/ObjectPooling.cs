using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    private GameObject objPref;

    private Queue<GameObject> pool = new Queue<GameObject>();

    // getter setter
    public GameObject ObjPref { get { return objPref; } set { objPref = value; } }

    // ����
    public GameObject CreateObject()
    {
        var obj = Instantiate(objPref, objPref.transform);
        obj.SetActive(false);
        pool.Enqueue(obj);

        return obj;
    }

    // ���
    public GameObject GetObject()
    {
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);

        return obj;
    }

    // ��ȯ
    public  void ReturnObject(GameObject obj)
    {
        if(obj == null)
        {
            return;
        }

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // ����
    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
