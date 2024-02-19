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
                    GameObject obj = new GameObject { name = typeof(T).ToString() };
                    _instance = obj.AddComponent(typeof(T)) as T;

                }

                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
}
