using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime; 

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = target.position;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
