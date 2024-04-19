using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehavior : SteeringBehavior
{
    [SerializeField] MonsterController controller;
    [SerializeField] MonsterStat stat;
    [SerializeField]
    private LayerMask obstaclesLayerMask, playerLayerMask;
    [SerializeField] MonsterAI monsterAi;
    [SerializeField] private float targetRechedThreshold = 0.5f;
    bool reachedLastTarget = true;

    private Vector2 targetPositionCached;
    private float[] interestsTemp;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[]interest,AIData aiData)
    {
        if(reachedLastTarget)
        {
            if(aiData.targets == null || aiData.targets.Count <= 0)
            {
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                aiData.currentTarget = aiData.targets.OrderBy(target=>Vector2.Distance(target.position,transform.position)).First();
            }
        }

        if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
             targetPositionCached = aiData.currentTarget.position; 

        if(Vector2.Distance(transform.position,targetPositionCached) < targetRechedThreshold)
        {
            reachedLastTarget=true;
            aiData.currentTarget=null;
            return (danger, interest);
        }

        Vector2 directionToTarget;
        if (controller.canRun)
        {
            if (controller.atkCur < stat.atkCool) //공격 쿨타임이 아직 안 될때
            {
                //도망
                if (Vector2.Distance((Vector2)transform.position, targetPositionCached) > stat.atkRange)   //공격 범위 밖에 있을때
                    directionToTarget = (targetPositionCached - (Vector2)transform.position); //플레이어 방향
                else
                    directionToTarget = ((Vector2)transform.position - targetPositionCached);
            }
            else
            {
                Vector2 direction = (aiData.currentTarget.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, stat.seekRange, obstaclesLayerMask);
                if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)  //플레이어와 자신 사이에 벽이 없을때
                {
                    directionToTarget = Vector2.zero;
                    controller.canAtk = true;
                    controller.isLost = false;
                }
                else
                {
                    directionToTarget = (targetPositionCached - (Vector2)transform.position);
                    controller.canAtk = false;
                    controller.isLost = true;
                    controller.desPos = targetPositionCached;
                }
            }
        }
        else
        {
            if (controller.atkCur < stat.atkCool) //공격 쿨타임이 아직 안 될때
            {
                //도망
                if (Vector2.Distance((Vector2)transform.position, targetPositionCached) > stat.atkRange)   //공격 범위 밖에 있을때
                    directionToTarget = (targetPositionCached - (Vector2)transform.position); //플레이어 방향
                else
                    directionToTarget = Vector2.zero;
                
            }
            else
            {
                Vector2 direction = (aiData.currentTarget.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, stat.seekRange, obstaclesLayerMask);
                if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)  //플레이어와 자신 사이에 벽이 없을때
                {
                    directionToTarget = Vector2.zero;
                    controller.canAtk = true;
                }
                else
                {
                    directionToTarget = (targetPositionCached - (Vector2)transform.position);
                    controller.canAtk = false;
                }
            }
        }
      

        for (int i = 0;i<interest.Length;i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            if(result > 0)
            {
                float valueToPutIn = result;
                if(valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }
        interestsTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if(interestsTemp != null)
        {
            Gizmos.color = Color.green;
            for(int i=0;i<interestsTemp.Length;i++)
            {
                Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]);
            }
            if (!reachedLastTarget)
            {
                Gizmos.color= Color.red;
                Gizmos.DrawSphere(targetPositionCached, 0.1f);
            }
        }
    }
}
