using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonsterController
{
    [SerializeField] LayerMask obstacleLayer;
    protected override void Init()
    {
        base.Init();
        OnIdle -= UpdateIdle; OnIdle += UpdateIdle;
        OnMove -= UpdateMove; OnMove += UpdateMove;
        OnFight -= UpdateFight; OnFight += UpdateFight;
        OnDie -= UpdateDie; OnDie += UpdateDie;
    }

    private void UpdateIdle()
    {
        if ((_target.transform.position - transform.position).magnitude < 5)
        {
            state = State.Fight;
            return;
        }

        if (idleCur >= 2.5f)
        {
            desPos = spawnPos + new Vector3(Random.Range(-2f,2f),Random.Range(-2f,2f));
            state = State.Move;
            return;
        }
        else
            idleCur += Time.deltaTime;
    }

    private void UpdateMove()
    {
        if ((_target.transform.position - transform.position).magnitude < 5)
        {
            state = State.Fight;
            return;
        }

        Vector3 dir = (desPos - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1,obstacleLayer);
        Debug.DrawRay(transform.position, dir, Color.magenta,1);
        if (hit.collider != null)
        {
            state = State.Idle;
            return; 
        }

        transform.position += dir * _stat.Speed * Time.deltaTime;
        if((desPos - transform.position).magnitude < 0.1f)
        {
            state = State.Idle;
            return;
        }
    }


    private void UpdateFight()
    {
        Vector3 dir = movementDirectionSolver.GetDirectionToMove(steeringBehaviors, aiData);
        transform.position += dir * _stat.Speed * Time.deltaTime;

        if ((_target.transform.position - transform.position).magnitude > _stat.seekRange + 3f)
        {
            state = State.Idle;
            monsterAi.CancelFight();
            return;
        }

        if (atkCur >= _stat.atkCool)
        {
            if (!canAtk)
                return;
            atkCur = 0;
            Debug.Log($"{name}ÀÇ °ø°Ý");
        }
        else
            atkCur += Time.deltaTime;

    }

    private void Attack()
    {

    }

    private void UpdateDie()
    {

    }
}
