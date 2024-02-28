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

    protected Action OnIdle = null;
    protected Action OnMove = null;
    protected Action OnAttack = null;
    protected Action OnDie = null; 

    protected GameObject _target;
    protected Rigidbody2D rigid;
    protected Animator anim;
    void Start()
    {
        Init();    
    }

    protected virtual void Init()
    {
        _target = Managers.Game.player;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
