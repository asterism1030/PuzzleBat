using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T Instance {
        get {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if(_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent(typeof(T)) as T;
                    obj.name = typeof(T).ToString();

                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        if (_instance == null)
        {
            GameObject obj = new GameObject();
            _instance = obj.AddComponent(typeof(T)) as T;
            obj.name = typeof(T).ToString();

            DontDestroyOnLoad(obj);
        }
        else if(_instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
