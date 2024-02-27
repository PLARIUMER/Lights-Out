using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance { get { return instance; } }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if(instance == null)
        {
            instance = gameObject.GetComponent<Managers>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
