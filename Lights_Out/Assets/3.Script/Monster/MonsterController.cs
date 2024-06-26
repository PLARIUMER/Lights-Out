using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    State _state = State.Idle;
    public State state
    {
        get { return _state; }
        set
        {
            if((_state == State.Idle || _state == State.Move) && value == State.Fight)
            {
                sign.SetActive(false);
                sign.SetActive(true);
            }
            _state = value;

            switch(_state)
            {
                case State.Idle:
                    idleCur = 0;
                    anim.Play("Idle");
                    break;
                case State.Move:
                    anim.Play("Move");
                    break;
                case State.Fight:
                    monsterAi.Fight();
                    anim.Play("Move");
                    break;
                case State.Attack:
                    anim.Play("Attack");
                    break;
                case State.Die:
                    OnDie.Invoke();
                    break;
            }
        }
    }

    public enum State
    {
        Idle,
        Move,
        Fight,
        Attack,
        Die,
    }

    protected Action OnIdle = null;
    protected Action OnMove = null;
    protected Action OnAttack = null;
    protected Action OnFight = null;
    protected Action OnDie = null;

    protected float idleCur;
    [HideInInspector]
    public float atkCur;
    [HideInInspector]
    public Vector3 desPos;
    protected Vector3 spawnPos;

    [SerializeField]
    protected GameObject _target;
    protected Rigidbody2D rigid;
    protected MonsterStat _stat;
    protected Animator anim;

    [Header("Steering")]
    [SerializeField] protected MonsterAI monsterAi;
    [SerializeField] protected ContextSolver movementDirectionSolver;
    [SerializeField] protected List<SteeringBehavior> steeringBehaviors;
    [SerializeField] protected AIData aiData;
    public bool canRun;
    [HideInInspector]
    public bool canAtk = true;
    [HideInInspector]
    public bool isLost;

    [Header("Sign")]
    public GameObject sign;
    void Start()
    {
        Init();    
    }

    protected virtual void Init()
    {
        _stat = GetComponent<MonsterStat>();
        spawnPos = transform.position;
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
            case State.Fight:
                OnFight.Invoke();
                break;
        }

        Debug.Log(state);
    }

    void EndAtk()
    {
        state = State.Fight;
    }
}
