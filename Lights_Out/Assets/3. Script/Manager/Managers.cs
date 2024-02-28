using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get { return _instance; } }

    //GameManager _game = new GameManager();
    //public static GameManager Game { get { return _instance._game; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    private void Init()
    {
        if(_instance == null)
        {
            _instance = gameObject.GetComponent<Managers>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
