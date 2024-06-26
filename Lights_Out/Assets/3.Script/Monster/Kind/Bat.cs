using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonsterController
{
    [SerializeField] LayerMask obstacleLayer;
    protected override void Init()
    {
        base.Init();
        OnIdle += UpdateIdle;
        OnMove += UpdateMove;
        OnFight += UpdateFight;
        OnDie += UpdateDie;
    }

    private void UpdateIdle()
    {
        movementDirectionSolver.GetDirectionToMove(steeringBehaviors, aiData);
        if (!isLost)
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
        movementDirectionSolver.GetDirectionToMove(steeringBehaviors, aiData);
        if (!isLost)
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
        GetComponent<SpriteRenderer>().flipX = dir.x > 0 ? true : false; 
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
       if(!isLost) GetComponent<SpriteRenderer>().flipX = _target.transform.position.x - transform.position.x > 0 ? true : false;
       else GetComponent<SpriteRenderer>().flipX = dir.x > 0 ? true : false;
        transform.position += dir * _stat.Speed * Time.deltaTime;

        if (isLost && Vector2.Distance(transform.position, desPos) < 1f)
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
            state = State.Attack;
        }
        else
            atkCur += Time.deltaTime;

    }

    private void Attack()
    {
        Vector3 dir = _target.transform.position - transform.position;
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Blood"),transform.position,Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = dir;
    }

    private void UpdateDie()
    {

    }
}
