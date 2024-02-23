using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // Search for an instance of the requested type in the scene
                instance = FindObjectOfType<T>();

                // If no instance is found, create a new GameObject and add the component
                //if (instance == null)
                //{
                //    GameObject singletonObject = new GameObject(typeof(T).Name);
                //    instance = singletonObject.AddComponent<T>();
                //}
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        // If an instance already exists and it is not this one, destroy this instance
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}