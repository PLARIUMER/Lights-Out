using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("Des", 6);
    }

    protected virtual void Bump()
    {
        anim.Play("Bump");
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }


    private void Des()
    {
        Destroy(gameObject);
    }
}
