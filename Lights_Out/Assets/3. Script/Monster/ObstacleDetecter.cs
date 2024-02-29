using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetecter : Detector
{
    [SerializeField]
    private float detectRadius;

    [SerializeField]
    private LayerMask layerMask;

    Collider2D[] colliders;

    public override void Detect(AIData aiData)
    {
        //CircleOverlap으로 감지해서 AiData에 obstacle 장애물을 넣어준다
        colliders = Physics2D.OverlapCircleAll(transform.position,detectRadius,layerMask);
        aiData.obstacles = colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
            {
                Gizmos.DrawSphere(collider.transform.position, 0.2f);
            }
        }
    }
}
