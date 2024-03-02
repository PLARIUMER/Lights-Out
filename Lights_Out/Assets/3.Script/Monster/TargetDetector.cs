using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField]
    MonsterStat stat;

    [SerializeField]
    private LayerMask obstaclesLayerMask, playerLayerMask;

    private List<Transform> colliders;
    public override void Detect(AIData aiData)
    {
       //�÷��̾ ã�� ��
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, stat.seekRange, playerLayerMask);
        if(playerCollider != null)
        {
            //�÷��̾ �־�����
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, stat.seekRange, obstaclesLayerMask);

            if(hit.collider != null && (playerLayerMask & (1<<hit.collider.gameObject.layer)) != 0)
            {
                colliders = new List<Transform>() { playerCollider.transform};
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            //�÷��̾ ��������
            colliders = null;
        }
        aiData.targets = colliders;
    }
}
