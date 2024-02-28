using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    State _state = State.Idle;
    protected State state
    {
        get { return _state; }
        set
        {
            _state = value;
            switch(_state)
            {
                case State.Idle:
                    break;
                case State.Move:
                    break;
                case State.Attack:
                    break;
                case State.Die:
                    OnDie.Invoke();
                    break;
            }
        }
    }

    protected enum State
    {
        Idle,
        Move,
        Attack,
        Die,
    }

    protected Action OnIdle;
    protected Action OnMove;
    protected Action OnAttack;
    protected Action OnDie;


    void Start()
    {
        Init();    
    }

    protected virtual void Init()
    {

    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                OnIdle.Invoke();
                break;
            case State.Move:
                OnMove.Invoke();
                break;
            case State.Attack:
                OnAttack.Invoke();
                break;
        }
    }
}
