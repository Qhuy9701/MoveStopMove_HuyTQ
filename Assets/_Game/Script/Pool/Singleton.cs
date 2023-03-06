using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject gb = new GameObject("Singleton");
                    instance = gb.AddComponent<T>();
                    DontDestroyOnLoad(gb);
                }
            }
            return instance;
        }
    }

    public static T GetInstance()
    {
        instance = FindObjectOfType<T>();
        Debug.Log("11111111");
        if(instance == null)
        {
            Debug.Log("2222");
            GameObject gb = new GameObject("Singleton");
            instance = gb.AddComponent<T>();
            DontDestroyOnLoad(gb);
        }
        Debug.Log("333333");
        return instance;
    }
}
