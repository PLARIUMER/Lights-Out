using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : SteeringBehavior
{
    [SerializeField] private float radius = 2f, agentColliderSize = 0.6f;

    float[] dangersResultTemp = null;

    public override (float[] danger,float[]interest) GetSteering(float[] dangers,float[] interest,AIData aIData)
    {
        foreach(Collider2D obstacleCollider in aIData.obstacles)
        {
            //ClosetPoint는 transform.postion에서 obstacleCollider의 Collider에 가장 가까운 지점
            Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            float weight = distanceToObstacle <= agentColliderSize ? 1 :(radius - distanceToObstacle) / radius;

            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            for(int i = 0;i<Directions.eightDirections.Count;i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);
                float valueToPutIn = result * weight;

                if(valueToPutIn > dangers[i])
                {
                    dangers[i] = valueToPutIn;
                }
            }
        }
        dangersResultTemp = dangers;
        return (dangers, interest);
    }

    private void OnDrawGizmos()
    {
        if(dangersResultTemp != null)
        {
            Gizmos.color = Color.red;
            for(int i = 0; i < dangersResultTemp.Length; i++)
            {
                Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * dangersResultTemp[i]);
            }
        }
        else
        {
            Gizmos.color= Color.cyan;
            Gizmos.DrawWireSphere(transform.position,radius);
        }
    }
}

public static class Directions 
{
    public static List<Vector2> eightDirections = new List<Vector2>
    {
        new Vector2(0,1).normalized,
        new Vector2(1,1).normalized,
        new Vector2(1,0).normalized,
        new Vector2(1,-1).normalized,
        new Vector2(0,-1).normalized,
        new Vector2(-1,-1).normalized,
        new Vector2(1,0).normalized,
        new Vector2(-1,1).normalized
    };
}

